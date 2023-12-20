namespace BindOpen.Scoping
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
        public static T WithDefinition<T>(
            this T obj,
            BdoExtensionKinds kind)
            where T : IBdoExtension
        {
            if (obj != null)
            {
                obj.ExtensionKind = kind;
            }
            return obj;
        }
    }
}