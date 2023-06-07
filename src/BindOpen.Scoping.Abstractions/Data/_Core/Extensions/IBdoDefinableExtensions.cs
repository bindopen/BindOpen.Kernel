namespace BindOpen.Scoping.Data
{
    /// <summary>
    /// 
    /// </summary>
    public static class IBdoDefinableExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param key="detail"></param>
        public static T WithDefinition<T>(
            this T obj,
            string definitionUniqueName)
            where T : IBdoDefinable
        {
            if (obj != null)
            {
                obj.DefinitionUniqueName = definitionUniqueName;
            }
            return obj;
        }
    }
}