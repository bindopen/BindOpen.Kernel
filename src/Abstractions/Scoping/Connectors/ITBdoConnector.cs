using BindOpen.Logging;

namespace BindOpen.Scoping.Connectors;

/// <summary>
/// Defines a strongly-typed BindOpen connector factory.
/// </summary>
/// <typeparam name="TConnection">The specific type of connection produced by this connector.</typeparam>
/// <remarks>
/// This interface specializes the connector factory to ensure that all created 
/// sessions are of a specific, predictable type, facilitating type-safe data operations.
/// </remarks>
public interface ITBdoConnector<TConnection> : IBdoConnector
    where TConnection : IBdoConnection
{
    /// <summary>
    /// Creates a new strongly-typed connection session.
    /// </summary>
    /// <param name="log">The log to use for tracking the connection creation process.</param>
    /// <returns>Returns a new instance of <typeparamref name="TConnection"/>.</returns>
    new TConnection NewConnection(IBdoLog log = null);
}