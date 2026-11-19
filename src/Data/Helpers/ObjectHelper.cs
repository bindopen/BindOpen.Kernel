using BindOpen.Data.Meta;
using BindOpen.Scoping.Script;
using System;
using System.Globalization;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace BindOpen.Data.Helpers;

/// <summary>
/// This class represents a helper for objects.
/// </summary>
public static partial class ObjectHelper
{
    /// <summary>
    /// Returns a normalized key for the object. 
    /// </summary>
    public static string ToBdoKey(this object obj)
    {
        if (obj == null || obj is DBNull) return string.Empty;

        if (obj is string s) return s;

        if (obj is IReferenced referenced)
        {
            var key = referenced.Key();
            // Si la clé est nulle, on tente de creuser dans le MetaObject
            if (key == null && obj is IBdoMetaObject meta)
            {
                key = (meta.GetData() as IReferenced)?.Key();
            }
            return key ?? string.Empty;
        }

        // Handle generic KeyValuePair<,> where the Key can be a string
        var objType = obj.GetType();
        if (objType.IsGenericType && objType.GetGenericTypeDefinition() == typeof(KeyValuePair<,>))
        {
            var keyProp = objType.GetProperty("Key");
            if (keyProp != null)
            {
                var keyVal = keyProp.GetValue(obj);
                if (keyVal != null)
                    return keyVal.ToString() ?? string.Empty;
            }
        }

        if (obj is KeyValuePair<string, string> kvp) return kvp.Key;

        // Par défaut, on retourne le ToString
        return obj.ToString() ?? string.Empty;
    }

    /// <summary>
    /// Checks if the BDO keys representing the specified objects are equal.
    /// This comparison is case-insensitive.
    /// </summary>
    /// <param name="object1">The first object to consider.</param>
    /// <param name="object2">The second object to compare with.</param>
    /// <returns>Returns True if the keys of the considered objects are equal.</returns>
    public static bool BdoKeyEquals(this object object1, object object2)
    {
        // 1. Performance optimization: check for same reference or both null
        if (ReferenceEquals(object1, object2)) return true;

        // 2. If one is null and the other isn't, they are not equal
        if (object1 == null || object2 == null) return false;

        // 3. Compare the generated keys using a case-insensitive ordinal comparison
        return string.Equals(
            object1.ToBdoKey(),
            object2.ToBdoKey(),
            StringComparison.OrdinalIgnoreCase);
    }

    /// <summary>
    /// Safely casts the specified object to the specified type.
    /// </summary>
    /// <typeparam name="Q">The target type for the cast.</typeparam>
    /// <param name="obj">The object to consider.</param>
    /// <returns>Returns the casted object if the cast is successful; otherwise, returns the default value of the target type.</returns>
    public static Q As<Q>(this object obj)
    {
        if (obj is Q q)
        {
            return q;
        }

        return default;
    }

    /// <summary>
    /// Converts the specified object to a string based on the provided value type.
    /// </summary>
    /// <param name="obj">The object to convert.</param>
    /// <param name="valueType">The BDO data value type to consider.</param>
    /// <param name="isScriptMode">True if the result should be formatted for script evaluation (e.g., quoted).</param>
    /// <returns>Returns the string representation of the object.</returns>
    public static string ToBdoString(
        this object obj,
        DataValueTypes valueType = DataValueTypes.Any,
        bool isScriptMode = false)
    {
        if (obj == null) return null;

        // 1. Resolve 'Any' type to the object's actual type
        if (valueType == DataValueTypes.Any)
        {
            valueType = obj.GetValueType();
        }

        // 2. Handle Scriptwords
        // Crucial: Use ToString(false) to avoid climbing back to root during serialization
        if (obj is IBdoScriptword scriptword)
        {
            return scriptword.ToString(false);
        }

        // 3. Handle MetaData mismatch
        if (obj is IBdoMetaData && valueType != DataValueTypes.MetaData)
        {
            return string.Empty;
        }

        // 4. Handle Collections (excluding strings and byte arrays)
        if (obj is not byte[] && obj is not string && obj is IEnumerable objEnum)
        {
            // For collections, we iterate and recurse
            var items = objEnum.Cast<object>().Select(q => q.ToBdoString(DataValueTypes.Any, isScriptMode));
            return string.Join(",", items);
        }

        string stringValue = null;

        // 5. Handle Scalars with Culture Invariance
        switch (valueType)
        {
            case DataValueTypes.Boolean:
                stringValue = (obj is bool b && b) ? "true" : "false";
                // In script mode, booleans are keywords, not quoted strings
                break;

            case DataValueTypes.Date:
                if (obj is DateTime dateTime)
                    stringValue = dateTime.ToString(StringHelper.__DateTimeFormat, System.Globalization.CultureInfo.InvariantCulture);
                if (isScriptMode && stringValue != null) stringValue = stringValue.ToQuoted();
                break;

            case DataValueTypes.Number:
                // Numbers MUST NOT be quoted in script mode
                if (obj is IFormattable formattable)
                    stringValue = formattable.ToString(null, System.Globalization.CultureInfo.InvariantCulture);
                else
                    stringValue = obj.ToString().Replace(",", ".");
                break;

            case DataValueTypes.Time:
                if (obj is TimeSpan timeSpan)
                    stringValue = timeSpan.ToString(StringHelper.__TimeFormat, System.Globalization.CultureInfo.InvariantCulture);
                if (isScriptMode && stringValue != null) stringValue = stringValue.ToQuoted();
                break;

            case DataValueTypes.Binary:
                if (obj is byte[] byteArray)
                    stringValue = Convert.ToBase64String(byteArray);
                if (isScriptMode && stringValue != null) stringValue = stringValue.ToQuoted();
                break;

            case DataValueTypes.Text:
            default:
                stringValue = obj.ToString();
                // Text always needs quotes in script mode
                if (isScriptMode && stringValue != null) stringValue = stringValue.ToQuoted();
                break;
        }

        return stringValue;
    }

    /// <summary>
    /// Retrieves the PropertyInfo and the associated BDO attribute for a specified property.
    /// </summary>
    /// <param name="objectType">The type of the object to reflect.</param>
    /// <param name="propertyName">The name of the property to find.</param>
    /// <param name="attributeTypes">The types of attributes to look for.</param>
    /// <param name="attribute">The first matching BDO property attribute found.</param>
    /// <returns>Returns the PropertyInfo of the property if found; otherwise, null.</returns>
    public static PropertyInfo GetPropertyInfo(
        this Type objectType,
        string propertyName,
        Type[] attributeTypes,
        out BdoPropertyAttribute attribute)
    {
        attribute = null;
        PropertyInfo propertyInfo = null;

        if (objectType != null && !string.IsNullOrEmpty(propertyName))
        {
            propertyInfo = objectType.GetProperty(propertyName);

            if (propertyInfo != null && attributeTypes != null)
            {
                foreach (Type attributeType in attributeTypes)
                {
                    if (propertyInfo.GetCustomAttribute(attributeType) is BdoPropertyAttribute elementAttribute)
                    {
                        attribute = elementAttribute;
                        break;
                    }
                }
            }
        }

        return propertyInfo;
    }

    /// <summary>
    /// Retrieves the PropertyInfo from a typed lambda expression.
    /// </summary>
    /// <typeparam name="T">The class type.</typeparam>
    /// <param name="property">The property expression (e.g., x => x.Name).</param>
    /// <returns>Returns the PropertyInfo extracted from the expression.</returns>
    /// <exception cref="ArgumentException">Thrown if the expression does not refer to a property.</exception>
    public static PropertyInfo GetPropertyInfo<T>(this Expression<Func<T, object>> property)
    {
        if (property == null) throw new ArgumentNullException(nameof(property));

        MemberExpression memberExpression = null;

        // Handle boxing/unboxing for value types (UnaryExpression)
        if (property.Body is UnaryExpression unaryExpression)
        {
            memberExpression = unaryExpression.Operand as MemberExpression;
        }
        else if (property.Body is MemberExpression m)
        {
            memberExpression = m;
        }

        if (memberExpression?.Member is PropertyInfo propertyInfo)
        {
            return propertyInfo;
        }

        throw new ArgumentException("The expression must refer to a valid class property.", nameof(property));
    }

    /// <summary>
    /// Converts the specified string into a byte array using the provided encoding.
    /// </summary>
    /// <param name="str">The string to convert.</param>
    /// <param name="encoding">The encoding to use. If null, <see cref="Encoding.Default"/> is used.</param>
    /// <returns>Returns a byte array representing the string content.</returns>
    /// <remarks>
    /// If no encoding is specified, the system's default encoding is applied. 
    /// In most modern environments, this defaults to UTF-8.
    /// </remarks>
    public static byte[] ToByteArray(
        this string str,
        Encoding encoding = null)
    {
        if (str == null) return Array.Empty<byte>();

        // Default to UTF8 to have consistent behaviour across platforms
        return (encoding ?? Encoding.UTF8).GetBytes(str);
    }

    /// <summary>
    /// Normalizes the specified object for the BindOpen data system.
    /// If the object is a list with a single element, that element is returned.
    /// Otherwise, the original object or the full list is returned.
    /// </summary>
    /// <param name="obj">The object to normalize.</param>
    /// <returns>Returns the normalized data object.</returns>
    public static object ToBdoData(
        this object obj)
    {
        // Check if the object is a list using the IsList() extension
        if (obj?.IsList() == true)
        {
            var list = obj.ToObjectList();

            // If the list has exactly one item, unwrap it; otherwise return the list
            return list.Count == 1 ? list.FirstOrDefault() : list;
        }

        // Return the original object if it's not a list
        return obj;
    }

    /// <summary>
    /// Indicates whether the specified type is a numeric type.
    /// </summary>
    /// <param name="type">The type to evaluate.</param>
    /// <returns>True if the type is numeric; otherwise, false.</returns>
    /// <remarks>
    /// This method correctly identifies standard numeric primitives as well as 
    /// their Nullable counterparts (e.g., int?).
    /// </remarks>
    public static bool IsNumeric(this Type type)
    {
        if (type == null) { return false; }

        // If the type is Nullable<T>, we extract the underlying type T
        if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
        {
            type = Nullable.GetUnderlyingType(type);
        }

        // Check the TypeCode of the underlying type
        return Type.GetTypeCode(type) switch
        {
            TypeCode.Byte
                or TypeCode.SByte
                or TypeCode.UInt16
                or TypeCode.UInt32
                or TypeCode.UInt64
                or TypeCode.Int16
                or TypeCode.Int32
                or TypeCode.Int64
                or TypeCode.Decimal
                or TypeCode.Double
                or TypeCode.Single => true,
            _ => false,
        };
    }
}
