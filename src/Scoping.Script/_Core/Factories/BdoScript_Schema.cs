using BindOpen.Data.Schema;

namespace BindOpen.Scoping.Script
{
    /// <summary>
    /// This static class provides methods to create script elements.
    /// </summary>
    public static partial class BdoScript
    {
        public static ITBdoScriptword<IBdoSchema> _Parent(this ITBdoScriptword<IBdoSchema> schema)
            => schema.Var<IBdoSchema>("parent");

        public static ITBdoScriptword<string> _Name(this ITBdoScriptword<IBdoSchema> schema)
            => schema.Var<string>("name");

        public static ITBdoScriptword<bool> Value(this ITBdoScriptword<IBdoSchema> schema)
            => schema.Func<bool>("value");

        public static ITBdoScriptword<bool> _Has(this ITBdoScriptword<IBdoSchema> schema, string name)
            => schema.Func<bool>("has", name);

        public static ITBdoScriptword<IBdoSchema> _Descendant(this ITBdoScriptword<IBdoSchema> schema, params object[] tokens)
            => schema.Func<IBdoSchema>("descendant", tokens);

        ///// <summary>
        ///// Returns the item TItem of this instance.
        ///// </summary>
        ///// <param key="log">The log to populate.</param>
        ///// <param key="scope">The scope to consider.</param>
        ///// <param key="metaSet">The variable meta set to use.</param>
        ///// <returns>Returns the items of this instance.</returns>
        //[BdoFunction("descendant")]
        //public static IBdoSchema BdoDescendant(
        //    [BdoThis] IBdoSchema schema,
        //    params object[] tokens)
        //{
        //    return schema?.Descendant<IBdoSchema>(tokens);
        //}
    }
}
