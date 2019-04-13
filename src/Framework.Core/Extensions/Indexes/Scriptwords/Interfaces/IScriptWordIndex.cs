using System.Collections.Generic;
using BindOpen.Framework.Core.Extensions.Definition.Scriptwords;

namespace BindOpen.Framework.Core.Extensions.Indexes.Scriptwords
{
    public interface IScriptwordIndex : ITAppExtensionItemIndex<IScriptwordDefinition>
    {
        List<IScriptwordDefinition> GetDefinitionsWithApproximativeName(string name, IScriptwordDefinition parentDefinition = null);

        List<IScriptwordDefinition> GetDefinitionsWithExactName(string name, IScriptwordDefinition parentDefinition = null);
    }
}