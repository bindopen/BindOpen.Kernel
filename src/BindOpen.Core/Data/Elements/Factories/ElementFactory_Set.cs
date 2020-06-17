using BindOpen.Data.Common;
using BindOpen.Data.Items;
using BindOpen.Extensions.Runtime;
using System;
using System.Linq;
using System.Reflection;

namespace BindOpen.Data.Elements
{
    /// <summary>
    /// This static class provides methods to create data element set.
    /// </summary>
    public static partial class ElementFactory
    {
        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <param name="elements">The parameters to consider.</param>
        /// <returns>Return this instance.</returns>
        public static DataElementSet CreateSet()
        {
            return CreateSet(Array.Empty<DataElement>());
        }

        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <param name="elements">The parameters to consider.</param>
        /// <returns>Return this instance.</returns>
        public static DataElementSet CreateSet(params IDataElement[] elements)
        {
            var elementSet = ItemFactory.CreateSet<DataElementSet, IDataElement>();
            elementSet.Items = elements?.ToList();

            return elementSet;
        }

        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <param name="parameters">The parameters to consider.</param>
        /// <returns>Return this instance.</returns>
        public static DataElementSet CreateSet(params (string name, object value)[] parameters)
        {
            return CreateSet(parameters?.Select(p => ElementFactory.CreateScalar(p.name, DataValueTypes.Any, p.value)).ToArray());
        }

        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <param name="parameters">The parameters to consider.</param>
        /// <returns>Return this instance.</returns>
        public static DataElementSet CreateSet(params (string name, DataValueTypes valueType, object value)[] parameters)
        {
            return CreateSet(parameters?.Select(p => ElementFactory.CreateScalar(p.name, p.valueType, p.value)).ToArray());
        }

        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <param name="objects">The parameters to consider.</param>
        /// <returns>Return this instance.</returns>
        public static DataElementSet CreateSetFromScalarObjects(params object[] objects)
        {
            var index = 0;
            return CreateSet(objects?.Select(p =>
            {
                var scalar = ElementFactory.CreateScalar(string.Empty, DataValueTypes.Any, p);
                scalar.Index = ++index;
                return scalar;
            }).ToArray());
        }

        /// <summary>
        /// Creates a data element set from a dynamic object.
        /// </summary>
        /// <param name="aObject">The objet to consider.</param>
        public static DataElementSet CreateSetFromObject(object aObject) => CreateSetFromObject<DataElementSet>(aObject);

        /// <summary>
        /// Creates a new instance of the IDataElementSet class.
        /// </summary>
        /// <param name="stringObject">The string to consider.</param>
        /// <returns>The collection.</returns>
        public static DataElementSet CreateSetFromString(string stringObject)
        {
            var elementSet = new DataElementSet();
            if (stringObject != null)
            {
                foreach (string subString in stringObject.Split('|'))
                {
                    if (subString.IndexOf("=") > 0)
                    {
                        int i = subString.IndexOf("=");
                        elementSet.Add(
                            ElementFactory.CreateScalar(
                                subString.Substring(0, i),
                                DataValueTypes.Text,
                                subString.Substring(i + 1)));
                    }
                }
            }
            return elementSet;
        }

        /// <summary>
        /// Creates a data element set from a dynamic object.
        /// </summary>
        /// <param name="aObject">The objet to consider.</param>
        public static T CreateSetFromObject<T>(object aObject) where T : DataElementSet, new()
        {
            T elementSet = new T();

            if (aObject != null)
            {
                foreach (PropertyInfo updatePropertyInfo in aObject.GetType().GetProperties())
                {
                    string propertyName = updatePropertyInfo.Name;
                    object propertyValue = updatePropertyInfo.GetValue(aObject);

                    elementSet.Add(ElementFactory.CreateScalar(propertyName, DataValueTypes.Any, propertyValue));
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
            var elementSet = new DataElementSet();
            if (aObject != null)
            {
                foreach (PropertyInfo propertyInfo in aObject.GetType().GetProperties())
                {
                    if (propertyInfo.GetCustomAttribute(typeof(T)) is DetailPropertyAttribute)
                    {
                        string propertyName = propertyInfo.Name;
                        object propertyValue = propertyInfo.GetValue(aObject);

                        elementSet.Add(ElementFactory.CreateScalar(propertyName, DataValueTypes.Any, propertyValue));
                    }
                }
            }

            return elementSet;
        }

    }
}
