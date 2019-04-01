using System;

namespace BindOpen.Framework.Core.Data.Items
{
    /// <summary>
    /// This interface represents an indexed data item.
    /// </summary>
    public interface IStoredDataItem : IIdentifiedDataItem
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// Creation date of this instance.
        /// </summary>
        String CreationDate
        {
            get;
            set;
        }

        /// <summary>
        /// Last modification date of this instance.
        /// </summary>
        String LastModificationDate
        {
            get;
            set;
        }

        /// <summary>
        /// Indicates whether this instance is locked.
        /// </summary>
        bool IsLocked
        {
            get;
            set;
        }

        #endregion
    }
}
