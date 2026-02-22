namespace BindOpen.Data.Schema;

/// <summary>
/// Provides extension methods for IBdoSchemaRule to facilitate fluent configuration 
/// of validation logic and reporting.
/// </summary>
public static partial class IBdoSchemaRuleExtensions
{
    /// <summary>
    /// Sets the result code of the specified schema rule.
    /// </summary>
    /// <typeparam name="T">The type of schema rule.</typeparam>
    /// <param name="obj">The schema rule to update.</param>
    /// <param name="resultCode">The result code to assign (e.g., from BdoSchemaRuleResultCodes).</param>
    /// <returns>Returns the updated schema rule instance.</returns>
    public static T WithResultCode<T>(
        this T obj,
        string resultCode)
        where T : IBdoSchemaRule
    {
        if (obj != null)
        {
            obj.ResultCode = resultCode;
        }

        return obj;
    }

    /// <summary>
    /// Sets the evaluation value of the specified schema rule.
    /// </summary>
    /// <typeparam name="T">The type of schema rule.</typeparam>
    /// <param name="obj">The schema rule to update.</param>
    /// <param name="value">The value to use for validation (e.g., a regex, a range, or an expression).</param>
    /// <returns>Returns the updated schema rule instance.</returns>
    public static T WithValue<T>(
        this T obj,
        object value)
        where T : IBdoSchemaRule
    {
        if (obj != null)
        {
            obj.Value = value;
        }

        return obj;
    }

    /// <summary>
    /// Sets the kind of the specified schema rule.
    /// </summary>
    /// <typeparam name="T">The type of schema rule.</typeparam>
    /// <param name="obj">The schema rule to update.</param>
    /// <param name="kind">The rule kind (Requirement or Constraint).</param>
    /// <returns>Returns the updated schema rule instance.</returns>
    public static T WithRuleKind<T>(
        this T obj,
        BdoSchemaRuleKinds kind)
        where T : IBdoSchemaRule
    {
        if (obj != null)
        {
            obj.Kind = kind;
        }

        return obj;
    }
}