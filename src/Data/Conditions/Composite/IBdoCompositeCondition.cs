namespace BindOpen.Data.Conditions;

/// <summary>
/// Defines a composite condition that aggregates multiple conditions 
/// using a specific logical composition kind (AND, OR, etc.).
/// </summary>
public interface IBdoCompositeCondition : IBdoCondition, ITParent<IBdoCondition>
{
    /// <summary>
    /// Gets or sets the logical operator used to aggregate child conditions 
    /// (e.g., And, Or, Not, Xor).
    /// </summary>
    BdoCompositeConditionKind CompositionKind { get; set; }
}