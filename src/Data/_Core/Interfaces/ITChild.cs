namespace BindOpen.Data
{
    /// <summary>
    /// This interface defines a child object.
    /// </summary>
    public interface ITChild<T> : IReferenced where T : IReferenced
    {
        /// <summary>
        /// The parent of this object.
        /// </summary>
        T Parent { get; set; }
    }
}