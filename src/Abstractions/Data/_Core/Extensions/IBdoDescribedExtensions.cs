﻿using BindOpen.Data.Helpers;

namespace BindOpen.Data
{
    /// <summary>
    /// This interface defines an described data item.
    /// </summary>
    public static class IBdoDescribedExtensions
    {
        public static T WithDescription<T>(
            this T obj,
            ITBdoDictionary<string> dictionary)
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
        /// <param key="obj"></param>
        /// <param key="key"></param>
        /// <param key="defaultKey"></param>
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
