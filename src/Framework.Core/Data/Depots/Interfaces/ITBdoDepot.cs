using BindOpen.Framework.Application.Scopes;
using BindOpen.Framework.Data.Items;

namespace BindOpen.Framework.Data.Depots
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ITBdoDepot<T> : IDataItemSet<T>, IBdoDepot where T : IdentifiedDataItem
    {
    }
}