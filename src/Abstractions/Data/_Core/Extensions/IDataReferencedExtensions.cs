namespace BindOpen.System.Data
{
    /// <summary>
    /// This interface defines a storable data item.
    /// </summary>
    public static class IDataReferencedExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param key="date"></param>
        /// <returns></returns>
        public static T WithDataReference<T>(
            this T obj,
            IBdoReference reference = null)
            where T : IBdoDataReferenced
        {
            if (obj != null)
            {
                obj.DataReference = reference;
            }

            return obj;
        }
    }
}
