using BindOpen.Data;
using BindOpen.Data.Meta;
using BindOpen.Data.Schema;
using BindOpen.Logging;
using BindOpen.Scoping;
using BindOpen.Scoping.Connectors;
using System.Linq;
using System.Reflection;

namespace BindOpen.Scoping
{
    /// <summary>
    /// This class represents a extension scope loader.
    /// </summary>
    internal partial class BdoExtensionStoreLoader : BdoObject, IBdoExtensionStoreLoader
    {
        /// <summary>
        /// Loads the connector dico from the specified assembly.
        /// </summary>
        /// <param key="assembly">The assembly to consider.</param>
        /// <param key="packageDefinition">The extension definition to consider.</param>
        /// <param key="log">The log to consider.</param>
        /// <returns></returns>
        private int LoadConnectorDictionaryFromAssembly(
            Assembly assembly,
            IBdoPackageDefinition packageDefinition,
            IBdoLog log = null)
        {
            if (assembly == null)
            {
                return -1;
            }

            // we feach connector classes

            var types = assembly.GetTypes().Where(p => typeof(IBdoConnector).IsAssignableFrom(p) && !p.IsAbstract);

            int count = 0;

            if (types?.Count() > 0)
            {
                // we load the entity dico from the assembly

                var dico = ExtractDictionaryFromAssembly<IBdoConnectorDefinition>(assembly, log);

                foreach (var type in types)
                {
                    var definition = new BdoConnectorDefinition(null, packageDefinition)
                    {
                        LibraryId = packageDefinition?.Identifier,
                    };

                    definition.UpdateFrom(type);

                    // we create the detail schema from detail property attributes

                    foreach (var prop in type.GetProperties().Where(p => p.GetCustomAttributes(typeof(BdoPropertyAttribute)).Any()))
                    {
                        var schema = BdoData.NewSchema();
                        schema.UpdateFrom(prop, typeof(BdoPropertyAttribute));
                        definition.Add(schema);
                    }

                    // we build the runtime definition

                    if (dico != null)
                    {
                        var indexDefinition = dico.Get(definition.Name);
                        definition.Update(indexDefinition);
                    }

                    _store.Add(definition);

                    count++;
                }
            }

            return count;
        }
    }
}
