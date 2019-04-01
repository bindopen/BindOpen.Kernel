using BindOpen.Framework.Core.Data.Items;

namespace BindOpen.Framework.Core.Data.References
{
    public interface ITBaseItemReference<T> : IDataItem where T : IDataItem
    {
        T Item { get; set; }
        IDataReference Reference { get; set; }
    }
}