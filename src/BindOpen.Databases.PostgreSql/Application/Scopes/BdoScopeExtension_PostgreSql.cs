using BindOpen.Extensions.Connectors;
using BindOpen.Extensions.Runtime;

namespace BindOpen.Application.Scopes
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
        /// <returns>Returns the connector.</returns>
        public static IBdoDbConnector CreatePostgreSqlConnector(this IBdoScope scope)
            => CreatePostgreSqlConnector(scope, null);

        /// <summary>
        /// Creates a new PostgreSql connector.
        /// </summary>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="connectionString">The connection string to consider.</param>
        /// <returns>Returns the connector.</returns>
        public static IBdoDbConnector CreatePostgreSqlConnector(this IBdoScope scope, string connectionString = null)
        {
            return scope?.CreateDbConnector<BdoDbConnector_PostgreSql>().WithConnectionString(connectionString) as IBdoDbConnector;
        }
    }
}
