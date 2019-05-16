using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.Data.Common;
using BindOpen.Framework.Core.Data.Elements;
using BindOpen.Framework.Core.Extensions.Attributes;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Core.System.Diagnostics.Events;
using BindOpen.Framework.Core.System.Scripting;
using BindOpen.Framework.Databases.Extensions.Connectors;
using BindOpen.Framework.Databases.MSSqlServer.Data.Queries.Builders;

namespace BindOpen.Framework.Databases.MSSqlServer.Extensions.Connectors
{
    /// <summary>
    /// This class represents a OleDb database connector.
    /// </summary>
    [Connector(Name= "database.mssqlserver$msSqlServer")]
    public class DatabaseConnector_MSSqlServer : DatabaseConnector
    {
        // ------------------------------------------
        // VARIABLES
        // ------------------------------------------

        #region Variables

        private SqlConnection _connection;

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the DatabaseConnector_MSSqlServer class.
        /// </summary>
        public DatabaseConnector_MSSqlServer() : base()
        {
        }

        /// <summary>
        /// Instantiates a new instance of the DatabaseConnector_MSSqlServer class.
        /// </summary>
        /// <param name="name">The name of this instance.</param>
        /// <param name="connectionString">The connection string to consider.</param>
        public DatabaseConnector_MSSqlServer(
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
            return this._connection;
        }

        /// <summary>
        /// Sets the database builder of this instance.
        /// </summary>
        /// <param name="appScope">The application scope to consider.</param>
        /// <returns>Returns the database builder.</returns>
        public override void SetQueryBuilder(AppScope appScope)
        {
            this.QueryBuilder = new DbQueryBuilder_MSSqlServer(appScope);
        }

        // Open / Close ---------------------------------------

        /// <summary>
        /// Opens an OleDb connection with the specified connection string.
        /// </summary>
        /// <returns>The log of the connection task.</returns>
        public override ILog Open()
        {
            ILog log = new Log();

            if (!log.Append(this.Check<DatabaseConnector_MSSqlServer>(), p => p.HasErrorsOrExceptions()).HasErrorsOrExceptions())
            {
                try
                {
                    // we close the connection if it is opened yet
                    if ((this._connection != null) && (this._connection.State != ConnectionState.Closed))
                        this._connection.Close();

                    // we load the dataset
                    this._connection = new SqlConnection(this.ConnectionString);
                    this._connection.Open();
                }
                catch (Exception ex)
                {
                    log.AddCheckpoint(
                        "Trying to open the following connection string: '" + this.ConnectionString + "'");
                    log.AddException(ex);
                }
            }

            return log;
        }

        /// <summary>
        /// Closes the current OleDb connection.
        /// </summary>
        /// <returns>The log of the connection-closing task.</returns>
        public override ILog Close()
        {
            ILog log = new Log();

            try
            {
                this._connection?.Close();
            }
            catch (Exception ex)
            {
                log.AddCheckpoint("Closing the current connection.");
                log.AddException(ex);
            }

            return log;
        }

        /// <summary>
        /// Indicates whether the instance is connected.
        /// </summary>
        public override Boolean IsConnected()
        {
            return (this._connection != null) && (this._connection.State == ConnectionState.Closed);
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
            String queryText,
            IScriptVariableSet scriptVariableSet = null,
            ILog log = null)
        {
            log = log ?? new Log();

            if (this._connection == null)
            {
                log.AddEvent(new LogEvent(EventKinds.Error) { ResultCode = "DBCONNECTION_NOTINITIALIZED" });
            }
            else
            {
                SqlCommand command = null; // OleDb command used to access the Platform database

                try
                {
                    if (!log.HasErrorsOrExceptions())
                    {
                        command = this._connection.CreateCommand();
                        command.CommandText = queryText;
                        command.CommandType = CommandType.Text;

                        log.Detail.AddElement(
                            ElementFactory.CreateScalar(
                                "lineNumber",
                                DataValueType.Integer,
                                command.ExecuteNonQuery().ToString()));
                    }
                }
                catch (Exception ex)
                {
                    log.AddCheckpoint("Trying to execute the following query: '" + queryText + "'");
                    log.AddException(ex);
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
        public void ExecuteQuery(
            String queryText,
            ref SqlDataReader dataReader,
            IScriptVariableSet scriptVariableSet = null,
            ILog log = null)
        {
            log = log ?? new Log();

            if (this._connection == null)
            {
                log.AddEvent(new LogEvent(EventKinds.Error) { ResultCode = "DBCONNECTION_NOTINITIALIZED" });
            }
            else
            {
                SqlCommand command = null;

                try
                {
                    if (!log.HasErrorsOrExceptions())
                    {
                        command = this._connection.CreateCommand();
                        command.CommandText = queryText;
                        command.CommandType = CommandType.Text;
                        dataReader = command.ExecuteReader();
                    }
                }
                catch (Exception ex)
                {
                    log.AddCheckpoint("Trying to execute the following query: '" + queryText + "'");
                    log.AddException(ex);
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
            String queryText,
            ref DataSet dataSet,
            IScriptVariableSet scriptVariableSet = null,
            ILog log = null)
        {
            log = log ?? new Log();

            if (this._connection == null)
            {
                log.AddEvent(new LogEvent(EventKinds.Error) { ResultCode = "DBCONNECTION_NOTINITIALIZED" });
            }
            else
            {
                try
                {
                    if (!log.HasErrorsOrExceptions())
                    {
                        SqlDataAdapter dataAdapter = new SqlDataAdapter(queryText, this._connection);

                        dataSet = new DataSet();
                        dataAdapter.Fill(dataSet, "TABLE");
                    }
                }
                catch (Exception ex)
                {
                    log.AddCheckpoint("Trying to execute the following query: '" + queryText + "'");
                    log.AddException(ex);
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
            ILog log = null)
        {
            log = log ?? new Log();

            if (this._connection == null)
            {
                log.AddEvent(new LogEvent(EventKinds.Error) { ResultCode="DBCONNECTION_NOTINITIALIZED" });
            }
            else
            {
                SqlCommand command = null; // OleDb command used to access the Platform database           

                try
                {
                    // retrieve all the alias platform users
                    command = this._connection.CreateCommand();
                    command.CommandText = "SELECT @@IDENTITY;";
                    command.CommandType = CommandType.Text;
                    id = int.Parse(command.ExecuteScalar().ToString());
                }
                catch (Exception ex)
                {
                    log.AddException(ex);
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
            String queryText,
            DataTable dataTable,
            ILog log = null)
        {
            log = log ?? new Log();

            if (dataTable == null)
                return;

            if (this._connection == null)
            {
                log.AddEvent(new LogEvent(EventKinds.Error) { ResultCode="DBCONNECTION_NOTINITIALIZED" });
            }
            else
            {
                try
                {
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(queryText, this._connection);
                    dataAdapter.Fill(dataTable);
                }
                catch (Exception ex)
                {
                    log.AddCheckpoint("Trying to execute the following query: '" + queryText + "'");
                    log.AddException(ex);
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
            String queryText,
            DataSet dataSet,
            List<string> tableNames,
            ILog log = null)
        {
            log = log ?? new Log();

            if (dataSet == null)
                return;

            if (this._connection == null)
            {
                log.AddEvent(new LogEvent(EventKinds.Error) { ResultCode="DBCONNECTION_NOTINITIALIZED" });
            }
            else
            {
                try
                {
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(queryText, this._connection);
                    foreach (String tableName in tableNames)
                        dataAdapter.Fill(dataSet, tableName);
                }
                catch (Exception ex)
                {
                    log.AddCheckpoint("Trying to execute the following query: '" + queryText + "'");
                    log.AddException(ex);
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
            String dataModuleName,
            String ownerName,
            String tableName,
            ILog log = null)
        {
            DataTable dataTable = this.GetTableColumnsDataTable(
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
            ILog log = null)
        {
            DataTable dataTable = null;
            try
            {
                // Get the DataTable with all the info
                dataTable = this._connection.GetSchema();
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
            String dataModuleName,
            String ownerName,
            ILog log = null)
        {
            DataTable dataTable = this.GetTableDataTable(dataModuleName, ownerName);

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
            String dataModuleName,
            String ownerName,
            ILog log = null)
        {
            DataTable dataTable = null;
            try
            {
                // Get the DataTable with all the info
                dataTable = this._connection.GetSchema();
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
            ILog log = null)
        {
            DataTable dataTable = null;
            try
            {
                // Get the DataTable with all the info
                dataTable = this._connection.GetSchema();
                //dataTable = this._OleDbConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Primary_Keys,
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
