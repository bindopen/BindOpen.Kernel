using System.Reflection;
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

            // we feach metrics classes

            int count = 0;

            return count;
        }
    }
}
