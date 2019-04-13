using BindOpen.Framework.Core.Data.Elements.Sets;
using BindOpen.Framework.Core.Data.Items.Dto;
using BindOpen.Framework.Core.Extensions.Definition.Scriptwords;
using BindOpen.Framework.Core.System.Scripting;

namespace BindOpen.Framework.Core.Extensions.Items.Scriptwords
{
    public interface IScriptword : ITAppExtensionItem<IScriptwordDefinition>, INamed
    {
        IScriptword Parent { get; set; }

        IScriptword SubScriptword { get; set; }

        ScriptItemKind Kind { get; set; }
        IDataElementSet ParameterDetail { get; set; }

        object Item { get; set; }
        string StringItem { get; }

        IScriptword Last();

        IScriptword Root();
    }
}