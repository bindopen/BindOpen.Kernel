using BindOpen.Data.Objects;

namespace BindOpen.Data.Conditions;

/// <summary>
/// Defines a condition that is evaluated based on a dynamic expression.
/// This allows for runtime script evaluation to determine logical truth.
/// </summary>
public interface IBdoExpressionCondition : IBdoCondition
{
    /// <summary>
    /// Gets or sets the dynamic expression to be evaluated.
    /// The result of this expression determines if the condition is met.
    /// </summary>
    IBdoExpression Expression { get; set; }
}