namespace BindOpen.System.Data
{
    /// <summary>
    /// This interface represents an unique object.
    /// </summary>
    public interface IUniqueNamed
    {
        /// <summary>
        /// The unique name of this instance.
        /// </summary>
        string UniqueName { get; set; }
    }
}
