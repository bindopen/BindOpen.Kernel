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
    public static partial class DbFluent
    {
        /// <summary>
        /// Creates a new instance of the DbField class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="tableName">The data table to consider.</param>
        /// <param name="schema">The schema to consider.</param>
        /// <param name="dataModule">The data module to consider.</param>
        public static DbField Field(
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
        public static DbField Field(
            string name,
            DbTable table)
            => Field(name, table?.Name, table?.Schema, table?.DataModule);

        // As literal -----

        /// <summary>
        /// Creates a new instance of the DbField class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="value">The value to consider.</param>
        /// <param name="valueType">The value type to consider.</param>
        public static DbField FieldAsLiteral(
            string name,
            object value,
            DataValueType valueType = DataValueType.Any)
        {
            return FieldAsLiteral(name, null, null, null, value, valueType);
        }

        /// <summary>
        /// Creates a new instance of the DbField class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="tableName">The data table to consider.</param>
        /// <param name="valueType">The value type to consider.</param>
        /// <param name="value">The value to consider.</param>
        public static DbField FieldAsLiteral(
            string name,
            string tableName,
            object value,
            DataValueType valueType = DataValueType.None)
        {
            return FieldAsLiteral(name, tableName, null, null, value, valueType);
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
        public static DbField FieldAsLiteral(
            string name,
            string tableName,
            string schema,
            string dataModule,
            object value,
            DataValueType valueType = DataValueType.None)
        {
            var field = Field(name, tableName, schema, dataModule);
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
        public static DbField FieldAsLiteral(
            string name,
            DbTable table,
            object value,
            DataValueType valueType = DataValueType.None)
            => FieldAsLiteral(name, table?.Name, table?.Schema, table?.DataModule, value, valueType);

        // As script -----

        /// <summary>
        /// Creates a new instance of the DbField class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="value">The value to consider.</param>
        public static DbField FieldAsScript(
            string name,
            string value)
        {
            return FieldAsScript(name, null, null, null, value);
        }

        /// <summary>
        /// Creates a new instance of the DbField class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="tableName">The data table to consider.</param>
        /// <param name="value">The value to consider.</param>
        public static DbField FieldAsScript(
            string name,
            string tableName,
            string value)
        {
            return FieldAsScript(name, tableName, null, null, value);
        }

        /// <summary>
        /// Creates a new instance of the DbField class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="tableName">The data table to consider.</param>
        /// <param name="schema">The schema to consider.</param>
        /// <param name="dataModule">The data module to consider.</param>
        /// <param name="value">The value to consider.</param>
        public static DbField FieldAsScript(
            string name,
            string tableName,
            string schema,
            string dataModule,
            string value)
        {
            var field = Field(name, tableName, schema, dataModule);
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
        public static DbField FieldAsScript(
            string name,
            DbTable table,
            string value)
            => FieldAsScript(name, table?.Name, table?.Schema, table?.DataModule, value);

        // As query -----

        /// <summary>
        /// Creates a new instance of the DbField class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="query">The query to consider.</param>
        public static DbField FieldAsQuery(
            string name,
            IDbQuery query)
        {
            return FieldAsQuery(name, null, null, null, query);
        }

        /// <summary>
        /// Creates a new instance of the DbField class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="tableName">The data table to consider.</param>
        /// <param name="query">The query to consider.</param>
        public static DbField FieldAsQuery(
            string name,
            string tableName,
            IDbQuery query)
        {
            return FieldAsQuery(name, tableName, null, null, query);
        }

        /// <summary>
        /// Creates a new instance of the DbField class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="tableName">The data table to consider.</param>
        /// <param name="schema">The schema to consider.</param>
        /// <param name="dataModule">The data module to consider.</param>
        /// <param name="query">The query to consider.</param>
        public static DbField FieldAsQuery(
            string name,
            string tableName,
            string schema,
            string dataModule,
            IDbQuery query)
        {
            var field = Field(name, tableName, schema, dataModule);
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
        public static DbField FieldAsQuery(
            string name,
            DbTable table,
            IDbQuery query)
            => FieldAsQuery(name, table?.Name, table?.Schema, table?.DataModule, query);

        // As other -----

        /// <summary>
        /// Creates a new instance of the DbField class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="otherField">The other field to consider.</param>
        public static DbField FieldAsOther(
            string name,
            DbField otherField)
        {
            return FieldAsOther(name, null, null, null, otherField);
        }

        /// <summary>
        /// Creates a new instance of the DbField class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="tableName">The data table to consider.</param>
        /// <param name="otherField">The other field to consider.</param>
        public static DbField FieldAsOther(
            string name,
            string tableName,
            DbField otherField)
        {
            return FieldAsOther(name, tableName, null, null, otherField);
        }

        /// <summary>
        /// Creates a new instance of the DbField class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="tableName">The data table to consider.</param>
        /// <param name="schema">The schema to consider.</param>
        /// <param name="dataModule">The data module to consider.</param>
        /// <param name="otherField">The other field to consider.</param>
        public static DbField FieldAsOther(
            string name,
            string tableName,
            string schema,
            string dataModule,
            DbField otherField)
        {
            var field = Field(name, tableName, schema, dataModule);
            field.Value = otherField.ToDataExpression();
            return field;
        }

        /// <summary>
        /// Creates a new instance of the DbField class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="table">The data table to consider.</param>
        /// <param name="otherField">The other field to consider.</param>
        public static DbField FieldAsOther(
            string name,
            DbTable table,
            DbField otherField)
            => FieldAsOther(name, table?.Name, table?.Schema, table?.DataModule, otherField);

        // As All

        /// <summary>
        /// Creates a new instance of the DbField class.
        /// </summary>
        /// <param name="tableName">The data table to consider.</param>
        /// <param name="schema">The schema to consider.</param>
        /// <param name="dataModule">The data module to consider.</param>
        public static DbField FieldAsAll(
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
        public static DbField FieldAsAll(DbTable table)
            => FieldAsAll(table?.Name, table?.Schema, table?.DataModule);

        // As parameter with name -----

        /// <summary>
        /// Creates a new instance of the DbField class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="parameterName">The parameter element to consider.</param>
        public static DbField FieldAsParameter(
            string name,
            string parameterName)
        {
            return FieldAsParameter(name, null, null, null, parameterName);
        }

        /// <summary>
        /// Creates a new instance of the DbField class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="tableName">The data table to consider.</param>
        /// <param name="parameterName">The parameter element to consider.</param>
        public static DbField FieldAsParameter(
            string name,
            string tableName,
            string parameterName)
        {
            return FieldAsParameter(name, tableName, null, null, parameterName);
        }

        /// <summary>
        /// Creates a new instance of the DbField class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="tableName">The data table to consider.</param>
        /// <param name="schema">The schema to consider.</param>
        /// <param name="dataModule">The data module to consider.</param>
        /// <param name="parameterName">The parameter element to consider.</param>
        public static DbField FieldAsParameter(
            string name,
            string tableName,
            string schema,
            string dataModule,
            string parameterName)
        {
            var field = Field(name, tableName, schema, dataModule);
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
        public static DbField FieldAsParameter(
            string name,
            DbTable table,
            string parameterName)
            => FieldAsParameter(name, table?.Name, table?.Schema, table?.DataModule, parameterName);

        // As parameter with index -----

        /// <summary>
        /// Creates a new instance of the DbField class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="parameterIndex">The parameter index to consider.</param>
        public static DbField FieldAsParameter(
            string name,
            byte parameterIndex)
        {
            return FieldAsParameter(name, null, null, null, parameterIndex);
        }

        /// <summary>
        /// Creates a new instance of the DbField class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="tableName">The data table to consider.</param>
        /// <param name="parameterIndex">The parameter index to consider.</param>
        public static DbField FieldAsParameter(
            string name,
            string tableName,
            byte parameterIndex)
        {
            return FieldAsParameter(name, tableName, null, null, parameterIndex);
        }

        /// <summary>
        /// Creates a new instance of the DbField class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="tableName">The data table to consider.</param>
        /// <param name="schema">The schema to consider.</param>
        /// <param name="dataModule">The data module to consider.</param>
        /// <param name="parameterIndex">The parameter index to consider.</param>
        public static DbField FieldAsParameter(
            string name,
            string tableName,
            string schema,
            string dataModule,
            byte parameterIndex)
        {
            var field = Field(name, tableName, schema, dataModule);
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
        public static DbField FieldAsParameter(
            string name,
            DbTable table,
            byte parameterIndex)
            => FieldAsParameter(name, table?.Name, table?.Schema, table?.DataModule, parameterIndex);

        // As parameter with parameter -----

        /// <summary>
        /// Creates a new instance of the DbField class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="parameter">The parameter to consider.</param>
        public static DbField FieldAsParameter(
            string name,
            IDataElement parameter)
        {
            return FieldAsParameter(name, null, null, null, parameter);
        }

        /// <summary>
        /// Creates a new instance of the DbField class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="tableName">The data table to consider.</param>
        /// <param name="parameter">The parameter to consider.</param>
        public static DbField FieldAsParameter(
            string name,
            string tableName,
            IDataElement parameter)
        {
            return FieldAsParameter(name, tableName, null, null, parameter);
        }

        /// <summary>
        /// Creates a new instance of the DbField class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="tableName">The data table to consider.</param>
        /// <param name="schema">The schema to consider.</param>
        /// <param name="dataModule">The data module to consider.</param>
        /// <param name="parameter">The parameter to consider.</param>
        public static DbField FieldAsParameter(
            string name,
            string tableName,
            string schema,
            string dataModule,
            IDataElement parameter)
        {
            var field = Field(name, tableName, schema, dataModule);
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
        public static DbField FieldAsParameter(
            string name,
            DbTable table,
            IDataElement parameter)
            => FieldAsParameter(name, table?.Name, table?.Schema, table?.DataModule, parameter);
    }
}
