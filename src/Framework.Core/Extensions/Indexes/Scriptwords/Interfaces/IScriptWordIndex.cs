using System.Collections.Generic;
using BindOpen.Framework.Core.Extensions.Items.Scriptwords;
using BindOpen.Framework.Core.Extensions.Items.Scriptwords.Definition;

namespace BindOpen.Framework.Core.Extensions.Indexes.Scriptwords
{
    public interface IScriptwordIndex : ITAppExtensionItemIndex<IScriptwordDefinition>
    {
        List<IScriptwordDefinition> GetDefinitionsWithApproximativeName(string name, IScriptwordDefinition parentDefinition = null);

        List<IScriptwordDefinition> GetDefinitionsWithExactName(string name, IScriptwordDefinition parentDefinition = null);
    }
}