using BindOpen.Data.Common;

namespace BindOpen.Data.Items
{
    /// <summary>
    /// This interface represents an identified data item.
    /// </summary>
    public interface IIdentifiedDataItem : IDataItem, IIdentified, IReferenced
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        IIdentifiedDataItem WithId(string id);
    }
}
