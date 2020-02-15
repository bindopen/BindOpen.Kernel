using BindOpen.Data.Queries;
using BindOpen.Extensions.Runtime;
using BindOpen.System.Diagnostics;
using BindOpen.System.Scripting;
using System.Data;

namespace BindOpen.Data.Connections
{
    /// <summary>
    /// This class proposes extensions for database connection.
    /// </summary>
    public static class BdoDbConnectionExtensions
    {
        /// <summary>
        /// Gets the SQL text of the specified query.
        /// </summary>
        /// <param name="connection">The connection to consider.</param>
        /// <param name="query">The query to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <returns>Returns the SQL text of the specified query.</returns>
        public static IDbCommand CreateCommand(
            this IBdoDbConnection connection,
            IDbQuery query,
            IBdoScriptVariableSet scriptVariableSet = null,
            IBdoLog log = null)
        {
            IDbCommand command = connection?.Connector?.CreateCommand(query, scriptVariableSet, log);
            command.Connection = connection?.Native;

            return command;
        }

        /// <summary>
        /// Gets the SQL text of the specified query.
        /// </summary>
        /// <param name="query">The query to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <returns>Returns the SQL text of the specified query.</returns>
        public static IDbCommand CreateCommand<T>(
            this IDbQuery query,
            IBdoScriptVariableSet scriptVariableSet = null,
            IBdoLog log = null) where T : BdoDbConnector, new()
        {
            T connector = new T();
            return connector?.CreateConnection()?.CreateCommand(query, scriptVariableSet, log);
        }

        /// <summary>
        /// Gets the SQL text of the specified query.
        /// </summary>
        /// <param name="connection">The connection to consider.</param>
        /// <param name="query">The query to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <returns>Returns the SQL text of the specified query.</returns>
        public static IDbCommand CreateCommand<T>(
            this IDbConnection connection,
            IDbQuery query,
            IBdoScriptVariableSet scriptVariableSet = null,
            IBdoLog log = null) where T : BdoDbConnector, new()
        {
            return connection?.CreateCommand<T>(query, scriptVariableSet, log);
        }

        /// <summary>
        /// Gets the SQL text of the specified query.
        /// </summary>
        /// <param name="transaction">The transaction to consider.</param>
        /// <param name="query">The query to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <returns>Returns the SQL text of the specified query.</returns>
        public static IDbCommand CreateCommand<T>(
            this IDbTransaction transaction,
            IDbQuery query,
            IBdoScriptVariableSet scriptVariableSet = null,
            IBdoLog log = null) where T : BdoDbConnector, new()
        {
            IDbCommand command = transaction?.Connection?.CreateCommand<T>(query, scriptVariableSet, log);
            command.Transaction = transaction;

            return command;
        }

        /// <summary>
        /// Begins a transaction.
        /// </summary>
        /// <param name="connection">The connection to consider.</param>
        /// <returns></returns>
        public static IDbTransaction BeginTransaction(this IBdoDbConnection connection)
            => connection?.Native?.BeginTransaction();

        /// <summary>
        /// Begins a transaction.
        /// </summary>
        /// <param name="connection">The connection to consider.</param>
        /// <param name="isolationLevel">The isolation level.</param>
        /// <returns></returns>
        public static IDbTransaction BeginTransaction(this IBdoDbConnection connection, IsolationLevel isolationLevel)
            => connection?.Native?.BeginTransaction(isolationLevel);

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
            connection?.GetIdentity(ref id, log);
        }
    }
}
