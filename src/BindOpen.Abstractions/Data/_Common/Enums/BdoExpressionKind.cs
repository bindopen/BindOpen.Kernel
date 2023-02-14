namespace BindOpen.Data
{
    /// <summary>
    /// This enumeration represents the possible kinds for data expression.
    /// </summary>
    public enum BdoExpressionKind
    {
        /// <summary>
        /// Any.
        /// </summary>
        Any,

        /// <summary>
        /// Literal expression that can be script if it begins by "=".
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
        /// Script word is considered as default.
        /// </summary>
        Word
    }

}