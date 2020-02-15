using BindOpen.Extensions.Runtime;
using System;

namespace BindOpen.Application.Services
{
    /// <summary>
    /// This interfaces represents a connected service.
    /// </summary>
    public interface IBdoConnectedService : IDisposable
    {
        /// <summary>
        /// The connector of the service.
        /// </summary>
        IBdoConnector Connector { get; }

        /// <summary>
        /// Set the specified connector.
        /// </summary>
        /// <param name="connector">The BindOpen connector to consider.</param>
        IBdoConnectedService WithConnector(IBdoConnector connector);
    }
}