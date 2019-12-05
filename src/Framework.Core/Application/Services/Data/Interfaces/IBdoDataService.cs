using BindOpen.Framework.Core.Data.Connections;
using System;

namespace BindOpen.Framework.Core.Application.Services.Data
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