namespace BindOpen.Data
{
    /// <summary>
    /// This interface defines an named data item.
    /// </summary>
    public static class INamedExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param key="name"></param>
        public static T WithName<T>(
            this T obj,
            string name)
            where T : INamed
        {
            if (obj != null)
            {
                obj.Name = name;
            }

            return obj;
        }
    }
}