using BindOpen.Data.Common;

namespace BindOpen.Data.Items
{
    /// <summary>
    /// This interface represents an titled data item.
    /// </summary>
    public interface ITitledDataItem : INamedDataItem, IGloballyTitled
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="updateBaseObject"></param>
        void Update(ITitledDataItem updateBaseObject);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        ITitledDataItem AddTitle(string text);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="text"></param>
        ITitledDataItem AddTitle(string key, string text);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="text"></param>
        ITitledDataItem WithTitle(string key = "*", string text = "*");

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        ITitledDataItem WithTitle(string text);
    }
}
