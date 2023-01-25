namespace BindOpen.Data
{
    /// <summary>
    /// This interface represents an unique data.
    /// </summary>
    public interface IUnique
    {
        /// <summary>
        /// Unique name of this instance.
        /// </summary>
        string UniqueId { get; set; }
    }
}
