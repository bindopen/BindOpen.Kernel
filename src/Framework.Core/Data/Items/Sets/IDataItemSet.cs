using System.Collections.Generic;

namespace BindOpen.Framework.Core.Data.Items.Sets
{
    public interface IDataItemSet<T> : IGenericDataItemSet<T> where T : IStoredDataItem
    {
        List<T> Items { get; set; }
    }
}