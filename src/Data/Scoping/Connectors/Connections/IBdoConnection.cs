using BindOpen.Logging;
using System;
using System.Data;

namespace BindOpen.Scoping.Connectors;

/// <summary>
/// Defines a connection session to an external resource.
/// </summary>
/// <remarks>
/// This interface manages the lifecycle of a resource session (Connect/Disconnect) 
/// and provides access to the underlying connector and session state.
/// </remarks>
public interface IBdoConnection : IDisposable
{
    /// <summary>
    /// Gets the connector that created this connection.
    /// </summary>
    IBdoConnector Connector { get; }

    /// <summary>
    /// Gets the string used to establish the connection.
    /// </summary>
    string ConnectionString { get; }

    /// <summary>
    /// Gets the time to wait (in seconds) while trying to establish a connection before terminating the attempt.
    /// </summary>
    int ConnectionTimeout { get; }

    /// <summary>
    /// Gets the current state of the connection.
    /// </summary>
    ConnectionState State { get; }

    // Open / Close -----------------------------

    /// <summary>
    /// Establishes the connection to the external resource.
    /// </summary>
    /// <param name="log">The log to use for tracking the connection process.</param>
    void Connect(IBdoLog log = null);

    /// <summary>
    /// Closes the existing connection and releases associated resources.
    /// </summary>
    /// <param name="log">The log to use for tracking the disconnection process.</param>
    void Disconnect(IBdoLog log = null);
}