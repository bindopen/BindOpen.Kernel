using System.Reflection;
using BindOpen.Framework.Core.Extensions.Items.Libraries;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Core.System.Diagnostics;

namespace BindOpen.Framework.Core.Extensions.Items.Libraries
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
        public static int IndexHandlers(
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

            // we feach handler classes

            int count = 0;

            //var types = assembly.GetTypes().Where(p => typeof(Handler).IsAssignableFrom(p));
            //foreach (Type type in types)
            //{
            //    IHandlerDefinition definition = new HandlerDefinition();

            //    if (type.GetCustomAttribute(typeof(HandlerAttribute)) is HandlerAttribute handlerAttribute)
            //    {
            //        definition.Update(handlerAttribute);
            //    }

            //    foreach(PropertyInfo property in type.GetProperties().Where(p => p.GetCustomAttribute(typeof(HandlerInputAttribute)) != null))
            //    {
            //        definition.InputSpecification.Add(property.CreateSpecification());
            //    }

            //    if (isIndexLoaded)
            //    {
            //        //definition.Update()
            //    }

            //    count++;
            //}

            return count;
        }
    }
}
