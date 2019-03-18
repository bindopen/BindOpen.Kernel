using System;
using System.Xml.Serialization;
using BindOpen.Framework.Core.Extensions.Configuration.Carriers;
using BindOpen.Framework.Core.Extensions.Configuration.Connectors;
using BindOpen.Framework.Core.Extensions.Configuration.Entities;
using BindOpen.Framework.Core.Extensions.Configuration.Formats;
using BindOpen.Framework.Core.Extensions.Configuration.Metrics;
using BindOpen.Framework.Core.Extensions.Configuration.Routines;
using BindOpen.Framework.Core.Extensions.Configuration.Scriptwords;
using BindOpen.Framework.Core.Extensions.Configuration.Tasks;
using BindOpen.Framework.Core.Extensions.Definition;
using BindOpen.Framework.Core.Extensions.Definition.Carriers;
using BindOpen.Framework.Core.Extensions.Definition.Connectors;
using BindOpen.Framework.Core.Extensions.Definition.Entities;
using BindOpen.Framework.Core.Extensions.Definition.Formats;
using BindOpen.Framework.Core.Extensions.Definition.Handlers;
using BindOpen.Framework.Core.Extensions.Definition.Metrics;
using BindOpen.Framework.Core.Extensions.Definition.Routines;
using BindOpen.Framework.Core.Extensions.Definition.Scriptwords;
using BindOpen.Framework.Core.Extensions.Definition.Tasks;

namespace BindOpen.Framework.Core.Extensions.Common
{
    /// <summary>
    /// This enumeration represents the possible kinds of library items.
    /// </summary>
    [Serializable()]
    [XmlType("AppExtensionItemKind", Namespace = "http://meltingsoft.com/bindopen/xsd")]
    public enum AppExtensionItemKind
    {
        /// <summary>
        /// Any.
        /// </summary>
        Any,

        /// <summary>
        /// None.
        /// </summary>
        None,

        /// <summary>
        /// Task.
        /// </summary>
        Task,

        /// <summary>
        /// Carrier.
        /// </summary>
        Carrier,

        /// <summary>
        /// Connector.
        /// </summary>
        Connector,

        /// <summary>
        /// Context extension.
        /// </summary>
        ContextExtension,

        /// <summary>
        /// Entity.
        /// </summary>
        Entity,

        /// <summary>
        /// Format.
        /// </summary>
        Format,

        /// <summary>
        /// Handler.
        /// </summary>
        Handler,

        /// <summary>
        /// Metrics.
        /// </summary>
        Metrics,

        /// <summary>
        /// RoutineConfiguration.
        /// </summary>
        RoutineConfiguration,

        /// <summary>
        /// Script word.
        /// </summary>
        ScriptWord,
    }


    // --------------------------------------------------
    // EXTENSION
    // --------------------------------------------------

    #region Extension

    /// <summary>
    /// This class represents an extension item kind extension.
    /// </summary>
    public static class AppExtensionItemKindExtension
    {

        /// <summary>
        /// Gets the extension item kind corresponding to the specified object.
        /// </summary>
        /// <param name="appExtensionItemDefinition">The application extension item to consider.</param>
        public static AppExtensionItemKind GetExtensionItemKind(this AppExtensionItemDefinition appExtensionItemDefinition)
        {
            AppExtensionItemKind libraryObjectKind = AppExtensionItemKind.None;

            if (appExtensionItemDefinition is CarrierDefinition)
                return AppExtensionItemKind.Carrier;
            else if (appExtensionItemDefinition is ConnectorDefinition)
                return AppExtensionItemKind.Connector;
            else if (appExtensionItemDefinition is EntityDefinition)
                return AppExtensionItemKind.Entity;
            else if (appExtensionItemDefinition is FormatDefinition)
                return AppExtensionItemKind.Format;
            else if (appExtensionItemDefinition is HandlerDefinition)
                return AppExtensionItemKind.Handler;
            else if (appExtensionItemDefinition is MetricsDefinition)
                return AppExtensionItemKind.Metrics;
            else if (appExtensionItemDefinition is RoutineDefinition)
                return AppExtensionItemKind.RoutineConfiguration;
            else if (appExtensionItemDefinition is ScriptWordDefinition)
                return AppExtensionItemKind.ScriptWord;
            else if (appExtensionItemDefinition is TaskDefinition)
                return AppExtensionItemKind.Task;

            return libraryObjectKind;
        }

        /// <summary>
        /// Gets the extension item kind corresponding to the specified type.
        /// </summary>
        /// <param name="type">The type to consider.</param>
        public static AppExtensionItemKind GetExtensionItemKind(this Type type)
        {
            AppExtensionItemKind libraryObjectKind = AppExtensionItemKind.None;

            if ((type == typeof(TaskDefinition)) || type == typeof(TaskConfiguration) || (type.IsSubclassOf(typeof(TaskConfiguration))))
                return AppExtensionItemKind.Task;
            else if ((type == typeof(CarrierDefinition)) || type == typeof(CarrierConfiguration) || (type.IsSubclassOf(typeof(CarrierConfiguration))))
                return AppExtensionItemKind.Carrier;
            else if ((type == typeof(ConnectorDefinition)) || type == typeof(ConnectorConfiguration) || (type.IsSubclassOf(typeof(ConnectorConfiguration))))
                return AppExtensionItemKind.Connector;
            else if ((type == typeof(RoutineDefinition)) || type == typeof(RoutineConfiguration) || (type.IsSubclassOf(typeof(RoutineConfiguration))))
                return AppExtensionItemKind.RoutineConfiguration;
            else if ((type == typeof(EntityDefinition)) || type == typeof(EntityConfiguration) || (type.IsSubclassOf(typeof(EntityConfiguration))))
                return AppExtensionItemKind.Entity;
            else if ((type == typeof(FormatDefinition)) || type == typeof(FormatConfiguration) || (type.IsSubclassOf(typeof(FormatConfiguration))))
                return AppExtensionItemKind.Format;
            else if ((type == typeof(MetricsDefinition)) || type == typeof(MetricsConfiguration) || (type.IsSubclassOf(typeof(MetricsConfiguration))))
                return AppExtensionItemKind.Metrics;
            else if (type == typeof(HandlerDefinition))
                return AppExtensionItemKind.Handler;
            else if ((type == typeof(ScriptWordDefinition)) || type == typeof(ScriptWord) || (type.IsSubclassOf(typeof(ScriptWord))))
                return AppExtensionItemKind.ScriptWord;

            return libraryObjectKind;
        }
    }

    #endregion

}
