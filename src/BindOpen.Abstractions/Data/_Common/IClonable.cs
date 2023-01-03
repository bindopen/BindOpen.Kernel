namespace BindOpen.Data
{
    /// <summary>
    /// 
    /// </summary>
    public interface IClonable
    {
        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <param name="areas">The areas to consider.</param>
        /// <returns>Returns a cloned instance.</returns>
        object Clone(params string[] areas);

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <param name="areas">The areas to consider.</param>
        /// <returns>Returns a cloned instance.</returns>
        T Clone<T>(params string[] areas) where T : class;
    }
}