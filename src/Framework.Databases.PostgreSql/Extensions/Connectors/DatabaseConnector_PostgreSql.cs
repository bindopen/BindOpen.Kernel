using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.Data.Common;
using BindOpen.Framework.Core.Data.Elements;
using BindOpen.Framework.Core.Extensions.Attributes;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Core.System.Diagnostics.Events;
using BindOpen.Framework.Core.System.Scripting;
using BindOpen.Framework.Databases.Extensions.Connectors;
using BindOpen.Framework.Databases.PostgreSql.Data.Queries.Builders;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace BindOpen.Framework.Databases.PostgreSql.Extensions.Connectors
{
    /// <summary>
    /// This class represents a OleDb database connector.
    /// </summary>
    [BdoConnector(Name = "databases.postgresql$client")]
    public class DatabaseConnector_PostgreSql : DatabaseConnector
    {
        // ------------------------------------------
        // VARIABLES
        // ------------------------------------------

        #region Variables

        private NpgsqlConnection _connection;

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the DatabaseConnector_PostgreSql class.
        /// </summary>
        public DatabaseConnector_PostgreSql() : base()
        {
        }

        /// <summary>
        /// Instantiates a new instance of the DatabaseConnector_PostgreSql class.
        /// </summary>
        /// <param name="name">The name of this instance.</param>
        /// <param name="connectionString">The connection string to consider.</param>
        public DatabaseConnector_PostgreSql(
            string name, string connectionString = null) : base(name, connectionString)
        {
        }

        #endregion

        // ------------------------------------------
        // MANAGEMENT
        // ------------------------------------------

        #region Management

        /// <summary>
        /// Gets the database connection of this instance.
        /// </summary>
        /// <returns>Returns the connection of this instance.</returns>
        public override IDbConnection GetDbConnection()
        {
            return _connection;
        }

        /// <summary>
        /// Updates this instance considering the specified scope.
        /// </summary>
        /// <param name="scope">The scope to consider.</param>
        /// <returns>Returns the database builder.</returns>
        public override void UpdateWithScope(IBdoScope scope)
        {
            QueryBuilder = new DbQueryBuilder_PostgreSql(scope);
        }

        // Open / Close ---------------------------------------

        /// <summary>
        /// Opens an OleDb connection with the specified connection string.
        /// </summary>
        /// <returns>The log of the connection task.</returns>
        public override IBdoLog Open()
        {
            IBdoLog log = new BdoLog();

            if (!log.Append(Check<DatabaseConnector_PostgreSql>(), p => p.HasErrorsOrExceptions()).HasErrorsOrExceptions())
            {
                try
                {
                    // we close the connection if it is opened yet
                    if ((_connection != null) && (_connection.State != ConnectionState.Closed))
                        _connection.Close();

                    // we load the dataset
                    _connection = new NpgsqlConnection(ConnectionString);
                    _connection.Open();
                }
                catch (Exception ex)
                {
                    log?.AddCheckpoint(
                        "Trying to open the following connection string: '" + ConnectionString + "'");
                    log?.AddException(ex);
                }
            }

            return log;
        }

        /// <summary>
        /// Closes the current OleDb connection.
        /// </summary>
        /// <returns>The log of the connection-closing task.</returns>
        public override IBdoLog Close()
        {
            IBdoLog log = new BdoLog();

            try
            {
                _connection?.Close();
            }
            catch (Exception ex)
            {
                log?.AddCheckpoint("Closing the current connection.");
                log?.AddException(ex);
            }

            return log;
        }

        /// <summary>
        /// Indicates whether the instance is connected.
        /// </summary>
        public override Boolean IsConnected()
        {
            return (_connection != null) && (_connection.State == ConnectionState.Closed);
        }

        // Execution non query  ---------------------------------------

        /// <summary>
        /// Executes a database data query that returns nothing.
        /// </summary>
        /// <param name="queryText">The text to execute.</param>
        /// <param name="scriptVariableSet">The interpretation variables to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <returns>The log of the data query execution task.</returns>
        public override void ExecuteNonQuery(
            string queryText,
            IBdoScriptVariableSet scriptVariableSet = null,
            IBdoLog log = null)
        {
            if (_connection == null)
            {
                log?.AddEvent(new BdoLogEvent(EventKinds.Error) { ResultCode = "DBCONNECTION_NOTINITIALIZED" });
            }
            else
            {
                NpgsqlCommand command = null; // OleDb command used to access the Platform database

                try
                {
                    command = _connection.CreateCommand();
                    command.CommandText = queryText;
                    command.CommandType = CommandType.Text;

                    log?.Detail.AddElement(
                        ElementFactory.CreateScalar(
                            "lineNumber",
                            DataValueType.Integer,
                            command.ExecuteNonQuery().ToString()));
                }
                catch (Exception ex)
                {
                    log?.AddCheckpoint("Trying to execute the following query: '" + queryText + "'");
                    log?.AddException(ex);
                }
            }
        }

        // Execution reader  ---------------------------------------

        /// <summary>
        /// Executes a database data query putting the result into a OleDb Data Reader.
        /// </summary>
        /// <param name="queryText">The text to execute.</param>
        /// <param name="dataReader">The output OleDb Data Reader.</param>
        /// <param name="scriptVariableSet">The interpretation variables to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <returns>The log of the data query execution task.</returns>
        public override void ExecuteQuery(
            string queryText,
            ref IDataReader dataReader,
            IBdoScriptVariableSet scriptVariableSet = null,
            IBdoLog log = null)
        {
            if (_connection == null)
            {
                log?.AddEvent(new BdoLogEvent(EventKinds.Error) { ResultCode = "DBCONNECTION_NOTINITIALIZED" });
            }
            else
            {
                NpgsqlCommand command = null;

                try
                {
                    command = _connection.CreateCommand();
                    command.CommandText = queryText;
                    command.CommandType = CommandType.Text;
                    dataReader = command.ExecuteReader();
                }
                catch (Exception ex)
                {
                    log?.AddCheckpoint("Trying to execute the following query: '" + queryText + "'");
                    log?.AddException(ex);
                }
            }
        }

        // Execution query dataset  ---------------------------------------

        /// <summary>
        /// Executes a database data query putting the result into a dataset.
        /// </summary>
        /// <param name="queryText">The text to execute.</param>
        /// <param name="dataSet">The output dataset.</param>
        /// <param name="scriptVariableSet">The interpretation variables to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <returns>The log of the data query execution task.</returns>
        public override void ExecuteQuery(
            string queryText,
            ref DataSet dataSet,
            IBdoScriptVariableSet scriptVariableSet = null,
            IBdoLog log = null)
        {
            if (_connection == null)
            {
                log?.AddEvent(new BdoLogEvent(EventKinds.Error) { ResultCode = "DBCONNECTION_NOTINITIALIZED" });
            }
            else
            {
                try
                {
                    NpgsqlDataAdapter dataAdapter = new NpgsqlDataAdapter(queryText, _connection);

                    dataSet = new DataSet();
                    dataAdapter.Fill(dataSet, "TABLE");
                }
                catch (Exception ex)
                {
                    log?.AddCheckpoint("Trying to execute the following query: '" + queryText + "'");
                    log?.AddException(ex);
                }
            }
        }

        // Other -----------------------------------

        /// <summary>
        /// Retrieves the identity of the recently-added record and puts the value in the specified ID.
        /// </summary>
        /// <param name="id">The output ID.</param>
        /// <param name="log">The log to consider.</param>
        /// <returns>The log of the task.</returns>
        public override void GetIdentity(
            ref long id,
            IBdoLog log = null)
        {
            if (_connection == null)
            {
                log?.AddEvent(new BdoLogEvent(EventKinds.Error) { ResultCode = "DBCONNECTION_NOTINITIALIZED" });
            }
            else
            {
                NpgsqlCommand command = null; // OleDb command used to access the Platform database           

                try
                {
                    // retrieve all the alias platform users
                    command = _connection.CreateCommand();
                    command.CommandText = "SELECT @@IDENTITY;";
                    command.CommandType = CommandType.Text;
                    id = int.Parse(command.ExecuteScalar().ToString());
                }
                catch (Exception ex)
                {
                    log?.AddException(ex);
                }
            }
        }

        /// <summary>
        /// Executes the specified data query and updates the specified data table.
        /// </summary>
        /// <param name="queryText">The text to execute.</param>
        /// <param name="dataTable">The data table to update.</param>
        /// <param name="log">The log to consider.</param>
        /// <returns>The log of the task.</returns>
        public override void UpdateDataTable(
            string queryText,
            DataTable dataTable,
            IBdoLog log = null)
        {
            if (dataTable == null)
                return;

            if (_connection == null)
            {
                log?.AddEvent(new BdoLogEvent(EventKinds.Error) { ResultCode = "DBCONNECTION_NOTINITIALIZED" });
            }
            else
            {
                try
                {
                    NpgsqlDataAdapter dataAdapter = new NpgsqlDataAdapter(queryText, _connection);
                    dataAdapter.Fill(dataTable);
                }
                catch (Exception ex)
                {
                    log?.AddCheckpoint("Trying to execute the following query: '" + queryText + "'");
                    log?.AddException(ex);
                }
            }
        }

        /// <summary>
        /// Executes the specified data query and updates the specified data table.
        /// </summary>
        /// <param name="queryText">The text to execute.</param>
        /// <param name="dataSet">The data set to update.</param>
        /// <param name="tableNames">The table names to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <returns>The log of the task.</returns>
        public override void UpdateDataSet(
            string queryText,
            DataSet dataSet,
            List<string> tableNames,
            IBdoLog log = null)
        {
            if (dataSet == null)
                return;

            if (_connection == null)
            {
                log?.AddEvent(new BdoLogEvent(EventKinds.Error) { ResultCode = "DBCONNECTION_NOTINITIALIZED" });
            }
            else
            {
                try
                {
                    NpgsqlDataAdapter dataAdapter = new NpgsqlDataAdapter(queryText, _connection);
                    foreach (string tableName in tableNames)
                        dataAdapter.Fill(dataSet, tableName);
                }
                catch (Exception ex)
                {
                    log?.AddCheckpoint("Trying to execute the following query: '" + queryText + "'");
                    log?.AddException(ex);
                }
            }
        }

        /// <summary>
        /// Retrieves the columns of the specified table.
        /// </summary>
        /// <param name="dataModuleName">The name of the table's data module.</param>
        /// <param name="ownerName">The owner of the table.</param>
        /// <param name="tableName">The name of the table.</param>
        /// <param name="log">The log to consider.</param>
        /// <returns>The columns of the table with the specified name.</returns>
        public List<string> GetTableColumns(
            string dataModuleName,
            string ownerName,
            string tableName,
            IBdoLog log = null)
        {
            DataTable dataTable = GetTableColumnsDataTable(
                dataModuleName,
                ownerName,
                tableName);

            List<string> strings = new List<string>();
            if (dataTable != null)
            {
                // we sort the columns by index
                List<DataRow> rows = (from DataRow dataRow in dataTable.Rows
                                      select dataRow).OrderBy(x => x["ORDINAL_POSITION"]).ToList();

                foreach (DataRow currentDataRow in rows)
                    strings.Add(currentDataRow["COLUMN_NAME"].ToString());
            }
            return strings;
        }

        /// <summary>
        /// Retrieves the data table containing the schema of the specified table.
        /// </summary>
        /// <param name="dataModuleName">The name of the table's data module.</param>
        /// <param name="ownerName">The owner of the table.</param>
        /// <param name="tableName">The name of the table.</param>
        /// <param name="log">The log to consider.</param>
        /// <returns>The data table containing the schema of the specified table.</returns>
        public DataTable GetTableColumnsDataTable(
            string dataModuleName,
            string ownerName,
            string tableName,
            IBdoLog log = null)
        {
            DataTable dataTable = null;
            try
            {
                // Get the DataTable with all the info
                dataTable = _connection.GetSchema();
                //.GetOleDbSchemaTable(OleDbSchemaGuid.Columns,
                //                new object[] { dataModuleName, ownerName, tableName, null });
            }
            finally
            {
            }
            return dataTable;
        }

        /// <summary>
        /// Retrieves the tables of the specified table.
        /// </summary>
        /// <param name="dataModuleName">The name of the table's data module.</param>
        /// <param name="ownerName">The owner of the table.</param>
        /// <param name="log">The log to consider.</param>
        /// <returns>The tables of the table with the specified name.</returns>
        public List<string> GetTables(
            string dataModuleName,
            string ownerName,
            IBdoLog log = null)
        {
            DataTable dataTable = GetTableDataTable(dataModuleName, ownerName);

            List<string> strings = new List<string>();
            if (dataTable != null)
            {
                foreach (DataRow currentDataRow in dataTable.Rows)
                    strings.Add(currentDataRow["TABLE_NAME"].ToString());
            }
            return strings;
        }

        /// <summary>
        /// Retrieves the data table containing the schema of the specified table.
        /// </summary>
        /// <param name="dataModuleName">The name of the table's data module.</param>
        /// <param name="ownerName">The owner of the table.</param>
        /// <param name="log">The log to consider.</param>
        /// <returns>The data table containing the schema of the specified table.</returns>
        public DataTable GetTableDataTable(
            string dataModuleName,
            string ownerName,
            IBdoLog log = null)
        {
            DataTable dataTable = null;
            try
            {
                // Get the DataTable with all the info
                dataTable = _connection.GetSchema();
                //.GetOleDbSchemaTable(OleDbSchemaGuid.Tables,
                //                new object[] { dataModuleName, ownerName, null, null });
            }
            finally
            {
            }

            return dataTable;
        }

        /// <summary>
        /// Retrieves the data table containing the schema of the table with the specified name.
        /// </summary>
        /// <param name="dataModuleName">The name of the table's data module.</param>
        /// <param name="ownerName">The owner of the table.</param>
        /// <param name="tableName">The name of the table.</param>
        /// <param name="log">The log to consider.</param>
        /// <returns>The data table containing the schema of the table with the specified name.</returns>
        public DataTable GetPrimaryKeysTable(
            string dataModuleName,
            string ownerName,
            string tableName,
            IBdoLog log = null)
        {
            DataTable dataTable = null;
            try
            {
                // Get the DataTable with all the info
                dataTable = _connection.GetSchema();
                //dataTable = _OleDbConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Primary_Keys,
                //                new object[] { dataModuleName, ownerName, tableName });
            }
            finally
            {
            }
            return dataTable;
        }

        #endregion
    }
}
