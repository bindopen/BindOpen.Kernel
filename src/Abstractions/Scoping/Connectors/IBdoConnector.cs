using BindOpen.Logging;

namespace BindOpen.Scoping.Connectors;

/// <summary>
/// Defines a BindOpen connector used to create and manage connections to external resources.
/// </summary>
/// <remarks>
/// This interface acts as a factory for <see cref="IBdoConnection"/> sessions, 
/// utilizing specific configuration settings like connection strings and timeouts.
/// </remarks>
public interface IBdoConnector : IBdoExtension
{
    /// <summary>
    /// Gets or sets the connection string used to establish a session with the external resource.
    /// </summary>
    string ConnectionString { get; set; }

    /// <summary>
    /// Gets or sets the connection timeout (in seconds) for sessions created by this connector.
    /// </summary>
    int ConnectionTimeOut { get; set; }

    /// <summary>
    /// Creates a new connection session.
    /// </summary>
    /// <param name="log">The log to use for tracking the connection creation process.</param>
    /// <returns>Returns a new <see cref="IBdoConnection"/> instance.</returns>
    IBdoConnection NewConnection(IBdoLog log = null);
}