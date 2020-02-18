using BindOpen.Extensions.Connectors;
using BindOpen.Extensions.Runtime;

namespace BindOpen.Application.Services
{
    /// <summary>
    /// This interfaces represents a database service.
    /// </summary>
    public interface IBdoDbService : IBdoConnectedService
    {
        /// <summary>
        /// The connector of the service.
        /// </summary>
        new IBdoDbConnector Connector { get; }
    }
}