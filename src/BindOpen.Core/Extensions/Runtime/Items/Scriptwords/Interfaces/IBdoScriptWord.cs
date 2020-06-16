using BindOpen.Extensions.Definition;
using BindOpen.System.Scripting;
using System.Collections.Generic;

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
        List<object> Parameters { get; set; }

        /// <summary>
        /// 
        /// </summary>
        object Item { get; set; }

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

        /// <summary>
        /// 
        /// </summary>
        IBdoScriptword AddParameter(object value);
    }
}