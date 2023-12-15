using BindOpen.Kernel.Data;

namespace BindOpen.Kernel.Scoping.Script
{
    /// <summary>
    /// 
    /// </summary>
    public interface IScriptItem : IBdoObject, INamed, IIndexed
    {
        /// <summary>
        /// The kind.
        /// </summary>
        ScriptItemKinds Kind { get; set; }

    }
}