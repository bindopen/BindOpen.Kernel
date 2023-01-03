using BindOpen.Logging;
using System;

namespace BindOpen.Extensions.Connecting
{
    /// <summary>
    /// 
    /// </summary>
    public interface ITBdoConnector<TConnection> : IBdoConnector
        where TConnection : IBdoConnection
    {
        /// <summary>
        /// 
        /// </summary>
        new ITBdoConnector<TConnection> WithConnectionString(string connectionString);

        /// <summary>
        /// 
        /// </summary>
        new ITBdoConnector<TConnection> WithConnectionTimeOut(int timeOut);

        /// <summary>
        /// Creates a new connection.
        /// </summary>
        /// <returns></returns>
        /// <param name="log">The log to consider.</param>
        new TConnection NewConnection(IBdoLog log = null);

        /// <summary>

        /// <summary>
        /// Executes the specified function.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="action">The action using the created connection and the current log to consider.</param>
        /// <param name="isAutoConnected">Indicates whether the connection is automatically opened.</param>
        /// <param name="log">The log to consider.</param>
        /// <returns></returns>
        ITBdoConnector<TConnection> UsingConnection(
            Action<TConnection> action,
            bool isAutoConnected = true,
            IBdoLog log = null);

        /// <summary>
        /// Executes the specified function.
        /// </summary>
        /// <param name="action">The action using the created connection and the current log to consider.</param>
        /// <param name="isAutoConnected">Indicates whether the connection is automatically opened.</param>
        /// <param name="log">The log to consider.</param>
        /// <returns></returns>
        ITBdoConnector<TConnection> UsingConnection(
            Action<TConnection, IBdoLog> action,
            bool isAutoConnected = true,
            IBdoLog log = null);
    }
}