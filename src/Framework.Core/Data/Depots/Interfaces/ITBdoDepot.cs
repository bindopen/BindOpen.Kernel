using BindOpen.Framework.Core.Data.Items;

namespace BindOpen.Framework.Core.Data.Depots
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ITBdoDepot<T> : IDataItemSet<T>, IBdoDepot where T : IdentifiedDataItem
    {
    }
}