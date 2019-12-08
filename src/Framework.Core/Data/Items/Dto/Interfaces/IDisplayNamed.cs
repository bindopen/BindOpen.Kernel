namespace BindOpen.Framework.Core.Data.Items
{
    /// <summary>
    /// This interface represents a display named DTO.
    /// </summary>
    public interface IDisplayNamed
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// The display name of this instance.
        /// </summary>
        string DisplayName
        {
            get;
            set;
        }

        #endregion
    }
}
