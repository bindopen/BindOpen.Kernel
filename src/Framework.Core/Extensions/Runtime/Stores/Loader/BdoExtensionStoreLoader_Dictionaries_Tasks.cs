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
        /// Loads the task dictionary from the specified assembly.
        /// </summary>
        /// <param name="assembly">The assembly to consider.</param>
        /// <param name="extensionDefinition">The extension definition to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <returns></returns>
        private int LoadTaskDictionaryFromAssembly(
            Assembly assembly,
            IBdoExtensionDefinition extensionDefinition,
            IBdoLog log = null)
        {
            if (assembly == null)
            {
                return -1;
            }

            // we load the carrier dictionary from the assembly

            IBdoTaskDictionaryDto dictionaryDto = (IBdoTaskDictionaryDto)ExtractDictionaryFromAssembly<BdoTaskDefinitionDto>(assembly, log);

            // we feach task classes

            int count = 0;

            var types = assembly.GetTypes().Where(p => typeof(IBdoTask).IsAssignableFrom(p));
            foreach (Type type in types)
            {
                IBdoTaskDefinitionDto definitionDto = new BdoTaskDefinitionDto();

                if (type.GetCustomAttributes(typeof(BdoTaskAttribute)).FirstOrDefault() is BdoTaskAttribute taskAttribute)
                {
                    UpdateDictionary(definitionDto, taskAttribute);
                }
                definitionDto.ItemClass = type.FullName;
                definitionDto.LibraryId = extensionDefinition?.Dto?.Id;

                foreach (PropertyInfo property in type.GetProperties().Where(p => p.GetCustomAttributes(typeof(TaskInputAttribute)).Any()))
                {
                    definitionDto.InputSpecification.Add(ElementSpecFactory.Create(property.Name, property.PropertyType));
                }

                foreach (PropertyInfo property in type.GetProperties().Where(p => p.GetCustomAttributes(typeof(TaskOutputAttribute)).Any()))
                {
                    definitionDto.OutputSpecification.Add(ElementSpecFactory.Create(property.Name, property.PropertyType));
                }

                // we build the runtime definition

                IBdoTaskDefinition itemDefinition = new BdoTaskDefinition(extensionDefinition, definitionDto)
                {
                    RuntimeType = type
                };

                if (dictionaryDto != null)
                {
                    // retrieve the definition index

                    // update definition with index
                }

                _store.Add<IBdoTaskDefinition>(itemDefinition);

                count++;
            }

            return count;
        }
    }
}
