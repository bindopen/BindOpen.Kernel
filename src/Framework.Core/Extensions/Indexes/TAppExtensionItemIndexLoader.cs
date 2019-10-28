using System;
using System.IO;
using System.Reflection;
using System.Xml.Serialization;
using BindOpen.Framework.Core.Extensions.Common;
using BindOpen.Framework.Core.Extensions.Items;
using BindOpen.Framework.Core.Extensions.Indexes.Carriers;
using BindOpen.Framework.Core.Extensions.Indexes.Connectors;
using BindOpen.Framework.Core.Extensions.Indexes.Entities;
using BindOpen.Framework.Core.Extensions.Indexes.Handlers;
using BindOpen.Framework.Core.Extensions.Indexes.Metrics;
using BindOpen.Framework.Core.Extensions.Indexes.Routines;
using BindOpen.Framework.Core.Extensions.Indexes.Scriptwords;
using BindOpen.Framework.Core.Extensions.Indexes.Tasks;
using BindOpen.Framework.Core.System.Diagnostics;

namespace BindOpen.Framework.Core.Extensions.Indexes
{
    /// <summary>
    /// This static class provides methods to load library application extension item definitions.
    /// </summary>
    public static class TAppExtensionItemIndexLoader
    {
        /// <summary>
        /// Loads the specified application extension item index.
        /// </summary>
        /// <param name="assembly">The assembly to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <returns>The created library.</returns>
        public static TAppExtensionItemIndexDto<T> LoadFrom<T>(
            Assembly assembly, ILog log = null) where T : AppExtensionItemDefinitionDto
        {
            TAppExtensionItemIndexDto<T> index = default;

            if (assembly != null)
            {
                string resourceFileName = GetItemIndexFileName<T>();

                string resourceFullName = Array.Find(
                    assembly.GetManifestResourceNames(), p => p.EndsWith(resourceFileName, StringComparison.OrdinalIgnoreCase));

                Stream stream = null;
                if (resourceFullName == null)
                {
                    log?.AddWarning("Could not find any item definition in assembly (named '" + resourceFileName.ToLower() + "')");
                }
                else
                {
                    try
                    {
                        stream = assembly.GetManifestResourceStream(resourceFullName);
                        if (stream == null)
                        {
                            log?.AddError("Could not find the item definition named '" + resourceFullName + "' in assembly");
                        }
                        else
                        {
                            Type type = GetItemIndexType<T>();
                            XmlSerializer serializer = new XmlSerializer(type);
                            index = (TAppExtensionItemIndexDto<T>)serializer.Deserialize(stream);
                        }
                    }
                    catch (Exception ex)
                    {
                        log?.AddException(ex);
                    }
                    finally
                    {
                        stream?.Close();
                    }
                }
            }

            return index;
        }

        /// <summary>
        /// Gets the item definition file name of the TO specified extension item definition class.
        /// </summary>
        /// <returns>Returns the class of the specified dictionary.</returns>
        private static string GetItemIndexFileName<T>() where T : IAppExtensionItemDefinitionDto
        {
            AppExtensionItemKind itemKind = typeof(T).GetExtensionItemKind();

            switch (itemKind)
            {
                case AppExtensionItemKind.Carrier:
                    return "Carriers.index";
                case AppExtensionItemKind.Connector:
                    return "Connectors.index";
                case AppExtensionItemKind.ContextExtension:
                    return "Context.index";
                case AppExtensionItemKind.Entity:
                    return "Entities.index";
                case AppExtensionItemKind.Format:
                    return "Formats.index";
                case AppExtensionItemKind.Handler:
                    return "Handlers.index";
                case AppExtensionItemKind.Metrics:
                    return "Metrics.index";
                case AppExtensionItemKind.Routine:
                    return "Routines.index";
                case AppExtensionItemKind.Scriptword:
                    return "Scriptwords.index";
                case AppExtensionItemKind.Task:
                    return "Tasks.index";
            }

            return null;
        }

        /// <summary>
        /// Gets the item definition file name of the TO specified extension item definition class.
        /// </summary>
        /// <returns>Returns the class of the specified dictionary.</returns>
        private static Type GetItemIndexType<T>() where T : IAppExtensionItemDefinitionDto
        {
            AppExtensionItemKind itemKind = typeof(T).GetExtensionItemKind();

            switch (itemKind)
            {
                case AppExtensionItemKind.Carrier:
                    return typeof(CarrierIndexDto);
                case AppExtensionItemKind.Connector:
                    return typeof(ConnectorIndexDto);
                case AppExtensionItemKind.Entity:
                    return typeof(EntityIndexDto);
                case AppExtensionItemKind.Handler:
                    return typeof(HandlerIndexDto);
                case AppExtensionItemKind.Metrics:
                    return typeof(MetricsIndexDto);
                case AppExtensionItemKind.Routine:
                    return typeof(RoutineIndexDto);
                case AppExtensionItemKind.Scriptword:
                    return typeof(ScriptwordIndexDto);
                case AppExtensionItemKind.Task:
                    return typeof(TaskIndexDto);
            }

            return null;
        }
    }
}