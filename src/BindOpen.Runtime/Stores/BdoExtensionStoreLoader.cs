using BindOpen.Data;
using BindOpen.Data.Assemblies;
using BindOpen.Data.Helpers;
using BindOpen.Data.Items;
using BindOpen.Extensions;
using BindOpen.Logging;
using BindOpen.Runtime.Assemblies;
using BindOpen.Runtime.Definitions;
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
        /// <param key="appDomain">The application domain to consider.</param>
        /// <param key="store">The extension store to consider.</param>
        /// <param key="loadOptions">The load options to consider.</param>
        public BdoExtensionStoreLoader(AppDomain appDomain, IBdoExtensionStore store, IExtensionLoadOptions loadOptions)
        {
            _appDomain = appDomain;
            _store = store;

            if (loadOptions == null)
            {
                loadOptions = new ExtensionLoadOptions().WithSourceKinds(
                    DatasourceKind.Memory);
            }
            _loadOptions = loadOptions;
        }

        /// <summary>
        /// Loads the specified extensions into the specified scope.
        /// </summary>
        /// <param key="references">The library references to consider.</param>
        /// <param key="log">The log to consider.</param>
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

            foreach (var reference in references)
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
        /// <param key="libraryReference">The library reference to consider.</param>
        /// <param key="log">The log to consider.</param>
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
                                    assembly = _appDomain.LoadAssembly(
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
                                    assembly = _appDomain.LoadAssemblyFromFile(filePath, subLog);

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

                        IBdoPackageDefinition extensionDefinition = ExtractPackageDefinition(assembly, null, log);

                        // we load the using assemblies

                        if (extensionDefinition?.UsingAssemblyFileNames != null)
                        {
                            foreach (var dependency in assembly.GetReferencedAssemblies())
                            {
                                var reference = BdoData.Assembly(dependency.Name);

                                IBdoLog subSubLog = log?.NewLog()
                                    .WithDisplayName("Loading using extensions...");
                                loaded &= LoadExtensionsInStore(new[] { reference }, subSubLog);
                            }
                        }

                        // we load the item definition specifiying the extension definition

                        foreach (BdoExtensionKind kind in new[] {
                                BdoExtensionKind.Entity,
                                BdoExtensionKind.Connector,
                                BdoExtensionKind.Entity,
                                BdoExtensionKind.Metrics,
                                BdoExtensionKind.Routine,
                                BdoExtensionKind.Scriptword,
                                BdoExtensionKind.Task })
                        {
                            var subSubLog = log?.NewLog();
                            int count = LoadDictionary(assembly, kind, extensionDefinition, subSubLog);

                            if (subSubLog?.HasEvent(EventKinds.Error, EventKinds.Exception, EventKinds.Warning) == true)
                            {
                                log?.AddSubLog(
                                    subSubLog,
                                    title: "Dictionary '" + kind.ToString() + "' not loaded correctly (" + count.ToString() + " items added)");
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
        /// <param key="isDisposing">Indicates whether this instance is disposing</param>
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