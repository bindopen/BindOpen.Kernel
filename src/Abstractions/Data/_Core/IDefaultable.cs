namespace BindOpen.System.Data
{
    /// <summary>
    /// This interface represents a described data.
    /// </summary>
    public interface IDefaultable
    {
        /// <summary>
        /// The description of this instance.
        /// </summary>
        bool IsDefault { get; set; }
    }
}
