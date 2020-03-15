using BindOpen.Data.Common;
using BindOpen.Data.Elements;
using BindOpen.Data.Expression;
using BindOpen.Data.Helpers.Strings;

namespace BindOpen.Data.Queries
{
    /// <summary>
    /// This static class represents a factory of data query parameter.
    /// </summary>
    public static partial class DbFluent
    {
        // As parameter -----

        /// <summary>
        /// Creates a new instance of the DataElement class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="value">The data table to consider.</para>
        public static IDataElement Parameter(
            string name,
            object value = null)
        {
            return DbFluent.Parameter(name, DataValueType.Any, value);
        }

        /// <summary>
        /// Creates a new instance of the DataElement class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="valueType">The data value type to consider.</param>
        /// <param name="value">The data table to consider.</param>
        public static IDataElement Parameter(
            string name,
            DataValueType valueType,
            object value)
        {
            return ElementFactory.CreateScalar(name, valueType, value);
        }

        /// <summary>
        /// Creates a new instance of the DataElement class.
        /// </summary>
        /// <param name="element">The element to consider.</param>
        public static DataExpression AsScript(this ScalarElement parameter)
        {
            return CreateParameterWildString(parameter).CreateScript();
        }

        /// <summary>
        /// Creates a parameter wild string from the specified parameter.
        /// </summary>
        /// <param name="parameter">The parameter to consider.</param>
        /// <returns>Returns the string corresponding to the specified parameter.</returns>
        internal static string CreateParameterWildString(this IDataElement parameter)
            => StringHelper.__UniqueToken + (parameter?.Name ?? parameter.Index.ToString()) + StringHelper.__UniqueToken;

        /// <summary>
        /// Creates a parameter string from the specified parameter.
        /// </summary>
        /// <param name="parameter">The parameter to consider.</pram>
        /// <returns>Returns the string corresponding to the specified parameter.</returns>
        internal static string CreateParameterString(this IDataElement parameter)
            => "@" + parameter?.Name ?? parameter.Index.ToString();
    }
}
