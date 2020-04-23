using BindOpen.Data.Items;
using BindOpen.Extensions.Runtime;
using BindOpen.System.Diagnostics;
using System.Data;
using System.Xml.Serialization;

namespace BindOpen.Data.Connections
{
    /// <summary>
    /// This class represents a connection.
    /// </summary>
    public abstract class BdoConnection : DataItem, IBdoConnection
    {
        // ------------------------------------------
        // VARIABLES
        // ------------------------------------------

        #region Variables

        /// <summary>
        /// The connector of this instance.
        /// </summary>
        protected IBdoConnector _connector = null;

        #endregion

        // -----------------------------------------------
        // PROPERTIES
        // -----------------------------------------------

        #region Properties

        /// <summary>
        /// The connector of this instance.
        /// </summary>
        [XmlIgnore()]
        public IBdoConnector Connector => _connector;

        /// <summary>
        /// The connection string of this instance.
        /// </summary>
        public string ConnectionString => _connector?.ConnectionString;

        /// <summary>
        /// The connection timeout of this instance.
        /// </summary>
        public int ConnectionTimeout => _connector?.ConnectionTimeOut ?? 0;

        /// <summary>
        /// The state of this instance.
        /// </summary>
        public ConnectionState State => ConnectionState.Closed;

        #endregion

        // ------------------------------------------
        // IDISPOSABLE METHODS
        // ------------------------------------------

        #region IDisposable_Methods

        private bool _isDisposed = false;

        /// <summary>
        /// Disposes this instance. 
        /// </summary>
        /// <param name="isDisposing">Indicates whether this instance is disposing</param>
        protected override void Dispose(bool isDisposing)
        {
            if (_isDisposed)
            {
                return;
            }

            _connector?.Dispose();

            _isDisposed = true;

            base.Dispose(isDisposing);
        }

        #endregion

        // ------------------------------------------
        // MANAGEMENT
        // ------------------------------------------

        #region Management

        // Open / Close -----------------------------

        /// <summary>
        /// Connects this instance.
        /// </summary>
        public abstract IBdoLog Connect();

        /// <summary>
        /// Disconnects this instance.
        /// </summary>
        public abstract IBdoLog Disconnect();

        /// <summary>
        /// Updates the connector.
        /// </summary>
        /// <param name="connector">The connector to consider.</param>
        public void WithConnector(IBdoConnector connector)
        {
            _connector = connector;
        }

        #endregion
    }
}
