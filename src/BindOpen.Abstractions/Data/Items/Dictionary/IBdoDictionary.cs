using System.Collections.Generic;

namespace BindOpen.Data.Items
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoDictionary :
        IBdoItem,
        ITIdentifiedPoco<IBdoDictionary>,
        IDictionary<string, string>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defaultKey"></param>
        /// <returns></returns>
        string this[string key, string defaultKey] { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pair"></param>
        IBdoDictionary Add(params KeyValuePair<string, string>[] pairs);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        IBdoDictionary Add(string text);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        IBdoDictionary Add(string key, string text, List<string> availableKeys = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keys"></param>
        IBdoDictionary Remove(params string[] keys);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pair"></param>
        IBdoDictionary Set(params KeyValuePair<string, string>[] pairs);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        IBdoDictionary Set(string text);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="text"></param>
        IBdoDictionary Set(string key, string text);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="alternateKey"></param>
        /// <returns></returns>
        string Get(string key = "*", string alternateKey = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dictionary"></param>
        /// <returns></returns>
        bool Equals(IBdoDictionary dictionary);
    }
}