using BindOpen.Data;
using BindOpen.Data.Items;

namespace BindOpen.Extensions.Scripting
{
    /// <summary>
    /// 
    /// </summary>
    public interface IScriptItem :
        IBdoItem, INamed
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