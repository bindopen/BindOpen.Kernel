using BindOpen.Logging;

namespace BindOpen.Scoping.Connectors
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoConnector : IBdoExtension
    {
        /// <summary>
        /// The connection string of this instance.
        /// </summary>
        string ConnectionString { get; set; }

        /// <summary>
        /// The connection timeout of this instance.
        /// </summary>
        int ConnectionTimeOut { get; set; }

        /// <summary>
        /// Creates a new connection.
        /// </summary>
        /// <returns></returns>
        /// <param key="log">The log to consider.</param>
        IBdoConnection NewConnection(IBdoLog log = null);
    }
}
