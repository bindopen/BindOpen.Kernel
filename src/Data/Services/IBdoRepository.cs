using BindOpen.Logging;
using BindOpen.Scoping.Connectors;
using System;

namespace BindOpen.Data.Services;

/// <summary>
/// Defines a BindOpen repository.
/// A repository abstracts the data access logic and manages the lifecycle 
/// of connections to external data sources.
/// </summary>
public interface IBdoRepository : IBdoConnected
{
    /// <summary>
    /// Executes a specified action using a managed connection.
    /// </summary>
    /// <param name="action">The action to execute, providing the active connection and a log.</param>
    /// <param name="autoConnect">If true, the service automatically opens the connection before execution and closes it after.</param>
    /// <param name="log">The BindOpen log used for tracking data access operations.</param>
    void UsingConnection(
        Action<IBdoConnection, IBdoLog> action,
        bool autoConnect = true,
        IBdoLog log = null);
}