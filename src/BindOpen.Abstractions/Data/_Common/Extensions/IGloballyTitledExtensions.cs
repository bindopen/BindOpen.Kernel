using BindOpen.Data.Helpers;
using BindOpen.Data.Items;

namespace BindOpen.Data
{
    /// <summary>
    /// This interface represents an described data item.
    /// </summary>
    public static class IGloballyTitledExtensions
    {
        public static T WithTitle<T>(
            this T obj,
            IBdoDictionary dictionary)
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
        /// <param name="obj"></param>
        /// <param name="key"></param>
        /// <param name="defaultKey"></param>
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
