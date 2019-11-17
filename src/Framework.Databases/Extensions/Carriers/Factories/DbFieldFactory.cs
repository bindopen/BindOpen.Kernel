using BindOpen.Framework.Core.Data.Common;
using BindOpen.Framework.Core.Data.Expression;
using BindOpen.Framework.Core.Data.Helpers.Objects;
using BindOpen.Framework.Databases.Data.Queries;

namespace BindOpen.Framework.Databases.Extensions.Carriers
{
    /// <summary>
    /// This static class represents a factory of data field.
    /// </summary>
    public static class DbFieldFactory
    {
        /// <summary>
        /// Creates a new instance of the DbField class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="tableName">The data table to consider.</param>
        /// <param name="schema">The schema to consider.</param>
        /// <param name="dataModule">The data module to consider.</param>
        public static DbField Create(
            string name,
            string tableName = null,
            string schema = null,
            string dataModule = null)
        {
            return new DbField()
            {
                Name = name,
                DataTable = tableName,
                Schema = schema,
                DataModule = dataModule
            };
        }

        // As literal -----

        /// <summary>
        /// Creates a new instance of the DbField class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="value">The value to consider.</param>
        /// <param name="valueType">The value type to consider.</param>
        public static DbField CreateAsLiteral(
            string name,
            object value,
            DataValueType valueType = DataValueType.Any)
        {
            return CreateAsLiteral(name, null, null, null, value, valueType);
        }

        /// <summary>
        /// Creates a new instance of the DbField class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="tableName">The data table to consider.</param>
        /// <param name="valueType">The value type to consider.</param>
        /// <param name="value">The value to consider.</param>
        public static DbField CreateAsLiteral(
            string name,
            string tableName,
            object value,
            DataValueType valueType = DataValueType.None)
        {
            return CreateAsLiteral(name, tableName, null, null, value, valueType);
        }

        /// <summary>
        /// Creates a new instance of the DbField class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="tableName">The data table to consider.</param>
        /// <param name="schema">The schema to consider.</param>
        /// <param name="dataModule">The data module to consider.</param>
        /// <param name="valueType">The value type to consider.</param>
        /// <param name="value">The value to consider.</param>
        public static DbField CreateAsLiteral(
            string name,
            string tableName,
            string schema,
            string dataModule,
            object value,
            DataValueType valueType = DataValueType.None)
        {
            var field = Create(name, tableName, schema, dataModule);
            field.ValueType = DataValueType.Text;
            if (value != null)
                field.Value = value.ToString(valueType).CreateLiteral();
            return field;
        }

        // As script -----

        /// <summary>
        /// Creates a new instance of the DbField class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="value">The value to consider.</param>
        /// <param name="valueType">The value type to consider.</param>
        public static DbField CreateAsScript(
            string name,
            object value,
            DataValueType valueType = DataValueType.None)
        {
            return CreateAsScript(name, null, null, null, value, valueType);
        }

        /// <summary>
        /// Creates a new instance of the DbField class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="tableName">The data table to consider.</param>
        /// <param name="valueType">The value type to consider.</param>
        /// <param name="value">The value to consider.</param>
        public static DbField CreateAsScript(
            string name,
            string tableName,
            object value,
            DataValueType valueType = DataValueType.None)
        {
            return CreateAsScript(name, tableName, null, null, value, valueType);
        }

        /// <summary>
        /// Creates a new instance of the DbField class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="tableName">The data table to consider.</param>
        /// <param name="schema">The schema to consider.</param>
        /// <param name="dataModule">The data module to consider.</param>
        /// <param name="valueType">The value type to consider.</param>
        /// <param name="value">The value to consider.</param>
        public static DbField CreateAsScript(
            string name,
            string tableName,
            string schema,
            string dataModule,
            object value,
            DataValueType valueType = DataValueType.None)
        {
            var field = Create(name, tableName, schema, dataModule);
            field.ValueType = DataValueType.Text;
            if (value != null)
                field.Value = value.ToString(valueType).CreateScript();
            return field;
        }

        // As query -----

        /// <summary>
        /// Creates a new instance of the DbField class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="query">The query to consider.</param>
        public static DbField CreateAsQuery(
            string name,
            IDbQuery query)
        {
            return CreateAsQuery(name, null, null, null, query);
        }

        /// <summary>
        /// Creates a new instance of the DbField class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="tableName">The data table to consider.</param>
        /// <param name="query">The query to consider.</param>
        public static DbField CreateAsQuery(
            string name,
            string tableName,
            IDbQuery query)
        {
            return CreateAsQuery(name, tableName, null, null, query);
        }

        /// <summary>
        /// Creates a new instance of the DbField class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="tableName">The data table to consider.</param>
        /// <param name="schema">The schema to consider.</param>
        /// <param name="dataModule">The data module to consider.</param>
        /// <param name="query">The query to consider.</param>
        public static DbField CreateAsQuery(
            string name,
            string tableName,
            string schema,
            string dataModule,
            IDbQuery query)
        {
            var field = Create(name, tableName, schema, dataModule);
            field.ValueType = DataValueType.None;
            field.Query = query as DbQuery;
            return field;
        }

        // As other -----

        /// <summary>
        /// Creates a new instance of the DbField class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="otherField">The other field to consider.</param>
        public static DbField CreateAsOther(
            string name,
            DbField otherField)
        {
            return CreateAsOther(name, null, null, null, otherField);
        }

        /// <summary>
        /// Creates a new instance of the DbField class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="tableName">The data table to consider.</param>
        /// <param name="otherField">The other field to consider.</param>
        public static DbField CreateAsOther(
            string name,
            string tableName,
            DbField otherField)
        {
            return CreateAsOther(name, tableName, null, null, otherField);
        }

        /// <summary>
        /// Creates a new instance of the DbField class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="tableName">The data table to consider.</param>
        /// <param name="schema">The schema to consider.</param>
        /// <param name="dataModule">The data module to consider.</param>
        /// <param name="otherField">The other field to consider.</param>
        public static DbField CreateAsOther(
            string name,
            string tableName,
            string schema,
            string dataModule,
            DbField otherField)
        {
            var field = Create(name, tableName, schema, dataModule);
            field.Value = otherField.ToDataExpression();
            return field;
        }
    }
}
