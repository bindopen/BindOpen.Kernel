namespace BindOpen.Data
{
    /// <summary>
    /// 
    /// </summary>
    public interface ITChildClonable<TParent>
    {
        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <param key="areas">The areas to consider.</param>
        /// <returns>Returns a cloned instance.</returns>
        TParent Clone(TParent parent, params string[] areas);

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <param key="areas">The areas to consider.</param>
        /// <returns>Returns a cloned instance.</returns>
        TChild Clone<TChild>(TParent parent, params string[] areas);
    }
}