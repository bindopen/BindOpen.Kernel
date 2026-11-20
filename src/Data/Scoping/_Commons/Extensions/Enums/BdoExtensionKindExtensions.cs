using BindOpen.Data;
using BindOpen.Scoping.Connectors;
using BindOpen.Scoping.Entities;
using BindOpen.Scoping.Functions;
using BindOpen.Scoping.Tasks;
using System;

namespace BindOpen.Scoping;

/// <summary>
/// This static class provides extension methods to resolve the extension kind of various objects and types.
/// </summary>
public static class BdoExtensionKindExtensions
{
    /// <summary>
    /// Gets the extension kind corresponding to the specified extension definition.
    /// </summary>
    /// <param name="extensionDefinition">The extension definition to consider.</param>
    /// <returns>Returns the corresponding BdoExtensionKinds.</returns>
    public static BdoExtensionKinds GetExtensionKind(
        this IBdoExtensionDefinition extensionDefinition)
    {
        return extensionDefinition?.GetType().GetExtensionKind() ?? BdoExtensionKinds.None;
    }

    /// <summary>
    /// Gets the extension kind corresponding to the specified extension instance.
    /// </summary>
    /// <param name="extension">The extension instance to consider.</param>
    /// <returns>Returns the corresponding BdoExtensionKinds.</returns>
    public static BdoExtensionKinds GetExtensionKind(
        this IBdoExtension extension)
    {
        return extension?.GetType().GetExtensionKind() ?? BdoExtensionKinds.None;
    }

    /// <summary>
    /// Gets the extension kind corresponding to the specified C# type.
    /// </summary>
    /// <param name="type">The type to consider.</param>
    /// <returns>Returns the corresponding BdoExtensionKinds based on interface implementation.</returns>
    public static BdoExtensionKinds GetExtensionKind(
        this Type type)
    {
        if (type == null) return BdoExtensionKinds.None;

        if (typeof(IBdoConnectorDefinition).IsAssignableFrom(type)
            || typeof(IBdoConnector).IsAssignableFrom(type))
        {
            return BdoExtensionKinds.Connector;
        }

        if (typeof(IBdoEntityDefinition).IsAssignableFrom(type)
            || typeof(IBdoEntity).IsAssignableFrom(type))
        {
            return BdoExtensionKinds.Entity;
        }

        if (typeof(IBdoFunctionDefinition).IsAssignableFrom(type))
        {
            return BdoExtensionKinds.Function;
        }

        if (typeof(IBdoTaskDefinition).IsAssignableFrom(type)
            || typeof(IBdoTask).IsAssignableFrom(type))
        {
            return BdoExtensionKinds.Task;
        }

        return BdoExtensionKinds.None;
    }

    /// <summary>
    /// Maps a DataValueTypes enumeration value to its corresponding BdoExtensionKinds.
    /// </summary>
    /// <param name="valueType">The data value type to consider.</param>
    /// <returns>Returns the mapped BdoExtensionKinds.</returns>
    public static BdoExtensionKinds GetExtensionKind(
        this DataValueTypes valueType)
    {
        return valueType switch
        {
            DataValueTypes.Connector => BdoExtensionKinds.Connector,
            DataValueTypes.Entity => BdoExtensionKinds.Entity,
            DataValueTypes.Scriptword => BdoExtensionKinds.Scriptword,
            DataValueTypes.Task => BdoExtensionKinds.Task,
            _ => BdoExtensionKinds.None
        };
    }
}