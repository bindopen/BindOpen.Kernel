using BindOpen.Kernel.Data;
using BindOpen.Kernel.Data.Meta;

namespace BindOpen.Kernel.Scoping.Script
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoScriptword : IBdoMetaObject, IBdoObjectNotMetable, ITSingleChildParent<IBdoScriptword>
    {
        /// <summary>
        /// 
        /// </summary>
        ScriptItemKinds Kind { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IBdoScriptword Last(int levelMax = 50);
    }
}