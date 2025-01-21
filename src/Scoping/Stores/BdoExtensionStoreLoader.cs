using BindOpen.Data;
using BindOpen.Data.Assemblies;
using BindOpen.Data.Helpers;
using BindOpen.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace BindOpen.Scoping
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
        /// Loads all the extensions from the specified application domain.
        /// </summary>
        /// <param key="log">The log to consider.</param>
        public bool LoadAll(IBdoLog log = null)
        {
            if (_store == null) return false;

            var childLog = log?.NewLog();

            // we load libraries

            var loaded = true;

            _loadOptions.Sources ??= [];
            if (_loadOptions.Sources.Any() != true)
            {
                _loadOptions.AddSource(DatasourceKind.Memory);
            }

            _loadOptions.References = _appDomain.GetAssemblyReferences(_loadOptions.References?.ToArray())?.ToArray();

            if (_loadOptions?.References?.Any() != true)
            {
                childLog?.AddEvent(BdoEventKinds.Message, "No extensions found");
            }
            else
            {
                var loadedAssemblyNames = new List<string>();

                foreach (var reference in _loadOptions.References)
                {
                    if (reference != null)
                    {
                        loaded &= LoadAll(reference, loadedAssemblyNames, childLog);

                        if (log?.HasEvent(BdoEventKinds.Error, BdoEventKinds.Exception, BdoEventKinds.Warning) == true)
                        {
                            log?.AddChild(childLog, title: "Loading extension '" + (reference?.AssemblyName ?? "?") + "'");
                        }
                        else
                        {
                            log?.AddEvent(BdoEventKinds.Message, "Extension '" + (reference?.AssemblyName ?? "?") + "' loaded");
                        }
                    }
                }
            }

            return loaded;
        }

        /// <summary>
        /// Loads all the extensions from the specifeid reference.
        /// </summary>
        /// <param key="reference">The assembly reference to consider.</param>
        /// <param key="loadedAssemblyNames">The loaded assembly names to consider.</param>
        /// <param key="log">The log to consider.</param>
        /// <returns>Returns the loaded library.</returns>
        private bool LoadAll(
            IBdoAssemblyReference reference,
            List<string> loadedAssemblyNames,
            IBdoLog log = null)
        {
            var loaded = true;

            if (reference != null && _loadOptions?.Sources != null)
            {
                var referenceUniqueName = reference.Key();

                if (!loadedAssemblyNames?.Any(q => referenceUniqueName.BdoKeyEquals(q)) == true)
                {
                    // we update the list of loaded assemblies

                    loadedAssemblyNames.Add(referenceUniqueName);

                    // first we load the assembly

                    IBdoLog newLog = log?.NewLog()
                        .WithTitle("Loading package '" + reference.AssemblyName + "'");

                    try
                    {
                        Assembly assembly = null;

                        foreach (var source in _loadOptions?.Sources)
                        {
                            IBdoLog childLog = newLog?.InsertChild(
                                BdoEventKinds.Message,
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
                                        childLog?.AddEvent(BdoEventKinds.Error, "Could not find the assembly file path '" + filePath + "'");
                                        loaded = false;
                                    }
                                    else
                                    {
                                        assembly = _appDomain.LoadAssemblyFromFile(filePath, childLog);

                                        if (assembly == null)
                                        {
                                            childLog?.AddEvent(BdoEventKinds.Error, "Could not load assembly '" + filePath + "'");
                                            loaded = false;
                                        }
                                        else
                                        {
                                            childLog?.AddEvent(BdoEventKinds.Checkpoint, "Loading assembly from file '" + filePath + "'");
                                            assembly = Assembly.LoadFrom(filePath);
                                        }
                                    }
                                    break;
                                case DatasourceKind.RestApi:
                                    break;
                            }

                            if (assembly != null)
                            {
                                childLog?.AddEvent(BdoEventKinds.Message, "Assembly '" + reference.ToString() + " loaded");
                                break;
                            }
                        }

                        // if we have an assembly then we index library items

                        if (assembly == null)
                        {
                            if (log.HasEvent(BdoEventKinds.Error, BdoEventKinds.Exception, BdoEventKinds.Warning))
                            {
                                log?.AddChild(newLog);
                            }

                            loaded = false;
                        }
                        else
                        {
                            // we get the extension definition

                            var packageDefinition = ExtractPackageDefinition(assembly, null, log);
                            if (packageDefinition != null)
                            {
                                packageDefinition.Alias = reference.Alias;
                            }

                            // we load the using assemblies

                            if (packageDefinition?.UsingAssemblyReferences?.Any() == true)
                            {
                                foreach (var usingReference in packageDefinition?.UsingAssemblyReferences)
                                {
                                    IBdoLog subChildLog = log?.NewLog()
                                        .WithTitle("Loading using extensions...");
                                    loaded &= LoadAll(usingReference, loadedAssemblyNames, subChildLog);
                                }
                            }

                            // we load the item definition specifiying the extension definition

                            var extensionKinds = _loadOptions.ExtensionKinds?.ToArray()
                                ??
                                [
                                    BdoExtensionKinds.Connector,
                                    BdoExtensionKinds.Entity,
                                    BdoExtensionKinds.Function,
                                    BdoExtensionKinds.Task
                                ];

                            foreach (var extensionKind in extensionKinds)
                            {
                                var subChildLog = log?.NewLog();

                                int count = LoadDictionary(assembly, extensionKind, packageDefinition, subChildLog);

                                if (subChildLog?.HasEvent(
                                    BdoEventKinds.Error,
                                    BdoEventKinds.Exception,
                                    BdoEventKinds.Warning) == true)
                                {
                                    log?.AddChild(
                                        subChildLog,
                                        title: "Dictionary '" + extensionKind.ToString() + "' not loaded correctly (" + count.ToString() + " items added)");
                                }
                                else
                                {
                                    log?.AddEvent(BdoEventKinds.Message, "Dictionary '" + extensionKind.ToString() + "' loaded (" + count.ToString() + " items added)");
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