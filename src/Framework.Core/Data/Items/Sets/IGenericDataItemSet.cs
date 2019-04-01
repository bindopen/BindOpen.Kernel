using System.Collections.Generic;
using System.ComponentModel;
using BindOpen.Framework.Core.Data.Dto;

namespace BindOpen.Framework.Core.Data.Items.Sets
{
    public interface IGenericDataItemSet<T> : IDataItem, IGloballyDescribed, INotifyPropertyChanged
        where T : IStoredDataItem
    {
        T this[int index] { get; }
        T this[string key] { get; }

        int Count { get; }

        void Add(List<T> items, GenericDataItemSet<T> referenceCollection = null);
        void Add(params T[] items);
        void Add(T item, GenericDataItemSet<T> referenceCollection = null);
        void ClearItems();
        List<string> GetCommonItemKeys(GenericDataItemSet<T> itemSet);
        T GetItem(string key);
        bool HasItem(string key);
        bool HasItems();
        void Remove(params string[] keys);

        bool DescriptionSpecified { get; }
    }
}