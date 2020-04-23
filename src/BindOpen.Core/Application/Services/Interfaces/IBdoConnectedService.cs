using BindOpen.Data.Connections;
using BindOpen.Extensions.Runtime;
using BindOpen.System.Diagnostics;
using System;

namespace BindOpen.Application.Services
{
    /// <summary>
    /// This interfaces represents a connected service.
    /// </summary>
    public interface IBdoConnectedService : IBdoScoped, IDisposable
    {
        /// <summary>
        /// The connector of the service.
        /// </summary>
        IBdoConnector Connector { get; }

        /// <summary>
        /// Set the specified connector.
        /// </summary>
        /// <param name="connector">The BindOpen connector to consider.</param>
        IBdoConnectedService WithConnector(IBdoConnector connector);

        /// <summary>
        /// Executing the specified action during a new connection.
        /// </summary>
        /// <param name="action">The action to execute.</param>
        /// <param name="isAutoConnected">Indicates whether the connection must be automatically connected.</param>
        void UsingConnection(Action<IBdoConnection> action, bool isAutoConnected = true);

        /// <summary>
        /// Executing the specified action during a new connection.
        /// </summary>
        /// <param name="action">The action to execute.</param>
        /// <param name="log">The log to consider.</param>
        /// <param name="isAutoConnected">Indicates whether the connection must be automatically connected.</param>
        void UsingConnection(Action<IBdoConnection, IBdoLog> action, IBdoLog log, bool isAutoConnected = true);
    }
}