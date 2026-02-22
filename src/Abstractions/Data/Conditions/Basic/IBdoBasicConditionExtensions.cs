using BindOpen.Data.Conditions;
using BindOpen.Data.Meta;

namespace BindOpen.Data;

/// <summary>
/// Provides extension methods for IBdoBasicCondition to facilitate fluent construction 
/// of logical comparison units.
/// </summary>
public static class IBdoBasicConditionExtensions
{
    /// <summary>
    /// Sets the first argument of the condition.
    /// </summary>
    /// <typeparam name="T">The type implementing IBdoBasicCondition.</typeparam>
    /// <param name="obj">The condition instance.</param>
    /// <param name="argument1">The metadata to be used as the first argument.</param>
    /// <returns>Returns the updated condition instance.</returns>
    public static T WithArgument1<T>(
        this T obj,
        IBdoMetaData argument1)
        where T : IBdoBasicCondition
    {
        if (obj != null)
        {
            obj.Argument1 = argument1;
        }

        return obj;
    }

    /// <summary>
    /// Sets the second argument of the condition.
    /// </summary>
    /// <typeparam name="T">The type implementing IBdoBasicCondition.</typeparam>
    /// <param name="obj">The condition instance.</param>
    /// <param name="argument2">The metadata to be used as the second argument.</param>
    /// <returns>Returns the updated condition instance.</returns>
    public static T WithArgument2<T>(
        this T obj,
        IBdoMetaData argument2)
        where T : IBdoBasicCondition
    {
        if (obj != null)
        {
            obj.Argument2 = argument2;
        }

        return obj;
    }

    /// <summary>
    /// Sets the comparison operator for the condition.
    /// </summary>
    /// <typeparam name="T">The type implementing IBdoBasicCondition.</typeparam>
    /// <param name="obj">The condition instance.</param>
    /// <param name="op">The logical operator to apply.</param>
    /// <returns>Returns the updated condition instance.</returns>
    public static T WithOperator<T>(
        this T obj,
        DataOperators op)
        where T : IBdoBasicCondition
    {
        if (obj != null)
        {
            obj.Operator = op;
        }

        return obj;
    }
}