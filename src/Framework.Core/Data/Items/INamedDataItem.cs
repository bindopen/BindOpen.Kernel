using System;

namespace BindOpen.Framework.Core.Data.Items
{
    /// <summary>
    /// This interface represents a named data item.
    /// </summary>
    public interface INamedDataItem
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// The name of this instance.
        /// </summary>
        String Name
        {
            get;
            set;
        }

        #endregion
    }
}
