using System.Collections.Generic;
using BindOpen.Framework.Core.Extensions.Items.Scriptwords.Definition.Dto;

namespace BindOpen.Framework.Core.Extensions.Items.Scriptwords.Definition
{
    /// <summary>
    /// 
    /// </summary>
    public interface IScriptwordDefinition : ITAppExtensionItemDefinition<IScriptwordDefinitionDto>
    {
        /// <summary>
        /// 
        /// </summary>
        IScriptwordDefinition Parent { get; set; }

        /// <summary>
        /// 
        /// </summary>
        List<IScriptwordDefinition> Children { get; }

        /// <summary>
        /// 
        /// </summary>
        ScriptwordFunction RuntimeFunction { get; set; }
    }
}