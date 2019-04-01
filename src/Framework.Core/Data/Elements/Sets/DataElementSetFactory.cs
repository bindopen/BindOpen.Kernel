using System;
using System.Dynamic;
using System.Reflection;
using BindOpen.Framework.Core.Data.Common;
using BindOpen.Framework.Core.Data.Elements.Scalar;
using BindOpen.Framework.Core.Extensions.Attributes;

namespace BindOpen.Framework.Core.Data.Elements.Sets
{
    /// <summary>
    /// This static class provides methods to create data element.
    /// </summary>
    public static class DataElementSetFactory
    {
        // Creations --------------------------------

        /// <summary>
        /// Instantiates a new instance of the IDataElementSet class.
        /// </summary>
        /// <param name="detail">The detail table.</param>
        public static IDataElementSet Create(string[][] detail)
        {
            IDataElementSet elementSet = new DataElementSet();
            if (detail != null)
            {
                foreach (String[] strings in detail)
                {
                    elementSet.AddElement(strings[0], strings[1], DataValueType.Text);
                }
            }

            return elementSet;
        }

        /// <summary>
        /// Creates a new instance of the IDataElementSet class.
        /// </summary>
        /// <param name="stringObject">The string to consider.</param>
        /// <returns>The collection.</returns>
        public static IDataElementSet Create(string stringObject)
        {
            IDataElementSet elementSet = new DataElementSet();
            if (stringObject != null)
            {
                foreach (string subString in stringObject.Split('|'))
                {
                    if (subString.IndexOf("=") > 0)
                    {
                        int i = subString.IndexOf("=");
                        elementSet.AddElement(
                            new ScalarElement(
                                subString.Substring(0, i),
                                DataValueType.Text,
                                subString.Substring(i + 1)));
                    }
                }
            }
            return elementSet;
        }

        /// <summary>
        /// Creates a data element set from a dynamic object.
        /// </summary>
        /// <param name="dynamicObject">The objet to consider.</param>
        public static IDataElementSet Create(DynamicObject dynamicObject)
        {
            IDataElementSet elementSet = new DataElementSet();
            if (dynamicObject != null)
                foreach (PropertyInfo updatePropertyInfo in dynamicObject.GetType().GetProperties())
                    {
                        string propertyName = updatePropertyInfo.Name;
                        object propertyValue = updatePropertyInfo.GetValue(dynamicObject);

                        elementSet.AddElement(DataElement.Create(propertyValue, DataValueType.Any, propertyName));
                    }

            return elementSet;
        }

        /// <summary>
        /// Creates a data element set from a dynamic object.
        /// </summary>
        /// <param name="aObject">The objet to consider.</param>
        public static IDataElementSet Create<T>(object aObject) where T : DataElementAttribute
        {
            IDataElementSet elementSet = new DataElementSet();
            if (aObject != null)
            {
                foreach (PropertyInfo propertyInfo in aObject.GetType().GetProperties())
                {
                    DataElementAttribute attribute = propertyInfo.GetCustomAttribute(typeof(T)) as DetailPropertyAttribute;

                    if (attribute != null)
                    {
                        string name = attribute.Name;

                        string propertyName = propertyInfo.Name;
                        object propertyValue = propertyInfo.GetValue(aObject);

                        elementSet.AddElement(DataElement.Create(propertyValue, DataValueType.Any, propertyName));
                    }
                }
            }

            return elementSet;
        }
    }
}

