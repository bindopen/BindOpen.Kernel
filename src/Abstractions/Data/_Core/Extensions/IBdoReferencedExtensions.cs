namespace BindOpen.Kernel.Data
{
    /// <summary>
    /// This interface defines a storable data item.
    /// </summary>
    public static class IBdoReferencedExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param key="date"></param>
        /// <returns></returns>
        public static T WithReference<T>(
            this T obj,
            IBdoReference reference = null)
            where T : IBdoReferenced
        {
            if (obj != null)
            {
                obj.Reference = reference;
            }

            return obj;
        }
    }
}
