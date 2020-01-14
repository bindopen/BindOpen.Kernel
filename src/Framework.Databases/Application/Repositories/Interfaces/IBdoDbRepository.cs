using BindOpen.Framework.Extensions.Runtime;

namespace BindOpen.Framework.Application.Repositories
{
    /// <summary>
    /// This interfaces represents a repository.
    /// </summary>
    public interface IBdoDbRepository : IBdoRepository
    {
        /// <summary>
        /// The connector of the service.
        /// </summary>
        new IBdoDbConnector Connector { get; }
    }
}