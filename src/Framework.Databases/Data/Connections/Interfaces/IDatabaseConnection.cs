using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using BindOpen.Framework.Core.Data.Connections;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Core.System.Scripting;
using BindOpen.Framework.Databases.Data.Queries;
using BindOpen.Framework.Databases.Extensions.Connectors;

namespace BindOpen.Framework.Databases.Data.Connections
{
    /// <summary>
    /// This interfaces represents a connection.
    /// </summary>
    public interface IDatabaseConnection : IBdoConnection
    {
        /// <summary>
        /// Connector of the connection.
        /// </summary>
        new DatabaseConnector Connector { get; set; }

        /// <summary>
        /// Executes the specified non query text.
        /// </summary>
        /// <param name="queryText">The query text to execute.</param>
        /// <param name="scriptVariableSet">The script variable set to consider.</param>
        /// <param name="log">The log to append.</param>
        void ExecuteNonQuery(string queryText, IBdoScriptVariableSet scriptVariableSet = null, IBdoLog log = null);

        /// <summary>
        /// Executes the specified query text.
        /// </summary>
        /// <param name="query">The query to execute.</param>
        /// <param name="scriptVariableSet">The script variable set to consider.</param>
        /// <param name="log">The log to append.</param>
        void ExecuteQuery(IDbQuery query, IBdoScriptVariableSet scriptVariableSet = null, IBdoLog log = null);

        /// <summary>
        /// Executes the specified query text and populate the specified data set.
        /// </summary>
        /// <param name="query">The query to execute.</param>
        /// <param name="dataSet">The data set to populate.</param>
        /// <param name="scriptVariableSet">The script variable set to consider.</param>
        /// <param name="log">The log to append.</param>
        void ExecuteQuery(IDbQuery query, ref DataSet dataSet, IBdoScriptVariableSet scriptVariableSet = null, IBdoLog log = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <param name="dataReader"></param>
        /// <param name="scriptVariableSet"></param>
        /// <param name="log"></param>
        void ExecuteQuery(IDbQuery query, ref IDataReader dataReader, IBdoScriptVariableSet scriptVariableSet = null, IBdoLog log = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="queryText"></param>
        /// <param name="dataSet"></param>
        /// <param name="scriptVariableSet"></param>
        /// <param name="log"></param>
        void ExecuteQuery(string queryText, ref DataSet dataSet, IBdoScriptVariableSet scriptVariableSet = null, IBdoLog log = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="queryText"></param>
        /// <param name="dataReader"></param>
        /// <param name="scriptVariableSet"></param>
        /// <param name="log"></param>
        void ExecuteQuery(string queryText, ref IDataReader dataReader, IBdoScriptVariableSet scriptVariableSet = null, IBdoLog log = null);

        /// <summary>
        /// Get the database connection.
        /// </summary>
        /// <returns>Returns the database connection.</returns>
        IDbConnection GetIDbConnection();

        /// <summary>
        /// Get the identity of the last tuple processed.
        /// </summary>
        /// <returns>Returns the identity of the last tuple processed.</returns>
        void GetIdentity(ref long id, IBdoLog log = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <param name="dataSet"></param>
        /// <param name="tableNames"></param>
        /// <param name="log"></param>
        void UpdateDataSet(IDbQuery query, DataSet dataSet, List<string> tableNames, IBdoLog log = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="queryText"></param>
        /// <param name="dataSet"></param>
        /// <param name="tableNames"></param>
        /// <param name="log"></param>
        void UpdateDataSet(string queryText, DataSet dataSet, List<string> tableNames, IBdoLog log = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <param name="dataTable"></param>
        /// <param name="log"></param>
        void UpdateDataTable(IDbQuery query, DataTable dataTable, IBdoLog log = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="queryText"></param>
        /// <param name="dataTable"></param>
        /// <param name="log"></param>
        void UpdateDataTable(string queryText, DataTable dataTable, IBdoLog log = null);
    }
}
