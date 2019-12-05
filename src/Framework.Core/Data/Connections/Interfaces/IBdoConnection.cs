using BindOpen.Framework.Core.Extensions.Runtime.Items;
using BindOpen.Framework.Core.System.Diagnostics;
using System;

namespace BindOpen.Framework.Core.Data.Connections
{
    /// <summary>
    /// This interfaces represents a connection.
    /// </summary>
    public interface IBdoConnection : IDisposable
    {
        /// <summary>
        /// Connector.
        /// </summary>
        IBdoConnector Connector { get; }

        /// <summary>
        /// Sets the specified connector.
        /// </summary>
        /// <param name="connector">The connector to consider.</param>
        void SetConnector(IBdoConnector connector);

        // Open / Close -----------------------------

        /// <summary>
        /// Opens a connection.
        /// </summary>
        IBdoLog Open();

        /// <summary>
        /// Closes the existing connection.
        /// </summary>
        IBdoLog Close();

        /// <summary>
        /// Indicates whether the instance is connected.
        /// </summary>
        bool IsConnected();
    }
}
