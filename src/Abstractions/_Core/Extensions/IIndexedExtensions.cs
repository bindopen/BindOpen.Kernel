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

        /// <summary>
        /// 
        /// </summary>
        /// <param key="index"></param>
        public static int? GetIndexValue(this IIndexed obj)
        {
            if (obj == null) return -1;

            return obj.Index < 0 ? int.MaxValue : obj.Index;
        }
    }
}
