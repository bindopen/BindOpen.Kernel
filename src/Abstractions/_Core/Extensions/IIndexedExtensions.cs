namespace BindOpen.Kernel
{
    /// <summary>
    /// This interface represents a indexed data.
    /// </summary>
    public static class IIndexedExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param key="index"></param>
        public static T WithIndex<T>(
            this T obj,
            int? index)
            where T : IIndexed
        {
            if (obj != null)
            {
                obj.Index = index;
            }

            return obj;
        }
    }
}
