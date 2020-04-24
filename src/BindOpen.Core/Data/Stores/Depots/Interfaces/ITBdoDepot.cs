using BindOpen.Data.Items;

namespace BindOpen.Data.Stores
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ITBdoDepot<T> : ITDataItemSet<T>, IBdoDepot where T : IIdentifiedDataItem
    {
    }
}