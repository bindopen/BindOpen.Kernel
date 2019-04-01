using BindOpen.Framework.Core.Data.Items;

namespace BindOpen.Framework.Core.Data.References
{
    public interface ITItemReference<T> : IDataItem where T : IDataItem
    {
        T ObjectItem { get; set; }
    }
}