using BindOpen.Data.Common;

namespace BindOpen.Data.Items
{
    /// <summary>
    /// 
    /// </summary>
    public interface IIndexedDataItem : IDescribedDataItem, IIndexed
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        IIndexedDataItem WithIndex(int index);
    }
}