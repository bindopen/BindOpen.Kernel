using BindOpen.Data.Conditions;
using System;

namespace BindOpen.Data.Schema;

/// <summary>
/// This class represents a data element set.
/// </summary>
public static class IBdoSchematizedExtensions
{
    public static T WithSchema<T, TSpec>(
        this T obj,
        Action<IBdoSchema> action)
        where T : IBdoSchematized
        where TSpec : IBdoSchema, new()
    {
        if (obj != null)
        {
            obj.Schema ??= BdoData.NewSchema<TSpec>();
            action?.Invoke(obj.Schema);
        }

        return obj;
    }

    public static T WithSchema<T>(
        this T obj,
        Action<IBdoSchema> action)
        where T : IBdoSchematized
        => obj.WithSchema<T, BdoSchema>(action);

    public static T WithSchemaRules<T>(
        this T obj,
        params IBdoSchemaRule[] rules)
        where T : IBdoSchematized
    {
        if (obj != null)
        {
            obj.Schema = BdoData.NewSchema()
                .WithRules(rules);
        }

        return obj;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param key="level"></param>
    public static T WithSchemaRuleRequirements<T>(
        this T obj,
        params (string Reference, object Value, IBdoCondition Condition)[] rules)
        where T : IBdoSchematized
    {
        if (obj != null)
        {
            obj.Schema = BdoData.NewSchema();

            foreach (var (Reference, Value, Condition) in rules)
            {
                obj.AddSchemaRuleRequirement(Reference, Value, Condition);
            }
        }

        return obj;
    }

    public static T AddSchemaRule<T>(
        this T obj,
        IBdoSchemaRule rule)
        where T : IBdoSchematized
    {
        if (obj != null)
        {
            obj.Schema ??= BdoData.NewSchema();
            obj.Schema.GetOrNewRuleSet().Add(rule);
        }

        return obj;
    }

    public static T AddSchemaRuleRequirement<T>(
        this T obj,
        string groupId,
        object value,
        IBdoCondition condition = null)
        where T : IBdoSchematized
    {
        return obj.AddSchemaRule((BdoSchemaRule)(groupId, value, condition));
    }
}
