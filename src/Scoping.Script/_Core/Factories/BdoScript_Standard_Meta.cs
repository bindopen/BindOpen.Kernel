using BindOpen.System.Data.Meta;

namespace BindOpen.System.Scoping.Script
{
    /// <summary>
    /// This static class provides methods to create script elements.
    /// </summary>
    public static partial class BdoScript
    {
        public static ITBdoScriptword<bool> Name(this ITBdoScriptword<IBdoMetaData> parent)
            => parent.Func<bool>("name");

        public static ITBdoScriptword<bool> Value(this ITBdoScriptword<IBdoMetaData> parent)
            => parent.Func<bool>("value");
    }
}
