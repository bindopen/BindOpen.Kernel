using BindOpen.Extensions.Connectors;
using BindOpen.Extensions.Runtime;

namespace BindOpen.Application.Scopes
{
    /// <summary>
    /// This class represents a BindOpen scope extension.
    /// </summary>
    public static class BdoScopeExtension_MSSqlServer
    {
        /// <summary>
        /// Creates a new MSSqlServer connector.
        /// </summary>
        /// <param name="scope">The scope to consider.</param>
        /// <returns>Returns the connector.</returns>
        public static IBdoDbConnector CreateMSSqlServerConnector(this IBdoScope scope)
            => CreateMSSqlServerConnector(scope, null);

        /// <summary>
        /// Creates a new MSSqlServer connector.
        /// </summary>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="connectionString">The connection string to consider.</param>
        /// <returns>Returns the connector.</returns>
        public static IBdoDbConnector CreateMSSqlServerConnector(this IBdoScope scope, string connectionString = null)
        {
            return scope?.CreateDbConnector<BdoDbConnector_MSSqlServer>().WithConnectionString(connectionString) as IBdoDbConnector;
        }
    }
}
