using BindOpen.Data.Helpers;
using BindOpen.Data.Meta;
using BindOpen.Logging;
using BindOpen.Scoping;

namespace BindOpen.Data.Schema;

/// <summary>
/// This static class extends the data schema.
/// </summary>
public static partial class IBdoSchemaExtensions
{
    /// <summary>
    /// 
    /// </summary>
    /// <param key="aliases"></param>
    public static T WithAvailableDataModes<T>(
        this T schema,
        params DataMode[] modes)
        where T : IBdoSchema
    {
        if (schema != null)
        {
            schema.AvailableDataModes = modes;
        }
        return schema;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param key="aliases"></param>
    public static T WithAliases<T>(
        this T schema,
        params string[] aliases)
        where T : IBdoSchema
    {
        if (schema != null)
        {
            schema.Aliases = aliases;
        }
        return schema;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param key="item"></param>
    public static T WithDefaultData<T>(
        this T schema,
        object item)
        where T : IBdoSchema
    {
        if (schema != null)
        {
            if (item is IBdoReference reference)
            {
                schema.WithReference(reference);
            }
            else
            {
                schema.DefaultData = item;
            }
        }
        return schema;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param key="number"></param>
    public static T WithMaxDataItemNumber<T>(
        this T schema,
        uint? number = null)
        where T : IBdoSchema
    {
        if (schema != null)
        {
            schema.MaxDataItemNumber = number;
        }
        return schema;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param key="number"></param>
    public static T WithMinDataItemNumber<T>(
        this T schema,
        uint number)
        where T : IBdoSchema
    {
        if (schema != null)
        {
            schema.MinDataItemNumber = number;
        }
        return schema;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param key="level"></param>
    public static T WithAccessibilityLevel<T>(
        this T schema,
        AccessibilityLevels level)
        where T : IBdoSchema
    {
        if (schema != null)
        {
            schema.AccessibilityLevel = level;
        }

        return schema;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param key="level"></param>
    public static T WithInheritanceLevel<T>(
        this T schema,
        InheritanceLevels level)
        where T : IBdoSchema
    {
        if (schema != null)
        {
            schema.InheritanceLevel = level;
        }

        return schema;
    }

    /// <summary>
    /// 
    /// </summary>
    public static T WithLabel<T>(
        this T schema,
        string label)
        where T : IBdoSchema
    {
        if (schema != null)
        {
            schema.Label = label;
        }

        return schema;
    }

    /// <summary>
    /// 
    /// </summary>
    public static T WithLabel<T>(
        this T schema,
        LabelFormats label)
        where T : IBdoSchema
    {
        if (schema != null)
        {
            schema.Label = label.GetScript();
        }

        return schema;
    }

    public static T GetRuleValue<T>(
        this IBdoSchema schema,
        string groupId,
        BdoSchemaRuleKinds mode,
        IBdoScope scope = null,
        IBdoMetaSet varSet = null,
        IBdoLog log = null)
    {
        if (schema != null)
        {
            return schema.GetRuleValue(groupId, mode, scope, varSet, log).As<T>();
        }

        return default;
    }
}
