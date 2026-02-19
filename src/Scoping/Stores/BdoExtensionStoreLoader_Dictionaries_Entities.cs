using BindOpen.Data;
using BindOpen.Data.Meta;
using BindOpen.Data.Schema;
using BindOpen.Logging;
using BindOpen.Scoping.Entities;
using System.Linq;
using System.Reflection;

namespace BindOpen.Scoping;

/// <summary>
/// This partial class provides methods to load entity definitions from assemblies.
/// </summary>
internal partial class BdoExtensionStoreLoader : BdoObject, IBdoExtensionStoreLoader
{
    /// <summary>
    /// Loads the entity dictionary from the specified assembly.
    /// </summary>
    /// <param name="assembly">The assembly to scan.</param>
    /// <param name="packageDefinition">The package definition to associate with discovered entities.</param>
    /// <param name="log">The log to populate with loading events.</param>
    /// <returns>Returns the number of entities successfully loaded; -1 if the assembly is null.</returns>
    private int LoadEntityDictionaryFromAssembly(
        Assembly assembly,
        IBdoPackageDefinition packageDefinition,
        IBdoLog log = null)
    {
        if (assembly == null) return -1;

        // 1. Extract the entity dictionary from the assembly resources (XML metadata)
        var dictionary = ExtractDictionaryFromAssembly<IBdoEntityDefinition>(assembly, log);

        // 2. Fetch concrete entity classes implementing IBdoEntity
        var entityTypes = assembly.GetTypes()
            .Where(p => typeof(IBdoEntity).IsAssignableFrom(p) && !p.IsInterface && !p.IsAbstract)
            .ToList();

        int count = 0;

        foreach (var type in entityTypes)
        {
            // 3. Initialize the definition with its runtime context
            var definition = new BdoEntityDefinition(null, packageDefinition)
            {
                LibraryId = packageDefinition?.Identifier,
                RuntimeType = type
            };

            // 4. Extract metadata from class-level attributes
            definition.UpdateFrom(type);

            // 5. Scan properties for BindOpen schema attributes
            var propertyInfos = type.GetProperties()
                .Where(p => p.GetCustomAttributes(typeof(BdoPropertyAttribute)).Any());

            foreach (var prop in propertyInfos)
            {
                var schema = BdoData.NewSchema();
                schema.UpdateFrom(prop, typeof(BdoPropertyAttribute));
                definition.Add(schema);
            }

            // 6. Enrich the definition with indexed XML metadata if available
            if (dictionary != null)
            {
                var indexDefinition = dictionary.Get(definition.Name);
                if (indexDefinition != null)
                {
                    definition.Update(indexDefinition);
                }
            }

            // 7. Register the entity in the global store
            _store?.Add(definition);
            count++;
        }

        return count;
    }
}