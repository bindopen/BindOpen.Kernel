namespace BindOpen.Data;

/// <summary>
/// Enumerates the possible kinds of conditions within the BindOpen framework.
/// This allows the interpreter to differentiate between static comparisons, 
/// logical groups, and dynamic scripts.
/// </summary>
public enum BdoConditionKind
{
    /// <summary>
    /// A binary comparison between two metadata arguments (e.g., A == B).
    /// </summary>
    Basic,

    /// <summary>
    /// A group of conditions aggregated by a logical gate (AND, OR).
    /// </summary>
    Composite,

    /// <summary>
    /// A condition evaluated through a dynamic script or expression.
    /// </summary>
    Expression,

    /// <summary>
    /// No specific condition kind. Used for simple script words or placeholders.
    /// </summary>
    None,

    /// <summary>
    /// Represents any condition kind. Primarily used for filtering or searching.
    /// </summary>
    Any
}