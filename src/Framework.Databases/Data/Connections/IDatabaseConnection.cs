using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using BindOpen.Framework.Core.Data.Connections;
using BindOpen.Framework.Core.Extensions.Items.Connectors;
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
        DatabaseConnector Connector { get; set; }

        void ExecuteNonQuery(string queryText, IScriptVariableSet scriptVariableSet = null, ILog log = null);
        void ExecuteQuery(IDbDataQuery query, IScriptVariableSet scriptVariableSet = null, ILog log = null);
        void ExecuteQuery(IDbDataQuery query, ref DataSet dataSet, IScriptVariableSet scriptVariableSet = null, ILog log = null);
        void ExecuteQuery(IDbDataQuery query, ref DbDataReader dataReader, IScriptVariableSet scriptVariableSet = null, ILog log = null);
        void ExecuteQuery(string queryText, ref DataSet dataSet, IScriptVariableSet scriptVariableSet = null, ILog log = null);
        void ExecuteQuery(string queryText, ref DbDataReader dataReader, IScriptVariableSet scriptVariableSet = null, ILog log = null);
        IDbConnection GetIDbConnection();
        void GetIdentity(ref long id, ILog log = null);
        void SetConnector(IConnector connector);
        void UpdateDataSet(IDbDataQuery query, DataSet dataSet, List<string> tableNames, ILog log = null);
        void UpdateDataSet(string queryText, DataSet dataSet, List<string> tableNames, ILog log = null);
        void UpdateDataTable(IDbDataQuery query, DataTable dataTable, ILog log = null);
        void UpdateDataTable(string queryText, DataTable dataTable, ILog log = null);
    }
}
