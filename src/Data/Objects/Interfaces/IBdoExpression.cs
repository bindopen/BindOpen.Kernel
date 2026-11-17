namespace BindOpen.Data.Objects.Interfaces;

/// <summary>
/// Defines a data expression within the BindOpen framework.
/// An expression represents a text-based instruction or value that can be 
/// parsed or executed based on its specific kind.
/// </summary>
public interface IBdoExpression : IBdoObjectNotMetable, IIdentified, IReferenced
{
    /// <summary>
    /// Gets or sets the kind of this expression.
    /// This determines how the expression text will be interpreted (e.g., as a script, a literal, etc.).
    /// </summary>
    BdoExpressionKind ExpressionKind { get; set; }

    /// <summary>
    /// Gets or sets the raw text of the expression.
    /// </summary>
    string Text { get; set; }
}