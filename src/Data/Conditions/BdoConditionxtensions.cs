using BindOpen.Data.Conditions;

namespace BindOpen.Data;

/// <summary>
/// This static class provides methods to handle conditions.
/// </summary>
public static class BdoConditionxtensions
{
    public static T WithChildren<T>(
        this T obj,
        params IBdoCondition[] conditions)
        where T : IBdoCompositeCondition
    {
        if (obj != null)
        {
            obj._Children = new TBdoSet<IBdoCondition>(conditions);
        }

        return obj;
    }

}