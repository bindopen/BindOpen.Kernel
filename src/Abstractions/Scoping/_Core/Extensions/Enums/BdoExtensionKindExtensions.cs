using BindOpen.Kernel.Data;
using BindOpen.Kernel.Scoping.Connectors;
using BindOpen.Kernel.Scoping.Entities;
using BindOpen.Kernel.Scoping.Functions;
using BindOpen.Kernel.Scoping.Tasks;
using System;

namespace BindOpen.Kernel.Scoping
{
    /// <summary>
    /// This class represents an extension of the DataValueType enumeration.
    /// </summary>
    public static class BdoExtensionKindExtensions
    {
        /// <summary>
        /// Gets the extension item kind corresponding to the specified object.
        /// </summary>
        public static BdoExtensionKinds GetExtensionKind(
            this IBdoExtensionDefinition extensionDefinition)
        {
            return (extensionDefinition?.GetType()).GetExtensionKind();
        }

        /// <summary>
        /// Gets the extension item kind corresponding to the specified object.
        /// </summary>
        public static BdoExtensionKinds GetExtensionKind(
            this IBdoExtension extension)
        {
            return (extension?.GetType()).GetExtensionKind();
        }

        /// <summary>
        /// Gets the extension item kind corresponding to the specified type.
        /// </summary>
        /// <param key="type">The type to consider.</param>
        public static BdoExtensionKinds GetExtensionKind(
            this Type type)
        {
            if (typeof(IBdoConnectorDefinition).IsAssignableFrom(type)
                || typeof(IBdoConnector).IsAssignableFrom(type))
            {
                return BdoExtensionKinds.Connector;
            }
            else if (typeof(IBdoEntityDefinition).IsAssignableFrom(type)
                || typeof(IBdoEntity).IsAssignableFrom(type))
            {
                return BdoExtensionKinds.Entity;
            }
            else if (typeof(IBdoFunctionDefinition).IsAssignableFrom(type))
            {
                return BdoExtensionKinds.Function;
            }
            else if (typeof(IBdoTaskDefinition).IsAssignableFrom(type)
                || typeof(IBdoTask).IsAssignableFrom(type))
            {
                return BdoExtensionKinds.Task;
            }

            return BdoExtensionKinds.None;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="valueType"></param>
        /// <returns></returns>
        public static BdoExtensionKinds GetExtensionKind(
            this DataValueTypes valueType)
        {
            return valueType
                switch
            {
                DataValueTypes.Connector => BdoExtensionKinds.Connector,
                DataValueTypes.Entity => BdoExtensionKinds.Entity,
                DataValueTypes.Scriptword => BdoExtensionKinds.Scriptword,
                DataValueTypes.Task => BdoExtensionKinds.Task,
                _ => BdoExtensionKinds.None
            };
        }
    }
}
