using System;
using System.Collections.Generic;
using BindOpen.Kernel.Data;

namespace BindOpen.Kernel.Scoping
{
    /// <summary>
    /// This interface defines an extension store.
    /// </summary>
    public interface IBdoExtensionStore : IIdentified
    {
        /// <summary>
        /// Adds the specified extension definition.
        /// </summary>
        /// <param key="definition">The definition to add.</param>
        IBdoExtensionStore Add(IBdoExtensionDefinition definition);

        /// <summary>
        /// Gets the specified extension definitions.
        /// </summary>
        /// <param key="kind">The definition kind to consider.</param>
        /// <returns>Returns the extension definitions of the specified kind.</returns>
        IEnumerable<IBdoExtensionDefinition> GetDefinitions(BdoExtensionKinds kind);

        /// <summary>
        /// Get the specified extension definition.
        /// </summary>
        /// <param key="kind">The definition kind to consider.</param>
        /// <param key="uniqueName">The unique name to consider.</param>
        /// <returns>Returns the extension definitions of the specified kind and the specified unique name.</returns>
        IBdoExtensionDefinition GetDefinition(BdoExtensionKinds kind, string uniqueName);

        /// <summary>
        /// Get the specified extension definition.
        /// </summary>
        /// <param key="type">The type to consider.</param>
        /// <returns>Returns the extension definitions of the specified type.</returns>
        IBdoExtensionDefinition GetDefinitionFromType(Type type);

        /// <summary>
        /// Clears this instance.
        /// </summary>
        IBdoExtensionStore Clear();
    }
}