namespace BindOpen.Extensions.Functions
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoFunctionDictionary : ITBdoExtensionDictionary<IBdoFunctionDefinition>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param key="definitionName"></param>
        /// <param key="methodName"></param>
        /// <param key="parent"></param>
        /// <returns></returns>
        IBdoFunctionDefinition GetDefinition(
            string definitionName,
            string methodName);
    }
}