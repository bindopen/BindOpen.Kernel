using BindOpen.Data;
using BindOpen.Data.Assemblies;
using BindOpen.Data.Helpers;
using BindOpen.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace BindOpen.Scoping;

/// <summary>
/// This class represents a specialized loader for the BindOpen extension store.
/// </summary>
/// <remarks>
/// It scans assemblies from various sources (Memory, File System) to discover 
/// and register extension definitions.
/// </remarks>
internal partial class BdoExtensionStoreLoader : BdoObject, IBdoExtensionStoreLoader
{
    private readonly AppDomain _appDomain;
    private readonly IBdoExtensionStore _store;
    private readonly IExtensionLoadOptions _loadOptions;

    // ------------------------------------------
    // CONSTRUCTORS
    // ------------------------------------------

    /// <summary>
    /// Initializes a new instance of the <see cref="BdoExtensionStoreLoader"/> class.
    /// </summary>
    /// <param name="appDomain">The application domain to use for loading.</param>
    /// <param name="store">The destination extension store.</param>
    /// <param name="loadOptions">The options guiding the loading process.</param>
    public BdoExtensionStoreLoader(
        AppDomain appDomain,
        IBdoExtensionStore store,
        IExtensionLoadOptions loadOptions)
    {
        _appDomain = appDomain ?? AppDomain.CurrentDomain;
        _store = store;
        _loadOptions = loadOptions ?? new ExtensionLoadOptions();
    }

    // ------------------------------------------
    // PUBLIC METHODS
    // ------------------------------------------

    /// <summary>
    /// Loads all configured extensions into the store.
    /// </summary>
    /// <param name="log">The log to populate with the loading report.</param>
    /// <returns>Returns True if all extensions were loaded successfully; otherwise, False.</returns>
    public bool LoadAll(IBdoLog log = null)
    {
        if (_store == null) return false;

        var childLog = log?.NewLog();
        bool loaded = true;

        // Ensure we have at least a default source
        _loadOptions.Sources ??= new List<(DatasourceKind Kind, string Uri)>();
        if (!_loadOptions.Sources.Any())
        {
            _loadOptions.AddSource(DatasourceKind.Memory);
        }

        // Resolve assembly references based on the current AppDomain
        _loadOptions.References = _appDomain.GetAssemblyReferences(_loadOptions.References?.ToArray())?.ToList();

        if (_loadOptions.References?.Any() != true)
        {
            childLog?.AddEvent(BdoEventKinds.Message, "No extension references found to load.");
        }
        else
        {
            var loadedAssemblyNames = new List<string>();

            foreach (var reference in _loadOptions.References)
            {
                if (reference == null) continue;

                loaded &= LoadAll(reference, loadedAssemblyNames, childLog);

                // Report progress to the parent log
                string assemblyName = reference.AssemblyName ?? "?";
                if (childLog?.HasEvent(BdoEventKinds.Error, BdoEventKinds.Exception) == true)
                {
                    log?.AddChild(childLog, title: $"Issues encountered while loading extension '{assemblyName}'");
                }
                else
                {
                    log?.AddEvent(BdoEventKinds.Message, $"Extension '{assemblyName}' loaded successfully.");
                }
            }
        }

        return loaded;
    }

    // ------------------------------------------
    // PRIVATE METHODS
    // ------------------------------------------

    /// <summary>
    /// Loads extensions from a specific assembly reference and its dependencies.
    /// </summary>
    private bool LoadAll(
        IBdoAssemblyReference reference,
        List<string> loadedAssemblyNames,
        IBdoLog log = null)
    {
        if (reference == null || _loadOptions?.Sources == null) return false;

        string referenceKey = reference.Key();
        if (loadedAssemblyNames.Any(q => referenceKey.BdoKeyEquals(q))) return true;

        bool loaded = true;
        loadedAssemblyNames.Add(referenceKey);

        IBdoLog packageLog = log?.NewLog().WithTitle($"Processing package '{reference.AssemblyName}'");

        try
        {
            Assembly assembly = null;

            // Attempt to load from various data sources
            foreach (var source in _loadOptions.Sources)
            {
                IBdoLog sourceLog = packageLog?.InsertChild(BdoEventKinds.Message, title: $"Probing source: {source.Kind}");

                switch (source.Kind)
                {
                    case DatasourceKind.Memory:
                        assembly = _appDomain.LoadAssembly(reference, sourceLog);
                        break;

                    case DatasourceKind.Repository:
                        string fileName = string.IsNullOrEmpty(reference.AssemblyFileName)
                            ? reference.AssemblyName + ".dll"
                            : reference.AssemblyFileName;

                        string filePath = Path.Combine(source.Uri.ToPath(), fileName);

                        if (File.Exists(filePath))
                        {
                            assembly = _appDomain.LoadAssemblyFromFile(filePath, sourceLog);
                        }
                        else
                        {
                            sourceLog?.AddEvent(BdoEventKinds.Error, $"File not found: {filePath}");
                        }
                        break;
                }

                if (assembly != null) break;
            }

            if (assembly == null)
            {
                log?.AddChild(packageLog);
                return false;
            }

            // Extract package metadata and discover definitions
            var packageDefinition = ExtractPackageDefinition(assembly, null, log);
            if (packageDefinition != null)
            {
                packageDefinition.Alias = reference.Alias;
            }

            // Recursive loading of dependencies ("Using" references)
            if (packageDefinition?.UsingAssemblyReferences?.Any() == true)
            {
                foreach (var usingRef in packageDefinition.UsingAssemblyReferences)
                {
                    loaded &= LoadAll(usingRef, loadedAssemblyNames, log);
                }
            }

            // Load dictionaries for each extension kind
            var kindsToLoad = _loadOptions.ExtensionKinds?.Any() == true
                ? _loadOptions.ExtensionKinds
                : new[] { BdoExtensionKinds.Connector, BdoExtensionKinds.Entity, BdoExtensionKinds.Function, BdoExtensionKinds.Task };

            foreach (var kind in kindsToLoad)
            {
                var dictLog = log?.NewLog();
                int itemCount = LoadDictionary(assembly, kind, packageDefinition, dictLog);

                if (dictLog?.HasEvent(BdoEventKinds.Error, BdoEventKinds.Exception) == true)
                {
                    log?.AddChild(dictLog, title: $"Dictionary '{kind}' failed ({itemCount} items found)");
                }
                else if (itemCount > 0)
                {
                    log?.AddEvent(BdoEventKinds.Message, $"Dictionary '{kind}' loaded with {itemCount} items.");
                }
            }
        }
        catch (Exception ex)
        {
            log?.AddException(ex);
            loaded = false;
        }

        return loaded;
    }

    // ------------------------------------------
    // DISPOSAL
    // ------------------------------------------

    #region IDisposable Implementation

    private bool _isDisposed;

    protected override void Dispose(bool isDisposing)
    {
        if (_isDisposed) return;

        if (isDisposing)
        {
            _loadOptions?.Dispose();
        }

        _isDisposed = true;
        base.Dispose(isDisposing);
    }

    #endregion   
}