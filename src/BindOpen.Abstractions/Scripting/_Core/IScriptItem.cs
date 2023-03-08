using BindOpen.Data;

namespace BindOpen.Scripting
{
    /// <summary>
    /// 
    /// </summary>
    public interface IScriptItem : IBdoItem, INamed
    {
        /// <summary>
        /// The kind.
        /// </summary>
        ScriptItemKinds Kind { get; set; }

        /// <summary>
        /// The index.
        /// </summary>
        int Index { get; set; }
    }
}