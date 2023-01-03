using BindOpen.Runtime.Scopes;
using System;
using BindOpen.Logging;

namespace BindOpen.Extensions.Connecting
{
    /// <summary>
    /// This interfaces represents a connected service.
    /// </summary>
    public interface IBdoConnected : IBdoScoped, IDisposable
    {
        /// <summary>
        /// The connector of the service.
        /// </summary>
        IBdoConnector Connector { get; }

        /// <summary>
        /// Set the specified connector.
        /// </summary>
        /// <param name="connector">The BindOpen connector to consider.</param>
        IBdoConnected WithConnector(IBdoConnector connector);

        /// <summary>
        /// Executing the specified action during a new connection.
        /// </summary>
        /// <param name="action">The action to execute.</param>
        /// <param name="isAutoConnected">Indicates whether the connection must be automatically connected.</param>
        IBdoConnected UsingConnection(
            Action<IBdoConnection> action,
            bool isAutoConnected = true,
            IBdoLog log = null);

        /// <summary>
        /// Executing the specified action during a new connection.
        /// </summary>
        /// <param name="action">The action to execute.</param>
        /// <param name="log">The log to consider.</param>
        /// <param name="isAutoConnected">Indicates whether the connection must be automatically connected.</param>
        IBdoConnected UsingConnection(
            Action<IBdoConnection, IBdoLog> action,
            bool isAutoConnected = true,
            IBdoLog log = null);
    }
}