namespace BindOpen.Framework.Core.Data.Items.Dto
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
