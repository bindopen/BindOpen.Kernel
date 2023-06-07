namespace BindOpen.Scoping.Data
{
    /// <summary>
    /// This enumeration represents the possible kinds of expressions.
    /// </summary>
    public enum BdoExpressionKind
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
        /// Literal expression that can include scripts embedded in braces.
        /// </summary>
        Auto,

        /// <summary>
        /// Literal expression is considered as default. A literal mode can then be applied.
        /// </summary>
        Literal,

        /// <summary>
        /// Script expression is considered as default.
        /// </summary>
        Script
    }

}