using BindOpen.Framework.Core.Data.Elements.Sets;
using BindOpen.Framework.Core.Extensions.Items.Scriptwords.Definition;
using BindOpen.Framework.Core.System.Scripting;

namespace BindOpen.Framework.Core.Extensions.Items.Scriptwords
{
    /// <summary>
    /// 
    /// </summary>
    public interface IScriptword : ITAppExtensionItem<IScriptwordDefinition>
    {
        /// <summary>
        /// 
        /// </summary>
        IScriptword Parent { get; set; }

        /// <summary>
        /// 
        /// </summary>
        IScriptword SubScriptword { get; set; }

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
        IScriptword Last();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IScriptword Root();
    }
}