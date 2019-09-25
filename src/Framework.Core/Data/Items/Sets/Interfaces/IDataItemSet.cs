using System.Collections.Generic;
using BindOpen.Framework.Core.Data.Items.Dto;

namespace BindOpen.Framework.Core.Data.Items.Sets
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IDataItemSet<T> : IIdentifiedDataItem
        where T : IdentifiedDataItem
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
        /// 
        /// </summary>
        int Count { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="items"></param>
        void Add(params T[] items);

        /// <summary>
        /// 
        /// </summary>
        void ClearItems();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="items"></param>
        /// <param name="referenceCollection"></param>
        void Add(List<T> items, IDataItemSet<T> referenceCollection = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <param name="referenceCollection"></param>
        void Add(T item, IDataItemSet<T> referenceCollection = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="itemSet"></param>
        /// <returns></returns>
        List<string> GetCommonItemKeys(IDataItemSet<T> itemSet);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        T GetItem(string key);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        bool HasItem(string key);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        bool HasItems();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keys"></param>
        void Remove(params string[] keys);
    }
}