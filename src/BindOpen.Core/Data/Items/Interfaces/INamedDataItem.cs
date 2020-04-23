using BindOpen.Data.Common;

namespace BindOpen.Data.Items
{
    /// <summary>
    /// This interface represents a named data item.
    /// </summary>
    public interface INamedDataItem : IStoredDataItem, INamed
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        INamedDataItem WithName(string name);

    }
}
