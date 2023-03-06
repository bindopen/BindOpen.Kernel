using BindOpen.Data;
using BindOpen.Data.Items;
using BindOpen.Data.Meta;
using BindOpen.Extensions.Connecting;
using BindOpen.Logging;
using BindOpen.Runtime.Definitions;
using System.Linq;
using System.Reflection;

namespace BindOpen.Runtime.Stores
{
    /// <summary>
    /// This class represents a extension scope loader.
    /// </summary>
    internal partial class BdoExtensionStoreLoader : BdoItem, IBdoExtensionStoreLoader
    {
        /// <summary>
        /// Loads the connector dico from the specified assembly.
        /// </summary>
        /// <param key="assembly">The assembly to consider.</param>
        /// <param key="extensionDefinition">The extension definition to consider.</param>
        /// <param key="log">The log to consider.</param>
        /// <returns></returns>
        private int LoadConnectorDictionaryFromAssembly(
            Assembly assembly,
            IBdoPackageDefinition extensionDefinition,
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
                    var definition = new BdoConnectorDefinition(null, extensionDefinition)
                    {
                        ClassReference = BdoData.Class(type),
                        LibraryId = extensionDefinition?.Id,
                        RuntimeType = type
                    };

                    // we update definition from connector attribute

                    foreach (var attribute in type.GetCustomAttributes(typeof(BdoConnectorAttribute)))
                    {
                        var connectorAttribute = attribute as BdoConnectorAttribute;
                        UpdateDictionary(definition, connectorAttribute);
                    }

                    // we create the detail specification from detail property attributes

                    foreach (var property in type.GetProperties().Where(p => p.GetCustomAttributes(typeof(BdoPropertyAttribute)).Any()))
                    {
                        definition.SpecDetail.Add(
                            BdoMeta.NewSpec(property.Name, property.PropertyType));
                    }

                    // we build the runtime definition

                    if (dico != null)
                    {
                        // retrieve the definition index

                        //dico.WithDefinitions

                        // update definition with index
                    }

                    _store.Add(definition);

                    count++;
                }
            }

            return count;
        }
    }
}
