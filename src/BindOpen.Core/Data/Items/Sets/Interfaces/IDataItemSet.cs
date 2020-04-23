using System.Collections.Generic;

namespace BindOpen.Data.Items
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IDataItemSet<T> : IIdentifiedDataItem where T : IIdentifiedDataItem
    {
        /// <summary>
        /// 
        /// </summary>
        List<T> Items { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        T this[int index] { get; }

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
        Q Get<Q>(string key = null) where Q : class, T;

        /// <summary>
        /// Returns the specified item of this instance.
        /// </summary>
        /// <param name="index">The index to consider.</param>
        /// <returns>Returns the item of this instance.</returns>
        Q Get<Q>(int index) where Q : class, T;

        /// <summary>
        /// 
        /// </summary>
        int Count { get; }

        /// <summary>
        /// 
        /// </summary>
        IDataItemSet<T> ClearItems();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="items"></param>
        IDataItemSet<T> Add(params T[] items);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="items"></param>
        IDataItemSet<T> WithItems(params T[] items);

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
        IDataItemSet<T> Remove(params string[] keys);

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