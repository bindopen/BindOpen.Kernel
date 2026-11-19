using BindOpen.Data.Conditions;

namespace BindOpen.Data;

/// <summary>
/// Provides extension methods for IBdoCompositeCondition to facilitate the fluent 
/// definition of logical groups.
/// </summary>
public static class IBdoCompositeConditionExtensions
{
    /// <summary>
    /// Sets the composition kind (AND, OR, etc.) for the specified composite condition.
    /// </summary>
    /// <typeparam name="T">The type implementing IBdoCompositeCondition.</typeparam>
    /// <param name="obj">The composite condition instance to update.</param>
    /// <param name="kind">The logical composition kind to apply.</param>
    /// <returns>Returns the updated condition instance for fluent chaining.</returns>
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