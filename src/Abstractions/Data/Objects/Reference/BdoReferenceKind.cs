namespace BindOpen.Data
{
    /// <summary>
    /// This enumeration represents the possible kinds for reference.
    /// </summary>
    public enum BdoReferenceKind
    {
        /// <summary>
        /// Expression.
        /// </summary>
        Expression,

        /// <summary>
        /// Target identifier.
        /// </summary>
        Identifier,

        /// <summary>
        /// Meta data.
        /// </summary>
        MetaData,

        /// <summary>
        /// None. Such as script word.
        /// </summary>
        None,

        /// <summary>
        /// Any.
        /// </summary>
        Any
    }

}