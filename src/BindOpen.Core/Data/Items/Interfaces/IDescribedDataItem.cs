using BindOpen.Data.Items;

namespace BindOpen.Data.Items
{
    /// <summary>
    /// This interface represents an described data item.
    /// </summary>
    public interface IDescribedDataItem : ITitledDataItem, IGloballyDescribed
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="updateBaseObject"></param>
        void Update(IDescribedDataItem updateBaseObject);
    }
}
