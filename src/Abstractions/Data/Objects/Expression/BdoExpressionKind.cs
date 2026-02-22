namespace BindOpen.Data;

/// <summary>
/// Specifies the possible kinds of expressions within the BindOpen framework.
/// </summary>
public enum BdoExpressionKind
{
    /// <summary>
    /// Any kind of expression. Used for filtering or broad definitions.
    /// </summary>
    Any,

    /// <summary>
    /// Automatic expression. Typically a literal string that may contain 
    /// embedded scripts within curly braces (e.g., "Hello {{user.name}}").
    /// </summary>
    Auto,

    /// <summary>
    /// Pure literal expression. The content is treated as raw text and is not parsed for scripts.
    /// </summary>
    Literal,

    /// <summary>
    /// Script expression. The content is treated as a functional script or instruction 
    /// to be executed by the interpreter.
    /// </summary>
    Script,

    /// <summary>
    /// A single word expression. Often used for keyword-based resolution or simple identifiers.
    /// </summary>
    Word,

    /// <summary>
    /// No specific expression kind. Used for base script words or undefined states.
    /// </summary>
    None
}