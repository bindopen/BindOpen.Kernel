﻿using System.ComponentModel;

namespace BindOpen.Data.Items
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