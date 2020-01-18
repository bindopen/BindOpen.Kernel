using BindOpen.Framework.Data.Common;
using BindOpen.Framework.Data.Elements;

namespace BindOpen.Framework.Data.Queries
{
    /// <summary>
    /// This static class represents a factory of data query parameter.
    /// </summary>
    public static partial class DbFactory
    {
        // As parameter -----

        /// <summary>
        /// Creates a new instance of the DataElement class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        public static IDataElement CreateParameter(
            string name)
        {
            return DbFactory.CreateParameter(name, DataValueType.Any, null);
        }

        /// <summary>
        /// Creates a new instance of the DataElement class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="value">The data table to consider.</param>
        public static IDataElement CreateParameter(
            string name,
            object value)
        {
            return DbFactory.CreateParameter(name, DataValueType.Any, value);
        }

        /// <summary>
        /// Creates a new instance of the DataElement class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="valueType">The data value type to consider.</param>
        /// <param name="value">The data table to consider.</param>
        public static IDataElement CreateParameter(
            string name,
            DataValueType valueType,
            object value)
        {
            return ElementFactory.CreateScalar(name, valueType, value);
        }

        /// <summary>
        /// Creates a parameter string from the specified parameter.
        /// </summary>
        /// <param name="parameter">The parameter to consider.</param>
        /// <returns>Returns the string corresponding to the specified parameter.</returns>
        public static string CreateParameterString(this IDataElement parameter) => "@" + parameter?.Name ?? parameter.Index.ToString();
    }
}
