using System.Reflection;
using BindOpen.Framework.Core.Data.Items.Dictionary;
using BindOpen.Framework.Core.Extensions.Attributes;
using BindOpen.Framework.Core.Extensions.Common;
using BindOpen.Framework.Core.Extensions.Definitions;
using BindOpen.Framework.Core.Extensions.Definitions.Carriers;
using BindOpen.Framework.Core.Extensions.Definitions.Connectors;
using BindOpen.Framework.Core.Extensions.Definitions.Scriptwords;
using BindOpen.Framework.Core.Extensions.Indexes;
using BindOpen.Framework.Core.Extensions.Indexes.Carriers;
using BindOpen.Framework.Core.Extensions.Indexes.Connectors;
using BindOpen.Framework.Core.Extensions.Indexes.Scriptwords;
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
                    CarrierIndexDto carrierIndex = null;
                    if (isIndexLoaded)
                        carrierIndex = (CarrierIndexDto)TAppExtensionItemIndexLoader.LoadFrom<CarrierDefinitionDto>(assembly, log);
                    return library.IndexCarriers(assembly, carrierIndex, log);
                case AppExtensionItemKind.Connector:
                    ConnectorIndexDto connectorIndex = null;
                    if (isIndexLoaded)
                        connectorIndex = (ConnectorIndexDto)TAppExtensionItemIndexLoader.LoadFrom<ConnectorDefinitionDto>(assembly, log);
                    return library.IndexConnectors(assembly, connectorIndex, log);
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
                    ScriptwordIndexDto scriptwordIndex = (ScriptwordIndexDto)TAppExtensionItemIndexLoader.LoadFrom<ScriptwordDefinitionDto>(assembly, log);

                    return library.IndexScriptwords(assembly, scriptwordIndex, log);
                case AppExtensionItemKind.Task:
                    return library.IndexTasks(assembly, isIndexLoaded, log);
            }
            return -1;
        }

        /// <summary>
        /// Updates this instance with the specified attribute information.
        /// </summary>
        /// <param name="definition">The definition to consider.</param>
        /// <param name="attribute">The attribute to consider.</param>
        public static void Update(
            this IAppExtensionItemDefinitionDto definition,
            AppExtensionItemAttribute attribute)
        {
            definition.Name = attribute.Name?.IndexOf("$") > 0 ?
                attribute.Name.Substring(attribute.Name.IndexOf("$")+1) : attribute.Name;

            definition.Title = new DictionaryDataItem(attribute.Title);
            definition.Description = new DictionaryDataItem(attribute.Description);
            definition.CreationDate = attribute.CreationDate;
            definition.LastModificationDate = attribute.LastModificationDate;
            definition.Index = attribute.Index;
        }
    }
}
