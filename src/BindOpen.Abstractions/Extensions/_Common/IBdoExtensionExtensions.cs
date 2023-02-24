namespace BindOpen.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class IBdoExtensionExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param key="detail"></param>
        public static T WithDefinitionUniqueName<T>(
            this T obj,
            string definitionUniqueName)
            where T : IBdoExtension
        {
            if (obj != null)
            {
                obj.DefinitionUniqueName = definitionUniqueName;
            }
            return obj;
        }
    }
}