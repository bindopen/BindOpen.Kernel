using BindOpen.Application.Scopes;
using BindOpen.Data.Helpers.Files;
using BindOpen.Data.Helpers.Strings;
using BindOpen.Data.Items;
using BindOpen.Extensions.Definition;
using BindOpen.System.Assemblies;
using BindOpen.System.Assemblies.References;
using BindOpen.System.Diagnostics;
using BindOpen.System.Diagnostics.Events;
using System;
using System.IO;
using System.Reflection;

namespace BindOpen.Extensions.Runtime
{
    /// <summary>
    /// This class represents a extension scope loader.
    /// </summary>
    internal partial class BdoExtensionStoreLoader : DataItem, IBdoExtensionStoreLoader
    {
        private readonly AppDomain _appDomain;
        private readonly IBdoExtensionStore _store;
        private readonly IExtensionLoadOptions _loadOptions;

        /// <summary>
        /// Initializes a new instance of BdoExtensionStoreLoader the class.
        /// </summary>
        /// <param name="appDomain">The application domain to consider.</param>
        /// <param name="store">The extension store to consider.</param>
        /// <param name="loadOptions">The load options to consider.</param>
        public BdoExtensionStoreLoader(AppDomain appDomain, IBdoExtensionStore store, IExtensionLoadOptions loadOptions)
        {
            _appDomain = appDomain;
            _store = store;

            if (loadOptions == null)
            {
                loadOptions = new ExtensionLoadOptions().WithSourceKinds(DatasourceKind.Memory);
            }
            _loadOptions = loadOptions;
        }

        /// <summary>
        /// Loads the specified extensions into the specified scope.
        /// </summary>
        /// <param name="references">The library references to consider.</param>
        public IBdoLog LoadExtensionsInStore(params IBdoAssemblyReference[] references)
        {
            var log = new BdoLog();
            if (_store == null) return log;

            // we load libraries

            foreach (IBdoAssemblyReference reference in references)
            {
                if (reference != null)
                {
                    IBdoLog subLog = LoadLibrary(reference);

                    if (subLog.HasErrorsOrExceptionsOrWarnings())
                        log.AddSubLog(subLog, title: "Loading extension '" + (reference?.Name ?? "?") + "'");
                    else
                        log.AddMessage("Extension '" + (reference?.Name ?? "?") + "' loaded");
                }
            }

            return log;
        }

        /// <summary>
        /// Loads the specified library.
        /// </summary>
        /// <param name="libraryReference">The library reference to consider.</param>
        /// <returns>Returns the loaded library.</returns>
        private IBdoLog LoadLibrary(IBdoAssemblyReference libraryReference)
        {
            var log = new BdoLog();

            if (libraryReference != null && _loadOptions?.SourceKinds != null)
            {
                try
                {
                    Assembly assembly = null;

                    // first we load the assembly

                    IBdoLog firstLog = new BdoLog() { DisplayName = "Loading library '" + libraryReference.Name + "'" };

                    foreach (DatasourceKind dataSourceKind in _loadOptions?.SourceKinds)
                    {
                        IBdoLog subLog = firstLog.AddSubLog(title: "Loading assembly from '" + dataSourceKind.ToString() + "'", eventKind: EventKinds.Message);

                        switch (dataSourceKind)
                        {
                            case DatasourceKind.Memory:
                                if (!string.IsNullOrEmpty(libraryReference.Name))
                                {
                                    assembly = AppDomainPool.LoadAssembly(_appDomain, libraryReference.Name, subLog);
                                }
                                else
                                {
                                    subLog?.AddWarning("File name missing");
                                }
                                break;
                            case DatasourceKind.Repository:
                                string fileName = libraryReference.FileName;
                                if (string.IsNullOrEmpty(fileName))
                                {
                                    fileName = libraryReference.Name + ".dll";
                                }

                                string filePath = _loadOptions.LibraryFolderPath.EndingWith(@"\").ToPath() + fileName;
                                if (!File.Exists(filePath))
                                {
                                    subLog?.AddError("Could not find the assembly file path '" + filePath + "'");
                                }
                                else
                                {
                                    assembly = AppDomainPool.LoadAssemblyFromFile(_appDomain, filePath, subLog);

                                    if (assembly == null)
                                    {
                                        subLog?.AddError("Could not load assembly '" + filePath + "'");
                                    }
                                    else
                                    {
                                        subLog?.AddCheckpoint("Loading assembly from file '" + filePath + "'");
                                        assembly = Assembly.LoadFrom(filePath);
                                    }
                                }
                                break;
                            case DatasourceKind.RestApi:
                                break;
                        }

                        if (assembly != null)
                        {
                            subLog?.AddMessage("Assembly loaded");
                            break;
                        }
                    }

                    // if we have an assembly then we index library items

                    if (assembly == null)
                    {
                        log?.AddSubLog(firstLog, p => p.HasErrorsOrExceptionsOrWarnings());
                    }
                    else
                    {
                        firstLog.GetEvents(true, EventKinds.Error, EventKinds.Exception).ForEach(p => p.Kind = EventKinds.Warning);
                        log?.AddSubLog(firstLog);

                        // we get the extension definition

                        IBdoExtensionDefinition extensionDefinition = ExtractExtensionDefinition(assembly, null, log);

                        // we load the using assemblies

                        if (extensionDefinition?.Dto?.UsingAssemblyFileNames != null)
                        {
                            foreach (var st in extensionDefinition.Dto.UsingAssemblyFileNames)
                            {
                                var fileName = st;
                                if (!fileName.EndsWith(".dll", StringComparison.OrdinalIgnoreCase))
                                {
                                    fileName += ".dll";
                                }
                                IBdoAssemblyReference reference = BdoAssemblyReferenceFactory.Create(st).WithFileName(fileName);

                                log.AddSubLog(LoadExtensionsInStore(reference), title: "Loading using extensions...");
                            }
                        }

                        // we load the item definition specifiying the extension definition

                        foreach (BdoExtensionItemKind kind in new[] {
                                BdoExtensionItemKind.Carrier,
                                BdoExtensionItemKind.Connector,
                                BdoExtensionItemKind.Entity,
                                BdoExtensionItemKind.Handler,
                                BdoExtensionItemKind.Metrics,
                                BdoExtensionItemKind.Routine,
                                BdoExtensionItemKind.Scriptword,
                                BdoExtensionItemKind.Task })
                        {
                            IBdoLog subSubLog = new BdoLog();
                            int count = LoadDictionary(assembly, kind, extensionDefinition, subSubLog);

                            if (subSubLog.HasErrorsOrExceptionsOrWarnings())
                            {
                                log.AddSubLog(
                                    subSubLog,
                                    title: "Dictionary '" + kind.ToString() + "' loaded (" + count.ToString() + " items added)");
                            }
                            else
                            {
                                log.AddMessage("Dictionary '" + kind.ToString() + "' loaded (" + count.ToString() + " items added)");
                            }
                        }
                    }
                }
                catch (Exception exception)
                {
                    log?.AddException(exception);
                }
            }

            return log;
        }

        // ------------------------------------------
        // IDISPOSABLE METHODS
        // ------------------------------------------

        #region IDisposable_Methods

        private bool _isDisposed = false;

        /// <summary>
        /// Disposes this instance. 
        /// </summary>
        /// <param name="isDisposing">Indicates whether this instance is disposing</param>
        protected override void Dispose(bool isDisposing)
        {
            if (_isDisposed)
            {
                return;
            }

            _loadOptions?.Dispose();

            _isDisposed = true;

            base.Dispose(isDisposing);
        }

        #endregion   
    }
}