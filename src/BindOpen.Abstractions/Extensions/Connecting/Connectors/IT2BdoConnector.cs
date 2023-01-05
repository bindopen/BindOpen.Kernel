using BindOpen.Logging;
using System;

namespace BindOpen.Extensions.Connecting
{
    /// <summary>
    /// 
    /// </summary>
    public interface ITBdoConnector<T, TConnection> : ITBdoConnector<TConnection>
        where T : IBdoConnector
        where TConnection : IBdoConnection
    {
        /// <summary>
        /// 
        /// </summary>
        new T WithConnectionString(string connectionString);

        /// <summary>
        /// 
        /// </summary>
        new T WithConnectionTimeOut(int timeOut);

        /// <summary>
        /// 
        /// <summary>
        /// Executes the specified function.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="action">The action using the created connection and the current log to consider.</param>
        /// <param name="isAutoConnected">Indicates whether the connection is automatically opened.</param>
        /// <param name="log">The log to consider.</param>
        /// <returns></returns>
        new T UsingConnection(
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
        new T UsingConnection(
            Action<TConnection, IBdoLog> action,
            bool isAutoConnected = true,
            IBdoLog log = null);
    }
}