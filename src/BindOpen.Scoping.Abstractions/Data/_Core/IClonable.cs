namespace BindOpen.Scoping.Data
{
    /// <summary>
    /// 
    /// </summary>
    public interface IClonable
    {
        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <param key="areas">The areas to consider.</param>
        /// <returns>Returns a cloned instance.</returns>
        object Clone();

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <param key="areas">The areas to consider.</param>
        /// <returns>Returns a cloned instance.</returns>
        T Clone<T>() where T : class;
    }
}