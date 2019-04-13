using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.Data.Common;
using BindOpen.Framework.Core.Data.Elements;
using BindOpen.Framework.Core.Data.Elements.Factories;
using BindOpen.Framework.Core.Data.Elements.Sets;
using BindOpen.Framework.Core.Data.Helpers.Serialization;
using BindOpen.Framework.Core.Data.Helpers.Strings;
using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.Data.Items.Dictionary;
using BindOpen.Framework.Core.Extensions.Attributes;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Core.System.Scripting;

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
        public static String ToKey(this object object1)
        {
            if (object1 == null)
                return String.Empty;
            else if (object1 is string x)
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
        public static bool KeyEquals(this object object1, object object2)
        {
            return object1 == null || object2 == null ? false : string.Compare(object1.ToKey(), object2.ToKey(), StringComparison.OrdinalIgnoreCase) == 0;
        }

        /// <summary>
        /// Returns the string representation of the specified object.
        /// </summary>
        /// <param name="object1">The object to consider.</param>
        /// <returns></returns>
        public static String ToNotNullString(this object object1)
        {
            return (object1 == null ? "" : object1.ToString());
        }

        /// <summary>
        /// Returns the string value from an object based on this instance's specification.
        /// </summary>
        /// <param name="object1">The object value to convert.</param>
        /// <param name="valueType">The value type to consider.</param>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to use.</param>
        /// <returns>The result string.</returns>
        public static string ToString(
            this object object1,
            DataValueType valueType = DataValueType.Any,
            IAppScope appScope = null,
            IScriptVariableSet scriptVariableSet = null)
        {
            string stringValue = "";
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
                    case DataValueType.Text:
                        stringValue = object1 as string;
                        break;
                    default:
                        stringValue = XmlHelper.ToXml(object1);
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
        public static string GetStringAtIndex(this object[] objects, int index)
        {
            return (objects != null && objects.Length > index && objects[index] != null ? objects[index].ToString() : "");
        }

        /// <summary>
        /// Gets the object at the specified index from the specified index.
        /// </summary>
        /// <param name="objects">The objects to consider.</param>
        /// <param name="index">The index to consider.</param>
        /// <returns>Returns the normalized string.</returns>
        public static object GetObjectAtIndex(this object[] objects, int index)
        {
            return objects.Length > index && objects[index] != null ? objects[index] : null;
        }

        /// <summary>
        /// Gets the result of the serialization of the specified object.
        /// </summary>
        /// <param name="object1">The object to serialize.</param>
        /// <param name="updateObject">The update object to consider.</param>
        /// <returns>The Xml string serializing the specified object.</returns>
        public static void Update(this object object1, object updateObject)
        {
            if ((object1 != null) && (updateObject != null))
            {
                foreach (PropertyInfo updatePropertyInfo in updateObject.GetType().GetProperties())
                {
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
            }
        }

        /// <summary>
        /// Gets the result of the serialization of the specified object.
        /// </summary>
        /// <param name="object1">The object to serialize.</param>
        /// <param name="elementSet">The element set to consider.</param>
        /// <typeparam name="T">The data element attribute to consider.</typeparam>
        /// <returns>The Xml string serializing the specified object.</returns>
        public static void UpdateFromElementSet<T>(this object object1, IDataElementSet elementSet) where T : DataElementAttribute
        {
            if (object1 != null && elementSet != null)
            {
                foreach (PropertyInfo propertyInfo in object1.GetType().GetProperties())
                {
                    DataElementAttribute attribute = propertyInfo.GetCustomAttribute(typeof(T)) as DetailPropertyAttribute;

                    if (attribute != null)
                    {
                        String name = attribute.Name;
                        if (string.IsNullOrEmpty(name))
                            name = propertyInfo.Name;
                        try
                        {
                            var value = elementSet.GetElementObject(name);
                            if (propertyInfo.PropertyType.IsEnum && value != null)
                            {
                                if (Enum.IsDefined(propertyInfo.PropertyType, value))
                                    value = Enum.Parse(propertyInfo.PropertyType, value as string);
                            }

                            propertyInfo.SetValue(object1, value);
                        }
                        catch (Exception ex)
                        {
                            String st = ex.ToString();
                        }
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
        public static void UpdateFromObject<T>(this DataElementSet elementSet, object object1) where T : DataElementAttribute
        {
            if (elementSet != null && object1 != null)
            {
                foreach (PropertyInfo propertyInfo in object1.GetType().GetProperties())
                {
                    DetailPropertyAttribute attribute = propertyInfo.GetCustomAttribute(typeof(DetailPropertyAttribute)) as DetailPropertyAttribute;
                    if (attribute != null)
                    {
                        String name = attribute.Name;
                        if (string.IsNullOrEmpty(name))
                            name = propertyInfo.Name;
                        elementSet.AddElement(
                            ElementFactory.CreateScalar(
                                name, propertyInfo.PropertyType.GetValueType(), propertyInfo.GetValue(object1)));
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
        public static object GetObjectAtIndex(this List<object> objects, int index)
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
            out DataElementAttribute attribute)
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
                        if (propertyInfo.GetCustomAttribute(attributeType) is DataElementAttribute elementAttribute)
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
        /// Sets information of the specified property.
        /// </summary>
        /// <param name="aObject">The object to update.</param>
        /// <param name="elementSet">The set of elements to return.</param>
        /// <param name="attributeType">The type of attribute to update.</param>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to use.</param>
        public static ILog MapProperties(
            this Object aObject,
            IDataElementSet elementSet,
            Type attributeType = null,
            IAppScope appScope = null,
            IScriptVariableSet scriptVariableSet = null)
        {
            if(attributeType==null) attributeType = typeof(DetailPropertyAttribute);

            ILog log = new Log();
            if (aObject == null || elementSet.Elements == null) return null;

            foreach(PropertyInfo property in aObject.GetType().GetProperties().Where(p=>p.GetCustomAttribute(attributeType) != null))
            {
                Attribute attribute = property.GetCustomAttribute(attributeType);
                if (attribute is DataElementAttribute elementAttribute)
                {
                    IDataElement element = elementSet[elementAttribute.Name];
                    if (element!=null)
                    {
                        property.SetValue(aObject, element.GetObject(appScope, scriptVariableSet, log));
                    }
                }
            }

            return log;
        }
    }
}
