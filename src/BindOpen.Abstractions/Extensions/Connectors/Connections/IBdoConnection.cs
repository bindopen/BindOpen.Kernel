using BindOpen.Extensions.Connectors;
using BindOpen.Logging;
using System;
using System.Data;

namespace BindOpen.Extensions.Connectors
{
    /// <summary>
    /// This interfaces represents a connection.
    /// </summary>
    public interface IBdoConnection : IDisposable
    {
        /// <summary>
        /// Connector.
        /// </summary>
        IBdoConnector Connector { get; set; }

        /// <summary>
        /// Sets the specified connector.
        /// </summary>
        /// <param key="connector">The connector to consider.</param>
        IBdoConnection WithConnector(IBdoConnector connector)
        {
            Connector = connector;
            return this;
        }

        /// <summary>
        /// The connection string.
        /// </summary>
        string ConnectionString { get; }

        /// <summary>
        /// The connection timeout.
        /// </summary>
        int ConnectionTimeout { get; }

        /// <summary>
        /// The state.
        /// </summary>
        ConnectionState State { get; }

        // Open / Close -----------------------------

        /// <summary>
        /// Opens a connection.
        /// </summary>
        IBdoConnection Connect(IBdoLog log = null);

        /// <summary>
        /// Closes the existing connection.
        /// </summary>
        IBdoConnection Disconnect(IBdoLog log = null);
    }
}
