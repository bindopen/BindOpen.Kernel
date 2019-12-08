using BindOpen.Framework.Core.Data.Elements;
using BindOpen.Framework.Core.Extensions.Definition.Items;
using BindOpen.Framework.Core.System.Scripting;

namespace BindOpen.Framework.Core.Extensions.Runtime.Items
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoScriptword : ITBdoExtensionItem<IBdoScriptwordDefinition>
    {
        /// <summary>
        /// 
        /// </summary>
        IBdoScriptword Parent { get; set; }

        /// <summary>
        /// 
        /// </summary>
        IBdoScriptword SubScriptword { get; set; }

        /// <summary>
        /// 
        /// </summary>
        ScriptItemKinds Kind { get; set; }

        /// <summary>
        /// 
        /// </summary>
        IDataElementSet ParameterDetail { get; set; }

        /// <summary>
        /// 
        /// </summary>
        object Item { get; set; }

        /// <summary>
        /// 
        /// </summary>
        string StringItem { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IBdoScriptword Last();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IBdoScriptword Root();
    }
}