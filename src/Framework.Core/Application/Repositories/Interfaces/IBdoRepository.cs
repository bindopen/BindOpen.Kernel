using BindOpen.Framework.Extensions.Runtime;
using System;

namespace BindOpen.Framework.Application.Repositories
{
    /// <summary>
    /// This interfaces represents a repository.
    /// </summary>
    public interface IBdoRepository : IDisposable
    {
        /// <summary>
        /// The connector of the service.
        /// </summary>
        IBdoConnector Connector { get; }

        /// <summary>
        /// Set the specified connector.
        /// </summary>
        /// <param name="connector">The BindOpen connector to consider.</param>
        void SetConnector(IBdoConnector connector);
    }
}