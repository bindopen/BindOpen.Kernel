using BindOpen.Logging;
using BindOpen.Scoping.Connectors;
using System;
using System.Runtime.InteropServices;

namespace BindOpen.Scoping;

/// <summary>
/// This static class provides extension methods for <see cref="IBdoConnector"/>.
/// </summary>
public static class IBdoConnectorExtensions
{
    /// <summary>
    /// Sets the connection string of the specified connector.
    /// </summary>
    /// <typeparam name="T">The type of the connector.</typeparam>
    /// <param name="connector">The connector to configure.</param>
    /// <param name="connectionString">The connection string to set.</param>
    /// <returns>Returns the specified connector for fluent chaining.</returns>
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
    /// Sets the connection timeout of the specified connector.
    /// </summary>
    /// <typeparam name="T">The type of the connector.</typeparam>
    /// <param name="connector">The connector to configure.</param>
    /// <param name="timeOut">The timeout value in seconds.</param>
    /// <returns>Returns the specified connector for fluent chaining.</returns>
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
    /// Executes an action using a managed connection instance.
    /// </summary>
    /// <param name="connector">The connector to use.</param>
    /// <param name="action">The action to execute, receiving the connection and the current log.</param>
    /// <param name="autoConnect">Indicates whether the connection should be automatically opened before the action.</param>
    /// <param name="log">The log to use for tracking the operation.</param>
    /// <remarks>The connection is automatically disposed of after the action completes.</remarks>
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
                        log?.AddEvent(BdoEventKinds.Exception,
                            "An exception occurred while trying to open the connection",
                            ex.ToString());
                    }
                }

                action?.Invoke(connection, log);
            }
        }
    }

    /// <summary>
    /// Executes an action using a strongly-typed managed connection instance.
    /// </summary>
    /// <typeparam name="TConnection">The type of the connection.</typeparam>
    /// <param name="connector">The generic connector to use.</param>
    /// <param name="action">The action to execute.</param>
    /// <param name="autoConnect">Indicates whether the connection should be automatically opened.</param>
    /// <param name="log">The log to use for tracking.</param>
    public static void UsingConnection<TConnection>(
        this ITBdoConnector<TConnection> connector,
        Action<TConnection, IBdoLog> action,
        bool autoConnect = true,
        IBdoLog log = null)
        where TConnection : IBdoConnection
    {
        (connector as IBdoConnector).UsingConnection(
            (conn, log) => action?.Invoke((TConnection)conn, log),
            autoConnect, log);
    }
}