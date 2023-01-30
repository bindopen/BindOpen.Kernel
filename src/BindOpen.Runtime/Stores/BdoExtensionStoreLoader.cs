using BindOpen.Data;
using BindOpen.Data.Assemblies;
using BindOpen.Data.Items;
using BindOpen.Logging;
using BindOpen.Runtime.Assemblies;
using BindOpen.Runtime.Definition;
using BindOpen.Runtime.Scopes;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace BindOpen.Runtime.Stores
{
    /// <summary>
    /// This class represents a extension scope loader.
    /// </summary>
    internal partial class BdoExtensionStoreLoader : BdoItem, IBdoExtensionStoreLoader
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
        /// <param name="log">The log to consider.</param>
        public bool LoadExtensionsInStore(
            IBdoAssemblyReference[] references,
            IBdoLog log = null)
        {
            if (_store == null) return false;

            var subLog = log?.NewLog();

            // we load libraries

            var loaded = true;

            if (references?.Any(q => q.BdoKeyEquals(StringHelper.__Star)) == true)
            {
                references = _appDomain.GetAssemblies().Select(q => BdoData.Assembly(q)).ToArray();
            }

            foreach (IBdoAssemblyReference reference in references)
            {
                if (reference != null)
                {
                    loaded &= LoadLibrary(reference, subLog);

                    if (log?.HasEvent(EventKinds.Error, EventKinds.Exception, EventKinds.Warning) == true)
                    {
                        log?.AddSubLog(subLog, title: "Loading extension '" + (reference?.AssemblyName ?? "?") + "'");
                    }
                    else
                    {
                        log?.AddMessage("Extension '" + (reference?.AssemblyName ?? "?") + "' loaded");
                    }
                }
            }

            return loaded;
        }

        /// <summary>
        /// Loads the specified library.
        /// </summary>
        /// <param name="libraryReference">The library reference to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <returns>Returns the loaded library.</returns>
        private bool LoadLibrary(IBdoAssemblyReference libraryReference, IBdoLog log = null)
        {
            var loaded = true;

            if (libraryReference != null && _loadOptions?.SourceKinds != null)
            {
                try
                {
                    Assembly assembly = null;

                    // first we load the assembly

                    IBdoLog newLog = log?.NewLog()
                        .WithDisplayName("Loading library '" + libraryReference.AssemblyName + "'");

                    foreach (DatasourceKind dataSourceKind in _loadOptions?.SourceKinds)
                    {
                        IBdoLog subLog = newLog?.AddSubLog(
                            title: "Loading assembly from '" + dataSourceKind.ToString() + "'",
                            eventKind: EventKinds.Message);

                        switch (dataSourceKind)
                        {
                            case DatasourceKind.Memory:
                                if (!string.IsNullOrEmpty(libraryReference.AssemblyName))
                                {
                                    assembly = AppDomainPool.LoadAssembly(
                                        _appDomain,
                                        libraryReference.AssemblyName,
                                        subLog);
                                }
                                else
                                {
                                    subLog?.AddWarning("File name missing");
                                }
                                break;
                            case DatasourceKind.Repository:
                                string fileName = libraryReference.AssemblyFileName;
                                if (string.IsNullOrEmpty(fileName))
                                {
                                    fileName = libraryReference.AssemblyName + ".dll";
                                }

                                string filePath = _loadOptions.LibraryFolderPath.EndingWith(@"\").ToPath() + fileName;
                                if (!File.Exists(filePath))
                                {
                                    subLog?.AddError("Could not find the assembly file path '" + filePath + "'");
                                    loaded = false;
                                }
                                else
                                {
                                    assembly = AppDomainPool.LoadAssemblyFromFile(_appDomain, filePath, subLog);

                                    if (assembly == null)
                                    {
                                        subLog?.AddError("Could not load assembly '" + filePath + "'");
                                        loaded = false;
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
                        log?.AddSubLog(newLog, p => p.HasEvent(EventKinds.Error, EventKinds.Exception, EventKinds.Warning));
                        loaded = false;
                    }
                    else
                    {
                        //firstLog.GetEvents(true, EventKinds.Error, EventKinds.Exception).ForEach(p => p.WithKind(EventKinds.Warning));
                        //log?.AddSubLog(firstLog);

                        // we get the extension definition

                        IBdoExtensionDefinition extensionDefinition = ExtractExtensionDefinition(assembly, null, log);

                        // we load the using assemblies

                        if (extensionDefinition?.UsingAssemblyFileNames != null)
                        {
                            foreach (var st in extensionDefinition.UsingAssemblyFileNames)
                            {
                                var fileName = st;
                                if (!fileName.EndsWith(".dll", StringComparison.OrdinalIgnoreCase))
                                {
                                    fileName += ".dll";
                                }
                                IBdoAssemblyReference reference = BdoData.Assembly(st)
                                    .WithFileName(fileName);

                                IBdoLog subSubLog = log?.NewLog()
                                    .WithDisplayName("Loading using extensions...") as IBdoLog;
                                loaded &= LoadExtensionsInStore(new[] { reference }, subSubLog);
                            }
                        }

                        // we load the item definition specifiying the extension definition

                        foreach (BdoExtensionItemKind kind in new[] {
                                BdoExtensionItemKind.Entity,
                                BdoExtensionItemKind.Connector,
                                BdoExtensionItemKind.Entity,
                                BdoExtensionItemKind.Handler,
                                BdoExtensionItemKind.Metrics,
                                BdoExtensionItemKind.Routine,
                                BdoExtensionItemKind.Scriptword,
                                BdoExtensionItemKind.Task })
                        {
                            var subSubLog = log?.NewLog();
                            int count = LoadDictionary(assembly, kind, extensionDefinition, subSubLog);

                            if (subSubLog.HasEvent(EventKinds.Error, EventKinds.Exception, EventKinds.Warning))
                            {
                                log?.AddSubLog(
                                    subSubLog,
                                    title: "Dictionary '" + kind.ToString() + "' not loaded correctly (" + count.ToString() + " items added)");
                                loaded = false;
                            }
                            else
                            {
                                log?.AddMessage("Dictionary '" + kind.ToString() + "' loaded (" + count.ToString() + " items added)");
                            }
                        }
                    }
                }
                catch (Exception exception)
                {
                    log?.AddException(exception);
                    loaded = false;
                }
            }

            return loaded;
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