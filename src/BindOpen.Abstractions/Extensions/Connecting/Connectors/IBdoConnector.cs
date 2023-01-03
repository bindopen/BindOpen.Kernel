using BindOpen.Runtime.Definition;
using System;
using BindOpen.Logging;

namespace BindOpen.Extensions.Connecting
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoConnector :
        ITBdoExtensionItem<IBdoConnectorDefinition, IBdoConnectorConfiguration, IBdoConnector>
    {
        /// <summary>
        /// The connection string of this instance.
        /// </summary>
        string ConnectionString { get; set; }

        /// <summary>
        /// 
        /// </summary>
        IBdoConnector WithConnectionString(string connectionString)
        {
            ConnectionString = connectionString;
            return this;
        }

        /// <summary>
        /// The connection timeout of this instance.
        /// </summary>
        int ConnectionTimeOut { get; set; }

        /// <summary>
        /// 
        /// </summary>
        IBdoConnector WithConnectionTimeOut(int timeOut)
        {
            ConnectionTimeOut = timeOut;
            return this;
        }

        /// <summary>
        /// Creates a new connection.
        /// </summary>
        /// <returns></returns>
        /// <param name="log">The log to consider.</param>
        IBdoConnection NewConnection(IBdoLog log = null);

        /// <summary>
        /// Executes the specified function.
        /// </summary>
        /// <typeparam name="Q"></typeparam>
        /// <param name="repository">The connected service to consider.</param>
        /// <param name="action">The action using the created connection and the current log to consider.</param>
        /// <param name="isAutoConnected">Indicates whether the connection is automatically opened.</param>
        /// <returns></returns>
        IBdoConnector UsingConnection(
            Action<IBdoConnection> action,
            bool isAutoConnected = true,
            IBdoLog log = null);

        /// <summary>
        /// Executes the specified function.
        /// </summary>
        /// <param name="action">The action using the created connection and the current log to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <param name="isAutoConnected">Indicates whether the connection is automatically opened.</param>
        /// <returns></returns>
        IBdoConnector UsingConnection(
            Action<IBdoConnection, IBdoLog> action,
            bool isAutoConnected = true,
            IBdoLog log = null);
    }
}
