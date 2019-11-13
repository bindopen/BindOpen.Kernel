using BindOpen.Framework.Core.Application.Services.Data;
using BindOpen.Framework.Core.Data.Expression;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Core.System.Scripting;
using BindOpen.Framework.Databases.Data.Connections;
using BindOpen.Framework.Databases.Extensions.Carriers;
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
            this IDataService dataService,
            ILog log,
            IDbDataQuery query,
            Func<IDbConnection, string, T> function,
            IScriptVariableSet scriptVariableSet = null)
        {
            T result = default;

            if (dataService?.Connection is DatabaseConnection connection)
            {
                if (connection.Connector == null)
                {
                    log?.AddError("Connector missing");
                }
                else
                {
                    string sql = "";
                    ILog subLog = connection.Connector.QueryBuilder?.BuildQuery(query, scriptVariableSet, out sql);
                    log?.Append(subLog);

                    if (function != null)
                    {
                        result = function(connection.GetIDbConnection(), sql);
                    }
                }
            }
            else
            {
                log?.AddError("Connection missing");
            }

            return result;
        }

        /// <summary>
        /// Executes the specified function.
        /// </summary>
        /// <typeparam name="Q"></typeparam>
        /// <typeparam name="T"></typeparam>
        /// <param name="connection">The database connection to use.</param>
        /// <param name="log">The log to consider.</param>
        /// <returns></returns>
        public static T GetId<Q, T>(this IDatabaseConnection connection, ILog log) where Q : IDataService
        {
            T value = default;

            IDataReader reader = null;

            IBasicDbDataQuery query = new BasicDbDataQuery(DbDataQueryKind.Select)
            {
                Fields =
                {
                    new DbField() { Value= "$sqlNewGuid()".CreateScript() }
                }
            };

            connection?.ExecuteQuery(query, ref reader);

            if (reader != null)
            {
                while (reader.Read())
                {
                    value = (T)reader[0];
                }
            }

            return value;
        }
    }
}
