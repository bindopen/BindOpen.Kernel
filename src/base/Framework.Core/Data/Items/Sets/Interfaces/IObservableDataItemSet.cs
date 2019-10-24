using System.ComponentModel;

namespace BindOpen.Framework.Core.Data.Items.Sets
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IObservableDataItemSet<T> : IDataItemSet<T>, INotifyPropertyChanged
        where T : IdentifiedDataItem
    {
    }
}