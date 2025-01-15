namespace BindOpen.Data.Conditions;

/// <summary>
/// This interface defines an expression condition.
/// </summary>
public interface IBdoExpressionCondition : IBdoCondition
{
    /// <summary>
    /// The expression.
    /// </summary>
    IBdoExpression Expression { get; set; }
}