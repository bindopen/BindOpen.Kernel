using BindOpen.Kernel.Data.Meta;

namespace BindOpen.Kernel.Scoping.Script
{
    /// <summary>
    /// This static class provides methods to create script elements.
    /// </summary>
    public static partial class BdoScript
    {
        public static TBdoScriptword<IBdoSpec> _Parent(this TBdoScriptword<IBdoSpec> spec)
            => spec.Var<IBdoSpec>("parent");

        public static TBdoScriptword<string> _Name(this TBdoScriptword<IBdoSpec> spec)
            => spec.Var<string>("name");

        public static TBdoScriptword<bool> _Value(this TBdoScriptword<IBdoSpec> spec)
            => spec.Func<bool>("value");

        public static TBdoScriptword<bool> _Has(this TBdoScriptword<IBdoSpec> spec, string name)
            => spec.Func<bool>("has", name);

        public static TBdoScriptword<IBdoSpec> _Descendant(this TBdoScriptword<IBdoSpec> spec, params object[] tokens)
            => spec.Func<IBdoSpec>("descendant", tokens);

        ///// <summary>
        ///// Returns the item TItem of this instance.
        ///// </summary>
        ///// <param key="log">The log to populate.</param>
        ///// <param key="scope">The scope to consider.</param>
        ///// <param key="varSet">The variable meta set to use.</param>
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
