namespace BindOpen.Data;

/// <summary>
/// This enumerates the possible kinds of composite conditions.
/// Used by IBdoCompositeCondition to determine how to evaluate its children.
/// </summary>
public enum BdoCompositeConditionKind
{
    /// <summary>
    /// The condition is true only if all child conditions evaluate to true.
    /// </summary>
    And,

    /// <summary>
    /// The condition is true if at least one child condition evaluates to true.
    /// </summary>
    Or
}