using System.Reflection;
using BindOpen.Framework.Core.Extensions.Common;
using BindOpen.Framework.Core.Extensions.Items.Libraries;
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
        /// <param name="kind">The kind of item to consider.</param>
        /// <param name="isIndexLoaded">Indicates whether item indexes must be loaded.</param>
        /// <param name="log">The log to consider.</param>
        /// <returns></returns>
        public static int IndexItems(
            this ILibrary library,
            Assembly assembly,
            AppExtensionItemKind kind,
            bool isIndexLoaded = false,
            ILog log = null)
        {
            if ((library==null)||(assembly==null))
            {
                return -1;
            }

            log = log ?? new Log();
            switch(kind)
            {
                case AppExtensionItemKind.Carrier:
                    return library.IndexCarriers(assembly, isIndexLoaded, log);
                case AppExtensionItemKind.Connector:
                    return library.IndexConnectors(assembly, isIndexLoaded, log);
                case AppExtensionItemKind.Entity:
                    return library.IndexEntities(assembly, isIndexLoaded, log);
                case AppExtensionItemKind.Format:
                    return library.IndexFormats(assembly, isIndexLoaded, log);
                case AppExtensionItemKind.Handler:
                    return library.IndexHandlers(assembly, isIndexLoaded, log);
                case AppExtensionItemKind.Metrics:
                    return library.IndexMetrics(assembly, isIndexLoaded, log);
                case AppExtensionItemKind.Routine:
                    return library.IndexRoutines(assembly, isIndexLoaded, log);
                case AppExtensionItemKind.Scriptword:
                    return library.IndexScriptwords(assembly, isIndexLoaded, log);
                case AppExtensionItemKind.Task:
                    return library.IndexTasks(assembly, isIndexLoaded, log);
            }
            return -1;
        }
    }
}
