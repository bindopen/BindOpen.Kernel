using BindOpen.System.Data.Meta;

namespace BindOpen.System.Scoping.Script
{
    /// <summary>
    /// This static class provides methods to create script elements.
    /// </summary>
    public static partial class BdoScript
    {
        public static TBdoScriptword<bool> Name(this TBdoScriptword<IBdoMetaData> parent)
            => parent.Func<bool>("name");

        public static TBdoScriptword<bool> Value(this TBdoScriptword<IBdoMetaData> parent)
            => parent.Func<bool>("value");
    }
}
