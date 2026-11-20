namespace BindOpen.Data
{
    /// <summary>
    /// This interface defines a parent object.
    /// </summary>
    public interface ITSingleChildParent<T> : IReferenced where T : IReferenced
    {
        /// <summary>
        /// The child of this object.
        /// </summary>
        T Child { get; set; }
    }
}