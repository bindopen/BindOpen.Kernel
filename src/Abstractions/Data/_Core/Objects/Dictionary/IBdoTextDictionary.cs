using BindOpen.System.Data.Helpers;
using System.Collections.Generic;

namespace BindOpen.System.Data
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoTextDictionary :
        IBdoObjectNotMetable, IIdentified,
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
        /// <param key="key"></param>
        /// <param key="text"></param>
        /// <returns></returns>
        new IBdoTextDictionary Add(string key, string text);

        /// <summary>
        /// 
        /// </summary>
        /// <param key="keys"></param>
        void Remove(params string[] keys);

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
        bool Equals(IBdoTextDictionary dictionary);
    }
}