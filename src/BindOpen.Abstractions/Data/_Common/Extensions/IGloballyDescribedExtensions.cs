using BindOpen.Data.Helpers;
using BindOpen.Data.Items;

namespace BindOpen.Data
{
    /// <summary>
    /// This interface represents an described data item.
    /// </summary>
    public static class IGloballyDescribedExtensions
    {
        public static T WithDescription<T>(
            this T obj,
            IBdoDictionary dictionary)
            where T : IBdoDescribed
        {
            if (obj != null)
            {
                obj.Description = dictionary;
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
        public static string GetDescriptionText<T>(
            this T obj,
            string key = StringHelper.__Star,
            string defaultKey = StringHelper.__Star)
            where T : IBdoDescribed
        {
            return obj?.Description?[key, defaultKey];
        }
    }
}
