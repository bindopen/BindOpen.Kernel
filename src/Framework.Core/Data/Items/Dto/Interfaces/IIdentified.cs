namespace BindOpen.Framework.Data.Items
{
    /// <summary>
    /// This interface represents an identified DTO.
    /// </summary>
    public interface IIdentified
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// ID of this instance.
        /// </summary>
        string Id { get; set; }

        #endregion
    }
}
