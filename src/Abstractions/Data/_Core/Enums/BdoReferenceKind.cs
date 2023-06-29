namespace BindOpen.System.Data
{
    /// <summary>
    /// This enumeration represents the possible kinds for data expression.
    /// </summary>
    public enum BdoReferenceKind
    {
        /// <summary>
        /// Any.
        /// </summary>
        Any,

        /// <summary>
        /// None. Such as script word.
        /// </summary>
        None,

        /// <summary>
        /// Expression.
        /// </summary>
        Expression,

        /// <summary>
        /// Word.
        /// </summary>
        Word,

        /// <summary>
        /// Target identifier.
        /// </summary>
        Identifier,

        /// <summary>
        /// Meta data.
        /// </summary>
        MetaData
    }

}