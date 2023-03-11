using BindOpen.Data;
using BindOpen.Extensions;

namespace BindOpen.Scopes.Stores
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
        ITBdoSet<T> GetDefinitions<T>() where T : IBdoExtensionDefinition;

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
        /// Clears this instance.
        /// </summary>
        IBdoExtensionStore Clear();
    }
}