using BindOpen.Framework.Core.Extensions.Definition.Scriptwords;

namespace BindOpen.Framework.Core.Extensions.Indexes.Scriptwords
{
    public interface IScriptwordIndexDto : ITAppExtensionItemIndexDto<IScriptwordDefinitionDto>
    {
        string DefinitionClass { get; set; }
    }
}