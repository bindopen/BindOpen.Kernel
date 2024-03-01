namespace BindOpen.Data
{
    /// <summary>
    /// This interface defines an unique object.
    /// </summary>
    public interface IUniqueNamed
    {
        /// <summary>
        /// The unique name of this instance.
        /// </summary>
        string UniqueName { get; set; }
    }
}
