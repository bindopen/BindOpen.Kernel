using BindOpen.Extensions.Scripting;

namespace BindOpen.Runtime.Definition
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoScriptwordDictionary : ITBdoExtensionDictionary<IBdoScriptwordDefinition>
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
        IBdoScriptwordDefinition GetDefinition(
            string definitionName,
            string methodName,
            IBdoScriptwordDefinition parent = null);
    }
}