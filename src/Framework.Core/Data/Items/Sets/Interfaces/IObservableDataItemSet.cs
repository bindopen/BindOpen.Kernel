using System.ComponentModel;

namespace BindOpen.Framework.Core.Data.Items.Sets
{
    public interface IObservableDataItemSet<T> : IDataItemSet<T>, INotifyPropertyChanged
        where T : IdentifiedDataItem
    {
    }
}