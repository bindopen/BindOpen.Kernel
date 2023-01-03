using BindOpen.Data.Elements;
using BindOpen.Data.Items;
using BindOpen.Extensions.Modeling;
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
        /// Loads the entity dictionary from the specified assembly.
        /// </summary>
        /// <param name="assembly">The assembly to consider.</param>
        /// <param name="extensionDefinition">The extension definition to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <returns></returns>
        private int LoadEntityDictionaryFromAssembly(
            Assembly assembly,
            IBdoExtensionDefinition extensionDefinition,
            IBdoLog log = null)
        {
            if (assembly == null)
            {
                return -1;
            }

            // we load the entity dictionary from the assembly

            var dictionary = ExtractDictionaryFromAssembly<IBdoEntityDefinition>(assembly, log);


            // we feach entity classes

            var types = assembly.GetTypes().Where(p => typeof(IBdoEntity).IsAssignableFrom(p));
            int count = 0;
            foreach (Type type in types)
            {
                var definition = new BdoEntityDefinition(null, extensionDefinition)
                {
                    ItemClass = type.FullName,
                    LibraryId = extensionDefinition?.Id,
                    RuntimeType = type
                };

                if (type.GetCustomAttributes(typeof(BdoEntityAttribute)).FirstOrDefault() is BdoEntityAttribute entityAttribute)
                {
                    UpdateDictionary(definition, entityAttribute);
                }

                foreach (PropertyInfo property in type.GetProperties().Where(p => p.GetCustomAttributes(typeof(BdoElementAttribute)).Any()))
                {
                    definition.DetailSpec.Add(BdoElements.NewSpec(property.Name, property.PropertyType));
                }

                // we build the runtime definition

                if (dictionary != null)
                {
                    // retrieve the definition index

                    // update definition with index
                }

                _store.Add<IBdoEntityDefinition>(definition);

                count++;
            }

            return count;
        }
    }
}
