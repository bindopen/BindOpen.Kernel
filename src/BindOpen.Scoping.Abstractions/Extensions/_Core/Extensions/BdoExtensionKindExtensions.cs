﻿using BindOpen.Scoping.Extensions;
using BindOpen.Scoping.Extensions.Connectors;
using BindOpen.Scoping.Extensions.Entities;
using BindOpen.Scoping.Extensions.Functions;
using BindOpen.Scoping.Extensions.Tasks;
using System;

namespace BindOpen.Scoping.Data
{
    /// <summary>
    /// This class represents an extension of the DataValueType enumeration.
    /// </summary>
    public static class BdoExtensionKindExtensions
    {
        /// <summary>
        /// Gets the extension item kind corresponding to the specified object.
        /// </summary>
        public static BdoExtensionKind GetExtensionKind(
            this IBdoExtensionDefinition extensionDefinition)
        {
            return (extensionDefinition?.GetType()).GetExtensionKind();
        }

        /// <summary>
        /// Gets the extension item kind corresponding to the specified object.
        /// </summary>
        public static BdoExtensionKind GetExtensionKind(
            this IBdoExtension extension)
        {
            return (extension?.GetType()).GetExtensionKind();
        }

        /// <summary>
        /// Gets the extension item kind corresponding to the specified type.
        /// </summary>
        /// <param key="type">The type to consider.</param>
        public static BdoExtensionKind GetExtensionKind(
            this Type type)
        {
            if (typeof(IBdoConnectorDefinition).IsAssignableFrom(type)
                || typeof(IBdoConnector).IsAssignableFrom(type))
            {
                return BdoExtensionKind.Connector;
            }
            else if (typeof(IBdoEntityDefinition).IsAssignableFrom(type)
                || typeof(IBdoEntity).IsAssignableFrom(type))
            {
                return BdoExtensionKind.Entity;
            }
            else if (typeof(IBdoFunctionDefinition).IsAssignableFrom(type))
            {
                return BdoExtensionKind.Function;
            }
            else if (typeof(IBdoTaskDefinition).IsAssignableFrom(type)
                || typeof(IBdoTask).IsAssignableFrom(type))
            {
                return BdoExtensionKind.Task;
            }

            return BdoExtensionKind.None;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="valueType"></param>
        /// <returns></returns>
        public static BdoExtensionKind GetExtensionKind(
            this DataValueTypes valueType)
        {
            return valueType
                switch
            {
                DataValueTypes.Connector => BdoExtensionKind.Connector,
                DataValueTypes.Entity => BdoExtensionKind.Entity,
                DataValueTypes.Task => BdoExtensionKind.Task,
                _ => BdoExtensionKind.None
            };
        }
    }
}