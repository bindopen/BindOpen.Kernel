using System;
using System.Linq;
using System.Reflection;
using BindOpen.Framework.Core.Data.Elements.Factories;
using BindOpen.Framework.Core.Extensions.Attributes;
using BindOpen.Framework.Core.Extensions.Carriers;
using BindOpen.Framework.Core.Extensions.Definitions.Carriers;
using BindOpen.Framework.Core.Extensions.Indexes.Carriers;
using BindOpen.Framework.Core.Extensions.Items.Carriers;
using BindOpen.Framework.Core.System.Diagnostics;

namespace BindOpen.Framework.Core.Extensions.Libraries
{
    /// <summary>
    /// This static class provices methods to index library items.
    /// </summary>
    public static partial class LibraryIndexer
    {
        /// <summary>
        /// References the specified carriers into the specified library.
        /// </summary>
        /// <param name="library">The library to consider.</param>
        /// <param name="assembly">The assembly to consider.</param>
        /// <param name="indexDto">The script word index to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <returns></returns>
        public static int IndexCarriers(
            this ILibrary library,
            Assembly assembly,
            CarrierIndexDto indexDto = null,
            ILog log = null)
        {
            if ((library==null)||(assembly==null))
            {
                return -1;
            }

            log = log ?? new Log();

            // we feach carrier classes

            var types = assembly.GetTypes().Where(p => typeof(ICarrier).IsAssignableFrom(p) && !p.IsAbstract);
            int count = 0;
            foreach(Type type in types)
            {
                ICarrierDefinitionDto definitionDto = new CarrierDefinitionDto();

                // we update definition from carrier attribute

                if (type.GetCustomAttribute(typeof(CarrierAttribute)) is CarrierAttribute carrierAttribute)
                {
                    (definitionDto).Update(carrierAttribute);
                }
                definitionDto.ItemClass = type.FullName;
                definitionDto.LibraryName = library?.Name;

                // we create the detail specification from detail property attributes

                foreach (PropertyInfo property in type.GetProperties().Where(p => p.GetCustomAttribute(typeof(DetailPropertyAttribute)) != null))
                {
                    definitionDto.DetailSpec.Add(ElementSpecFactory.Create(property.Name, property.PropertyType));
                }

                // we build the runtime definition

                ICarrierDefinition definition = new CarrierDefinition(library, definitionDto)
                {
                    RuntimeType = type
                };

                if (indexDto!=null)
                {
                    // retrieve the definition index

                    // update definition with index
                }

                library.Add<ICarrierDefinition>(definition);

                count++;
            }

            return count;
        }
    }
}
