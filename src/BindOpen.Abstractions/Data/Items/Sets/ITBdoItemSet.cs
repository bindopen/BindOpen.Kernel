using System.Collections.Generic;

namespace BindOpen.Data.Items
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ITBdoItemSet<T> :
        IBdoItem,
        ITIdentifiedPoco<ITBdoItemSet<T>>,
        IEnumerable<T>
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
        /// Returns the specified item of this instance.
        /// </summary>
        /// <param name="key">The key to consider.</param>
        /// <returns>Returns the item of this instance.</returns>
        T Get(string key = null);

        /// <summary>
        /// Returns the specified item of this instance.
        /// </summary>
        /// <param name="key">The key to consider.</param>
        /// <returns>Returns the item of this instance.</returns>
        Q Get<Q>(string key = null) where Q : T;

        /// <summary>
        /// Returns the specified item of this instance.
        /// </summary>
        /// <param name="index">The index to consider.</param>
        /// <returns>Returns the item of this instance.</returns>
        Q Get<Q>(int index) where Q : T;

        /// <summary>
        /// 
        /// </summary>
        ITBdoItemSet<T> ClearItems();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="items"></param>
        ITBdoItemSet<T> Add(params T[] items);

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
        ITBdoItemSet<T> WithItems(params T[] items);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        bool HasItem(string key = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keys"></param>
        ITBdoItemSet<T> Remove(params string[] keys);

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