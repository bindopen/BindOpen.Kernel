using BindOpen.Data.Helpers;
using BindOpen.Data.Meta;
using BindOpen.Logging;
using BindOpen.Scoping;
using System.Collections.Generic;
using System.Linq;

namespace BindOpen.Data.Schema;

/// <summary>
/// 
/// </summary>
public static partial class IBdoSchemaSetExtensions
{
    /// <summary>
    /// Resolves the appropriate child schema from the set based on evaluated conditions.
    /// </summary>
    /// <param name="specSet">The schema set to search within.</param>
    /// <param name="scope">The scope used to evaluate conditions via the interpreter.</param>
    /// <param name="varSet">The set of variables available for condition resolution.</param>
    /// <param name="log">The log for tracking evaluation details.</param>
    /// <returns>
    /// Returns the first schema whose condition evaluates to true. 
    /// If no conditional schema matches, returns the first schema without a condition (the default).
    /// </returns>
    public static IBdoSchema Child(
        this IBdoSchemaSet specSet,
        IBdoScope scope,
        IBdoMetaSet varSet = null,
        IBdoLog log = null)
    {
        IBdoSchema schema = null;

        if (specSet != null)
        {
            // 1. Try to find the first schema where the condition is met
            schema = specSet.FirstOrDefault(
                q => scope?.Interpreter?.Evaluate(q?.Condition, varSet, log) == true);

            // 2. Fallback to the first schema that has no condition (default schema)
            schema ??= specSet.FirstOrDefault(q => q?.Condition == null);
        }

        return schema;
    }

    // Add

    /// <summary>
    /// Adds the specified item.
    /// </summary>
    /// <param key="items">The items of the item to add.</param>
    /// <returns>Returns the new item that has been added.
    /// Returns null if the new item is null or else its name is null.</returns>
    /// <remarks>The new item must have a name.</remarks>
    public static T With<T>(
       this T set,
       string key,
       DataValueTypes valueType)
       where T : IBdoSchemaSet
    {
        set.With((key, valueType));

        return set;
    }

    /// <summary>
    /// Adds a new value to this instance.
    /// </summary>
    /// <param key="pairs">The value to add.</param>
    public static T With<T>(
        this T set,
        params KeyValuePair<string, DataValueTypes>[] pairs)
        where T : IBdoSchemaSet
    {
        set.With(pairs.Select(q => (q.Key, q.Value)).ToArray());

        return set;
    }

    /// <summary>
    /// Adds a new value to this instance with the specified key and text.
    /// </summary>
    /// <param key="text">The text to consider.</param>
    /// <returns>Returns the added data key value.</returns>
    public static T With<T>(
        this T set,
        params (string Name, DataValueTypes ValueType)[] pairs)
        where T : IBdoSchemaSet
    {
        if (set != null)
        {
            foreach (var (Name, ValueType) in pairs)
            {
                set.AddItems(BdoData.NewSchema(Name, ValueType));
            }
        }

        return set;
    }

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