namespace BindOpen.Runtime.Definitions
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
        /// <param key="definitionName"></param>
        /// <param key="methodName"></param>
        /// <param key="parent"></param>
        /// <returns></returns>
        IBdoScriptwordDefinition GetDefinition(
            string definitionName,
            string methodName,
            IBdoScriptwordDefinition parent = null);
    }
}