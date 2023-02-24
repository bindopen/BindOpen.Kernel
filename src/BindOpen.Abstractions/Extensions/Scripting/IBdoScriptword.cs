using BindOpen.Data.Items;
using BindOpen.Data.Meta;

namespace BindOpen.Extensions.Scripting
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