using BindOpen.Data;
using BindOpen.Data.Items;
using BindOpen.Data.Meta;
using BindOpen.Extensions.Connecting;
using BindOpen.Logging;
using BindOpen.Runtime.Definition;
using System;
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
        /// <param name="assembly">The assembly to consider.</param>
        /// <param name="extensionDefinition">The extension definition to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <returns></returns>
        private int LoadConnectorDictionaryFromAssembly(
            Assembly assembly,
            IBdoExtensionDefinition extensionDefinition,
            IBdoLog log = null)
        {
            if (assembly == null)
            {
                return -1;
            }

            // we load the entity dico from the assembly

            var dico = ExtractDictionaryFromAssembly<IBdoConnectorDefinition>(assembly, log);

            // we feach connector classes

            var types = assembly.GetTypes().Where(p => typeof(IBdoConnector).IsAssignableFrom(p) && !p.IsAbstract);
            int count = 0;
            foreach (Type type in types)
            {
                var definition = new BdoConnectorDefinition(null, extensionDefinition)
                {
                    ItemClass = type.FullName,
                    LibraryId = extensionDefinition?.Id,
                    RuntimeType = type
                };

                // we update definition from connector attribute

                if (type.GetCustomAttribute(typeof(BdoConnectorAttribute)) is BdoConnectorAttribute connectorAttribute)
                {
                    UpdateDictionary(definition, connectorAttribute);
                }

                // we create the detail specification from detail property attributes

                foreach (PropertyInfo property in type.GetProperties().Where(p => p.GetCustomAttributes(typeof(BdoDataAttribute)).Any()))
                {
                    definition.DatasourceDetailSpec.Add(
                        BdoMeta.NewSpec(property.Name, property.PropertyType));
                }

                // we build the runtime definition

                if (dico != null)
                {
                    // retrieve the definition index

                    // update definition with index
                }

                _store.Add<IBdoConnectorDefinition>(definition);

                count++;
            }

            return count;
        }
    }
}
