namespace BindOpen.Kernel.Data
{
    /// <summary>
    /// 
    /// </summary>
    public static class IBdoDefinableExtensions
    {
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