using BindOpen.Data;
using BindOpen.Data.Items;
using BindOpen.Extensions.Modeling;
using BindOpen.Logging;
using BindOpen.Runtime.Definitions;
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
        /// Loads the entity dico from the specified assembly.
        /// </summary>
        /// <param key="assembly">The assembly to consider.</param>
        /// <param key="extensionDefinition">The extension definition to consider.</param>
        /// <param key="log">The log to consider.</param>
        /// <returns></returns>
        private int LoadEntityDictionaryFromAssembly(
            Assembly assembly,
            IBdoPackageDefinition extensionDefinition,
            IBdoLog log = null)
        {
            if (assembly == null)
            {
                return -1;
            }

            // we load the entity dico from the assembly

            var dico = ExtractDictionaryFromAssembly<IBdoEntityDefinition>(assembly, log);


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

                foreach (PropertyInfo property in type.GetProperties().Where(p => p.GetCustomAttributes(typeof(BdoPropertyAttribute)).Any()))
                {
                    definition.DetailSpec.Add(BdoMeta.NewSpec(property.Name, property.PropertyType));
                }

                // we build the runtime definition

                if (dico != null)
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
