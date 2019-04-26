using BindOpen.Framework.Core.Extensions.Items.Connectors;
using BindOpen.Framework.Core.System.Diagnostics;

namespace BindOpen.Framework.Core.Data.Connections
{
    /// <summary>
    /// This interfaces represents a connection.
    /// </summary>
    public interface IConnection
    {
        IConnector Connector { get; }

        void SetConnector(IConnector connector);

        // Open / Close -----------------------------

        /// <summary>
        /// Opens a connection.
        /// </summary>
        ILog Open();

        /// <summary>
        /// Closes the existing connection.
        /// </summary>
        ILog Close();

        /// <summary>
        /// Indicates whether the instance is connected.
        /// </summary>
        bool IsConnected();
    }
}
