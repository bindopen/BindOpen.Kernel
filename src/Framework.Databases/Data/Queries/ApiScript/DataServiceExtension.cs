using BindOpen.Framework.Core.Application.Services.Data;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Core.System.Scripting;
using BindOpen.Framework.Databases.Data.Connections;
using System;
using System.Data;

namespace BindOpen.Framework.Databases.Data.Queries.ApiScript
{
    /// <summary>
    /// This class represents a data service extension.
    /// </summary>
    public static partial class DataServiceExtension
    {
        /// <summary>
        /// Retrieves the specified data.
        /// </summary>
        /// <param name="dataService">The data service to consider.</param>
        /// <typeparam name="T">The class of the data to consider.</typeparam>
        /// <param name="log">The log to consider.</param>
        /// <param name="query">The query to consider.</param>
        /// <param name="function">The function to get data.</param>
        /// <param name="scriptVariableSet">The script variable set to consider.</param>
        /// <returns>Returns the specified data.</returns>
        public static T GetData<T>(
            this IBdoDataService dataService,
            IBdoLog log,
            IDbQuery query,
            Func<IDbConnection, string, T> function,
            IBdoScriptVariableSet scriptVariableSet = null)
        {
            T result = default;

            if (dataService?.Connection is BdoDbConnection connection)
            {
                if (connection.Connector == null)
                {
                    log?.AddError("Connector missing");
                }
                else
                {
                    string sql = "";
                    IBdoLog subLog = connection.Connector.QueryBuilder?.BuildQuery(query, null, false, scriptVariableSet, out sql);
                    log?.Append(subLog);

                    if (function != null)
                    {
                        result = function(connection.NativeConnection, sql);
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
