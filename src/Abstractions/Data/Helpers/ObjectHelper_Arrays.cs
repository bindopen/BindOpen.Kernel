using BindOpen.Data.Meta;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace BindOpen.Data.Helpers;

/// <summary>
/// This class represents a helper for objects.
/// </summary>
public static partial class ObjectHelper
{
    /// <summary>
    /// Gets the element at the specified index in a safe manner.
    /// </summary>
    /// <typeparam name="Q">The type of the elements.</typeparam>
    /// <param name="obj">The list to consider.</param>
    /// <param name="index">The index to retrieve.</param>
    /// <returns>Returns the element if the index is valid; otherwise, the default value.</returns>
    public static Q GetAt<Q>(
        this IList<Q> obj,
        int index)
        => obj != null && index >= 0 && index < obj.Count ? obj[index] : default;

    /// <summary>
    /// Gets the element at the specified index in a safe manner for arrays.
    /// </summary>
    public static T GetAt<T>(
        this T[] arr,
        int index)
    {
        return arr != null && index >= 0 && index < arr.Length ? arr[index] : default;
    }

    /// <summary>
    /// Gets the string representation of the object at the specified index.
    /// </summary>
    public static string GetString(
        this IList<object> objects,
        int index)
    {
        return objects?.GetAt(index).ToBdoString(DataValueTypes.Any);
    }

    /// <summary>
    /// Gets and converts the object at the specified index.
    /// </summary>
    public static T Get<T>(
        this IList<object> objects,
        int index = 0,
        Func<object, T> converter = null)
    {
        var obj = objects != null && index >= 0 && index < objects.Count ? objects[index] : default;

        if (converter != null)
        {
            return converter.Invoke(obj);
        }

        return obj.As<T>();
    }

    /// <summary>
    /// Gets the object at the specified index from the specified index.
    /// </summary>
    /// <param key="objects">The objects to consider.</param>
    /// <param key="index">The index to consider.</param>
    /// <returns>Returns the normalized string.</returns>
    public static T First<T>(
        this IList<object> objects,
        Func<object, T> converter = null)
    {
        var obj = objects?.FirstOrDefault();

        if (converter != null)
        {
            obj = converter.Invoke(obj);
        }

        return obj.As<T>();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param key="type"></param>
    /// <returns></returns>
    public static object[] ToObjectArray(this object obj)
        => obj.ToObjectList()?.ToArray();

    /// <summary>
    /// Converts an object to a list of objects. If the object is not a list, returns a list containing it.
    /// </summary>
    public static IList<object> ToObjectList(this object obj)
    {
        if (obj == null) return null;

        if (obj.IsList())
        {
            return [.. (obj as IEnumerable).Cast<object>()];
        }

        return new List<object> { obj };
    }

    /// <summary>
    /// Indicates whether the specified object should be treated as a list.
    /// </summary>
    /// <param name="obj">The object to consider.</param>
    /// <returns>True if the object is a collection (excluding strings, byte arrays, and meta sets); otherwise, false.</returns>
    public static bool IsList(this object obj)
        => obj?.GetType().IsList() ?? false;

    /// <summary>
    /// Determines if the specified type should be treated as a collection in the BindOpen system.
    /// </summary>
    /// <remarks>
    /// Strings and byte arrays are excluded from being identified as lists. 
    /// IBdoMetaSet is also excluded as it is handled as a single composite entity.
    /// </remarks>
    public static bool IsList(this Type type)
    {
        if (type == null || type == typeof(string) || type == typeof(byte[]))
            return false;

        return !typeof(IBdoMetaSet).IsAssignableFrom(type)
            && (typeof(IEnumerable).IsAssignableFrom(type) || type.IsArray);
    }
}
