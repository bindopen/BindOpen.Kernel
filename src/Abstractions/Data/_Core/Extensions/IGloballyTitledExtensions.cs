using BindOpen.Kernel.Data.Helpers;

namespace BindOpen.Kernel.Data
{
    /// <summary>
    /// This interface represents an described data item.
    /// </summary>
    public static class IGloballyTitledExtensions
    {
        public static T WithTitle<T>(
            this T obj,
            ITBdoDictionary<string> dictionary)
            where T : IBdoTitled
        {
            if (obj != null)
            {
                obj.Title = dictionary;
            }

            return obj;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param key="obj"></param>
        /// <param key="key"></param>
        /// <param key="defaultKey"></param>
        /// <returns></returns>
        public static string GetTitleText<T>(
            this T obj,
            string key = StringHelper.__Star,
            string defaultKey = StringHelper.__Star)
            where T : IBdoTitled
        {
            return obj?.Title?[key, defaultKey];
        }
    }
}
