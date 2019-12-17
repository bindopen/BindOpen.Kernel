using BindOpen.Framework.Core.Data.Common;
using BindOpen.Framework.Core.Data.Elements;

namespace BindOpen.Framework.Databases.Data.Queries
{
    /// <summary>
    /// This static class represents a factory of data query parameter.
    /// </summary>
    public static class DbQueryParameterFactory
    {
        // As parameter -----

        /// <summary>
        /// Creates a new instance of the DbField class.
        /// </summary>
        /// <param name="query">The query to consider.</param>
        /// <param name="name">The name to consider.</param>
        public static IDataElement UseParameter(
            this IDbQuery query,
            string name)
        {
            return query?.UseParameter(name, DataValueType.Any, null);
        }

        /// <summary>
        /// Creates a new instance of the DbField class.
        /// </summary>
        /// <param name="query">The query to consider.</param>
        /// <param name="name">The name to consider.</param>
        /// <param name="value">The data table to consider.</param>
        public static IDataElement UseParameter(
            this IDbQuery query,
            string name,
            object value)
        {
            return query?.UseParameter(name, DataValueType.Any, value);
        }

        /// <summary>
        /// Creates a new instance of the DbField class.
        /// </summary>
        /// <param name="query">The query to consider.</param>
        /// <param name="name">The name to consider.</param>
        /// <param name="valueType">The data value type to consider.</param>
        /// <param name="value">The data table to consider.</param>
        public static IDataElement UseParameter(
            this IDbQuery query,
            string name,
            DataValueType valueType,
            object value)
        {
            if (query.ParameterSet == null)
            {
                query.ParameterSet = new DataElementSet();
            }

            DataElement parameter;
            if ((parameter = query.ParameterSet[name]) != null)
            {
                parameter.SetItem(value);
            }
            else
            {
                parameter = ElementFactory.CreateScalar(name, valueType, value);
                parameter.Index = query.ParameterSet.Count + 1;
                query.ParameterSet.Add(parameter);
            }

            return parameter;
        }

    }
}
