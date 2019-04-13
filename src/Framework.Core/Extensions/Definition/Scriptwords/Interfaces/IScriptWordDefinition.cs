using System.Collections.Generic;
using BindOpen.Framework.Core.Extensions.Items.Scriptwords;

namespace BindOpen.Framework.Core.Extensions.Definition.Scriptwords
{
    public interface IScriptwordDefinition : ITAppExtensionItemDefinition<IScriptwordDefinitionDto>
    {
        IScriptwordDefinition Parent { get; }
        List<IScriptwordDefinition> Children { get; }

        ScriptwordFunction RuntimeFunction { get; set; }
    }
}