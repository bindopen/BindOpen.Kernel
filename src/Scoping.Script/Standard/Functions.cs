using BindOpen.Data.Meta;

namespace BindOpen.Scoping.Script.Standard
{
    /// <summary>
    /// This static class provides methods to create script elements.
    /// </summary>
    public static class Functions
    {
        /// <summary>
        /// Returns the item TItem of this instance.
        /// </summary>
        /// <param key="log">The log to populate.</param>
        /// <param key="scope">The scope to consider.</param>
        /// <param key="metaSet">The variable meta set to use.</param>
        /// <returns>Returns the items of this instance.</returns>
        [BdoFunction("value")]
        public static object BdoValue(
            this IBdoMetaData data,
            IBdoScriptDomain scriptDomain = null)
        {
            return data?.GetData(scriptDomain?.Scope, scriptDomain?.VariableSet, scriptDomain?.Log);
        }

        /// <summary>
        /// Returns the item TItem of this instance.
        /// </summary>
        /// <param key="log">The log to populate.</param>
        /// <param key="scope">The scope to consider.</param>
        /// <param key="metaSet">The variable meta set to use.</param>
        /// <returns>Returns the items of this instance.</returns>
        [BdoFunction("descendant")]
        public static IBdoMetaData BdoDescendant(
            this IBdoMetaSet set,
            params object[] tokens)
        {
            return set?.Descendant<IBdoMetaData>(tokens);
        }

        /// <summary>
        /// Returns the item TItem of this instance.
        /// </summary>
        /// <param key="log">The log to populate.</param>
        /// <param key="scope">The scope to consider.</param>
        /// <param key="metaSet">The variable meta set to use.</param>
        /// <returns>Returns the items of this instance.</returns>
        [BdoFunction("has")]
        public static bool BdoHas(
            this IBdoMetaSet set,
            params object[] tokens)
        {
            return set?.Descendant<IBdoMetaData>(tokens) != null;
        }
    }
}
