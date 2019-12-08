using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Core.System.Scripting;
using BindOpen.Framework.Databases.Data.Queries;
using System.Collections.Generic;
using System.Data;

namespace BindOpen.Framework.Databases.Data.Connections
{
    /// <summary>
    /// This class proposes extensions for database connection.
    /// </summary>
    public static class BdoDbConnections
    {
        /// <summary>
        /// Executes a database data query that returns nothing.
        /// </summary>
        /// <param name="connection">The connection to consider.</param>
        /// <param name="queryText">The text to execute.</param>
        /// <param name="scriptVariableSet">The interpretation variables to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <returns>The log of the data query execution task.</returns>
        public static void ExecuteNonQuery(
            this IBdoDbConnection connection,
            string queryText,
            IBdoScriptVariableSet scriptVariableSet = null,
            IBdoLog log = null)
        {
            connection?.Connector?.ExecuteNonQuery(queryText, scriptVariableSet, log);
        }

        /// <summary>
        /// Executes a database data query putting the result into a data reader.
        /// </summary>
        /// <param name="connection">The connection to consider.</param>
        /// <param name="query">The data query to execute.</param>
        /// <param name="scriptVariableSet">The interpretation variables to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <returns>The log of the data query execution task.</returns>
        public static void ExecuteQuery(
            this IBdoDbConnection connection,
            IDbQuery query,
            IBdoScriptVariableSet scriptVariableSet = null,
            IBdoLog log = null)
        {
            if (connection != null && query != null)
            {
                // retrieve all the alias platform users
                string queryText = connection.Connector?.GetSqlText(query, scriptVariableSet, log);
                if (!log.HasErrorsOrExceptions())
                    connection.ExecuteNonQuery(queryText, scriptVariableSet, log);
            }
        }

        // Execution query data reader  ---------------------------------------

        /// <summary>
        /// Executes a database data query putting the result into a data reader.
        /// </summary>
        /// <param name="connection">The connection to consider.</param>
        /// <param name="queryText">The text to execute.</param>
        /// <param name="dataReader">The output data reader.</param>
        /// <param name="scriptVariableSet">The interpretation variables to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <returns>The log of the data query execution task.</returns>
        public static void ExecuteQuery(
            this IBdoDbConnection connection,
            string queryText,
            ref IDataReader dataReader,
            IBdoScriptVariableSet scriptVariableSet = null,
            IBdoLog log = null)
        {
            if (connection?.Connector != null)
            {
                connection.Connector.ExecuteQuery(queryText, ref dataReader, scriptVariableSet, log);
            }
        }

        /// <summary>
        /// Executes a database data query putting the result into a dataset.
        /// </summary>
        /// <param name="connection">The connection to consider.</param>
        /// <param name="query">The database data query to execute.</param>
        /// <param name="dataReader">The output data reader.</param>
        /// <param name="scriptVariableSet">The interpretation variables to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <returns>The log of the data query execution task.</returns>
        public static void ExecuteQuery(
            this IBdoDbConnection connection,
            IDbQuery query,
            ref IDataReader dataReader,
            IBdoScriptVariableSet scriptVariableSet = null,
            IBdoLog log = null)
        {
            if (connection != null && query != null)
            {
                // retrieve all the alias platform users
                string queryText = connection.Connector?.GetSqlText(query, scriptVariableSet, log);
                if (!log.HasErrorsOrExceptions())
                    connection.ExecuteQuery(queryText, ref dataReader, scriptVariableSet, log);
            }
        }

        // Execution query dataset  ---------------------------------------

        /// <summary>
        /// Executes a database data query putting the result into a dataset.
        /// </summary>
        /// <param name="connection">The connection to consider.</param>
        /// <param name="queryText">The text to execute.</param>
        /// <param name="dataSet">The output dataset.</param>
        /// <param name="scriptVariableSet">The interpretation variables to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <returns>The log of the data query execution task.</returns>
        public static void ExecuteQuery(
            this IBdoDbConnection connection,
            string queryText,
            ref DataSet dataSet,
            IBdoScriptVariableSet scriptVariableSet = null,
            IBdoLog log = null)
        {
            connection?.Connector?.ExecuteQuery(queryText, ref dataSet, scriptVariableSet, log);
        }

        /// <summary>
        /// Executes a database data query putting the result into a dataset.
        /// </summary>
        /// <param name="connection">The connection to consider.</param>
        /// <param name="query">The database data query to execute.</param>
        /// <param name="dataSet">The output dataset.</param>
        /// <param name="scriptVariableSet">The interpretation variables to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <returns>The log of the data query execution task.</returns>
        public static void ExecuteQuery(
            this IBdoDbConnection connection,
            IDbQuery query,
            ref DataSet dataSet,
            IBdoScriptVariableSet scriptVariableSet = null,
            IBdoLog log = null)
        {
            if (query != null)
            {
                // retrieve all the alias platform users
                string queryText = connection.Connector?.GetSqlText(query, scriptVariableSet, log);
                if (!log.HasErrorsOrExceptions())
                    connection.ExecuteQuery(queryText, ref dataSet, scriptVariableSet, log);
            }
        }

        // Table ---------------------------------------

        /// <summary>
        /// Gets the identity of the last inserted item
        /// </summary>
        /// <param name="connection">The connection to consider.</param>
        /// <param name="id">The long identifier to populate.</param>
        /// <param name="log">The log to consider.</param>
        public static void GetIdentity(
            this IBdoDbConnection connection,
            ref long id,
            IBdoLog log = null)
        {
            connection?.Connector?.GetIdentity(ref id, log);
        }

        /// <summary>
        /// Executes the specified data query and updates the specified data table.
        /// </summary>
        /// <param name="connection">The connection to consider.</param>
        /// <param name="queryText">The text to execute.</param>
        /// <param name="dataTable">The data table to update.</param>
        /// <param name="log">The log to consider.</param>
        /// <returns>The log of the task.</returns>
        public static void UpdateDataTable(
            this IBdoDbConnection connection,
            string queryText,
            DataTable dataTable,
            IBdoLog log = null)
        {
            connection?.Connector?.UpdateDataTable(queryText, dataTable, log);
        }

        /// <summary>
        /// Executes the specified data query and updates the specified data table.
        /// </summary>
        /// <param name="connection">The connection to consider.</param>
        /// <param name="query">The data query to execute.</param>
        /// <param name="dataTable">The data table to update.</param>
        /// <param name="log">The log to consider.</param>
        /// <returns>The log of the task.</returns>
        public static void UpdateDataTable(
            this IBdoDbConnection connection,
            IDbQuery query,
            DataTable dataTable,
            IBdoLog log = null)
        {
            if (connection != null && query != null)
            {
                // retrieve all the alias platform users
                string queryText = connection.Connector?.GetSqlText(query, null, log);
                if (!log.HasErrorsOrExceptions())
                    connection.UpdateDataTable(queryText, dataTable, log);
            }
        }

        /// <summary>
        /// Executes the specified data query and updates the specified data table.
        /// </summary>
        /// <param name="connection">The connection to consider.</param>
        /// <param name="queryText">The text to execute.</param>
        /// <param name="dataSet">The data set to update.</param>
        /// <param name="tableNames">The table names to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <returns>The log of the task.</returns>
        public static void UpdateDataSet(
            this IBdoDbConnection connection,
            string queryText,
            DataSet dataSet,
            List<string> tableNames,
            IBdoLog log = null)
        {
            connection?.Connector?.UpdateDataSet(queryText, dataSet, tableNames, log);
        }

        /// <summary>
        /// Executes the specified data query and updates the specified data table.
        /// </summary>
        /// <param name="connection">The connection to consider.</param>
        /// <param name="query">The data query to execute.</param>
        /// <param name="dataSet">The data set to update.</param>
        /// <param name="tableNames">The table names to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <returns>The log of the task.</returns>
        public static void UpdateDataSet(
            this IBdoDbConnection connection,
            IDbQuery query,
            DataSet dataSet,
            List<string> tableNames,
            IBdoLog log = null)
        {
            if (connection !=null && query != null)
            {
                // retrieve all the alias platform users
                string queryText = connection.Connector?.GetSqlText(query, null, log);
                if (!log.HasErrorsOrExceptions())
                    connection.UpdateDataSet(queryText, dataSet, tableNames, log);
            }
        }
    }
}
