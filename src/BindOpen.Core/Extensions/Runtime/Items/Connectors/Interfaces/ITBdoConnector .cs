using BindOpen.Data.Connections;
using BindOpen.System.Diagnostics;
using System;

namespace BindOpen.Extensions.Runtime
{
    /// <summary>
    /// 
    /// </summary>
    public interface ITBdoConnector<T> : IBdoConnector where T : IBdoConnection
    {
        /// <summary>
        /// Creates a new connection.
        /// </summary>
        /// <param name="log">The log to consider.</param>
        new T CreateConnection(IBdoLog log = null);

        /// <summary>
        /// Executes the specified function.
        /// </summary>
        /// <typeparam name="Q"></typeparam>
        /// <param name="repository">The connected service to consider.</param>
        /// <param name="action">The action using the created connection and the current log to consider.</param>
        /// <param name="isAutoConnected">Indicates whether the connection is automatically opened.</param>
        /// <returns></returns>
        new void UsingConnection(
            Action<T> action,
            bool isAutoConnected = true);

        /// <summary>
        /// Executes the specified function.
        /// </summary>
        /// <param name="repository">The connected service to consider</param>
        /// <param name="action">The action using the created connection and the current log to consider.</param>
        /// <param name="isAutoConnected">Indicates whether the connection is automatically opened.</param>
        /// <param name="log">The log to consider.</param>
        /// <param name="isAutoConnected">Indicates whether the connection is automatically opened.</param>
        /// <returns></returns>
        new void UsingConnection(
            Action<T, IBdoLog> action,
            IBdoLog log,
            bool isAutoConnected = true);
    }
}