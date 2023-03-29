using BindOpen.Extensions.Connectors;
using BindOpen.Scopes;

namespace BindOpen.Extensions.Connectors
{
    /// <summary>
    /// This interfaces represents a connected service.
    /// </summary>
    public interface IBdoConnected : IBdoScoped
    {
        /// <summary>
        /// The connector of the service.
        /// </summary>
        IBdoConnector Connector { get; set; }
    }
}