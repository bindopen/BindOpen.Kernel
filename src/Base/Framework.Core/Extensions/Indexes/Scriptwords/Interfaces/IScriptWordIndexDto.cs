using BindOpen.Framework.Core.Extensions.Items.Scriptwords.Definition;
using BindOpen.Framework.Core.Extensions.Items.Scriptwords.Definition.Dto;

namespace BindOpen.Framework.Core.Extensions.Indexes.Scriptwords
{
    /// <summary>
    /// 
    /// </summary>
    public interface IScriptwordIndexDto : ITAppExtensionItemIndexDto<ScriptwordDefinitionDto>
    {
        /// <summary>
        /// 
        /// </summary>
        string DefinitionClass { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="definitionName"></param>
        /// <param name="methodName"></param>
        /// <param name="parent"></param>
        /// <returns></returns>
        IScriptwordDefinitionDto GetDefinition(
            string definitionName,
            string methodName,
            IScriptwordDefinitionDto parent = null);
    }
}