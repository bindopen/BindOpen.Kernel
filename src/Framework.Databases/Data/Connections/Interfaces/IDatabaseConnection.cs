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
    public interface IDatabaseConnection : IConnection
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
        void ExecuteNonQuery(string queryText, IScriptVariableSet scriptVariableSet = null, ILog log = null);

        /// <summary>
        /// Executes the specified query text.
        /// </summary>
        /// <param name="query">The query to execute.</param>
        /// <param name="scriptVariableSet">The script variable set to consider.</param>
        /// <param name="log">The log to append.</param>
        void ExecuteQuery(IDbDataQuery query, IScriptVariableSet scriptVariableSet = null, ILog log = null);

        /// <summary>
        /// Executes the specified query text and populate the specified data set.
        /// </summary>
        /// <param name="query">The query to execute.</param>
        /// <param name="dataSet">The data set to populate.</param>
        /// <param name="scriptVariableSet">The script variable set to consider.</param>
        /// <param name="log">The log to append.</param>
        void ExecuteQuery(IDbDataQuery query, ref DataSet dataSet, IScriptVariableSet scriptVariableSet = null, ILog log = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <param name="dataReader"></param>
        /// <param name="scriptVariableSet"></param>
        /// <param name="log"></param>
        void ExecuteQuery(IDbDataQuery query, ref DbDataReader dataReader, IScriptVariableSet scriptVariableSet = null, ILog log = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="queryText"></param>
        /// <param name="dataSet"></param>
        /// <param name="scriptVariableSet"></param>
        /// <param name="log"></param>
        void ExecuteQuery(string queryText, ref DataSet dataSet, IScriptVariableSet scriptVariableSet = null, ILog log = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="queryText"></param>
        /// <param name="dataReader"></param>
        /// <param name="scriptVariableSet"></param>
        /// <param name="log"></param>
        void ExecuteQuery(string queryText, ref DbDataReader dataReader, IScriptVariableSet scriptVariableSet = null, ILog log = null);

        /// <summary>
        /// Get the database connection.
        /// </summary>
        /// <returns>Returns the database connection.</returns>
        IDbConnection GetIDbConnection();

        /// <summary>
        /// Get the identity of the last tuple processed.
        /// </summary>
        /// <returns>Returns the identity of the last tuple processed.</returns>
        void GetIdentity(ref long id, ILog log = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <param name="dataSet"></param>
        /// <param name="tableNames"></param>
        /// <param name="log"></param>
        void UpdateDataSet(IDbDataQuery query, DataSet dataSet, List<string> tableNames, ILog log = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="queryText"></param>
        /// <param name="dataSet"></param>
        /// <param name="tableNames"></param>
        /// <param name="log"></param>
        void UpdateDataSet(string queryText, DataSet dataSet, List<string> tableNames, ILog log = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <param name="dataTable"></param>
        /// <param name="log"></param>
        void UpdateDataTable(IDbDataQuery query, DataTable dataTable, ILog log = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="queryText"></param>
        /// <param name="dataTable"></param>
        /// <param name="log"></param>
        void UpdateDataTable(string queryText, DataTable dataTable, ILog log = null);
    }
}
