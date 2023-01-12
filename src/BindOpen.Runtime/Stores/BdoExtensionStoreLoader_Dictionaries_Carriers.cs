using BindOpen.Extensions.Modeling;
using BindOpen.Logging;
using BindOpen.Meta;
using BindOpen.Meta.Elements;
using BindOpen.Meta.Items;
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
        /// Loads the carrier dico from the specified assembly.
        /// </summary>
        /// <param name="assembly">The assembly to consider.</param>
        /// <param name="extensionDefinition">The extension definition to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <returns></returns>
        private int LoadCarrierDictionaryFromAssembly(
            Assembly assembly,
            IBdoExtensionDefinition extensionDefinition,
            IBdoLog log = null)
        {
            if (assembly == null)
            {
                return -1;
            }

            // we load the carrier dico from the assembly

            var dico = ExtractDictionaryFromAssembly<IBdoCarrierDefinition>(assembly, log);

            // we feach carrier classes

            var types = assembly.GetTypes().Where(p => typeof(IBdoCarrier).IsAssignableFrom(p) && !p.IsAbstract);
            int count = 0;
            foreach (Type type in types)
            {
                var definition = BdoRuntime.NewCarrierDefinition(extensionDefinition);
                definition.ItemClass = type.FullName;
                definition.LibraryId = extensionDefinition?.Id;
                definition.RuntimeType = type;

                // we update definition from carrier attribute

                if (type.GetCustomAttribute(typeof(BdoCarrierAttribute)) is BdoCarrierAttribute carrierAttribute)
                {
                    UpdateDictionary(definition, carrierAttribute);
                }

                // we create the detail specification from detail property attributes

                definition.DetailSpec = BdoMeta.NewSpecSet();
                foreach (PropertyInfo property in type.GetProperties().Where(p => p.GetCustomAttributes(typeof(BdoMetaAttribute)).Any()))
                {
                    definition.DetailSpec.Add(BdoMeta.NewSpec(property.Name, property.PropertyType));
                }

                // we build the runtime definition

                if (dico != null)
                {
                    // retrieve the definition index

                    // update definition with index
                }

                _store.Add<IBdoCarrierDefinition>(definition);

                count++;
            }

            return count;
        }
    }
}
