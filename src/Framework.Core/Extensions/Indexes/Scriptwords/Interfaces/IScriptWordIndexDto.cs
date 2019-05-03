using BindOpen.Framework.Core.Extensions.Items.Scriptwords.Definition;
using BindOpen.Framework.Core.Extensions.Items.Scriptwords.Definition.Dto;

namespace BindOpen.Framework.Core.Extensions.Indexes.Scriptwords
{
    public interface IScriptwordIndexDto : ITAppExtensionItemIndexDto<ScriptwordDefinitionDto>
    {
        string DefinitionClass { get; set; }

        IScriptwordDefinitionDto GetDefinition(
            string definitionName,
            string methodName,
            IScriptwordDefinitionDto parent = null);
    }
}