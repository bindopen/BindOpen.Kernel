using System;
using System.Xml.Serialization;
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
using BindOpen.Framework.Core.Extensions.Items.Carriers;
using BindOpen.Framework.Core.Extensions.Items.Connectors;
using BindOpen.Framework.Core.Extensions.Items.Entities;
using BindOpen.Framework.Core.Extensions.Items.Formats;
using BindOpen.Framework.Core.Extensions.Items.Scriptwords;
using BindOpen.Framework.Core.Extensions.Items.Tasks;

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
        /// RoutineDto.
        /// </summary>
        Routine,

        /// <summary>
        /// Script word.
        /// </summary>
        Scriptword,
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
        public static AppExtensionItemKind GetExtensionItemKind(this AppExtensionItemDefinitionDto appExtensionItemDefinition)
        {
            AppExtensionItemKind libraryObjectKind = AppExtensionItemKind.None;

            if (appExtensionItemDefinition is CarrierDefinitionDto)
                return AppExtensionItemKind.Carrier;
            else if (appExtensionItemDefinition is ConnectorDefinitionDto)
                return AppExtensionItemKind.Connector;
            else if (appExtensionItemDefinition is EntityDefinitionDto)
                return AppExtensionItemKind.Entity;
            else if (appExtensionItemDefinition is FormatDefinitionDto)
                return AppExtensionItemKind.Format;
            else if (appExtensionItemDefinition is HandlerDefinitionDto)
                return AppExtensionItemKind.Handler;
            else if (appExtensionItemDefinition is MetricsDefinitionDto)
                return AppExtensionItemKind.Metrics;
            else if (appExtensionItemDefinition is RoutineDefinitionDto)
                return AppExtensionItemKind.Routine;
            else if (appExtensionItemDefinition is ScriptwordDefinitionDto)
                return AppExtensionItemKind.Scriptword;
            else if (appExtensionItemDefinition is TaskDefinitionDto)
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

            if ((type == typeof(TaskDefinitionDto)) || type == typeof(TaskDto) || (type.IsSubclassOf(typeof(TaskDto))))
                return AppExtensionItemKind.Task;
            else if ((type == typeof(CarrierDefinitionDto)) || type == typeof(CarrierDto) || (type.IsSubclassOf(typeof(CarrierDto))))
                return AppExtensionItemKind.Carrier;
            else if ((type == typeof(ConnectorDefinitionDto)) || type == typeof(ConnectorDto) || (type.IsSubclassOf(typeof(ConnectorDto))))
                return AppExtensionItemKind.Connector;
            //else if ((type == typeof(RoutineDefinitionDto)) || type == typeof(RoutineDto) || (type.IsSubclassOf(typeof(RoutineDto))))
            //    return AppExtensionItemKind.Routine;
            else if ((type == typeof(EntityDefinitionDto)) || type == typeof(EntityDto) || (type.IsSubclassOf(typeof(EntityDto))))
                return AppExtensionItemKind.Entity;
            else if ((type == typeof(FormatDefinitionDto)) || type == typeof(FormatDto) || (type.IsSubclassOf(typeof(FormatDto))))
                return AppExtensionItemKind.Format;
            //else if ((type == typeof(MetricsDefinitionDto)) || type == typeof(MetricsDto) || (type.IsSubclassOf(typeof(MetricsDto))))
            //    return AppExtensionItemKind.Metrics;
            else if (type == typeof(HandlerDefinitionDto))
                return AppExtensionItemKind.Handler;
            else if ((type == typeof(ScriptwordDefinitionDto)) || type == typeof(Scriptword) || (type.IsSubclassOf(typeof(Scriptword))))
                return AppExtensionItemKind.Scriptword;

            return libraryObjectKind;
        }
    }

    #endregion

}
