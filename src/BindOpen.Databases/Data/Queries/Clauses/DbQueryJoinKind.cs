namespace BindOpen.Data.Queries
{
    /// <summary>
    /// This enumeration lists all the kinds of data query joins.
    /// </summary>
    public enum DbQueryJoinKind
    {
        /// <summary>
        /// None.
        /// </summary>
        None,

        /// <summary>
        /// Inner.
        /// </summary>
        Inner,

        /// <summary>
        /// Left.
        /// </summary>
        Left,

        /// <summary>
        /// Right.
        /// </summary>
        Right
    }
}