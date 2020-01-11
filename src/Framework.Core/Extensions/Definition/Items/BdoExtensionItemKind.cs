using BindOpen.Framework.Extensions.Runtime;
using System;
using System.Xml.Serialization;

namespace BindOpen.Framework.Extensions.Definition
{
    /// <summary>
    /// This enumeration represents the possible kinds of library items.
    /// </summary>
    [Serializable()]
    [XmlType("BdoExtensionItemKind", Namespace = "https://bindopen.org/xsd")]
    public enum BdoExtensionItemKind
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
    public static class BdoExtensionItemKindExtension
    {
        /// <summary>
        /// Gets the extension item kind corresponding to the specified object.
        /// </summary>
        /// <param name="appExtensionItemDefinition">The BindOpen extension item to consider.</param>
        public static BdoExtensionItemKind GetExtensionItemKind(this IBdoExtensionItemDefinitionDto appExtensionItemDefinition)
        {
            return (appExtensionItemDefinition?.GetType()).GetExtensionItemKind();
        }

        /// <summary>
        /// Gets the extension item kind corresponding to the specified type.
        /// </summary>
        /// <param name="type">The type to consider.</param>
        public static BdoExtensionItemKind GetExtensionItemKind(this Type type)
        {
            if ((typeof(IBdoTaskDefinition).IsAssignableFrom(type))
                || (typeof(IBdoTaskDefinitionDto).IsAssignableFrom(type))
                || (typeof(IBdoTaskConfiguration).IsAssignableFrom(type))
                || (typeof(IBdoTask).IsAssignableFrom(type)))
            {
                return BdoExtensionItemKind.Task;
            }
            else if ((typeof(IBdoCarrierDefinition).IsAssignableFrom(type))
                || (typeof(IBdoCarrierDefinitionDto).IsAssignableFrom(type))
                || (typeof(IBdoCarrierConfiguration).IsAssignableFrom(type))
                || (typeof(IBdoCarrier).IsAssignableFrom(type)))
            {
                return BdoExtensionItemKind.Carrier;
            }
            else if ((typeof(IBdoConnectorDefinition).IsAssignableFrom(type))
                || (typeof(IBdoConnectorDefinitionDto).IsAssignableFrom(type))
                || (typeof(IBdoConnectorConfiguration).IsAssignableFrom(type))
                || (typeof(IBdoConnector).IsAssignableFrom(type)))
            {
                return BdoExtensionItemKind.Connector;
            }
            else if ((typeof(IBdoRoutineDefinition).IsAssignableFrom(type))
                || (typeof(IBdoRoutineDefinitionDto).IsAssignableFrom(type))
                || (typeof(IBdoRoutineConfiguration).IsAssignableFrom(type))
                || (typeof(IBdoRoutine).IsAssignableFrom(type)))
            {
                return BdoExtensionItemKind.Routine;
            }
            else if ((typeof(IBdoEntityDefinition).IsAssignableFrom(type))
                || (typeof(IBdoEntityDefinitionDto).IsAssignableFrom(type))
                || (typeof(IBdoEntityConfiguration).IsAssignableFrom(type))
                || (typeof(IBdoEntity).IsAssignableFrom(type)))
            {
                return BdoExtensionItemKind.Entity;
            }
            else if ((typeof(IBdoFormatDefinition).IsAssignableFrom(type))
                || (typeof(IBdoFormatDefinitionDto).IsAssignableFrom(type))
                || (typeof(IBdoFormatConfiguration).IsAssignableFrom(type))
                || (typeof(IBdoFormat).IsAssignableFrom(type)))
            {
                return BdoExtensionItemKind.Format;
            }
            //else if ((type == typeof(BdoMetricsDefinitionDto)) || type == typeof(MetricsConfiguration) || (type.IsSubclassOf(typeof(MetricsConfiguration))))
            //    return BdoExtensionItemKind.Metrics;
            else if ((typeof(IBdoHandlerDefinition).IsAssignableFrom(type))
                || (typeof(IBdoHandlerDefinitionDto).IsAssignableFrom(type)))
            {
                return BdoExtensionItemKind.Handler;
            }
            else if ((typeof(IBdoScriptwordDefinition).IsAssignableFrom(type))
                || (typeof(IBdoScriptwordDefinitionDto).IsAssignableFrom(type))
                || (typeof(IBdoScriptword).IsAssignableFrom(type)))
            {
                return BdoExtensionItemKind.Scriptword;
            }

            return BdoExtensionItemKind.None;
        }
    }

    #endregion

}
