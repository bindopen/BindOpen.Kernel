using System.Collections.Generic;
using BindOpen.Framework.Core.Data.Items.Dto;

namespace BindOpen.Framework.Core.Data.Items.Sets
{
    public interface IDataItemSet<T> : IIdentifiedDataItem
        where T : IdentifiedDataItem
    {
        List<T> Items { get; set; }

        T this[int index] { get; }
        T this[string key] { get; }

        int Count { get; }

        void Add(params T[] items);
        void ClearItems();

        void Add(List<T> items, IDataItemSet<T> referenceCollection = null);
        void Add(T item, IDataItemSet<T> referenceCollection = null);
        List<string> GetCommonItemKeys(IDataItemSet<T> itemSet);

        T GetItem(string key);

        bool HasItem(string key);
        bool HasItems();
        void Remove(params string[] keys);
    }
}