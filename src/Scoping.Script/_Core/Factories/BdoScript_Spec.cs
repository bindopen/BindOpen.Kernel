using BindOpen.Data.Meta;

namespace BindOpen.Scoping.Script
{
    /// <summary>
    /// This static class provides methods to create script elements.
    /// </summary>
    public static partial class BdoScript
    {
        public static ITBdoScriptword<IBdoSpec> _Parent(this ITBdoScriptword<IBdoSpec> spec)
            => spec.Var<IBdoSpec>("parent");

        public static ITBdoScriptword<string> _Name(this ITBdoScriptword<IBdoSpec> spec)
            => spec.Var<string>("name");

        public static ITBdoScriptword<bool> Value(this ITBdoScriptword<IBdoSpec> spec)
            => spec.Func<bool>("value");

        public static ITBdoScriptword<bool> _Has(this ITBdoScriptword<IBdoSpec> spec, string name)
            => spec.Func<bool>("has", name);

        public static ITBdoScriptword<IBdoSpec> _Descendant(this ITBdoScriptword<IBdoSpec> spec, params object[] tokens)
            => spec.Func<IBdoSpec>("descendant", tokens);

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
