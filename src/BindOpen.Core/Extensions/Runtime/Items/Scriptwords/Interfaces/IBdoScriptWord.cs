using BindOpen.Data.Elements;
using BindOpen.Extensions.Definition;
using BindOpen.System.Scripting;

namespace BindOpen.Extensions.Runtime
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