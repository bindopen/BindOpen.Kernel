using System;
using System.Collections.Generic;
using System.Reflection;
using BindOpen.Framework.Core.Data.Common;
using BindOpen.Framework.Core.Data.Elements.Sets;
using BindOpen.Framework.Core.Data.Helpers.Strings;
using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.Data.Items.Dictionary;
using BindOpen.Framework.Core.Extensions.Attributes;

namespace BindOpen.Framework.Core.Data.Helpers.Objects
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
        public static String ToKey(this Object object1)
        {
            if (object1 == null)
                return String.Empty;
            else if (object1 is String x)
                return x;
            else if (object1 is IdentifiedDataItem identifiedDataItem)
                return (identifiedDataItem).Key() ?? String.Empty;
            else if (object1 is DataKeyValue dataKeyValue)
                return (dataKeyValue).Key ?? String.Empty;
            else
                return object1.ToString();
        }

        /// <summary>
        /// Indicates whether the key representing the specified object i.e. in lower case and empty if null.
        /// </summary>
        /// <param name="object1">The object to consider.</param>
        /// <param name="object2">The object to compare with.</param>
        /// <returns>Returns True if the keys of the considered objects equal.</returns>
        public static Boolean KeyEquals(this Object object1, Object object2)
        {
            return object1 == null || object2 == null ? false : string.Compare(object1.ToKey(), object2.ToKey(), StringComparison.OrdinalIgnoreCase) == 0;
        }

        /// <summary>
        /// Returns the string representation of the specified object.
        /// </summary>
        /// <param name="object1">The object to consider.</param>
        /// <returns></returns>
        public static String ToNotNullString(this Object object1)
        {
            return (object1 == null ? "" : object1.ToString());
        }

        /// <summary>
        /// Returns the string value from an object based on this instance's specification.
        /// </summary>
        /// <param name="object1">The object value to convert.</param>
        /// <param name="valueType">The value type to consider.</param>
        /// <returns>The result string.</returns>
        public static String GetString(
            this Object object1,
            DataValueType valueType = DataValueType.Any)
        {
            String stringValue = "";
            if (valueType == DataValueType.Any)
                valueType = object1.GetValueType();

            if (object1 != null)
            {
                switch (valueType)
                {
                    case DataValueType.Boolean:
                        stringValue = (object1 as Boolean?) == true ? "true" : "false";
                        break;
                    case DataValueType.Date:
                        if (object1 is DateTime dateTime)
                            stringValue = (dateTime).ToString(StringHelper.__DateFormat);
                        break;
                    case DataValueType.Number:
                        stringValue = object1.ToString().Replace(",", ".");
                        break;
                    case DataValueType.Time:
                        if (object1 is TimeSpan timeSpan)
                            stringValue = (timeSpan).ToString(StringHelper.__TimeFormat);
                        break;
                    default:
                        stringValue = object1.ToString();
                        break;
                }
            }

            return stringValue;
        }

        /// <summary>
        /// Gets the string at the specified index from the specified index.
        /// </summary>
        /// <param name="objects">The objects to consider.</param>
        /// <param name="index">The index to consider.</param>
        /// <returns>Returns the normalized string.</returns>
        public static string GetStringAtIndex(this Object[] objects, int index)
        {
            return (objects != null && objects.Length > index && objects[index] != null ? objects[index].ToString() : "");
        }

        /// <summary>
        /// Gets the object at the specified index from the specified index.
        /// </summary>
        /// <param name="objects">The objects to consider.</param>
        /// <param name="index">The index to consider.</param>
        /// <returns>Returns the normalized string.</returns>
        public static Object GetObjectAtIndex(this Object[] objects, int index)
        {
            return (objects.Length > index && objects[index] != null ? objects[index] : null);
        }

        /// <summary>
        /// Gets the result of the serialization of the specified object.
        /// </summary>
        /// <param name="object1">The object to serialize.</param>
        /// <param name="updateObject">The update object to consider.</param>
        /// <returns>The Xml string serializing the specified object.</returns>
        public static void Update(this Object object1, Object updateObject)
        {
            if ((object1 != null) && (updateObject != null))
                foreach (PropertyInfo updatePropertyInfo in updateObject.GetType().GetProperties())
                    try
                    {
                        PropertyInfo propertyInfo = object1.GetType().GetProperty(updatePropertyInfo.Name);
                        if (propertyInfo != null)
                        {
                            Object property = updatePropertyInfo.GetValue(updateObject);
                            if (property is DataItem)
                                property = (property as DataItem).Clone();
                            propertyInfo.SetValue(object1, property);
                        }
                    }
                    catch
                    {
                    }
        }

        /// <summary>
        /// Gets the result of the serialization of the specified object.
        /// </summary>
        /// <param name="object1">The object to serialize.</param>
        /// <param name="elementSet">The element set to consider.</param>
        /// <typeparam name="T">The data element attribute to consider.</typeparam>
        /// <returns>The Xml string serializing the specified object.</returns>
        public static void UpdateFromElementSet<T>(this Object object1, DataElementSet elementSet) where T : DataElementAttribute
        {
            if (object1 != null && elementSet != null)
                foreach (PropertyInfo propertyInfo in object1.GetType().GetProperties())
                {
                    DataElementAttribute attribute = propertyInfo.GetCustomAttribute(typeof(T)) as DetailPropertyAttribute;

                    if (attribute != null)
                    {
                        String name = attribute.Name;
                        if (String.IsNullOrEmpty(name))
                            name = propertyInfo.Name;
                        try
                        {
                            var value = elementSet.GetElementItemObject(name);
                            if (propertyInfo.PropertyType.IsEnum && value!=null)
                            {
                                if (Enum.IsDefined(propertyInfo.PropertyType, value))
                                    value = Enum.Parse(propertyInfo.PropertyType, value as String);
                            }

                            propertyInfo.SetValue(object1, value);
                        }
                        catch(Exception ex)
                        {
                            String st = ex.ToString();
                        }
                    }
                }

        }

        /// <summary>
        /// Gets the result of the serialization of the specified object.
        /// </summary>
        /// <param name="elementSet">The element set to consider.</param>
        /// <param name="object1">The object to serialize.</param>
        /// <typeparam name="T">The data element attribute to consider.</typeparam>
        /// <returns>The Xml string serializing the specified object.</returns>
        public static void UpdateFromObject<T>(this DataElementSet elementSet, Object object1) where T : DataElementAttribute
        {
            if (elementSet != null && object1 != null)
                foreach (PropertyInfo propertyInfo in object1.GetType().GetProperties())
                {
                    DetailPropertyAttribute attribute = propertyInfo.GetCustomAttribute(typeof(DetailPropertyAttribute)) as DetailPropertyAttribute;
                    if (attribute != null)
                    {
                        String name = attribute.Name;
                        if (String.IsNullOrEmpty(name))
                            name = propertyInfo.Name;
                        elementSet.AddElement(name, propertyInfo.GetValue(object1), propertyInfo.PropertyType.GetValueType());
                    }
                }
        }

        /// <summary>
        /// Gets the object at the specified index from the specified index.
        /// </summary>
        /// <param name="objects">The objects to consider.</param>
        /// <param name="index">The index to consider.</param>
        /// <returns>Returns the normalized string.</returns>
        public static Object GetObjectAtIndex(this List<Object> objects, int index)
        {
            return (objects != null && objects.Count > index && objects[index] != null ? objects[index] : null);
        }

        /// <summary>
        /// Using the specified item executing the specified action.
        /// </summary>
        /// <typeparam name="T">A type deriving from data item.</typeparam>
        /// <param name="item">The item to use.</param>
        /// <param name="action">The action to execute.</param>
        public static void Using<T>(this T item, Action<T> action) where T : DataItem
        {
            if (item != null)
                using (item)
                    action(item);
        }

    }
}
