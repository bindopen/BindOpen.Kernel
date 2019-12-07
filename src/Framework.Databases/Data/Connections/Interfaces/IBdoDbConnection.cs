using BindOpen.Framework.Core.Data.Connections;
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
        /// Connector of the connection.
        /// </summary>
        new DatabaseConnector Connector { get; set; }

        /// <summary>
        /// The .NET IDbConnection.
        /// </summary>
        IDbConnection DotNetDbConnection { get; }
    }
}
