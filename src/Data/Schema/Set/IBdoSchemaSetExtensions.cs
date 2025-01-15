using BindOpen.Data.Meta;
using System.Collections.Generic;
using System.Linq;

namespace BindOpen.Data.Schema;

/// <summary>
/// 
/// </summary>
public static partial class IBdoSchemaSetExtensions
{
    // Add

    /// <summary>
    /// Adds the specified item.
    /// </summary>
    /// <param key="items">The items of the item to add.</param>
    /// <returns>Returns the new item that has been added.
    /// Returns null if the new item is null or else its name is null.</returns>
    /// <remarks>The new item must have a name.</remarks>
    public static T Add<T>(
       this T set,
       string key,
       DataValueTypes valueType)
       where T : IBdoSchemaSet
    {
        set.Add((key, valueType));

        return set;
    }

    /// <summary>
    /// Adds a new value to this instance.
    /// </summary>
    /// <param key="pairs">The value to add.</param>
    public static T Add<T>(
        this T set,
        params KeyValuePair<string, DataValueTypes>[] pairs)
        where T : IBdoSchemaSet
    {
        set.Add(pairs.Select(q => (q.Key, q.Value)).ToArray());

        return set;
    }

    /// <summary>
    /// Adds a new value to this instance with the specified key and text.
    /// </summary>
    /// <param key="text">The text to consider.</param>
    /// <returns>Returns the added data key value.</returns>
    public static T Add<T>(
        this T set,
        params (string Name, DataValueTypes ValueType)[] pairs)
        where T : IBdoSchemaSet
    {
        if (set != null)
        {
            foreach (var (Name, ValueType) in pairs)
            {
                set.Add(BdoData.NewSchema(Name, ValueType));
            }
        }

        return set;
    }

    // With

    /// <summary>
    /// Withs a new value to this instance.
    /// </summary>
    /// <param key="pairs">The value to add.</param>
    public static T With<T>(
        this T set,
        params KeyValuePair<string, DataValueTypes>[] pairs)
        where T : IBdoSchemaSet
    {
        if (set != null)
        {
            set.Clear();
            set.Add(pairs);
        }

        return set;
    }

    /// <summary>
    /// Adds a new value to this instance with the specified key and text.
    /// </summary>
    /// <param key="text">The text to consider.</param>
    /// <returns>Returns the added data key value.</returns>
    public static T With<T>(
        this T set,
        params (string Name, DataValueTypes Value)[] pairs)
        where T : IBdoSchemaSet
    {
        if (set != null)
        {
            set.Clear();
            set.Add(pairs);
        }

        return set;
    }
}