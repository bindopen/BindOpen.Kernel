namespace BindOpen.Framework.Core.Data.Items
{
    /// <summary>
    /// This interface represents an indexed data item.
    /// </summary>
    public interface IIndexedDataItem
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// The index of this instance.
        /// </summary>
        int Index
        {
            get;
            set;
        }

        #endregion
    }
}
