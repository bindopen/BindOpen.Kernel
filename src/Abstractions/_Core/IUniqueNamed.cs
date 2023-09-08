namespace BindOpen.Kernel
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
