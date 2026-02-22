using BindOpen.Data.Helpers;
using BindOpen.Data.Meta;
using BindOpen.Logging;
using BindOpen.Scoping;

namespace BindOpen.Data.Schema;

/// <summary>
/// Provides extension methods for IBdoSchema to facilitate fluent configuration 
/// of data blueprints and rule resolution.
/// </summary>
public static partial class IBdoSchemaExtensions
{
    /// <summary>
    /// Sets the available data modes for the schema.
    /// </summary>
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
    /// Adds alternative names (aliases) to the schema for backward compatibility.
    /// </summary>
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
    /// Sets the default data. If the item is a reference, it configures it as a dynamic source.
    /// </summary>
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
    /// Sets the maximum number of items allowed for this data element.
    /// </summary>
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
    /// Sets the minimum number of items required for this data element.
    /// </summary>
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
    /// Sets the accessibility level for the schema (e.g., Private, Public).
    /// </summary>
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
    /// Sets the inheritance behavior for the schema.
    /// </summary>
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
    /// Sets a custom string label for the schema.
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
    /// Sets a label using a specific format script.
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

    /// <summary>
    /// Retrieves and casts the value of a specific schema rule.
    /// </summary>
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
            var value = schema.GetRuleValue(groupId, mode, scope, varSet, log);
            return value.As<T>();
        }
        return default;
    }
}