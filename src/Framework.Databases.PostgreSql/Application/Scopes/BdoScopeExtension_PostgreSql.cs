using BindOpen.Framework.Extensions.Connectors;
using BindOpen.Framework.Extensions.Runtime;

namespace BindOpen.Framework.Application.Scopes
{
    /// <summary>
    /// This class represents a BindOpen scope extension.
    /// </summary>
    public static class BdoScopeExtension_PostgreSql
    {
        /// <summary>
        /// Creates a new PostgreSql connector.
        /// </summary>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="connectionString">The connection string to consider.</param>
        /// <returns>Returns the connector.</returns>
        public static IBdoDbConnector CreatePostgreSqlConnector(this IBdoScope scope, string connectionString = null)
        {
            return scope?.CreateDbConnector<BdoDbConnector_PostgreSql>().WithConnectionString(connectionString);
        }
    }
}
