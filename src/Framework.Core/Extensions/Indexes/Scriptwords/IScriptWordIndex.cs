using System.Collections.Generic;
using BindOpen.Framework.Core.Extensions.Configuration.Scriptwords;
using BindOpen.Framework.Core.Extensions.Definition.Scriptwords;

namespace BindOpen.Framework.Core.Extensions.Indexes
{
    public interface IScriptWordIndex : ITAppExtensionItemIndex<IScriptWordDefinition>
    {
        string DefinitionClass { get; set; }

        List<IScriptWordDefinition> GetDefinitionsWithApproximativeName(string name, IScriptWordDefinition parentDefinition = null);

        List<IScriptWordDefinition> GetDefinitionsWithExactName(string name, IScriptWordDefinition parentDefinition = null);

        bool IsWordMatching(IScriptWord scriptWord, IScriptWordDefinition scriptWordDefinition);
    }
}