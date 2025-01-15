namespace BindOpen.Data.Schema;

/// <summary>
/// This static class extends a schema rule.
/// </summary>
public static partial class IBdoSchemaRuleExtensions
{
    /// <summary>
    /// Sets the result code of the specified object.
    /// </summary>
    /// <typeparam name="T">The IBdoSchemaRule type to consider.</typeparam>
    /// <param name="obj">The object to consider.</param>
    /// <param name="resultCode">The result code to set.</param>
    /// <returns>Returns the object.</returns>
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
    /// Sets the value of the specified object.
    /// </summary>
    /// <typeparam name="T">The IBdoSchemaRule type to consider.</typeparam>
    /// <param name="obj">The object to consider.</param>
    /// <param name="value">The value to set.</param>
    /// <returns>Returns the object.</returns>
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
    /// Sets the schema rule kind of the specified object.
    /// </summary>
    /// <typeparam name="T">The IBdoSchemaRule type to consider.</typeparam>
    /// <param name="obj">The object to consider.</param>
    /// <param name="kind">The schema rule kind to set.</param>
    /// <returns>Returns the object.</returns>
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
