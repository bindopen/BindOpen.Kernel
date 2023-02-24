using BindOpen.Runtime.Scopes;

namespace BindOpen.Extensions.Connecting
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