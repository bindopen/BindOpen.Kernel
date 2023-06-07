using BindOpen.Logging;
using System;
using System.Runtime.InteropServices;

namespace BindOpen.Scoping.Extensions.Connectors
{
    /// <summary>
    /// This interface represents an named data item.
    /// </summary>
    public static class IBdoConnectorExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        public static T WithConnectionString<T>(
            this T connector,
            string connectionString)
            where T : IBdoConnector
        {
            if (connector != null)
            {
                connector.ConnectionString = connectionString;
            }

            return connector;
        }

        /// <summary>
        /// 
        /// </summary>
        public static T WithConnectionTimeOut<T>(
            this T connector,
            int timeOut)
            where T : IBdoConnector
        {
            if (connector != null)
            {
                connector.ConnectionTimeOut = timeOut;
            }

            return connector;
        }

        /// <summary>
        /// Executes the specified function.
        /// </summary>
        /// <param key="action">The action using the created connection and the current log to consider.</param>
        /// <param key="isAutoConnected">Indicates whether the connection is automatically opened.</param>
        /// <returns></returns>
        public static T UsingConnection<T>(
            this T connector,
            Action<IBdoConnection> action,
            bool isAutoConnected = true,
            IBdoLog log = null)
            where T : IBdoConnector
            => connector.UsingConnection((c, l) => action?.Invoke(c), isAutoConnected, log);

        /// <summary>
        /// Executes the specified function.
        /// </summary>
        /// <param key="action">The action using the created connection and the current log to consider.</param>
        /// <param key="log">The log to consider.</param>
        /// <param key="isAutoConnected">Indicates whether the connection is automatically opened.</param>
        /// <returns></returns>
        public static T UsingConnection<T>(
            this T connector,
            Action<IBdoConnection, IBdoLog> action,
            bool isAutoConnected = true,
            IBdoLog log = null)
            where T : IBdoConnector
        {
            using var connection = connector?.NewConnection(log);
            if (connection != null)
            {
                if (isAutoConnected)
                {
                    try
                    {
                        connection.Connect(log);
                    }
                    catch (ExternalException ex)
                    {
                        log?.AddEvent(EventKinds.Exception,
                            "An exception occured while trying to open connection",
                            ex.ToString());
                    }
                }

                action?.Invoke(connection, log);
            }

            return connector;
        }

    }
}