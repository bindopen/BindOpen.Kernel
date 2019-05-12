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

        /// <summary>
        /// Locks this instance.
        /// </summary>
        /// <param name="isRecursive">Indicates whether the protection is applied to sub objects.</param>
        void Lock(bool isRecursive = true);

        /// <summary>
        /// Unlocks this instance.
        /// </summary>
        /// <param name="isRecursive">Indicates whether the protection is applied to sub objects.</param>
        void Unlock(bool isRecursive = true);
    }
}
