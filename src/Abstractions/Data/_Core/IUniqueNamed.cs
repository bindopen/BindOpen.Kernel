namespace BindOpen.System.Data
{
    /// <summary>
    /// This interface represents an unique data.
    /// </summary>
    public interface IUniqueNamed
    {
        /// <summary>
        /// Unique name of this instance.
        /// </summary>
        string UniqueName { get; set; }
    }
}
