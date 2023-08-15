using BindOpen.System.Data;
using System;
using System.Collections.Generic;

namespace BindOpen.System.Scoping.Stores
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoExtensionStore : IIdentified
    {
        /// <summary>
        /// Adds the specified definition.
        /// </summary>
        /// <param key="definition">The definition to add.</param>
        IBdoExtensionStore Add(IBdoExtensionDefinition definition);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IEnumerable<IBdoExtensionDefinition> GetDefinitions(BdoExtensionKinds kind);

        /// <summary>
        /// 
        /// </summary>
        /// <param key="uniqueName"></param>
        /// <returns></returns>
        IBdoExtensionDefinition GetDefinition(BdoExtensionKinds kind, string uniqueName);

        /// <summary>
        /// 
        /// </summary>
        /// <param key="uniqueName"></param>
        /// <returns></returns>
        IBdoExtensionDefinition GetDefinitionFromType(Type type);

        /// <summary>
        /// Clears this instance.
        /// </summary>
        IBdoExtensionStore Clear();
    }
}