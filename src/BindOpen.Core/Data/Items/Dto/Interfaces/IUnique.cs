namespace BindOpen.Data.Items
{
    /// <summary>
    /// This interface represents an unique DTO.
    /// </summary>
    public interface IUnique
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// Unique name of this instance.
        /// </summary>
        string UniqueId
        {
            get;
            set;
        }

        #endregion
    }
}
