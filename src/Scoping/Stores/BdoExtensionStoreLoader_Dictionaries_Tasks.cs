using BindOpen.Data;
using BindOpen.Data.Meta;
using BindOpen.Data.Schema;
using BindOpen.Logging;
using BindOpen.Scoping.Tasks;
using System.Linq;
using System.Reflection;

namespace BindOpen.Scoping;

/// <summary>
/// This partial class provides methods to load task definitions from assemblies.
/// </summary>
internal partial class BdoExtensionStoreLoader : BdoObject, IBdoExtensionStoreLoader
{
    /// <summary>
    /// Loads the task dictionary from the specified assembly.
    /// </summary>
    /// <param name="assembly">The assembly to scan for task classes.</param>
    /// <param name="packageDefinition">The package definition to associate with discovered tasks.</param>
    /// <param name="log">The log to populate with loading events.</param>
    /// <returns>Returns the number of tasks successfully loaded; -1 if the assembly is null.</returns>
    private int LoadTaskDictionaryFromAssembly(
        Assembly assembly,
        IBdoPackageDefinition packageDefinition,
        IBdoLog log = null)
    {
        if (assembly == null) return -1;

        // 1. Extract the task dictionary (XML metadata) from assembly resources
        var dictionary = ExtractDictionaryFromAssembly<IBdoTaskDefinition>(assembly, log);

        // 2. Fetch concrete task classes implementing IBdoTask
        var taskTypes = assembly.GetTypes()
            .Where(p => typeof(IBdoTask).IsAssignableFrom(p) && !p.IsInterface && !p.IsAbstract)
            .ToList();

        int count = 0;

        foreach (var type in taskTypes)
        {
            // 3. Initialize the task definition
            var definition = new BdoTaskDefinition(null, packageDefinition)
            {
                LibraryId = packageDefinition?.Identifier,
                RuntimeType = type
            };

            // 4. Update metadata from class attributes (e.g., [BdoTask])
            definition.UpdateFrom(type);

            // 5. Scan properties for BindOpen attributes (Properties, Inputs, Outputs)
            var properties = type.GetProperties();

            foreach (var prop in properties)
            {
                // Handle generic properties
                if (prop.GetCustomAttributes(typeof(BdoPropertyAttribute)).Any())
                {
                    var schema = BdoData.NewSchema();
                    schema.UpdateFrom(prop, typeof(BdoPropertyAttribute));
                    definition.Add(schema);
                }

                // Handle input specific properties
                if (prop.GetCustomAttributes(typeof(BdoInputAttribute)).Any())
                {
                    var schema = BdoData.NewSchema();
                    schema.UpdateFrom(prop, typeof(BdoInputAttribute));
                    definition.Add(schema);
                }

                // Handle output specific properties
                if (prop.GetCustomAttributes(typeof(BdoOutputAttribute)).Any())
                {
                    definition.Outputs ??= BdoData.NewSchemaSet();
                    var schema = BdoData.NewSchema();
                    schema.UpdateFrom(prop, typeof(BdoOutputAttribute));
                    definition.Outputs.Add(schema);
                }
            }

            // 6. Enrich with XML indexed metadata if available
            if (dictionary != null)
            {
                var indexDefinition = dictionary.Get(definition.Name);
                if (indexDefinition != null)
                {
                    definition.Update(indexDefinition);
                }
            }

            // 7. Add the completed definition to the global store
            _store?.Add(definition);
            count++;
        }

        return count;
    }
}