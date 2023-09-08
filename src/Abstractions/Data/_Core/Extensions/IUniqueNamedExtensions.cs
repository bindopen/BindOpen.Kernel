namespace BindOpen.Kernel.Data
{
    /// <summary>
    /// This interface represents an named data item.
    /// </summary>
    public static class IUniqueNamedExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param key="name"></param>
        public static T WithUniqueName<T>(
            this T obj,
            string name)
            where T : IUniqueNamed
        {
            if (obj != null)
            {
                obj.UniqueName = name;
            }

            return obj;
        }
    }
}