using System.Collections.Generic;

namespace BindOpen.Data.Conditions;

/// <summary>
/// This interface defines a composite condition.
/// </summary>
public interface IBdoCompositeCondition : IBdoCondition, ITParent<IBdoCondition>
{
    /// <summary>
    /// The kind of composition.
    /// </summary>
    BdoCompositeConditionKind CompositionKind { get; set; }

    /// <summary>
    /// The conditions.
    /// </summary>
    IList<IBdoCondition> Conditions { get; set; }
}