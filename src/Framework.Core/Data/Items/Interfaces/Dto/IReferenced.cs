namespace BindOpen.Framework.Core.Data.Items.Dto
{
    /// <summary>
    /// This interface represents an referenced DTO.
    /// </summary>
    public interface IReferenced
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// Gets the key of the item.
        /// </summary>
        /// <returns>Returns the key of the item.</returns>
        string Key();

        #endregion
    }
}
