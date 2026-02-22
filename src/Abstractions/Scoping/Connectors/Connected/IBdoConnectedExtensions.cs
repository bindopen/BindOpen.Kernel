using BindOpen.Scoping.Connectors;

namespace BindOpen.Scoping;

/// <summary>
/// This static class provides extension methods for <see cref="IBdoConnected"/> objects.
/// </summary>
public static class IBdoConnectedExtensions
{
    /// <summary>
    /// Sets the connector for the specified connected object.
    /// </summary>
    /// <typeparam name="T">The type of the connected object.</typeparam>
    /// <param name="connected">The object to which the connector will be attached.</param>
    /// <param name="connector">The BindOpen connector to attach.</param>
    /// <returns>Returns the specified connected object for fluent chaining.</returns>
    /// <remarks>
    /// Linking a connector allows the object to interact with external data sources 
    /// or services defined within the current scope.
    /// </remarks>
    public static T WithConnector<T>(
        this T connected,
        IBdoConnector connector)
        where T : IBdoConnected
    {
        if (connected != null)
        {
            connected.Connector = connector;
        }

        return connected;
    }
}