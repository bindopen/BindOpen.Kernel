using System;
using System.Linq;
using System.Reflection;
using BindOpen.Framework.Core.Data.Elements.Factories;
using BindOpen.Framework.Core.Extensions.Attributes;
using BindOpen.Framework.Core.Extensions.Items.Tasks;
using BindOpen.Framework.Core.Extensions.Items.Tasks;
using BindOpen.Framework.Core.Extensions.Items.Tasks.Definition;
using BindOpen.Framework.Core.Extensions.Items.Tasks.Definition.Dto;
using BindOpen.Framework.Core.System.Diagnostics;

namespace BindOpen.Framework.Core.Extensions.Libraries
{
    /// <summary>
    /// This static class provices methods to index library items.
    /// </summary>
    public static partial class LibraryIndexer
    {
        /// <summary>
        /// References the specified items into the specified library.
        /// </summary>
        /// <param name="library">The library to consider.</param>
        /// <param name="assembly">The assembly to consider.</param>
        /// <param name="isIndexLoaded">Indicates whether item indexes must be loaded.</param>
        /// <param name="log">The log to consider.</param>
        /// <returns></returns>
        public static int IndexTasks(
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

            // we feach task classes

            int count = 0;

            var types = assembly.GetTypes().Where(p => typeof(ITask).IsAssignableFrom(p));
            foreach(Type type in types)
            {
                ITaskDefinitionDto definition = new TaskDefinitionDto();

                if (type.GetCustomAttribute(typeof(TaskAttribute)) is TaskAttribute taskAttribute)
                {
                    definition.Update(taskAttribute);
                }

                foreach(PropertyInfo property in type.GetProperties().Where(p => p.GetCustomAttribute(typeof(TaskInputAttribute)) != null))
                {
                    definition.InputSpecification.Add(ElementSpecFactory.Create(property.Name, property.PropertyType));
                }

                foreach (PropertyInfo property in type.GetProperties().Where(p => p.GetCustomAttribute(typeof(TaskOutputAttribute)) != null))
                {
                    definition.OutputSpecification.Add(ElementSpecFactory.Create(property.Name, property.PropertyType));
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
