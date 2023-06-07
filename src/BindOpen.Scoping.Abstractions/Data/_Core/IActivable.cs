namespace BindOpen.Scoping.Data
{
    /// <summary>
    /// This interface represents a data with detail.
    /// </summary>
    public interface IActivable
    {
        /// <summary>
        /// Indicates whether it is active.
        /// </summary>
        bool IsActive { get; set; }
    }
}
