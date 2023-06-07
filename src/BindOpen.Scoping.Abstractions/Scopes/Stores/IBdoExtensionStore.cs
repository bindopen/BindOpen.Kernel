using BindOpen.Scoping.Data;
using BindOpen.Scoping.Data.Assemblies;
using BindOpen.Scoping.Extensions;
using System.Collections.Generic;

namespace BindOpen.Scoping.Scopes.Stores
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoExtensionStore : IIdentified
    {
        /// <summary>
        /// Adds the specified definition.
        /// </summary>
        /// <typeparam name="T">The BindOpen extension item definition class to consider.</typeparam>
        /// <param key="definition">The definition to add.</param>
        IBdoExtensionStore Add(IBdoExtensionDefinition definition);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IEnumerable<T> GetDefinitions<T>() where T : IBdoExtensionDefinition;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param key="uniqueName"></param>
        /// <returns></returns>
        T GetDefinition<T>(string uniqueName) where T : IBdoExtensionDefinition;

        /// <summary>
        /// 
        /// </summary>
        /// <param key="uniqueName"></param>
        /// <returns></returns>
        IBdoExtensionDefinition GetDefinition(
            BdoExtensionKind kind,
            string uniqueName);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param key="uniqueName"></param>
        /// <returns></returns>
        T GetDefinitionFromType<T>(IBdoClassReference reference) where T : IBdoExtensionDefinition;

        /// <summary>
        /// 
        /// </summary>
        /// <param key="uniqueName"></param>
        /// <returns></returns>
        IBdoExtensionDefinition GetDefinitionFromType(
            BdoExtensionKind kind,
            IBdoClassReference reference);

        /// <summary>
        /// Clears this instance.
        /// </summary>
        IBdoExtensionStore Clear();
    }
}