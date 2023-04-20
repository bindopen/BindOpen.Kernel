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

namespace BindOpen.Scopes.Stores
{
    /// <summary>
    /// This class represents a extension scope loader.
    /// </summary>
    internal partial class BdoExtensionStoreLoader : BdoObject, IBdoExtensionStoreLoader
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

            var childLog = log?.NewLog();

            // we load libraries

            var loaded = true;

            _loadOptions.Sources ??= new List<(DatasourceKind Kind, string Uri)>();
            if (_loadOptions.Sources.Any() != true)
            {
                _loadOptions.AddSource(DatasourceKind.Memory);
            }

            _loadOptions.References = _appDomain.GetAssemblyReferences(_loadOptions.References?.ToArray())?.ToArray();

            if (_loadOptions.References.Any() != true)
            {
                childLog?.AddEvent(EventKinds.Message, "No extensions found");
            }
            else
            {
                var loadedAssemblyNames = new List<string>();

                foreach (var reference in _loadOptions.References)
                {
                    if (reference != null)
                    {
                        loaded &= LoadPackage(reference, loadedAssemblyNames, childLog);

                        if (log?.HasEvent(EventKinds.Error, EventKinds.Exception, EventKinds.Warning) == true)
                        {
                            log?.AddChild(childLog, title: "Loading extension '" + (reference?.AssemblyName ?? "?") + "'");
                        }
                        else
                        {
                            log?.AddEvent(EventKinds.Message, "Extension '" + (reference?.AssemblyName ?? "?") + "' loaded");
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
                            IBdoLog childLog = newLog?.InsertChild(
                                EventKinds.Message,
                                title: "Loading assembly from '" + source.Kind.ToString() + "'");

                            switch (source.Kind)
                            {
                                case DatasourceKind.Memory:
                                    assembly = _appDomain.LoadAssembly(reference, childLog);
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
                                        childLog?.AddEvent(EventKinds.Error, "Could not find the assembly file path '" + filePath + "'");
                                        loaded = false;
                                    }
                                    else
                                    {
                                        assembly = _appDomain.LoadAssemblyFromFile(filePath, childLog);

                                        if (assembly == null)
                                        {
                                            childLog?.AddEvent(EventKinds.Error, "Could not load assembly '" + filePath + "'");
                                            loaded = false;
                                        }
                                        else
                                        {
                                            childLog?.AddEvent(EventKinds.Checkpoint, "Loading assembly from file '" + filePath + "'");
                                            assembly = Assembly.LoadFrom(filePath);
                                        }
                                    }
                                    break;
                                case DatasourceKind.RestApi:
                                    break;
                            }

                            if (assembly != null)
                            {
                                childLog?.AddEvent(EventKinds.Message, "Assembly '" + reference.ToString() + " loaded");
                                break;
                            }
                        }

                        // if we have an assembly then we index library items

                        if (assembly == null)
                        {
                            log?.AddChild(newLog, filter: p => p.HasEvent(EventKinds.Error, EventKinds.Exception, EventKinds.Warning));
                            loaded = false;
                        }
                        else
                        {
                            // we get the extension definition

                            var packageDefinition = ExtractPackageDefinition(assembly, null, log);

                            // we load the using assemblies

                            if (packageDefinition?.UsingAssemblyReferences?.Any() == true)
                            {
                                foreach (var usingReference in packageDefinition?.UsingAssemblyReferences)
                                {
                                    IBdoLog subChildLog = log?.NewLog()
                                        .WithDisplayName("Loading using extensions...");
                                    loaded &= LoadPackage(usingReference, loadedAssemblyNames, subChildLog);
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
                                var subChildLog = log?.NewLog();

                                int count = LoadDictionary(assembly, extensionKind, packageDefinition, subChildLog);

                                if (subChildLog?.HasEvent(
                                    EventKinds.Error,
                                    EventKinds.Exception,
                                    EventKinds.Warning) == true)
                                {
                                    log?.AddChild(
                                        subChildLog,
                                        title: "Dictionary '" + extensionKind.ToString() + "' not loaded correctly (" + count.ToString() + " items added)");
                                }
                                else
                                {
                                    log?.AddEvent(EventKinds.Message, "Dictionary '" + extensionKind.ToString() + "' loaded (" + count.ToString() + " items added)");
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