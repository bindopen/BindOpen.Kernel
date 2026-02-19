using BindOpen.Data;
using BindOpen.Logging;
using BindOpen.Scoping.Connectors;
using BindOpen.Scoping.Entities;
using BindOpen.Scoping.Functions;
using BindOpen.Scoping.Tasks;
using System;
using System.IO;
using System.Reflection;
using System.Xml.Serialization;

namespace BindOpen.Scoping;

/// <summary>
/// This partial class handles the extraction and loading of extension dictionaries from assemblies.
/// </summary>
internal partial class BdoExtensionStoreLoader : BdoObject, IBdoExtensionStoreLoader
{
    // ------------------------------------------
    // PRIVATE METHODS
    // ------------------------------------------

    #region Private Methods

    /// <summary>
    /// Loads the extension dictionary of the specified kind from the given assembly.
    /// </summary>
    /// <param name="assembly">The assembly to scan.</param>
    /// <param name="kind">The kind of extensions to load (Connector, Entity, etc.).</param>
    /// <param name="packageDefinition">The package definition associated with the assembly.</param>
    /// <param name="log">The log to populate with loading events.</param>
    /// <returns>Returns the number of loaded items; -1 if an error occurred.</returns>
    private int LoadDictionary(
        Assembly assembly,
        BdoExtensionKinds kind,
        IBdoPackageDefinition packageDefinition = null,
        IBdoLog log = null)
    {
        if (assembly == null) return -1;

        // Route to the specific loader based on the extension kind
        return kind switch
        {
            BdoExtensionKinds.Connector => LoadConnectorDictionaryFromAssembly(assembly, packageDefinition, log),
            BdoExtensionKinds.Entity => LoadEntityDictionaryFromAssembly(assembly, packageDefinition, log),
            BdoExtensionKinds.Function => LoadFunctionDictionaryFromAssembly(assembly, packageDefinition, log),
            BdoExtensionKinds.Task => LoadTaskDictionaryFromAssembly(assembly, packageDefinition, log),
            _ => -1
        };
    }

    /// <summary>
    /// Extracts a dictionary of the specified type from an assembly's manifest resources.
    /// </summary>
    /// <typeparam name="T">The type of extension definition.</typeparam>
    /// <param name="assembly">The assembly containing the resources.</param>
    /// <param name="log">The log to populate with diagnostic information.</param>
    /// <returns>Returns the deserialized dictionary if successful; otherwise, default.</returns>
    private static ITBdoExtensionDictionary<T> ExtractDictionaryFromAssembly<T>(
        Assembly assembly,
        IBdoLog log = null) where T : IBdoExtensionDefinition
    {
        if (assembly == null) return default;

        ITBdoExtensionDictionary<T> dico = default;
        string resourceFileName = GetDictionaryResourceName<T>();

        // Find the resource that matches our expected dictionary name
        string resourceFullName = Array.Find(
            assembly.GetManifestResourceNames(),
            p => p.EndsWith(resourceFileName, StringComparison.OrdinalIgnoreCase));

        if (resourceFullName == null)
        {
            log?.AddEvent(BdoEventKinds.Warning, $"No dictionary resource named '{resourceFileName}' found in assembly.");
            return default;
        }

        try
        {
            using Stream stream = assembly.GetManifestResourceStream(resourceFullName);
            if (stream == null)
            {
                log?.AddEvent(BdoEventKinds.Error, $"Could not open the resource stream '{resourceFullName}'.");
            }
            else
            {
                Type type = GetDictionaryType<T>();
                if (type != null)
                {
                    XmlSerializer serializer = new(type);
                    dico = (TBdoExtensionDictionary<T>)serializer.Deserialize(stream);
                }
            }
        }
        catch (Exception ex)
        {
            log?.AddException(ex);
        }

        return dico;
    }

    /// <summary>
    /// Gets the standard resource name for the specified extension definition kind.
    /// </summary>
    private static string GetDictionaryResourceName<T>() where T : IBdoExtensionDefinition
    {
        BdoExtensionKinds itemKind = typeof(T).GetExtensionKind();

        return itemKind switch
        {
            BdoExtensionKinds.Connector => "BindOpen.Connectors",
            BdoExtensionKinds.Entity => "BindOpen.Entities",
            BdoExtensionKinds.Function => "BindOpen.Functions",
            BdoExtensionKinds.Task => "BindOpen.Tasks",
            _ => null
        };
    }

    /// <summary>
    /// Gets the concrete dictionary type associated with the extension definition kind.
    /// </summary>
    private static Type GetDictionaryType<T>() where T : IBdoExtensionDefinition
    {
        BdoExtensionKinds itemKind = typeof(T).GetExtensionKind();

        return itemKind switch
        {
            BdoExtensionKinds.Connector => typeof(BdoConnectorDictionary),
            BdoExtensionKinds.Entity => typeof(BdoEntityDictionary),
            BdoExtensionKinds.Function => typeof(BdoFunctionDictionary),
            BdoExtensionKinds.Task => typeof(BdoTaskDictionary),
            _ => null
        };
    }

    #endregion
}