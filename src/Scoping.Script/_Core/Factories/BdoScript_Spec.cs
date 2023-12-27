using BindOpen.Data.Meta;

namespace BindOpen.Scoping.Script
{
    /// <summary>
    /// This static class provides methods to create script elements.
    /// </summary>
    public static partial class BdoScript
    {
        public static TBdoScriptword<IBdoNodeSpec> _Parent(this TBdoScriptword<IBdoNodeSpec> spec)
            => spec.Var<IBdoNodeSpec>("parent");

        public static TBdoScriptword<string> _Name(this TBdoScriptword<IBdoNodeSpec> spec)
            => spec.Var<string>("name");

        public static TBdoScriptword<bool> Value(this TBdoScriptword<IBdoNodeSpec> spec)
            => spec.Func<bool>("value");

        public static TBdoScriptword<bool> _Has(this TBdoScriptword<IBdoNodeSpec> spec, string name)
            => spec.Func<bool>("has", name);

        public static TBdoScriptword<IBdoNodeSpec> _Descendant(this TBdoScriptword<IBdoNodeSpec> spec, params object[] tokens)
            => spec.Func<IBdoNodeSpec>("descendant", tokens);

        ///// <summary>
        ///// Returns the item TItem of this instance.
        ///// </summary>
        ///// <param key="log">The log to populate.</param>
        ///// <param key="scope">The scope to consider.</param>
        ///// <param key="metaSet">The variable meta set to use.</param>
        ///// <returns>Returns the items of this instance.</returns>
        //[BdoFunction("descendant")]
        //public static IBdoSpec BdoDescendant(
        //    [BdoThis] IBdoSpec spec,
        //    params object[] tokens)
        //{
        //    return spec?.Descendant<IBdoSpec>(tokens);
        //}
    }
}
