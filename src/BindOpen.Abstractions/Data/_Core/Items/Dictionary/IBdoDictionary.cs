using BindOpen.Data.Helpers;
using System.Collections.Generic;

namespace BindOpen.Data
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoDictionary :
        IBdoNotMetableItem, IIdentified,
        IDictionary<string, string>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param key="key"></param>
        /// <param key="defaultKey"></param>
        /// <returns></returns>
        string this[string key, string defaultKey] { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param key="pair"></param>
        IBdoDictionary Add(params KeyValuePair<string, string>[] pairs);

        /// <summary>
        /// 
        /// </summary>
        /// <param key="text"></param>
        /// <returns></returns>
        IBdoDictionary Add(string text);

        /// <summary>
        /// 
        /// </summary>
        /// <param key="key"></param>
        /// <param key="text"></param>
        /// <returns></returns>
        IBdoDictionary Add(string key, string text, List<string> availableKeys = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param key="keys"></param>
        IBdoDictionary Remove(params string[] keys);

        /// <summary>
        /// 
        /// </summary>
        /// <param key="pair"></param>
        IBdoDictionary Set(params KeyValuePair<string, string>[] pairs);

        /// <summary>
        /// 
        /// </summary>
        /// <param key="text"></param>
        IBdoDictionary Set(string text);

        /// <summary>
        /// 
        /// </summary>
        /// <param key="key"></param>
        /// <param key="text"></param>
        IBdoDictionary Set(string key, string text);

        /// <summary>
        /// 
        /// </summary>
        /// <param key="key"></param>
        /// <param key="alternateKey"></param>
        /// <returns></returns>
        string Get(string key = StringHelper.__Star, string alternateKey = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param key="dictionary"></param>
        /// <returns></returns>
        bool Equals(IBdoDictionary dictionary);
    }
}