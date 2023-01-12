using BindOpen.Meta;
using System.Collections.Generic;

namespace BindOpen.Extensions.Scripting
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoScriptword : ITBdoExtensionItem<IBdoScriptwordDefinition, IBdoScriptwordConfiguration, IBdoScriptword>,
        ITNamedPoco<IBdoScriptword>
    {
        /// <summary>
        /// 
        /// </summary>
        ScriptItemKinds Kind { get; }

        /// <summary>
        /// 
        /// </summary>
        IBdoScriptword Parent { get; set; }

        /// <summary>
        /// 
        /// </summary>
        IBdoScriptword WithParent(IBdoScriptword scriptword);

        /// <summary>
        /// 
        /// </summary>
        IBdoScriptword SubScriptword { get; set; }

        /// <summary>
        /// 
        /// </summary>
        IBdoScriptword WithSubScriptword(IBdoScriptword scriptword);

        /// <summary>
        /// 
        /// </summary>
        List<object> Parameters { get; set; }

        /// <summary>
        /// 
        /// </summary>
        IBdoScriptword WithParameters(params object[] objects);

        /// <summary>
        /// 
        /// </summary>
        IBdoScriptword AddParameter(object value);

        /// <summary>
        /// 
        /// </summary>
        object Item { get; set; }

        /// <summary>
        /// 
        /// </summary>
        IBdoScriptword WithItem(object item);

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