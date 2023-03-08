using System.Linq;

namespace BindOpen.Data
{
    /// <summary>
    /// 
    /// </summary>
    public static class TBdoSetExtensions
    {
        // Add

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="Q"></typeparam>
        /// <typeparam name="T"></typeparam>
        /// <param key="list1"></param>
        /// <param key="list2"></param>
        /// <returns></returns>
        public static Q AddRange<Q, T>(
            this Q list1,
            ITBdoSet<T> list2)
            where Q : ITBdoSet<T>
            where T : IReferenced
        {
            list1?.Add(list2?.Items?
                .Cast<T>()
                .ToArray());
            return list1;
        }

        // With

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="Q"></typeparam>
        /// <typeparam name="T"></typeparam>
        /// <param key="list1"></param>
        /// <param key="list2"></param>
        /// <returns></returns>
        public static Q WithRange<Q, T>(
            this Q list1,
            ITBdoSet<T> list2)
            where Q : ITBdoSet<T>
            where T : IReferenced
        {
            list1?.With(list2?.Items?
                .Cast<T>()
                .ToArray());
            return list1;
        }
    }
}