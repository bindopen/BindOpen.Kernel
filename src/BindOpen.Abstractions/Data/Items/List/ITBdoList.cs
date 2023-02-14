using System.Collections.Generic;

namespace BindOpen.Data.Items
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ITBdoList<T> :
        IBdoItem, IEnumerable<T>,
        IIdentified, IReferenced
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
        /// <param name="key"></param>
        /// <returns></returns>
        List<T> Items { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        T this[string key] { get; }

        /// <summary>
        /// Returns the item of the specified key.
        /// </summary>
        /// <param name="key">The key to consider.</param>
        /// <param name="defaultKey">The default key to consider.</param>
        /// <returns>Returns the specified text.</returns>
        T this[string key, string alternateKey] { get; }

        /// <summary>
        /// Returns the specified item of this instance.
        /// </summary>
        /// <param name="key">The key to consider.</param>
        /// <returns>Returns the item of this instance.</returns>
        T Get(string key = null, string alternateKey = null);

        /// <summary>
        /// Returns the specified item of this instance.
        /// </summary>
        /// <param name="index">The index to consider.</param>
        /// <returns>Returns the item of this instance.</returns>
        T Get(int index);

        /// <summary>
        /// Returns the specified item of this instance.
        /// </summary>
        /// <param name="key">The key to consider.</param>
        /// <returns>Returns the item of this instance.</returns>
        Q Get<Q>(string key = null, string alternateKey = null) where Q : T;

        /// <summary>
        /// Returns the specified item of this instance.
        /// </summary>
        /// <param name="index">The index to consider.</param>
        /// <returns>Returns the item of this instance.</returns>
        Q Get<Q>(int index) where Q : T;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        T Insert(T item);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        ITBdoList<T> Add(params T[] items);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        ITBdoList<T> With(params T[] items);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
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