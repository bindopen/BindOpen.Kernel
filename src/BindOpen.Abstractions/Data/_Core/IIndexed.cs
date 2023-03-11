namespace BindOpen.Data
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
