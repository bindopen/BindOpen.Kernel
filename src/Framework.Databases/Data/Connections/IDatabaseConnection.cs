using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using BindOpen.Framework.Core.Data.Connections;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Core.System.Scripting;

namespace BindOpen.Framework.Databases.Data.Connections
{

    /// <summary>
    /// This interfaces represents a connection.
    /// </summary>
    public interface IDatabaseConnection : IConnection
    {
        // Execution non query  ---------------------------------------

        /// <summary>
        /// Executes a database data query that does not return anything.
        /// </summary>
        /// <param name="queryBuilder">The data query builder to use.</param>
        /// <param name="queryText">The text to execute.</param>
        /// <param name="scriptVariableSet">The interpretation variables to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <returns>The log of the data query execution task.</returns>
        void ExecuteNonQuery(
            String queryText,
            ScriptVariableSet scriptVariableSet = null,
            Log log = null);

        // Execution query data reader  ---------------------------------------

        /// <summary>
        /// Executes a database data query putting the result into a data reader.
        /// </summary>
        /// <param name="queryText">The text to execute.</param>
        /// <param name="dataReader">The output data reader.</param>
        /// <param name="scriptVariableSet">The interpretation variables to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <returns>The log of the data query execution task.</returns>
        void ExecuteQuery(
            String queryText,
            ref DbDataReader dataReader,
            ScriptVariableSet scriptVariableSet = null,
            Log log = null);

        // Execution query dataset  ---------------------------------------

        /// <summary>
        /// Executes a database data query putting the result into a dataset.
        /// </summary>
        /// <param name="queryText">The text to execute.</param>
        /// <param name="dataSet">The output dataset.</param>
        /// <param name="scriptVariableSet">The interpretation variables to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <returns>The log of the data query execution task.</returns>
        void ExecuteQuery(
            String queryText,
            ref DataSet dataSet,
            ScriptVariableSet scriptVariableSet = null,
            Log log = null);

        // Table ---------------------------------------

        /// <summary>
        /// Gets the identity of the last inserted item
        /// </summary>
        /// <param name="id">The long identifier to populate.</param>
        /// <param name="log">The log to consider.</param>
        void GetIdentity(
            ref long id,
            Log log = null);

        /// <summary>
        /// Executes the specified data query and updates the specified data table.
        /// </summary>
        /// <param name="queryText">The text to execute.</param>
        /// <param name="dataTable">The data table to update.</param>
        /// <param name="log">The log to consider.</param>
        /// <returns>The log of the task.</returns>
        void UpdateDataTable(
            String queryText,
            DataTable dataTable,
            Log log = null);

        /// <summary>
        /// Executes the specified data query and updates the specified data table.
        /// </summary>
        /// <param name="queryText">The text to execute.</param>
        /// <param name="dataSet">The data set to update.</param>
        /// <param name="tableNames">The table names to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <returns>The log of the task.</returns>
        void UpdateDataSet(
            String queryText,
            DataSet dataSet,
            List<String> tableNames,
            Log log = null);
    }
}
