namespace BindOpen.Framework.Data.Items
{
    /// <summary>
    /// This interface represents an indexed data item.
    /// </summary>
    public interface IIndexed
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
