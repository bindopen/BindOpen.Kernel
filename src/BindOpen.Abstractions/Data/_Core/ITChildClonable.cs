namespace BindOpen.Data
{
    /// <summary>
    /// 
    /// </summary>
    public interface ITChildClonable<T>
    {
        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <param key="areas">The areas to consider.</param>
        /// <returns>Returns a cloned instance.</returns>
        T Clone(T parent, params string[] areas);
    }
}