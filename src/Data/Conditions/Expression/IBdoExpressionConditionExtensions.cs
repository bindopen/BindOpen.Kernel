using BindOpen.Data.Conditions;
using BindOpen.Data.Objects.Interfaces;

namespace BindOpen.Data;

/// <summary>
/// Provides extension methods for IBdoExpressionCondition to facilitate 
/// the fluent assignment of dynamic scripts to logical conditions.
/// </summary>
public static class IBdoExpressionConditionExtensions
{
    /// <summary>
    /// Sets the expression for the specified condition and returns the instance for chaining.
    /// </summary>
    /// <typeparam name="T">The type implementing IBdoExpressionCondition.</typeparam>
    /// <param name="obj">The expression condition instance.</param>
    /// <param name="exp">The dynamic expression to assign.</param>
    /// <returns>Returns the updated condition instance.</returns>
    public static T WithExpression<T>(
        this T obj,
        IBdoExpression exp)
        where T : IBdoExpressionCondition
    {
        if (obj != null)
        {
            obj.Expression = exp;
        }

        return obj;
    }
}