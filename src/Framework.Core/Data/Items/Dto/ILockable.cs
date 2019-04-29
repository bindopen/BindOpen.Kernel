namespace BindOpen.Framework.Core.Data.Items.Dto
{
    /// <summary>
    /// This interface defines a lockable data.
    /// </summary>
    public interface ILockable
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

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
