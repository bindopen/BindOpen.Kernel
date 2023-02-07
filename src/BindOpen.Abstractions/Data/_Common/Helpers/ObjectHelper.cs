using BindOpen.Data.Items;
using BindOpen.Data.Meta;
using BindOpen.Extensions.Scripting;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace BindOpen.Data
{
    /// <summary>
    /// This class represents a helper for objects.
    /// </summary>
    public static class ObjectHelper
    {

        /// <summary>
        /// Returns the key representing the specified object i.e. in lower case and empty if null.
        /// </summary>
        /// <param name="object1">The object to consider.</param>
        /// <returns>Returns the key representing the specified object.</returns>
        public static string ToBdoKey(this object object1)
        {
            if (object1 == null)
                return string.Empty;
            else if (object1 is string x)
                return x;
            else if (object1 is IReferenced referenced)
                return referenced.Key() ?? string.Empty;
            else if (object1 is KeyValuePair<string, string> dataKeyValue)
                return dataKeyValue.Key ?? string.Empty;
            else
                return object1.ToString();
        }

        /// <summary>
        /// Indicates whether the key representing the specified object i.e. in lower case and empty if null.
        /// </summary>
        /// <param name="object1">The object to consider.</param>
        /// <param name="object2">The object to compare with.</param>
        /// <returns>Returns True if the keys of the considered objects equal.</returns>
        public static bool BdoKeyEquals(this object object1, object object2)
            => object1 != null
            && object2 != null
            && string.Compare(object1.ToBdoKey(), object2.ToBdoKey(), StringComparison.OrdinalIgnoreCase) == 0;

        /// <summary>
        /// Returns the string representation of the specified object.
        /// </summary>
        /// <param name="object1">The object to consider.</param>
        /// <returns></returns>
        public static string ToNotNullString(this object object1)
        {
            return object1 == null ? string.Empty : object1.ToString();
        }

        public static Q As<Q>(this object obj)
        {
            if (obj is Q q)
                return q;

            return default;
        }

        /// <summary>
        /// Returns the string value from an object based on this instance's specification.
        /// </summary>
        /// <param name="obj">The object value to convert.</param>
        /// <param name="valueType">The value type to consider.</param>
        /// <param name="isScriptMode">Indicates whether the script mode is activated.</param>
        /// <returns>The result string.</returns>
        public static string ToString(
            this object obj,
            DataValueTypes valueType,
            bool isScriptMode = false)
        {
            string stringValue = null;
            if (valueType == DataValueTypes.Any)
            {
                valueType = obj.GetValueType();
            }

            IEnumerable objEnum;

            // if object is a singleton of scalar list

            if (obj is not byte[]
                && obj is not string
                && (objEnum = obj as IEnumerable) != null)
            {
                return string.Join(",", objEnum.Cast<object>().Select(q => q.ToString(valueType, isScriptMode)));
            }

            if (obj != null)
            {
                switch (valueType)
                {
                    case DataValueTypes.Boolean:
                        stringValue = obj as bool? == true ? "true" : "false";

                        if (isScriptMode)
                        {
                            stringValue = stringValue.ToQuoted();
                        }
                        break;
                    case DataValueTypes.Date:
                        if (obj is DateTime dateTime)
                        {
                            stringValue = dateTime.ToString(StringHelper.__DateFormat);

                            if (isScriptMode)
                            {
                                stringValue = stringValue.ToQuoted();
                            }
                        }
                        break;
                    case DataValueTypes.Number:
                        stringValue = obj.ToString().Replace(",", ".");

                        if (isScriptMode)
                        {
                            stringValue = stringValue.ToQuoted();
                        }
                        break;
                    case DataValueTypes.Time:
                        if (obj is TimeSpan timeSpan)
                        {
                            stringValue = timeSpan.ToString(StringHelper.__TimeFormat);

                            if (isScriptMode)
                            {
                                stringValue = stringValue.ToQuoted();
                            }
                        }
                        break;
                    case DataValueTypes.ByteArray:
                        if (obj is byte[] byteArray)
                        {
                            stringValue = Convert.ToBase64String(byteArray);

                            if (isScriptMode)
                            {
                                stringValue = stringValue.ToQuoted();
                            }
                        }
                        break;
                    case DataValueTypes.Document:
                    case DataValueTypes.Entity:
                    case DataValueTypes.Object:
                        if (obj is IBdoScriptword scriptword)
                        {
                            stringValue = scriptword.ToString();
                        }
                        else if (obj is IBdoExpression expression)
                        {
                            stringValue = expression.ToString();
                        }
                        else
                        {
                            stringValue = obj.ToString();

                            if (isScriptMode)
                            {
                                stringValue = stringValue.ToQuoted();
                            }
                        }
                        break;
                    default:
                        stringValue = obj.ToString();

                        if (isScriptMode)
                        {
                            stringValue = stringValue.ToQuoted();
                        }
                        break;
                }
            }

            return stringValue;
        }

        /// <summary>
        /// Gets the result of the serialization of the specified object.
        /// </summary>
        /// <param name="obj">The object to serialize.</param>
        /// <param name="updateObj">The update object to consider.</param>
        /// <returns>The Xml string serializing the specified object.</returns>
        public static void Update(this object obj, object updateObj)
        {
            if (obj != null && updateObj != null)
            {
                foreach (PropertyInfo updatePropertyInfo in updateObj.GetType().GetProperties())
                {
                    try
                    {
                        PropertyInfo propertyInfo = obj.GetType().GetProperty(updatePropertyInfo.Name);

                        if (propertyInfo != null)
                        {
                            object property = updatePropertyInfo.GetValue(updateObj);
                            if (property is IBdoItem propertyDataItem)
                            {
                                property = propertyDataItem.Clone();
                            }
                            propertyInfo.SetValue(obj, property);
                        }
                    }
                    catch
                    {
                    }
                }
            }
        }

        /// <summary>
        /// Gets the object at the specified index from the specified index.
        /// </summary>
        /// <param name="objects">The objects to consider.</param>
        /// <param name="index">The index to consider.</param>
        /// <returns>Returns the normalized string.</returns>
        public static string GetString(this List<object> objects, int index)
        {
            return objects?.Get(index).ToString(DataValueTypes.Any);
        }

        /// <summary>
        /// Gets the object at the specified index from the specified index.
        /// </summary>
        /// <param name="objects">The objects to consider.</param>
        /// <param name="index">The index to consider.</param>
        /// <returns>Returns the normalized string.</returns>
        public static T Get<T>(this List<T> objects, int index)
        {
            return objects != null && objects.Count > index && objects[index] != null ? objects[index] : default;
        }

        /// <summary>
        /// Gets the object at the specified index from the specified index.
        /// </summary>
        /// <param name="objects">The objects to consider.</param>
        /// <param name="index">The index to consider.</param>
        /// <returns>Returns the normalized string.</returns>
        public static T Get<T>(this List<object> objects, int index, Func<object, T> converter)
        {
            var obj = objects != null && objects.Count > index && objects[index] != null ? objects[index] : default;

            return converter.Invoke(obj);
        }

        /// <summary>
        /// Gets the object at the specified index from the specified index.
        /// </summary>
        /// <param name="objects">The objects to consider.</param>
        /// <param name="index">The index to consider.</param>
        /// <returns>Returns the normalized string.</returns>
        public static T First<T>(this List<object> objects, int index, Func<object, T> converter)
        {
            var obj = objects?.FirstOrDefault();

            return converter.Invoke(obj);
        }

        /// <summary>
        /// Using the specified item executing the specified action.
        /// </summary>
        /// <typeparam name="T">A type deriving from data item.</typeparam>
        /// <param name="item">The item to use.</param>
        /// <param name="action">The action to execute.</param>
        public static void Using<T>(this T item, Action<T> action)
            where T : IBdoItem
        {
            if (item != null)
                using (item)
                {
                    action(item);
                }
        }

        /// <summary>
        /// Gets information of the specified property.
        /// </summary>
        /// <param name="objectType">The object type to consider.</param>
        /// <param name="propertyName">The property name to consider.</param>
        /// <param name="attributeTypes"></param>
        /// <param name="attribute">The attribute to return.</param>
        public static PropertyInfo GetPropertyInfo(
            this Type objectType,
            string propertyName,
            Type[] attributeTypes,
            out BdoMetaAttribute attribute)
        {
            attribute = null;
            PropertyInfo propertyInfo = null;

            if (objectType != null && propertyName != null)
            {
                propertyInfo = objectType.GetProperty(propertyName);
                if (propertyInfo != null)
                {
                    foreach (Type attributeType in attributeTypes)
                    {
                        if (propertyInfo.GetCustomAttribute(attributeType) is BdoMetaAttribute elementAttribute)
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
        /// Gets the specified property.
        /// </summary>
        /// <typeparam name="T">The class to consider.</typeparam>
        /// <param name="property">The property expression to consider.</param>
        /// <returns>Returns the property information.</returns>
        public static PropertyInfo GetProperty<T>(this Expression<Func<T, object>> property)
        {
            LambdaExpression lambda = property;
            MemberExpression memberExpression;

            if (lambda.Body is UnaryExpression unaryExpression)
            {
                memberExpression = (MemberExpression)unaryExpression.Operand;
            }
            else
            {
                memberExpression = (MemberExpression)lambda.Body;
            }

            return (PropertyInfo)memberExpression.Member;
        }

        /// <summary>
        /// Creates a byte array from the string, using the 
        /// System.Text.Encoding.Default encoding unless another is specified.
        /// </summary>
        public static byte[] ToByteArray(
            this string str,
            Encoding encoding = null)
        {
            return (encoding ?? Encoding.Default).GetBytes(str);
        }

        /// <summary>
        /// Gets the fields of the specified enumeration.
        /// </summary>
        /// <typeparam name="type">The enumeration type to consider.</typeparam>
        /// <returns>Returns the string array.</returns>
        public static string[] GetEnumFields(
            this Type type)
        {
            List<string> fieldNames = new();
            foreach (var field in Enum.GetValues(type))
            {
                fieldNames.Add(field?.ToString());
            }

            return fieldNames.ToArray();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="spec"></param>
        /// <returns></returns>
        public static object ToBdoData(
            this object obj,
            IBdoSpec spec)
        {
            object item = null;

            if (obj != null)
            {
                if (spec == null || spec.IsCompatibleWithData(obj))
                {
                    IEnumerable objEnum;

                    //// if object is a singleton of scalar list

                    //if (obj is not byte[]
                    //    && obj is not string
                    //    && (objEnum = obj as IEnumerable) != null
                    //    && objEnum.Cast<object>().Count() == 1
                    //    && objEnum.Cast<object>().FirstOrDefault() is IEnumerable list
                    //    && list is not string)
                    //{
                    //    item = list.Cast<object>().ToList();
                    //}
                    // if object is a scalar list
                    if (obj is not byte[]
                       && obj is not string
                       && (objEnum = obj as IEnumerable) != null
                       && objEnum.Cast<object>().FirstOrDefault() is not IBdoItem)
                    {
                        if (objEnum.Cast<object>().Count() > 1)
                        {
                            item = objEnum.Cast<object>().ToList();
                        }
                        else
                        {
                            item = objEnum.Cast<object>().FirstOrDefault();
                        }
                    }
                    else
                    {
                        item = obj;
                    }
                }
                else
                {
                    var list = new List<object>();

                    foreach (object subItem in obj as IEnumerable)
                    {
                        if (spec.MaximumItemNumber == -1
                            || (list?.Count ?? 0) < spec.MaximumItemNumber)
                        {
                            if (spec.IsCompatibleWithData(subItem))
                            {
                                list.Add(subItem);
                            }
                        }
                    }

                    item = list;
                }
            }

            return item;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static List<object> ToObjectList(this object obj)
        {
            List<object> objList = null;
            if (obj?.GetType().IsList() == true)
            {
                objList = (obj as IEnumerable).Cast<object>().ToList();
            }
            else
            {
                objList = obj == null ? null : new List<object>() { obj };
            }

            return objList;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsList(this object obj)
            => obj?.GetType().IsList() ?? false;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsList(this Type type)
        {
            if (type == null) { return false; }

            if (type == typeof(string)) { return false; }

            return typeof(Array).IsAssignableFrom(type)
                || typeof(IEnumerable).IsAssignableFrom(type)
                || type.IsArray;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsNumeric(this Type type)
        {
            if (type == null) { return false; }

            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                type = type.GetGenericArguments()[0];
            }

            switch (Type.GetTypeCode(type))
            {
                case TypeCode.Byte:
                case TypeCode.SByte:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.Decimal:
                case TypeCode.Double:
                case TypeCode.Single:
                    return true;
                default:
                    return false;
            }
        }
    }
}
