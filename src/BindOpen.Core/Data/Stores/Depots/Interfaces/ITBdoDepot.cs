using BindOpen.Data.Items;

namespace BindOpen.Data.Stores
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ITBdoDepot<T> : IDataItemSet<T>, IBdoDepot where T : IIdentifiedDataItem
    {
    }
}