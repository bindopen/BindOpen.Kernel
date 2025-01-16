using BindOpen.Data.Conditions;
using BindOpen.Data.Meta;
using System;

namespace BindOpen.Data.Schema;

/// <summary>
/// This class represents a data element set.
/// </summary>
public static partial class IBdoSchemaExtensions
{
    public static ITBdoGroupsOf<IBdoSchemaRule> GetOrNewRuleSet(this IBdoSchema schema)
    {
        return schema.RuleSet ??= BdoData.NewGroupsOf<IBdoSchemaRule>();
    }

    public static T WithRules<T>(this T schema, params IBdoSchemaRule[] rules) where T : IBdoSchema
    {
        schema?.GetOrNewRuleSet().With(rules);

        return schema;
    }

    public static IBdoSchemaSet GetOrNewItemSet(this IBdoSchema schema)
    {
        return schema.ItemSet ??= BdoData.NewSchemaSet();
    }

    public static T WithItems<T>(this T schema, params IBdoSchema[] items) where T : IBdoSchema
    {
        schema?.GetOrNewItemSet().With(items);

        return schema;
    }

    // Requirement

    /// <summary>
    /// 
    /// </summary>
    /// <param key="level"></param>
    public static T WithRequirement<T>(
        this T schema,
        params (RequirementLevels Level, IBdoCondition Condition)[] rules)
        where T : IBdoSchema
    {
        if (schema != null)
        {
            schema.GetOrNewRuleSet().RemoveOfGroup(BdoMetaDataProperties.RequirementLevel);

            foreach (var (Level, Condition) in rules)
            {
                schema.AddRequirement(Level, Condition);
            }
        }

        return schema;
    }

    public static T AddRequirement<T>(
        this T schema,
        RequirementLevels level,
        IBdoCondition condition = null)
        where T : IBdoSchema
    {
        if (schema != null)
        {
            BdoSchemaRule rule = (BdoMetaDataProperties.RequirementLevel, level, condition);
            schema.GetOrNewRuleSet().Insert(rule);
        }

        return schema;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public static T AsOptional<T>(
        this T schema,
        IBdoCondition condition = null)
        where T : IBdoSchema
    {
        schema?.AddRequirement(RequirementLevels.Optional, condition);

        return schema;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public static T AsRequired<T>(
        this T schema,
        IBdoCondition condition = null)
        where T : IBdoSchema
    {
        schema?.AddRequirement(RequirementLevels.Required, condition);

        return schema;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public static T AsForbidden<T>(
        this T schema,
        IBdoCondition condition = null)
        where T : IBdoSchema
    {
        schema?.AddRequirement(RequirementLevels.Forbidden, condition);

        return schema;
    }

    public static T WithItemRequirement<T>(
        this T schema,
        params (RequirementLevels Level, IBdoCondition Condition)[] rules)
        where T : IBdoSchema
    {
        if (schema != null)
        {
            schema.GetOrNewRuleSet().RemoveOfGroup(BdoMetaDataProperties.ItemRequirementLevel);

            foreach (var (Level, Condition) in rules)
            {
                schema.AddItemRequirement(Level, Condition);
            }
        }

        return schema;
    }

    public static T AddItemRequirement<T>(
        this T schema,
        RequirementLevels level,
        IBdoCondition condition = null)
        where T : IBdoSchema
    {
        if (schema != null)
        {
            BdoSchemaRule rule = (BdoMetaDataProperties.ItemRequirementLevel, level, condition);
            schema.GetOrNewRuleSet().Insert(rule);
        }

        return schema;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public static T AsItemOptional<T>(
        this T schema,
        IBdoCondition condition = null)
        where T : IBdoSchema
    {
        schema?.AddItemRequirement(RequirementLevels.Optional, condition);

        return schema;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public static T AsItemRequired<T>(
        this T schema,
        IBdoCondition condition = null)
        where T : IBdoSchema
    {
        schema?.AddItemRequirement(RequirementLevels.Required, condition);

        return schema;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public static T AsItemForbidden<T>(
        this T schema,
        IBdoCondition condition = null)
        where T : IBdoSchema
    {
        schema?.AddItemRequirement(RequirementLevels.Forbidden, condition);
        return schema;
    }

    public static IBdoSchema MustBeInList<T>(this IBdoSchema schema)
        where T : struct, IConvertible
    {
        return schema.MustBeInList<IBdoSchema, T>();
    }

    public static TSpec MustBeInList<TSpec, T>(this TSpec schema)
        where TSpec : IBdoSchema
        where T : struct, IConvertible
    {
        var set = schema.GetOrNewRuleSet();
        set.RemoveOfGroup(BdoMetaDataProperties.Value);
        set.Add(
            BdoData.NewConstraint(
                BdoMetaDataProperties.Value,
                null,
                BdoData.NewCondition(@"$inEnum($(this).value(), '" + typeof(RequirementLevels).AssemblyQualifiedName + @"'"))
        );

        return schema;
    }
}
