using BindOpen.Framework.Data.Common;
using BindOpen.Framework.Data.Elements;
using BindOpen.Framework.Data.Expression;
using BindOpen.Framework.Data.Helpers.Objects;
using BindOpen.Framework.Extensions.Carriers;

namespace BindOpen.Framework.Data.Queries
{
    /// <summary>
    /// This static class represents a factory of data field.
    /// </summary>
    public static partial class DbFactory
    {
        /// <summary>
        /// Creates a new instance of the DbField class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="tableName">The data table to consider.</param>
        /// <param name="schema">The schema to consider.</param>
        /// <param name="dataModule">The data module to consider.</param>
        public static DbField CreateField(
            string name = null,
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

        /// <summary>
        /// Creates a new instance of the DbField class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="table">The data table to consider.</param>
        public static DbField CreateField(
            string name,
            DbTable table)
            => CreateField(name, table?.Name, table?.Schema, table?.DataModule);

        // As literal -----

        /// <summary>
        /// Creates a new instance of the DbField class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="value">The value to consider.</param>
        /// <param name="valueType">The value type to consider.</param>
        public static DbField CreateFieldAsLiteral(
            string name,
            object value,
            DataValueType valueType = DataValueType.Any)
        {
            return CreateFieldAsLiteral(name, null, null, null, value, valueType);
        }

        /// <summary>
        /// Creates a new instance of the DbField class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="tableName">The data table to consider.</param>
        /// <param name="valueType">The value type to consider.</param>
        /// <param name="value">The value to consider.</param>
        public static DbField CreateFieldAsLiteral(
            string name,
            string tableName,
            object value,
            DataValueType valueType = DataValueType.None)
        {
            return CreateFieldAsLiteral(name, tableName, null, null, value, valueType);
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
        public static DbField CreateFieldAsLiteral(
            string name,
            string tableName,
            string schema,
            string dataModule,
            object value,
            DataValueType valueType = DataValueType.None)
        {
            var field = CreateField(name, tableName, schema, dataModule);
            field.ValueType = DataValueType.Text;
            if (value != null)
                field.Value = value.ToString(valueType).CreateLiteral();
            return field;
        }

        /// <summary>
        /// Creates a new instance of the DbField class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="table">The data table to consider.</param>
        /// <param name="value">The value to consider.</param>
        /// <param name="valueType">The value type to consider.</param>
        public static DbField CreateFieldAsLiteral(
            string name,
            DbTable table,
            object value,
            DataValueType valueType = DataValueType.None)
            => CreateFieldAsLiteral(name, table?.Name, table?.Schema, table?.DataModule, value, valueType);

        // As script -----

        /// <summary>
        /// Creates a new instance of the DbField class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="value">The value to consider.</param>
        public static DbField CreateFieldAsScript(
            string name,
            string value)
        {
            return CreateFieldAsScript(name, null, null, null, value);
        }

        /// <summary>
        /// Creates a new instance of the DbField class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="tableName">The data table to consider.</param>
        /// <param name="value">The value to consider.</param>
        public static DbField CreateFieldAsScript(
            string name,
            string tableName,
            string value)
        {
            return CreateFieldAsScript(name, tableName, null, null, value);
        }

        /// <summary>
        /// Creates a new instance of the DbField class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="tableName">The data table to consider.</param>
        /// <param name="schema">The schema to consider.</param>
        /// <param name="dataModule">The data module to consider.</param>
        /// <param name="value">The value to consider.</param>
        public static DbField CreateFieldAsScript(
            string name,
            string tableName,
            string schema,
            string dataModule,
            string value)
        {
            var field = CreateField(name, tableName, schema, dataModule);
            field.ValueType = DataValueType.Text;
            if (value != null)
            {
                field.Value = value.CreateScript();
            }
            return field;
        }

        /// <summary>
        /// Creates a new instance of the DbField class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="table">The data table to consider.</param>
        /// <param name="value">The value to consider.</param>
        public static DbField CreateFieldAsScript(
            string name,
            DbTable table,
            string value)
            => CreateFieldAsScript(name, table?.Name, table?.Schema, table?.DataModule, value);

        // As query -----

        /// <summary>
        /// Creates a new instance of the DbField class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="query">The query to consider.</param>
        public static DbField CreateFieldAsQuery(
            string name,
            IDbQuery query)
        {
            return CreateFieldAsQuery(name, null, null, null, query);
        }

        /// <summary>
        /// Creates a new instance of the DbField class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="tableName">The data table to consider.</param>
        /// <param name="query">The query to consider.</param>
        public static DbField CreateFieldAsQuery(
            string name,
            string tableName,
            IDbQuery query)
        {
            return CreateFieldAsQuery(name, tableName, null, null, query);
        }

        /// <summary>
        /// Creates a new instance of the DbField class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="tableName">The data table to consider.</param>
        /// <param name="schema">The schema to consider.</param>
        /// <param name="dataModule">The data module to consider.</param>
        /// <param name="query">The query to consider.</param>
        public static DbField CreateFieldAsQuery(
            string name,
            string tableName,
            string schema,
            string dataModule,
            IDbQuery query)
        {
            var field = CreateField(name, tableName, schema, dataModule);
            field.ValueType = DataValueType.None;
            field.Query = query as DbQuery;
            return field;
        }

        /// <summary>
        /// Creates a new instance of the DbField class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="table">The data table to consider.</param>
        /// <param name="query">The query to consider.</param>
        public static DbField CreateFieldAsQuery(
            string name,
            DbTable table,
            IDbQuery query)
            => CreateFieldAsQuery(name, table?.Name, table?.Schema, table?.DataModule, query);

        // As other -----

        /// <summary>
        /// Creates a new instance of the DbField class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="otherField">The other field to consider.</param>
        public static DbField CreateFieldAsOther(
            string name,
            DbField otherField)
        {
            return CreateFieldAsOther(name, null, null, null, otherField);
        }

        /// <summary>
        /// Creates a new instance of the DbField class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="tableName">The data table to consider.</param>
        /// <param name="otherField">The other field to consider.</param>
        public static DbField CreateFieldAsOther(
            string name,
            string tableName,
            DbField otherField)
        {
            return CreateFieldAsOther(name, tableName, null, null, otherField);
        }

        /// <summary>
        /// Creates a new instance of the DbField class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="tableName">The data table to consider.</param>
        /// <param name="schema">The schema to consider.</param>
        /// <param name="dataModule">The data module to consider.</param>
        /// <param name="otherField">The other field to consider.</param>
        public static DbField CreateFieldAsOther(
            string name,
            string tableName,
            string schema,
            string dataModule,
            DbField otherField)
        {
            var field = CreateField(name, tableName, schema, dataModule);
            field.Value = otherField.ToDataExpression();
            return field;
        }

        /// <summary>
        /// Creates a new instance of the DbField class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="table">The data table to consider.</param>
        /// <param name="otherField">The other field to consider.</param>
        public static DbField CreateFieldAsOther(
            string name,
            DbTable table,
            DbField otherField)
            => CreateFieldAsOther(name, table?.Name, table?.Schema, table?.DataModule, otherField);

        // As All

        /// <summary>
        /// Creates a new instance of the DbField class.
        /// </summary>
        /// <param name="tableName">The data table to consider.</param>
        /// <param name="schema">The schema to consider.</param>
        /// <param name="dataModule">The data module to consider.</param>
        public static DbField CreateFieldAsAll(
            string tableName = null,
            string schema = null,
            string dataModule = null)
        {
            return new DbField()
            {
                DataTable = tableName,
                Schema = schema,
                DataModule = dataModule
            }.AsAll();
        }

        /// <summary>
        /// Creates a new instance of the DbField class.
        /// </summary>
        /// <param name="table">The data table to consider.</param>
        public static DbField CreateFieldAsAll(DbTable table)
            => CreateFieldAsAll(table?.Name, table?.Schema, table?.DataModule);

        // As parameter with name -----

        /// <summary>
        /// Creates a new instance of the DbField class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="parameterName">The parameter element to consider.</param>
        public static DbField CreateFieldAsParameter(
            string name,
            string parameterName)
        {
            return CreateFieldAsParameter(name, null, null, null, parameterName);
        }

        /// <summary>
        /// Creates a new instance of the DbField class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="tableName">The data table to consider.</param>
        /// <param name="parameterName">The parameter element to consider.</param>
        public static DbField CreateFieldAsParameter(
            string name,
            string tableName,
            string parameterName)
        {
            return CreateFieldAsParameter(name, tableName, null, null, parameterName);
        }

        /// <summary>
        /// Creates a new instance of the DbField class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="tableName">The data table to consider.</param>
        /// <param name="schema">The schema to consider.</param>
        /// <param name="dataModule">The data module to consider.</param>
        /// <param name="parameterName">The parameter element to consider.</param>
        public static DbField CreateFieldAsParameter(
            string name,
            string tableName,
            string schema,
            string dataModule,
            string parameterName)
        {
            var field = CreateField(name, tableName, schema, dataModule);
            field.ValueType = DataValueType.None;
            field.Value = CreateParameterString(ElementFactory.CreateScalar(parameterName)).CreateLiteral();
            return field;
        }

        /// <summary>
        /// Creates a new instance of the DbField class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="table">The data table to consider.</param>
        /// <param name="parameterName">The parameter element to consider.</param>
        public static DbField CreateFieldAsParameter(
            string name,
            DbTable table,
            string parameterName)
            => CreateFieldAsParameter(name, table?.Name, table?.Schema, table?.DataModule, parameterName);

        // As parameter with index -----

        /// <summary>
        /// Creates a new instance of the DbField class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="parameterIndex">The parameter index to consider.</param>
        public static DbField CreateFieldAsParameter(
            string name,
            byte parameterIndex)
        {
            return CreateFieldAsParameter(name, null, null, null, parameterIndex);
        }

        /// <summary>
        /// Creates a new instance of the DbField class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="tableName">The data table to consider.</param>
        /// <param name="parameterIndex">The parameter index to consider.</param>
        public static DbField CreateFieldAsParameter(
            string name,
            string tableName,
            byte parameterIndex)
        {
            return CreateFieldAsParameter(name, tableName, null, null, parameterIndex);
        }

        /// <summary>
        /// Creates a new instance of the DbField class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="tableName">The data table to consider.</param>
        /// <param name="schema">The schema to consider.</param>
        /// <param name="dataModule">The data module to consider.</param>
        /// <param name="parameterIndex">The parameter index to consider.</param>
        public static DbField CreateFieldAsParameter(
            string name,
            string tableName,
            string schema,
            string dataModule,
            byte parameterIndex)
        {
            var field = CreateField(name, tableName, schema, dataModule);
            field.ValueType = DataValueType.None;
            field.Value = CreateParameterString(new ScalarElement() { Index = parameterIndex }).CreateLiteral();
            return field;
        }

        /// <summary>
        /// Creates a new instance of the DbField class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="table">The data table to consider.</param>
        /// <param name="parameterIndex">The parameter index to consider.</param>
        public static DbField CreateFieldAsParameter(
            string name,
            DbTable table,
            byte parameterIndex)
            => CreateFieldAsParameter(name, table?.Name, table?.Schema, table?.DataModule, parameterIndex);

        // As parameter with parameter -----

        /// <summary>
        /// Creates a new instance of the DbField class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="parameter">The parameter to consider.</param>
        public static DbField CreateFieldAsParameter(
            string name,
            IDataElement parameter)
        {
            return CreateFieldAsParameter(name, null, null, null, parameter);
        }

        /// <summary>
        /// Creates a new instance of the DbField class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="tableName">The data table to consider.</param>
        /// <param name="parameter">The parameter to consider.</param>
        public static DbField CreateFieldAsParameter(
            string name,
            string tableName,
            IDataElement parameter)
        {
            return CreateFieldAsParameter(name, tableName, null, null, parameter);
        }

        /// <summary>
        /// Creates a new instance of the DbField class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="tableName">The data table to consider.</param>
        /// <param name="schema">The schema to consider.</param>
        /// <param name="dataModule">The data module to consider.</param>
        /// <param name="parameter">The parameter to consider.</param>
        public static DbField CreateFieldAsParameter(
            string name,
            string tableName,
            string schema,
            string dataModule,
            IDataElement parameter)
        {
            var field = CreateField(name, tableName, schema, dataModule);
            field.ValueType = DataValueType.None;
            field.Value = CreateParameterString(parameter).CreateLiteral();
            return field;
        }

        /// <summary>
        /// Creates a new instance of the DbField class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="table">The data table to consider.</param>
        /// <param name="parameter">The parameter to consider.</param>
        public static DbField CreateFieldAsParameter(
            string name,
            DbTable table,
            IDataElement parameter)
            => CreateFieldAsParameter(name, table?.Name, table?.Schema, table?.DataModule, parameter);
    }
}
