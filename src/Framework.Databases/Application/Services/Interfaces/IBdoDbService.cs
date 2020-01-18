using BindOpen.Framework.Extensions.Runtime;

namespace BindOpen.Framework.Application.Services
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