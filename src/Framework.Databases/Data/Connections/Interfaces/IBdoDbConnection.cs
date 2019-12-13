using BindOpen.Framework.Core.Data.Connections;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Databases.Extensions.Connectors;
using System.Data;

namespace BindOpen.Framework.Databases.Data.Connections
{
    /// <summary>
    /// This interfaces represents a connection.
    /// </summary>
    public interface IBdoDbConnection : IBdoConnection
    {
        /// <summary>
        /// Gets the .NET database connection of this instance.
        /// </summary>
        /// <returns>Returns the connection of this instance.</returns>
        IDbConnection NativeConnection { get; }

        /// <summary>
        /// Connector of the connection.
        /// </summary>
        new BdoDbConnector Connector { get; }

        /// <summary>
        /// The database.
        /// </summary>
        string Database { get; }

        /// <summary>
        /// Changes the current database .
        /// </summary>
        /// <param name="databaseName">The name of the database to consider.</param>
        /// <returns>Returns the log of process.</returns>
        IBdoLog ChangeDatabase(string databaseName);
    }
}
