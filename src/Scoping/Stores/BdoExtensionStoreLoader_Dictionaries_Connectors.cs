using BindOpen.Data;
using BindOpen.Data.Meta;
using BindOpen.Data.Schema;
using BindOpen.Logging;
using BindOpen.Scoping.Connectors;
using System.Linq;
using System.Reflection;

namespace BindOpen.Scoping;

/// <summary>
/// This partial class handles the discovery and registration of connector definitions from assemblies.
/// </summary>
internal partial class BdoExtensionStoreLoader : BdoObject, IBdoExtensionStoreLoader
{
    /// <summary>
    /// Loads connector definitions from the specified assembly into the store.
    /// </summary>
    /// <param name="assembly">The assembly to scan for connector classes.</param>
    /// <param name="packageDefinition">The package definition to associate with discovered connectors.</param>
    /// <param name="log">The log to populate with loading events.</param>
    /// <returns>Returns the number of connectors successfully loaded; -1 if the assembly is null.</returns>
    private int LoadConnectorDictionaryFromAssembly(
        Assembly assembly,
        IBdoPackageDefinition packageDefinition,
        IBdoLog log = null)
    {
        if (assembly == null) return -1;

        // 1. Fetch non-abstract classes that implement IBdoConnector
        var connectorTypes = assembly.GetTypes()
            .Where(p => typeof(IBdoConnector).IsAssignableFrom(p) && !p.IsAbstract)
            .ToList();

        int count = 0;

        if (connectorTypes.Any())
        {
            // 2. Extract the connector dictionary resource from the assembly (XML metadata)
            var dictionary = ExtractDictionaryFromAssembly<IBdoConnectorDefinition>(assembly, log);

            foreach (var type in connectorTypes)
            {
                // 3. Initialize the runtime definition
                var definition = new BdoConnectorDefinition(null, packageDefinition)
                {
                    LibraryId = packageDefinition?.Identifier,
                    RuntimeType = type // Ensure the runtime type is tracked
                };

                // 4. Update metadata from class attributes (e.g., [BdoConnector])
                definition.UpdateFrom(type);

                // 5. Build schema from property attributes (discovery of configuration parameters)
                var propertyInfos = type.GetProperties()
                    .Where(p => p.GetCustomAttributes(typeof(BdoPropertyAttribute)).Any());

                foreach (var prop in propertyInfos)
                {
                    var schema = BdoData.NewSchema();
                    schema.UpdateFrom(prop, typeof(BdoPropertyAttribute));
                    definition.AddItems(schema);
                }

                // 6. Merge with XML dictionary metadata if available
                if (dictionary != null)
                {
                    var indexDefinition = dictionary.Get(definition.Name);
                    if (indexDefinition != null)
                    {
                        definition.Update(indexDefinition);
                    }
                }

                // 7. Register the final definition in the store
                _store?.Add(definition);
                count++;
            }
        }

        return count;
    }
}