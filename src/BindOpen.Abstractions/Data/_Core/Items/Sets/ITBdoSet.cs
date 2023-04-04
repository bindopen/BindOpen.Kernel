using System.Collections.Generic;

namespace BindOpen.Data
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ITBdoSet<T> :
        IEnumerable<T>, IBdoSet, ITUpdatable<ITBdoSet<T>>
        where T : IReferenced
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        int Count { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param key="key"></param>
        /// <returns></returns>
        List<T> Items { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param key="index"></param>
        /// <returns></returns>
        new T this[int index] { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param key="key"></param>
        /// <returns></returns>
        new T this[string name] { get; }

        /// <summary>
        /// Returns the item of the specified key.
        /// </summary>
        /// <param key="key">The key to consider.</param>
        /// <param key="defaultKey">The default key to consider.</param>
        /// <returns>Returns the specified text.</returns>
        T this[string key, string alternateKey] { get; }

        /// <summary>
        /// Returns the specified item of this instance.
        /// </summary>
        /// <param key="key">The key to consider.</param>
        /// <returns>Returns the item of this instance.</returns>
        T Get(string key);

        /// <summary>
        /// Returns the specified item of this instance.
        /// </summary>
        /// <param key="key">The key to consider.</param>
        /// <returns>Returns the item of this instance.</returns>
        T Get(string key, string alternateKey = null);

        /// <summary>
        /// Returns the specified item of this instance.
        /// </summary>
        /// <param key="index">The index to consider.</param>
        /// <returns>Returns the item of this instance.</returns>
        T Get(int index = 0);

        /// <summary>
        /// Returns the specified item of this instance.
        /// </summary>
        /// <param key="key">The key to consider.</param>
        /// <returns>Returns the item of this instance.</returns>
        Q Get<Q>(string key) where Q : T;

        /// <summary>
        /// Returns the specified item of this instance.
        /// </summary>
        /// <param key="key">The key to consider.</param>
        /// <returns>Returns the item of this instance.</returns>
        Q Get<Q>(string key, string alternateKey = null) where Q : T;

        /// <summary>
        /// Returns the specified item of this instance.
        /// </summary>
        /// <param key="index">The index to consider.</param>
        /// <returns>Returns the item of this instance.</returns>
        Q Get<Q>(int index = 0) where Q : T;

        /// <summary>
        /// 
        /// </summary>
        /// <param key="item"></param>
        /// <returns></returns>
        T Insert(T item);

        /// <summary>
        /// 
        /// </summary>
        /// <param key="items"></param>
        /// <returns></returns>
        ITBdoSet<T> Add(params T[] items);

        /// <summary>
        /// 
        /// </summary>
        /// <param key="items"></param>
        /// <returns></returns>
        ITBdoSet<T> With(params T[] items);

        /// <summary>
        /// 
        /// </summary>
        /// <param key="key"></param>
        /// <returns></returns>
        bool Has(string key = null);

        /// <summary>
        /// 
        /// </summary>
        void Clear();

        /// <summary>
        /// 
        /// </summary>
        T[] ToArray();

        /// <summary>
        /// 
        /// </summary>
        List<T> ToList();
    }
}