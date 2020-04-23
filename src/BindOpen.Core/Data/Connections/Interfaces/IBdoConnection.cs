using BindOpen.Extensions.Runtime;
using BindOpen.System.Diagnostics;
using System;
using System.Data;

namespace BindOpen.Data.Connections
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

        /// <summary>
        /// Sets the specified connector.
        /// </summary>
        /// <param name="connector">The connector to consider.</param>
        void WithConnector(IBdoConnector connector);

        // Open / Close -----------------------------

        /// <summary>
        /// Opens a connection.
        /// </summary>
        IBdoLog Connect();

        /// <summary>
        /// Closes the existing connection.
        /// </summary>
        IBdoLog Disconnect();
    }
}
