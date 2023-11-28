using BindOpen.Kernel.Data.Meta;

namespace BindOpen.Kernel.Scoping.Script
{
    /// <summary>
    /// This static class provides methods to create script elements.
    /// </summary>
    public static partial class BdoScript
    {
        public static TBdoScriptword<IBdoMetaData> _Parent(this TBdoScriptword<IBdoMetaData> meta)
            => meta.Var<IBdoMetaData>("parent");

        public static TBdoScriptword<string> _Name(this TBdoScriptword<IBdoMetaData> meta)
            => meta.Var<string>("name");

        public static TBdoScriptword<bool> Value(this TBdoScriptword<IBdoMetaData> meta)
            => meta.Func<bool>("value");

        public static TBdoScriptword<bool> _Has(this TBdoScriptword<IBdoMetaData> meta, string name)
            => meta.Func<bool>("has", name);

        public static TBdoScriptword<IBdoMetaData> _Descendant(this TBdoScriptword<IBdoMetaData> spec, params object[] tokens)
            => spec.Func<IBdoMetaData>("descendant", tokens);

        /// <summary>
        /// Returns the item TItem of this instance.
        /// </summary>
        /// <param key="log">The log to populate.</param>
        /// <param key="scope">The scope to consider.</param>
        /// <param key="metaSet">The variable meta set to use.</param>
        /// <returns>Returns the items of this instance.</returns>
        [BdoFunction("value")]
        public static object BdoValue(
            [BdoThis] IBdoMetaData data,
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
            [BdoThis] IBdoMetaSet set,
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
            [BdoThis] IBdoMetaSet set,
            params object[] tokens)
        {
            return set?.Descendant<IBdoMetaData>(tokens) != null;
        }
    }
}
