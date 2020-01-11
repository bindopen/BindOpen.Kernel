using BindOpen.Framework.Data.Connections;
using BindOpen.Framework.Data.Queries;
using BindOpen.Framework.System.Diagnostics;
using BindOpen.Framework.System.Scripting;
using System;
using System.Data;

namespace BindOpen.Framework.Data.Services
{
    /// <summary>
    /// This class represents a data service extension.
    /// </summary>
    public static partial class BdoDataServiceExtension
    {
        /// <summary>
        /// Executes the database query.
        /// </summary>
        /// <param name="dataService">The data service to consider.</param>
        /// <typeparam name="T">The class of the data to consider.</typeparam>
        /// <param name="log">The log to consider.</param>
        /// <param name="query">The query to consider.</param>
        /// <param name="function">The function to get data.</param>
        /// <param name="scriptVariableSet">The script variable set to consider.</param>
        /// <returns>Returns the specified data.</returns>
        public static T ExecuteDbQuery<T>(
            this IBdoDataService dataService,
            IDbQuery query,
            Func<IDbConnection, string, T> function,
            IBdoLog log = null,
            IBdoScriptVariableSet scriptVariableSet = null)
        {
            T result = default;

            if (dataService?.Connection is BdoDbConnection dbConnection)
            {
                if (dbConnection.Connector == null)
                {
                    log?.AddError("Connector missing");
                }
                else
                {
                    string sql = dbConnection.Connector.QueryBuilder?.Build(query, log, null, false, scriptVariableSet);

                    if (function != null)
                    {
                        result = function(dbConnection.NativeConnection, sql);
                    }
                }
            }
            else
            {
                log?.AddError("Connection missing");
            }

            return result;
        }
    }
}
