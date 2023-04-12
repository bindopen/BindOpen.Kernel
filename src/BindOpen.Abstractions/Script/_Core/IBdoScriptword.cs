using BindOpen.Data;
using BindOpen.Data.Meta;

namespace BindOpen.Script
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