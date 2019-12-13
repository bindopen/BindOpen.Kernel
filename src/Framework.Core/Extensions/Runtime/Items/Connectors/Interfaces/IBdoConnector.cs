using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.Data.Connections;
using BindOpen.Framework.Core.Data.Elements;
using BindOpen.Framework.Core.Extensions.Definition.Items;
using BindOpen.Framework.Core.System.Diagnostics;

namespace BindOpen.Framework.Core.Extensions.Runtime.Items
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
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        void UpdateConnectionString(string connectionString = null);

        /// <summary>
        /// Updates the instance considering the specified scope.
        /// </summary>
        /// <param name="scope">The scope to consider.</param>
        IBdoConnector WithScope(IBdoScope scope);
    }
}