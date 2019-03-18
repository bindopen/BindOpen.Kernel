using System;

namespace BindOpen.Framework.Core.Data.Items
{
    /// <summary>
    /// This interface represents an identified data item.
    /// </summary>
    public interface IIdentifiedDataItem
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// ID of this instance.
        /// </summary>
        String Id
        {
            get;
            set;
        }

        #endregion       
    }
}
