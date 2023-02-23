using BindOpen.Logging;

namespace BindOpen.Extensions.Connecting
{
    /// <summary>
    /// 
    /// </summary>
    public interface ITBdoConnector<TConnection> : IBdoConnector
        where TConnection : IBdoConnection
    {
        /// <summary>
        /// Creates a new connection.
        /// </summary>
        /// <returns></returns>
        /// <param key="log">The log to consider.</param>
        new TConnection NewConnection(IBdoLog log = null);
    }
}