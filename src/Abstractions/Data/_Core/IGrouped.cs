namespace BindOpen.Kernel.Data
{
    /// <summary>
    /// This interface represents an object that can be grouped.
    /// </summary>
    public interface IGrouped
    {
        /// <summary>
        /// The group identifier.
        /// </summary>
        string GroupId { get; set; }
    }
}
