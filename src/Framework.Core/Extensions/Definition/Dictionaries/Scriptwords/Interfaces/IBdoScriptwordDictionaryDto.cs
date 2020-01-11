using BindOpen.Framework.Extensions.Definition;

namespace BindOpen.Framework.Extensions.Definition
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoScriptwordDictionaryDto : ITBdoExtensionDictionaryDto<BdoScriptwordDefinitionDto>
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
        IBdoScriptwordDefinitionDto GetDefinition(
            string definitionName,
            string methodName,
            IBdoScriptwordDefinitionDto parent = null);
    }
}