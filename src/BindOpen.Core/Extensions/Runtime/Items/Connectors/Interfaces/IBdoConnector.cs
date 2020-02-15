using BindOpen.Application.Scopes;
using BindOpen.Data.Connections;
using BindOpen.Data.Elements;
using BindOpen.Extensions.Definition;
using BindOpen.System.Diagnostics;

namespace BindOpen.Extensions.Runtime
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoConnector : ITBdoExtensionItem<IBdoConnectorDefinition>
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
        /// Converts the connector as a source element.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        ISourceElement AsElement(string name = null, IBdoLog log = null);

        /// <summary>
        /// Creates a new connection.
        /// </summary>
        /// <returns></returns>
        /// <param name="log">The log to consider.</param>
        IBdoConnection CreateConnection(IBdoLog log = null);

        /// <summary>
        /// Updates the connection string with the specified string.
        /// </summary>
        /// <param name="connectionString">The connection string to consider.</param>
        IBdoConnector WithConnectionString(string connectionString = null);

        /// <summary>
        /// Updates the instance considering the specified scope.
        /// </summary>
        /// <param name="scope">The scope to consider.</param>
        IBdoConnector WithScope(IBdoScope scope);
    }
}