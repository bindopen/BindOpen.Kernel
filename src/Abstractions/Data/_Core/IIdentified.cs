namespace BindOpen.Kernel.Data
{
    /// <summary>
    /// This interface represents an identified object.
    /// </summary>
    public interface IIdentified
    {
        /// <summary>
        /// ID of this object.
        /// </summary>
        string Id { get; set; }
    }
}
