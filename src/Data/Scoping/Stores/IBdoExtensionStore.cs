using BindOpen.Data;
using System;
using System.Collections.Generic;

namespace BindOpen.Scoping;

/// <summary>
/// Defines a repository for storing and retrieving extension definitions.
/// </summary>
/// <remarks>
/// The extension store acts as the central registry for all blueprints (Tasks, Connectors, Entities, Functions)
/// discovered during the application bootstrap.
/// </remarks>
public interface IBdoExtensionStore : IIdentified
{
    /// <summary>
    /// Adds the specified extension definition to the store.
    /// </summary>
    /// <param name="definition">The extension definition to register.</param>
    /// <returns>Returns this instance for fluent chaining.</returns>
    IBdoExtensionStore Add(IBdoExtensionDefinition definition);

    /// <summary>
    /// Retrieves all extension definitions of a specific category.
    /// </summary>
    /// <param name="kind">The category of extension (Task, Connector, etc.) to filter by.</param>
    /// <returns>Returns an enumeration of matching extension definitions.</returns>
    IEnumerable<IBdoExtensionDefinition> GetDefinitions(BdoExtensionKinds kind);

    /// <summary>
    /// Retrieves a specific extension definition by its category and unique name.
    /// </summary>
    /// <param name="kind">The category of the extension.</param>
    /// <param name="uniqueName">The unique identifier of the definition.</param>
    /// <returns>Returns the matching extension definition, or null if not found.</returns>
    IBdoExtensionDefinition GetDefinition(BdoExtensionKinds kind, string uniqueName);

    /// <summary>
    /// Resolves an extension definition based on a physical C# type.
    /// </summary>
    /// <param name="type">The runtime type to look up.</param>
    /// <returns>Returns the extension definition associated with the specified type.</returns>
    /// <remarks>This is essential for mapping concrete classes back to their logical BindOpen metadata.</remarks>
    IBdoExtensionDefinition GetDefinitionFromType(Type type);

    /// <summary>
    /// Removes all registered definitions and resets the store.
    /// </summary>
    /// <returns>Returns this instance for fluent chaining.</returns>
    IBdoExtensionStore Clear();
}