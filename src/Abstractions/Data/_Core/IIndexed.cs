namespace BindOpen.Data
{
    /// <summary>
    /// This interface represents an indexed object.
    /// </summary>
    public interface IIndexed
    {
        /// <summary>
        /// The index of this object.
        /// </summary>
        int? Index { get; set; }
    }
}
