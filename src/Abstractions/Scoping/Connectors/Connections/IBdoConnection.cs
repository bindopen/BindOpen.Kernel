using BindOpen.Logging;
using System;
using System.Data;

namespace BindOpen.Scoping.Connectors
{
    /// <summary>
    /// This interfaces represents a connection.
    /// </summary>
    public interface IBdoConnection : IDisposable
    {
        /// <summary>
        /// Connector.
        /// </summary>
        IBdoConnector Connector { get; }

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
        void Connect(IBdoLog log = null);

        /// <summary>
        /// Closes the existing connection.
        /// </summary>
        void Disconnect(IBdoLog log = null);
    }
}
