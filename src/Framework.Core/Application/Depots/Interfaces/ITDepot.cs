using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.Data.Items.Sets;

namespace BindOpen.Framework.Core.Application.Depots
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ITDepot<T> : IDataItemSet<T>, IDepot where T : IdentifiedDataItem
    {
    }
}