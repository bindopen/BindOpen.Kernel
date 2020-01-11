using BindOpen.Framework.Data.Connections;
using System;

namespace BindOpen.Framework.Data.Services
{
    /// <summary>
    /// This interfaces represents a data service.
    /// </summary>
    public interface IBdoDataService : IDisposable
    {
        /// <summary>
        /// The connection of the service.
        /// </summary>
        IBdoConnection Connection { get; }

        /// <summary>
        /// Set the specified connection.
        /// </summary>
        /// <param name="connection">The connection to consider.</param>
        void SetConnection(IBdoConnection connection);
    }
}