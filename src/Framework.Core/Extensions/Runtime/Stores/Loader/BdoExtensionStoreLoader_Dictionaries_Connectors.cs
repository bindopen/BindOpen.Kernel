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
        /// Loads the connector dictionary from the specified assembly.
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

            // we load the carrier dictionary from the assembly

            IBdoConnectorDictionaryDto dictionaryDto = (IBdoConnectorDictionaryDto)ExtractDictionaryFromAssembly<BdoConnectorDefinitionDto>(assembly, log);

            // we feach connector classes

            var types = assembly.GetTypes().Where(p => typeof(IBdoConnector).IsAssignableFrom(p) && !p.IsAbstract);
            int count = 0;
            foreach (Type type in types)
            {
                IBdoConnectorDefinitionDto definitionDto = new BdoConnectorDefinitionDto();

                // we update definition from connector attribute

                if (type.GetCustomAttribute(typeof(BdoConnectorAttribute)) is BdoConnectorAttribute connectorAttribute)
                {
                    UpdateDictionary(definitionDto, connectorAttribute);
                }
                definitionDto.ItemClass = type.FullName;
                definitionDto.LibraryId = extensionDefinition?.Dto?.Id;

                // we create the detail specification from detail property attributes

                foreach (PropertyInfo property in type.GetProperties().Where(p => p.GetCustomAttributes(typeof(DetailPropertyAttribute)).Any()))
                {
                    definitionDto.DatasourceDetailSpec.Add(ElementSpecFactory.Create(property.Name, property.PropertyType));
                }

                // we build the runtime definition

                IBdoConnectorDefinition itemDefinition = new BdoConnectorDefinition(extensionDefinition, definitionDto)
                {
                    RuntimeType = type
                };

                if (dictionaryDto != null)
                {
                    // retrieve the definition index

                    // update definition with index
                }

                _store.Add<IBdoConnectorDefinition>(itemDefinition);

                count++;
            }

            return count;
        }
    }
}
