using BindOpen.Framework.Core.Data.Items.Dictionary;

namespace BindOpen.Framework.Core.Data.Items
{
    /// <summary>
    /// This interface represents an described data item.
    /// </summary>
    public interface IDescribedDataItem
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// Title of this instance.
        /// </summary>
        DictionaryDataItem Title
        {
            get;
            set;
        }

        /// <summary>
        /// Description of this instance.
        /// </summary>
        DictionaryDataItem Description
        {
            get;
            set;
        }

        #endregion
    }
}
