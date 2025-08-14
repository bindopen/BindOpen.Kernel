using BindOpen.Data.Conditions;

namespace BindOpen.Data;

/// <summary>
/// This static class provides methods to handle conditions.
/// </summary>
public static class IBdoCompositeConditionExtensions
{
    public static T WithCompositionKind<T>(
        this T obj,
        BdoCompositeConditionKind kind)
        where T : IBdoCompositeCondition
    {
        if (obj != null)
        {
            obj.CompositionKind = kind;
        }

        return obj;
    }
}