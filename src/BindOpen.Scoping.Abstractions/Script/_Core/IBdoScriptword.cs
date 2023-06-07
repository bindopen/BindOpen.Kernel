using BindOpen.Scoping.Data;
using BindOpen.Scoping.Data.Meta;

namespace BindOpen.Scoping.Script
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoScriptword :
        IBdoMetaObject, IBdoObjectNotMetable, IBdoDefinable
    {
        /// <summary>
        /// 
        /// </summary>
        ScriptItemKinds Kind { get; set; }

        /// <summary>
        /// 
        /// </summary>
        IBdoScriptword Child { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IBdoScriptword Last(int levelMax = 50);
    }
}