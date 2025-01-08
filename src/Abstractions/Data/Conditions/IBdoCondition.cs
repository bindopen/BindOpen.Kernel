namespace BindOpen.Data.Conditions;

/// <summary>
/// This interface defines a condition.
/// </summary>
public interface IBdoCondition : IIdentified,
    INamed, IReferenced, IBdoObjectNotMetable, ITChild<IBdoCompositeCondition>
{
}