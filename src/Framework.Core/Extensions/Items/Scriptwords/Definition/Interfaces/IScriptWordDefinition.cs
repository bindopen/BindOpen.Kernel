using System.Collections.Generic;

namespace BindOpen.Framework.Core.Extensions.Items.Scriptwords.Definition
{
    public interface IScriptwordDefinition : ITAppExtensionItemDefinition<IScriptwordDefinitionDto>
    {
        IScriptwordDefinition Parent { get; set; }
        List<IScriptwordDefinition> Children { get; }

        ScriptwordFunction RuntimeFunction { get; set; }
    }
}