namespace BindOpen.Data
{
    /// <summary>
    /// This interface defines a lockable data.
    /// </summary>
    public interface ILockable
    {
        /// <summary>
        /// Indicates whether this instance is locked.
        /// </summary>
        bool IsLocked
        {
            get;
        }

        /// <summary>
        /// Locks this instance.
        /// </summary>
        /// <param name="isRecursive">Indicates whether the protection is applied to sub objects.</param>
        ILockable Lock(bool isRecursive = true);

        /// <summary>
        /// Unlocks this instance.
        /// </summary>
        /// <param name="isRecursive">Indicates whether the protection is applied to sub objects.</param>
        ILockable Unlock(bool isRecursive = true);
    }
}
