namespace BindOpen.Data
{
    /// <summary>
    /// 
    /// </summary>
    public interface ITChildClonable<TChild, TParent>
    {
        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <param key="areas">The areas to consider.</param>
        /// <returns>Returns a cloned instance.</returns>
        TChild Clone(TParent parent, params string[] areas);
    }
}