using BindOpen.Framework.Core.Data.Elements.Sets;
using BindOpen.Framework.Core.Extensions.Definition.Scriptwords;
using BindOpen.Framework.Core.System.Scripting;

namespace BindOpen.Framework.Core.Extensions.Configuration.Scriptwords
{
    public interface IScriptWord : ITAppExtensionItemConfiguration<IScriptWordDefinition>
    {
        object Item { get; set; }
        ScriptItemKind Kind { get; set; }
        IDataElementSet ParameterDetail { get; set; }
        IScriptWord Parent { get; set; }
        string StringItem { get; }
        IScriptWord SubScriptWord { get; set; }

        IScriptWord Last();
        IDataElementSet NewParameterDetail();
        IScriptWord Root();
    }
}