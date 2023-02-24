using BindOpen.Extensions;
using BindOpen.Extensions.Connecting;
using BindOpen.Extensions.Modeling;
using BindOpen.Extensions.Processing;
using BindOpen.Extensions.Scripting;
using System;

namespace BindOpen.Runtime.Definitions
{
    /// <summary>
    /// This class represents an extension item kind extension.
    /// </summary>
    public static class BdoExtensionKindExtension
    {
        /// <summary>
        /// Gets the extension item kind corresponding to the specified object.
        /// </summary>
        /// <param key="extensionDefinition">The BindOpen extension item to consider.</param>
        public static BdoExtensionKind GetExtensionKind(this IBdoExtensionDefinition extensionDefinition)
        {
            return (extensionDefinition?.GetType()).GetExtensionKind();
        }

        /// <summary>
        /// Gets the extension item kind corresponding to the specified type.
        /// </summary>
        /// <param key="type">The type to consider.</param>
        public static BdoExtensionKind GetExtensionKind(this Type type)
        {
            if (typeof(IBdoTaskDefinition).IsAssignableFrom(type)
                || typeof(IBdoTask).IsAssignableFrom(type))
            {
                return BdoExtensionKind.Task;
            }
            else if (typeof(IBdoEntityDefinition).IsAssignableFrom(type)
                || typeof(IBdoEntity).IsAssignableFrom(type))
            {
                return BdoExtensionKind.Entity;
            }
            else if (typeof(IBdoConnectorDefinition).IsAssignableFrom(type)
                || typeof(IBdoConnector).IsAssignableFrom(type))
            {
                return BdoExtensionKind.Connector;
            }
            else if (typeof(IBdoRoutineDefinition).IsAssignableFrom(type)
                || typeof(IResourceAllocation).IsAssignableFrom(type))
            {
                return BdoExtensionKind.Routine;
            }
            else if (typeof(IBdoEntityDefinition).IsAssignableFrom(type)
                || typeof(IBdoEntity).IsAssignableFrom(type))
            {
                return BdoExtensionKind.Entity;
            }
            else if (typeof(IBdoFormatDefinition).IsAssignableFrom(type)
                || typeof(IBdoFormat).IsAssignableFrom(type))
            {
                return BdoExtensionKind.Format;
            }
            else if (typeof(IBdoScriptwordDefinition).IsAssignableFrom(type)
                || typeof(IBdoScriptword).IsAssignableFrom(type))
            {
                return BdoExtensionKind.Scriptword;
            }

            return BdoExtensionKind.None;
        }
    }
}
