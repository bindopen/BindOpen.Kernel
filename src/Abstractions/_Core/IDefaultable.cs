namespace BindOpen.Kernel
{
    /// <summary>
    /// This interface represents an object that can be default.
    /// </summary>
    public interface IDefaultable
    {
        /// <summary>
        /// Indicates whether this object is default.
        /// </summary>
        bool IsDefault { get; set; }
    }
}
