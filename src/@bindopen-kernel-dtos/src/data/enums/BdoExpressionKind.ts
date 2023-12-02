/// <summary>
/// This enumeration represents the possible kinds of expression.
/// </summary>
export enum BdoExpressionKind
{
    /// <summary>
    /// Literal expression that can include scripts embedded in braces.
    /// </summary>
    Auto = "Auto",

    /// <summary>
    /// Literal expression is considered as default. A literal mode can then be applied.
    /// </summary>
    Literal = "Literal",

    /// <summary>
    /// Script expression is considered as default.
    /// </summary>
    Script = "Script",

    /// <summary>
    /// Word.
    /// </summary>
    Word = "Word",

    /// <summary>
    /// None. Such as script word.
    /// </summary>
    None = "None",

    /// <summary>
    /// Any.
    /// </summary>
    Any = "Any"
}
