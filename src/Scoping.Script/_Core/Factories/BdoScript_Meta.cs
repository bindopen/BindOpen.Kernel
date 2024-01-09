using BindOpen.Data.Meta;

namespace BindOpen.Scoping.Script
{
    /// <summary>
    /// This static class provides methods to create script elements.
    /// </summary>
    public static partial class BdoScript
    {
        public static ITBdoScriptword<IBdoMetaData> _Parent(this ITBdoScriptword<IBdoMetaData> meta)
            => meta.Var<IBdoMetaData>("parent");

        public static ITBdoScriptword<string> _Name(this ITBdoScriptword<IBdoMetaData> meta)
            => meta.Var<string>("name");

        public static ITBdoScriptword<bool> _Value(this ITBdoScriptword<IBdoMetaData> meta)
            => meta.Func<bool>("value");

        public static ITBdoScriptword<bool> _Has(this ITBdoScriptword<IBdoMetaData> meta, string name)
            => meta.Func<bool>("has", name);

        public static ITBdoScriptword<IBdoMetaData> _Descendant(this ITBdoScriptword<IBdoMetaData> spec, params object[] tokens)
            => spec.Func<IBdoMetaData>("descendant", tokens);
    }
}
