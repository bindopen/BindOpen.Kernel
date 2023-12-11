using System;

namespace BindOpen.Kernel
{
    /// <summary>
    /// This interface defines a storable data item.
    /// </summary>
    public static class IDatedExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param key="date"></param>
        /// <returns></returns>
        public static T WithCreationDate<T>(
            this T obj,
            DateTime? date)
            where T : IDated
        {
            if (obj != null)
            {
                obj.CreationDate = date;
            }

            return obj;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param key="date"></param>
        /// <returns></returns>
        public static T WithLastModificationDate<T>(
            this T obj,
            DateTime? date)
            where T : IDated
        {
            if (obj != null)
            {
                obj.LastModificationDate = date;
            }

            return obj;
        }
    }
}
