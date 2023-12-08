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
    }
}
