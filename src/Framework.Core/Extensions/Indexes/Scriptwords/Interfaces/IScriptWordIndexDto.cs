using BindOpen.Framework.Core.Extensions.Definitions.Scriptwords;

namespace BindOpen.Framework.Core.Extensions.Indexes.Scriptwords
{
    public interface IScriptwordIndexDto : ITAppExtensionItemIndexDto<ScriptwordDefinitionDto>
    {
        string DefinitionClass { get; set; }
    }
}