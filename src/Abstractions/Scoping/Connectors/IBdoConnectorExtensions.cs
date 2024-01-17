using BindOpen.Logging;
using BindOpen.Scoping.Connectors;
using System;
using System.Runtime.InteropServices;

namespace BindOpen.Scoping
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
        /// <param key="log">The log to consider.</param>
        /// <param key="autoConnect">Indicates whether the connection is automatically opened.</param>
        /// <returns></returns>
        public static void UsingConnection(
            this IBdoConnector connector,
            Action<IBdoConnection, IBdoLog> action,
            bool autoConnect = true,
            IBdoLog log = null)
        {
            if (connector != null)
            {
                using var connection = connector.NewConnection(log);
                if (connection != null)
                {
                    if (autoConnect)
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
            }
        }

        public static void UsingConnection<TConnection>(
            this ITBdoConnector<TConnection> connector,
            Action<TConnection, IBdoLog> action,
            bool autoConnect = true,
            IBdoLog log = null)
            where TConnection : IBdoConnection
        {
            UsingConnection(
                connector, (connector, log) => action?.Invoke(connector, log),
                autoConnect, log);
        }

    }
}