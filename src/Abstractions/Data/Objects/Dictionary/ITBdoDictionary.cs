using BindOpen.Data;
using BindOpen.Data.Helpers;
using System.Collections.Generic;

namespace BindOpen.Data
{
    /// <summary>
    /// 
    /// </summary>
    public interface ITBdoDictionary<T> :
        IBdoObjectNotMetable, IIdentified, IReferenced,
        IDictionary<string, T>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param key="key"></param>
        /// <param key="defaultKey"></param>
        /// <returns></returns>
        T this[string key, string defaultKey] { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param key="key"></param>
        /// <param key="text"></param>
        /// <returns></returns>
        new ITBdoDictionary<T> Add(string key, T item);

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
        T Get(string key = StringHelper.__Star, string alternateKey = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param key="dictionary"></param>
        /// <returns></returns>
        bool Equals(ITBdoDictionary<T> dictionary);
    }
}