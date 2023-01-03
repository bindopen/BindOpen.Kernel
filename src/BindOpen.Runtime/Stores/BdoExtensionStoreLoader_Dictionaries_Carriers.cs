using BindOpen.Extensions.Modeling;
using BindOpen.Data.Elements;
using BindOpen.Data.Items;
using BindOpen.Runtime.Definition;
using System;
using System.Linq;
using System.Reflection;
using BindOpen.Logging;

namespace BindOpen.Runtime.Stores
{
    /// <summary>
    /// This class represents a extension scope loader.
    /// </summary>
    internal partial class BdoExtensionStoreLoader : BdoItem, IBdoExtensionStoreLoader
    {
        /// <summary>
        /// Loads the carrier dictionary from the specified assembly.
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

            // we load the carrier dictionary from the assembly

            var dictionary = ExtractDictionaryFromAssembly<IBdoCarrierDefinition>(assembly, log);

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

                definition.DetailSpec = BdoElements.NewSpecSet();
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

                _store.Add<IBdoCarrierDefinition>(definition);

                count++;
            }

            return count;
        }
    }
}
