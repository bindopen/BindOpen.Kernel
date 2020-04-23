using BindOpen.Data.Items;

namespace BindOpen.Data.Items
{
    /// <summary>
    /// This interface represents an identified data item.
    /// </summary>
    public interface IIdentifiedDataItem : IDataItem, IIdentified, IReferenced
    {
    }
}
