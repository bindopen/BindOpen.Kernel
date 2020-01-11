using BindOpen.Framework.Data.Items;

namespace BindOpen.Framework.Data.Items
{
    /// <summary>
    /// This interface represents an identified data item.
    /// </summary>
    public interface IIdentifiedDataItem : IDataItem, IIdentified, IReferenced
    {
    }
}
