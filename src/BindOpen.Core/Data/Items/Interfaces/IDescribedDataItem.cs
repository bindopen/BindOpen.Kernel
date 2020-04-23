using BindOpen.Data.Common;

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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        IDescribedDataItem AddDescription(string text);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="text"></param>
        IDescribedDataItem AddDescription(string key, string text);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="text"></param>
        IDescribedDataItem WithDescription(string key = "*", string text = "*");

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        IDescribedDataItem WithDescription(string text);
    }
}
