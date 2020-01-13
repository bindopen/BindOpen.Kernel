using BindOpen.Framework.Data.Elements;
using BindOpen.Framework.Data.Items;
using BindOpen.Framework.Extensions.Attributes;
using BindOpen.Framework.Extensions.Definition;
using BindOpen.Framework.System.Diagnostics;
using System;
using System.Linq;
using System.Reflection;

namespace BindOpen.Framework.Extensions.Runtime
{
    /// <summary>
    /// This class represents a extension scope loader.
    /// </summary>
    internal partial class BdoExtensionStoreLoader : DataItem, IBdoExtensionStoreLoader
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

            IBdoCarrierDictionaryDto dictionaryDto = (IBdoCarrierDictionaryDto)ExtractDictionaryFromAssembly<BdoCarrierDefinitionDto>(assembly, log);

            // we feach carrier classes

            var types = assembly.GetTypes().Where(p => typeof(IBdoCarrier).IsAssignableFrom(p) && !p.IsAbstract);
            int count = 0;
            foreach (Type type in types)
            {
                IBdoCarrierDefinitionDto definitionDto = new BdoCarrierDefinitionDto();

                // we update definition from carrier attribute

                if (type.GetCustomAttribute(typeof(BdoCarrierAttribute)) is BdoCarrierAttribute carrierAttribute)
                {
                    UpdateDictionary(definitionDto, carrierAttribute);
                }
                definitionDto.ItemClass = type.FullName;
                definitionDto.LibraryId = extensionDefinition?.Dto?.Id;

                // we create the detail specification from detail property attributes

                foreach (PropertyInfo property in type.GetProperties().Where(p => p.GetCustomAttributes(typeof(DetailPropertyAttribute)).Any()))
                {
                    definitionDto.DetailSpec.Add(ElementSpecFactory.Create(property.Name, property.PropertyType));
                }

                // we build the runtime definition

                IBdoCarrierDefinition itemDefinition = new BdoCarrierDefinition(extensionDefinition, definitionDto)
                {
                    RuntimeType = type
                };

                if (dictionaryDto != null)
                {
                    // retrieve the definition index

                    // update definition with index
                }

                _store.Add<IBdoCarrierDefinition>(itemDefinition);

                count++;
            }

            return count;
        }
    }
}
