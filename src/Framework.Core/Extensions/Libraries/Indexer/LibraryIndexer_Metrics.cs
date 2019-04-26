using System.Reflection;
using BindOpen.Framework.Core.Extensions.Libraries;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Core.System.Diagnostics;

namespace BindOpen.Framework.Core.Extensions.Libraries
{
    /// <summary>
    /// This static class provices methods to index library items.
    /// </summary>
    public static partial class LibraryIndexer
    {
        /// <summary>
        /// References the specified metrics into the specified library.
        /// </summary>
        /// <param name="library">The library to consider.</param>
        /// <param name="assembly">The assembly to consider.</param>
        /// <param name="isIndexLoaded">Indicates whether item indexes must be loaded.</param>
        /// <param name="log">The log to consider.</param>
        /// <returns>Returns the number of metrics indexed.</returns>
        public static int IndexMetrics(
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

            // we feach metrics classes

            int count = 0;

            //var types = assembly.GetTypes().Where(p => typeof(Runtime.Metrics.Metrics).IsAssignableFrom(p));
            //foreach(Type type in types)
            //{
            //    IMetricsDefinition definition = new MetricsDefinition();

            //    if (type.GetCustomAttribute(typeof(MetricsAttribute)) is MetricsAttribute metricsAttribute)
            //    {
            //        definition.Update(metricsAttribute);
            //    }

            //    foreach(PropertyInfo property in type.GetProperties().Where(p => p.GetCustomAttribute(typeof(MetricsInputAttribute)) != null))
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
