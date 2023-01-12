namespace BindOpen.Meta
{
    /// <summary>
    /// This interface represents an indexed data.
    /// </summary>
    public interface IIndexed
    {
        /// <summary>
        /// The index of this instance.
        /// </summary>
        int? Index { get; set; }
    }
}
