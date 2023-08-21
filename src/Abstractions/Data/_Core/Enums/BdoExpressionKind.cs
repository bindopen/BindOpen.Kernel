namespace BindOpen.System.Data
{
    /// <summary>
    /// This enumeration represents the possible kinds of expression.
    /// </summary>
    public enum BdoExpressionKind
    {
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
        Script,

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