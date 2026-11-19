namespace BindOpen.Data
{
    /// <summary>
    /// This interface defines an object that can be grouped.
    /// </summary>
    public interface IGrouped
    {
        /// <summary>
        /// The group identifier.
        /// </summary>
        string GroupId { get; set; }
    }
}
