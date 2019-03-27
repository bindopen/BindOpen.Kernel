using System;
using BindOpen.Framework.Core.Extensions.Runtime.Connectors;
using BindOpen.Framework.Core.System.Diagnostics;

namespace BindOpen.Framework.Core.Data.Connections
{
    /// <summary>
    /// This interfaces represents a connection.
    /// </summary>
    public interface IConnection
    {
        // Open / Close -----------------------------

        /// <summary>
        /// Opens a connection.
        /// </summary>
        Log Open();

        /// <summary>
        /// Closes the existing connection.
        /// </summary>
        Log Close();

        /// <summary>
        /// Indicates whether the instance is connected.
        /// </summary>
        Boolean IsConnected();
    }
}
