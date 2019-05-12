using System;
using System.Linq;
using System.Reflection;
using BindOpen.Framework.Core.Data.Elements.Factories;
using BindOpen.Framework.Core.Extensions.Attributes;
using BindOpen.Framework.Core.Extensions.Items.Entities;
using BindOpen.Framework.Core.Extensions.Items.Entities;
using BindOpen.Framework.Core.Extensions.Items.Entities.Definition;
using BindOpen.Framework.Core.Extensions.Items.Entities.Definition.Dto;
using BindOpen.Framework.Core.System.Diagnostics;

namespace BindOpen.Framework.Core.Extensions.Libraries
{
    /// <summary>
    /// This static class provices methods to index library items.
    /// </summary>
    public static partial class LibraryIndexer
    {
        /// <summary>
        /// References the specified entities into the specified library.
        /// </summary>
        /// <param name="library">The library to consider.</param>
        /// <param name="assembly">The assembly to consider.</param>
        /// <param name="isIndexLoaded">Indicates whether item indexes must be loaded.</param>
        /// <param name="log">The log to consider.</param>
        /// <returns></returns>
        public static int IndexEntities(
            this ILibrary library,
            Assembly assembly,
            bool isIndexLoaded = false,
            ILog log = null)
        {
            if ((library==null)||(assembly==null))
            {
                return -1;
            }

            log = log ?? new Log();

            // we feach entity classes

            var types = assembly.GetTypes().Where(p => typeof(IEntity).IsAssignableFrom(p));
            int count = 0;
            foreach(Type type in types)
            {
                IEntityDefinitionDto definition = new EntityDefinitionDto();
                
                if (type.GetCustomAttributes(typeof(EntityAttribute)).FirstOrDefault() is EntityAttribute entityAttribute)
                {
                    definition.Update(entityAttribute);
                }

                foreach (PropertyInfo property in type.GetProperties().Where(p => p.GetCustomAttributes(typeof(DetailPropertyAttribute)).Any()))
                {
                    definition.DetailSpec.Add(ElementSpecFactory.Create(property.Name, property.PropertyType));
                }

                if (isIndexLoaded)
                {
                    //definition.Update()
                }

                count++;
            }

            return count;
        }
    }
}
