using BindOpen.Data;
using BindOpen.Extensions;
using BindOpen.Extensions.Scripting;
using BindOpen.Runtime.Definition;
using System.Collections.Generic;

namespace BindOpen.Runtime.Stores
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoExtensionStore :
        IIdentified
    {
        /// <summary>
        /// Adds the specified definition.
        /// </summary>
        /// <typeparam name="T">The BindOpen extension item definition class to consider.</typeparam>
        /// <param name="definition">The definition to add.</param>
        IBdoExtensionStore Add<T>(T definition) where T : IBdoExtensionItemDefinition;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Dictionary<string, T> GetItemDefinitions<T>() where T : IBdoExtensionItemDefinition;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IEnumerable<T> GetItemDefinitionEnumerables<T>() where T : IBdoExtensionItemDefinition;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="uniqueName"></param>
        /// <returns></returns>
        T GetItemDefinitionWithUniqueName<T>(string uniqueName) where T : IBdoExtensionItemDefinition;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="uniqueName"></param>
        /// <returns></returns>
        IBdoExtensionItemDefinition GetItemDefinitionWithUniqueName(
            BdoExtensionItemKind kind,
            string uniqueName);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="uniqueName"></param>
        /// <param name="parentDefinition"></param>
        /// <returns></returns>
        IBdoScriptwordDefinition GetScriptwordDefinitionWithUniqueName(string uniqueName, IBdoScriptwordDefinition parentDefinition = null);

        /// <summary>
        /// Clears this instance.
        /// </summary>
        IBdoExtensionStore Clear();
    }
}