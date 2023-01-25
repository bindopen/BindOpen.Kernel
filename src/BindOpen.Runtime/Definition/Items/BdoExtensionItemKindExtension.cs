using BindOpen.Extensions.Connecting;
using BindOpen.Extensions.Modeling;
using BindOpen.Extensions.Processing;
using BindOpen.Extensions.Scripting;
using System;

namespace BindOpen.Runtime.Definition
{
    /// <summary>
    /// This class represents an extension item kind extension.
    /// </summary>
    public static class BdoExtensionItemKindExtension
    {
        /// <summary>
        /// Gets the extension item kind corresponding to the specified object.
        /// </summary>
        /// <param name="extensionItemDefinition">The BindOpen extension item to consider.</param>
        public static BdoExtensionItemKind GetExtensionItemKind(this IBdoExtensionItemDefinition extensionItemDefinition)
        {
            return (extensionItemDefinition?.GetType()).GetExtensionItemKind();
        }

        /// <summary>
        /// Gets the extension item kind corresponding to the specified type.
        /// </summary>
        /// <param name="type">The type to consider.</param>
        public static BdoExtensionItemKind GetExtensionItemKind(this Type type)
        {
            if ((typeof(IBdoTaskDefinition).IsAssignableFrom(type))
                || (typeof(IBdoTaskConfiguration).IsAssignableFrom(type))
                || (typeof(IBdoTask).IsAssignableFrom(type)))
            {
                return BdoExtensionItemKind.Task;
            }
            else if ((typeof(IBdoEntityDefinition).IsAssignableFrom(type))
                || (typeof(IBdoEntityConfiguration).IsAssignableFrom(type))
                || (typeof(IBdoEntity).IsAssignableFrom(type)))
            {
                return BdoExtensionItemKind.Entity;
            }
            else if ((typeof(IBdoConnectorDefinition).IsAssignableFrom(type))
                || (typeof(IBdoConnectorConfiguration).IsAssignableFrom(type))
                || (typeof(IBdoConnector).IsAssignableFrom(type)))
            {
                return BdoExtensionItemKind.Connector;
            }
            else if ((typeof(IBdoRoutineDefinition).IsAssignableFrom(type))
                || (typeof(IBdoRoutineConfiguration).IsAssignableFrom(type))
                || (typeof(IResourceAllocation).IsAssignableFrom(type)))
            {
                return BdoExtensionItemKind.Routine;
            }
            else if ((typeof(IBdoEntityDefinition).IsAssignableFrom(type))
                || (typeof(IBdoEntityConfiguration).IsAssignableFrom(type))
                || (typeof(IBdoEntity).IsAssignableFrom(type)))
            {
                return BdoExtensionItemKind.Entity;
            }
            else if ((typeof(IBdoFormatDefinition).IsAssignableFrom(type))
                || (typeof(IBdoFormatConfiguration).IsAssignableFrom(type))
                || (typeof(IBdoFormat).IsAssignableFrom(type)))
            {
                return BdoExtensionItemKind.Format;
            }
            //else if ((type == typeof(BdoMetricsDefinition)) || type == typeof(MetricsConfiguration) || (type.IsSubclassOf(typeof(MetricsConfiguration))))
            //    return BdoExtensionItemKind.Metrics;
            else if (typeof(IBdoHandlerDefinition).IsAssignableFrom(type))
            {
                return BdoExtensionItemKind.Handler;
            }
            else if ((typeof(IBdoScriptwordDefinition).IsAssignableFrom(type))
                || (typeof(IBdoScriptword).IsAssignableFrom(type)))
            {
                return BdoExtensionItemKind.Scriptword;
            }

            return BdoExtensionItemKind.None;
        }
    }
}
