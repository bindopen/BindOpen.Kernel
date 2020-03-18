using BindOpen.Data.Common;
using BindOpen.Extensions.Runtime;
using System.Linq;
using System.Reflection;

namespace BindOpen.Data.Elements
{
    /// <summary>
    /// This static class provides methods to create data element set.
    /// </summary>
    public static partial class ElementSetFactory
    {
        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <param name="parameters">The parameters to consider.</param>
        /// <returns>Return this instance.</returns>
        public static DataElementSet Create(params DataElement[] parameters)
        {
            return new DataElementSet(parameters);
        }

        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <param name="parameters">The parameters to consider.</param>
        /// <returns>Return this instance.</returns>
        public static DataElementSet Create(params (string name, object value)[] parameters)
        {
            return new DataElementSet(parameters?.Select(p => ElementFactory.CreateScalar(p.name, DataValueType.Any, p.value)).ToArray());
        }

        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <param name="parameters">The parameters to consider.</param>
        /// <returns>Return this instance.</returns>
        public static DataElementSet Create(params (string name, DataValueType valueType, object value)[] parameters)
        {
            return new DataElementSet(parameters?.Select(p => ElementFactory.CreateScalar(p.name, p.valueType, p.value)).ToArray());
        }

        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <param name="parameterValues">The parameters to consider.</param>
        /// <returns>Return this instance.</returns>
        public static DataElementSet Create(params object[] parameterValues)
        {
            var index = 0;
            return new DataElementSet(parameterValues?.Select(p =>
            {
                var scalar = ElementFactory.CreateScalar("", DataValueType.Any, p);
                scalar.Index = ++index;
                return scalar;
            }).ToArray());
        }

        /// <summary>
        /// Creates a new instance of the IDataElementSet class.
        /// </summary>
        /// <param name="stringObject">The string to consider.</param>
        /// <returns>The collection.</returns>
        public static DataElementSet CreateFromString(string stringObject)
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
                            ElementFactory.CreateScalar(
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
            return CreateFromObject<DataElementSet>(aObject)?.Elements?.ToArray();
        }

        /// <summary>
        /// Creates a data element set from a dynamic object.
        /// </summary>
        /// <param name="aObject">The objet to consider.</param>
        public static DataElementSet CreateFromObject(object aObject) => CreateFromObject<DataElementSet>(aObject);

        /// <summary>
        /// Creates a data element set from a dynamic object.
        /// </summary>
        /// <param name="aObject">The objet to consider.</param>
        public static T CreateFromObject<T>(object aObject) where T : DataElementSet, new()
        {
            T elementSet = new T();

            if (aObject != null)
            {
                foreach (PropertyInfo updatePropertyInfo in aObject.GetType().GetProperties())
                {
                    string propertyName = updatePropertyInfo.Name;
                    object propertyValue = updatePropertyInfo.GetValue(aObject);

                    elementSet.AddElement(ElementFactory.CreateScalar(propertyName, DataValueType.Any, propertyValue));
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
                    if (propertyInfo.GetCustomAttribute(typeof(T)) is DetailPropertyAttribute)
                    {
                        string propertyName = propertyInfo.Name;
                        object propertyValue = propertyInfo.GetValue(aObject);

                        elementSet.AddElement(ElementFactory.CreateScalar(propertyName, DataValueType.Any, propertyValue));
                    }
                }
            }

            return elementSet;
        }

    }
}
