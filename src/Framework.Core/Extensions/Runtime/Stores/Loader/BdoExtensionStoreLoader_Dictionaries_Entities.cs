using BindOpen.Framework.Core.Data.Elements;
using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.Extensions.Attributes;
using BindOpen.Framework.Core.Extensions.Definition.Dictionaries;
using BindOpen.Framework.Core.Extensions.Definition.Extensions;
using BindOpen.Framework.Core.Extensions.Definition.Items;
using BindOpen.Framework.Core.Extensions.Runtime.Items;
using BindOpen.Framework.Core.System.Diagnostics;
using System;
using System.Linq;
using System.Reflection;

namespace BindOpen.Framework.Core.Extensions.Runtime.Stores
{
    /// <summary>
    /// This class represents a extension scope loader.
    /// </summary>
    internal partial class BdoExtensionStoreLoader : DataItem, IBdoExtensionStoreLoader
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

            IBdoEntityDictionaryDto dictionaryDto = (IBdoEntityDictionaryDto)ExtractDictionaryFromAssembly<BdoEntityDefinitionDto>(assembly, log);


            // we feach entity classes

            var types = assembly.GetTypes().Where(p => typeof(IBdoEntity).IsAssignableFrom(p));
            int count = 0;
            foreach (Type type in types)
            {
                IBdoEntityDefinitionDto definitionDto = new BdoEntityDefinitionDto();

                if (type.GetCustomAttributes(typeof(BdoEntityAttribute)).FirstOrDefault() is BdoEntityAttribute entityAttribute)
                {
                    UpdateDictionary(definitionDto, entityAttribute);
                }
                definitionDto.ItemClass = type.FullName;
                definitionDto.LibraryId = extensionDefinition?.Dto?.Id;

                foreach (PropertyInfo property in type.GetProperties().Where(p => p.GetCustomAttributes(typeof(DetailPropertyAttribute)).Any()))
                {
                    definitionDto.DetailSpec.Add(ElementSpecFactory.Create(property.Name, property.PropertyType));
                }

                // we build the runtime definition

                IBdoEntityDefinition itemDefinition = new BdoEntityDefinition(extensionDefinition, definitionDto)
                {
                    RuntimeType = type
                };

                if (dictionaryDto != null)
                {
                    // retrieve the definition index

                    // update definition with index
                }

                _store.Add<IBdoEntityDefinition>(itemDefinition);

                count++;
            }

            return count;
        }
    }
}
