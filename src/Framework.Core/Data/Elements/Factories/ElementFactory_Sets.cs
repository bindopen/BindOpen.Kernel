using System;
using System.Reflection;
using BindOpen.Framework.Core.Data.Common;
using BindOpen.Framework.Core.Data.Elements.Sets;
using BindOpen.Framework.Core.Extensions.Attributes;

namespace BindOpen.Framework.Core.Data.Elements
{
    /// <summary>
    /// This static class provides methods to create data elements.
    /// </summary>
    public static partial class ElementFactory
    {
        // Creations --------------------------------

        /// <summary>
        /// Instantiates a new instance of the IDataElementSet class.
        /// </summary>
        /// <param name="detail">The detail table.</param>
        public static DataElementSet CreateSet(string[][] detail)
        {
            DataElementSet elementSet = new DataElementSet();
            if (detail != null)
            {
                foreach (String[] strings in detail)
                {
                    elementSet.AddElement(CreateScalar(strings[0], DataValueType.Text, strings[1]));
                }
            }

            return elementSet;
        }

        /// <summary>
        /// Creates a new instance of the IDataElementSet class.
        /// </summary>
        /// <param name="stringObject">The string to consider.</param>
        /// <returns>The collection.</returns>
        public static DataElementSet CreateSet(string stringObject)
        {
            DataElementSet elementSet = new DataElementSet();
            if (stringObject != null)
            {
                foreach (string subString in stringObject.Split('|'))
                {
                    if (subString.IndexOf("=") > 0)
                    {
                        int i = subString.IndexOf("=");
                        elementSet.AddElement(
                            CreateScalar(
                                subString.Substring(0, i),
                                DataValueType.Text,
                                subString.Substring(i + 1)));
                    }
                }
            }
            return elementSet;
        }

        /// <summary>
        /// Creates a data element array from a dynamic object.
        /// </summary>
        /// <param name="aObject">The objet to consider.</param>
        public static IDataElement[] CreateElementArray(object aObject)
        {
            return CreateSet<DataElementSet>(aObject)?.Elements?.ToArray();
        }

        /// <summary>
        /// Creates a data element set from a dynamic object.
        /// </summary>
        /// <param name="aObject">The objet to consider.</param>
        public static T CreateSet<T>(object aObject) where T : DataElementSet, new()
        {
            T elementSet = new T();

            if (aObject != null)
            {
                foreach (PropertyInfo updatePropertyInfo in aObject.GetType().GetProperties())
                {
                    string propertyName = updatePropertyInfo.Name;
                    object propertyValue = updatePropertyInfo.GetValue(aObject);

                    elementSet.AddElement(CreateScalar(propertyName, DataValueType.Any, propertyValue));
                }
            }

            return elementSet;
        }

        /// <summary>
        /// Creates a data element set from a dynamic object.
        /// </summary>
        /// <param name="aObject">The objet to consider.</param>
        public static DataElementSet CreateSetFromAttributes<T>(object aObject) where T : DataElementAttribute
        {
            DataElementSet elementSet = new DataElementSet();
            if (aObject != null)
            {
                foreach (PropertyInfo propertyInfo in aObject.GetType().GetProperties())
                {
                    if (propertyInfo.GetCustomAttribute(typeof(T)) is DetailPropertyAttribute attribute)
                    {
                        string name = attribute.Name;

                        string propertyName = propertyInfo.Name;
                        object propertyValue = propertyInfo.GetValue(aObject);

                        elementSet.AddElement(CreateScalar(propertyName, DataValueType.Any, propertyValue));
                    }
                }
            }

            return elementSet;
        }
    }
}

