using System;
using System.Xml.Serialization;
using BindOpen.Framework.Core.Extensions.Items;
using BindOpen.Framework.Core.Extensions.Items.Carriers;
using BindOpen.Framework.Core.Extensions.Items.Carriers.Definition;
using BindOpen.Framework.Core.Extensions.Items.Carriers.Definition.Dto;
using BindOpen.Framework.Core.Extensions.Items.Connectors;
using BindOpen.Framework.Core.Extensions.Items.Connectors.Definition;
using BindOpen.Framework.Core.Extensions.Items.Connectors.Definition.Dto;
using BindOpen.Framework.Core.Extensions.Items.Entities;
using BindOpen.Framework.Core.Extensions.Items.Entities.Definition;
using BindOpen.Framework.Core.Extensions.Items.Entities.Definition.Dto;
using BindOpen.Framework.Core.Extensions.Items.Formats;
using BindOpen.Framework.Core.Extensions.Items.Formats.Definition;
using BindOpen.Framework.Core.Extensions.Items.Formats.Definition.Dto;
using BindOpen.Framework.Core.Extensions.Items.Handlers.Definition;
using BindOpen.Framework.Core.Extensions.Items.Handlers.Definition.Dto;
using BindOpen.Framework.Core.Extensions.Items.Routines;
using BindOpen.Framework.Core.Extensions.Items.Routines.Definition;
using BindOpen.Framework.Core.Extensions.Items.Routines.Definition.Dto;
using BindOpen.Framework.Core.Extensions.Items.Scriptwords;
using BindOpen.Framework.Core.Extensions.Items.Scriptwords.Definition;
using BindOpen.Framework.Core.Extensions.Items.Scriptwords.Definition.Dto;
using BindOpen.Framework.Core.Extensions.Items.Tasks;
using BindOpen.Framework.Core.Extensions.Items.Tasks.Definition;
using BindOpen.Framework.Core.Extensions.Items.Tasks.Definition.Dto;

namespace BindOpen.Framework.Core.Extensions.Common
{
    /// <summary>
    /// This enumeration represents the possible kinds of library items.
    /// </summary>
    [Serializable()]
    [XmlType("AppExtensionItemKind", Namespace = "https://bindopen.org/xsd")]
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
        public static AppExtensionItemKind GetExtensionItemKind(this IAppExtensionItemDefinitionDto appExtensionItemDefinition)
        {
            return (appExtensionItemDefinition?.GetType()).GetExtensionItemKind();
        }

        /// <summary>
        /// Gets the extension item kind corresponding to the specified type.
        /// </summary>
        /// <param name="type">The type to consider.</param>
        public static AppExtensionItemKind GetExtensionItemKind(this Type type)
        {
            if ((typeof(ITaskDefinition).IsAssignableFrom(type))
                || (typeof(ITaskDefinitionDto).IsAssignableFrom(type))
                || (typeof(ITaskConfiguration).IsAssignableFrom(type))
                || (typeof(ITask).IsAssignableFrom(type)))
            {
                return AppExtensionItemKind.Task;
            }
            else if ((typeof(ICarrierDefinition).IsAssignableFrom(type))
                || (typeof(ICarrierDefinitionDto).IsAssignableFrom(type))
                || (typeof(ICarrierConfiguration).IsAssignableFrom(type))
                || (typeof(ICarrier).IsAssignableFrom(type)))
            {
                return AppExtensionItemKind.Carrier;
            }
            else if ((typeof(IConnectorDefinition).IsAssignableFrom(type))
                || (typeof(IConnectorDefinitionDto).IsAssignableFrom(type))
                || (typeof(IConnectorConfiguration).IsAssignableFrom(type))
                || (typeof(IConnector).IsAssignableFrom(type)))
            {
                return AppExtensionItemKind.Connector;
            }
            else if ((typeof(IRoutineDefinition).IsAssignableFrom(type))
                || (typeof(IRoutineDefinitionDto).IsAssignableFrom(type))
                || (typeof(IRoutineConfiguration).IsAssignableFrom(type))
                || (typeof(IRoutine).IsAssignableFrom(type)))
            {
                return AppExtensionItemKind.Routine;
            }
            else if ((typeof(IEntityDefinition).IsAssignableFrom(type))
                || (typeof(IEntityDefinitionDto).IsAssignableFrom(type))
                || (typeof(IEntityConfiguration).IsAssignableFrom(type))
                || (typeof(IEntity).IsAssignableFrom(type)))
            {
                return AppExtensionItemKind.Entity;
            }
            else if ((typeof(IFormatDefinition).IsAssignableFrom(type))
                || (typeof(IFormatDefinitionDto).IsAssignableFrom(type))
                || (typeof(IFormatConfiguration).IsAssignableFrom(type))
                || (typeof(IFormat).IsAssignableFrom(type)))
            {
                return AppExtensionItemKind.Format;
            }
            //else if ((type == typeof(MetricsDefinitionDto)) || type == typeof(MetricsConfiguration) || (type.IsSubclassOf(typeof(MetricsConfiguration))))
            //    return AppExtensionItemKind.Metrics;
            else if ((typeof(IHandlerDefinition).IsAssignableFrom(type))
                || (typeof(IHandlerDefinitionDto).IsAssignableFrom(type)))
            {
                return AppExtensionItemKind.Handler;
            }
            else if ((typeof(IScriptwordDefinition).IsAssignableFrom(type))
                || (typeof(IScriptwordDefinitionDto).IsAssignableFrom(type))
                || (typeof(IScriptword).IsAssignableFrom(type)))
            {
                return AppExtensionItemKind.Scriptword;
            }

            return AppExtensionItemKind.None;
        }
    }

    #endregion

}
