using BindOpen.Data;
using BindOpen.Data.Assemblies;
using BindOpen.Data.Helpers;
using BindOpen.Extensions;
using BindOpen.Logging;
using BindOpen.Scopes.Application;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using BindOpen.Scopes;

namespace BindOpen.Scopes.Stores
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
        public BdoExtensionStoreLoader(
            AppDomain appDomain,
            IBdoExtensionStore store,
            IExtensionLoadOptions loadOptions)
        {
            _appDomain = appDomain;
            _store = store;

            loadOptions ??= new ExtensionLoadOptions();

            _loadOptions = loadOptions;
        }

        /// <summary>
        /// Loads the specified extensions into the specified scope.
        /// </summary>
        /// <param key="references">The library references to consider.</param>
        /// <param key="log">The log to consider.</param>
        public IBdoExtensionStoreLoader LoadPackages(
            IBdoLog log = null)
        {
            if (_store == null) return this;

            var subLog = log?.NewLog();

            // we load libraries

            var loaded = true;

            _loadOptions.Sources ??= new();
            if (_loadOptions.Sources.Count == 0)
            {
                _loadOptions.AddSource(DatasourceKind.Memory);
            }

            _loadOptions.References ??= new() { BdoData.AssemblyAsAll() };
            _loadOptions.References = _appDomain.GetAssemblyReferences(_loadOptions.References);

            if (!(_loadOptions.References.Count > 0))
            {
                subLog?.AddMessage("No extensions found");
            }
            else
            {
                var loadedAssemblyNames = new List<string>();

                foreach (var reference in _loadOptions.References)
                {
                    if (reference != null)
                    {
                        loaded &= LoadPackage(reference, loadedAssemblyNames, subLog);

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
            }

            return this;
        }

        /// <summary>
        /// Loads the specified library.
        /// </summary>
        /// <param key="libraryReference">The library reference to consider.</param>
        /// <param key="log">The log to consider.</param>
        /// <returns>Returns the loaded library.</returns>
        private bool LoadPackage(
            IBdoAssemblyReference reference,
            List<string> loadedAssemblyNames,
            IBdoLog log = null)
        {
            var loaded = true;

            if (reference != null && _loadOptions?.Sources != null)
            {
                var referenceUniqueName = reference.Key();

                if (!loadedAssemblyNames.Any(q => referenceUniqueName.BdoKeyEquals(q)))
                {
                    // we update the list of loaded assemblies

                    loadedAssemblyNames.Add(referenceUniqueName);

                    // first we load the assembly

                    IBdoLog newLog = log?.NewLog()
                        .WithDisplayName("Loading package '" + reference.AssemblyName + "'");

                    try
                    {
                        Assembly assembly = null;

                        foreach (var source in _loadOptions?.Sources)
                        {
                            IBdoLog subLog = newLog?.AddSubLog(
                                title: "Loading assembly from '" + source.Kind.ToString() + "'",
                                eventKind: EventKinds.Message);

                            switch (source.Kind)
                            {
                                case DatasourceKind.Memory:
                                    assembly = _appDomain.LoadAssembly(reference, subLog);
                                    break;
                                case DatasourceKind.Repository:
                                    string fileName = reference.AssemblyFileName;
                                    if (string.IsNullOrEmpty(fileName))
                                    {
                                        fileName = reference.AssemblyName + ".dll";
                                    }

                                    string filePath = source.Uri.EndingWith(@"\").ToPath() + fileName;
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
                                subLog?.AddMessage("Assembly '" + reference.ToString() + " loaded");
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
                            // we get the extension definition

                            var packageDefinition = ExtractPackageDefinition(assembly, null, log);

                            // we load the using assemblies

                            if (packageDefinition?.UsingAssemblyReferences?.Count > 0)
                            {
                                foreach (var usingReference in packageDefinition?.UsingAssemblyReferences)
                                {
                                    IBdoLog subSubLog = log?.NewLog()
                                        .WithDisplayName("Loading using extensions...");
                                    loaded &= LoadPackage(usingReference, loadedAssemblyNames, subSubLog);
                                }
                            }

                            // we load the item definition specifiying the extension definition

                            var extensionKinds = _loadOptions.ExtensionKinds?.ToArray()
                                ?? new[]
                                {
                                    BdoExtensionKind.Connector,
                                    BdoExtensionKind.Entity,
                                    BdoExtensionKind.Function,
                                    BdoExtensionKind.Task
                                };

                            foreach (var extensionKind in extensionKinds)
                            {
                                var subSubLog = log?.NewLog();

                                int count = LoadDictionary(assembly, extensionKind, packageDefinition, subSubLog);

                                if (subSubLog?.HasEvent(
                                    EventKinds.Error,
                                    EventKinds.Exception,
                                    EventKinds.Warning) == true)
                                {
                                    log?.AddSubLog(
                                        subSubLog,
                                        title: "Dictionary '" + extensionKind.ToString() + "' not loaded correctly (" + count.ToString() + " items added)");
                                }
                                else
                                {
                                    log?.AddMessage("Dictionary '" + extensionKind.ToString() + "' loaded (" + count.ToString() + " items added)");
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