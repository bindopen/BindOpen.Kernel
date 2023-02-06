using System;

namespace BindOpen.Data
{
    /// <summary>
    /// This interface defines a storable data item.
    /// </summary>
    public static class IStorableExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static T WithCreationDate<T>(
            this T obj,
            DateTime? date)
            where T : IStorable
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
        /// <param name="date"></param>
        /// <returns></returns>
        public static T WithLastModificationDate<T>(
            this T obj,
            DateTime? date)
            where T : IStorable
        {
            if (obj != null)
            {
                obj.LastModificationDate = date;
            }

            return obj;
        }
    }
}
