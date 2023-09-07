namespace BindOpen.System.Data
{
    /// <summary>
    /// This instance represents a clonable object.
    /// </summary>
    public interface IClonable
    {
        /// <summary>
        /// Clones this object.
        /// </summary>
        /// <returns>Returns the cloned object.</returns>
        object Clone();

        /// <summary>
        /// Clones this object by specifying the class of the cloned object.
        /// </summary>
        /// <returns>Returns the cloned object.</returns>
        T Clone<T>() where T : class;
    }
}