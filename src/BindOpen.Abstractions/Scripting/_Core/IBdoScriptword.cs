using BindOpen.Data;
using BindOpen.Data.Meta;

namespace BindOpen.Scripting
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoScriptword : IBdoMetaObject, IBdoNotMetableItem
    {
        /// <summary>
        /// 
        /// </summary>
        string DefinitionUniqueName { get; set; }

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