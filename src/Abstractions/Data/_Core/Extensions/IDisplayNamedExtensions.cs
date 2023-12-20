namespace BindOpen.Data
{
    /// <summary>
    /// This interface represents an named data item.
    /// </summary>
    public static class IDisplayNamedExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param key="name"></param>
        public static T WithDisplayName<T>(
            this T obj,
            string displayName)
            where T : IDisplayNamed
        {
            if (obj != null)
            {
                obj.DisplayName = displayName;
            }

            return obj;
        }
    }
}