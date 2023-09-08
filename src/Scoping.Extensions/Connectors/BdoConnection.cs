using BindOpen.Kernel.Data;
using BindOpen.Kernel.Logging;
using System.Data;

namespace BindOpen.Kernel.Scoping.Connectors
{
    /// <summary>
    /// This class represents a connection.
    /// </summary>
    public abstract class BdoConnection : BdoObject, IBdoConnection
    {
        // -----------------------------------------------
        // IBdoConnection Implementation
        // -----------------------------------------------

        #region IBdoConnection

        /// <summary>
        /// The connector of this instance.
        /// </summary>
        public IBdoConnector Connector { get; set; }

        /// <summary>
        /// The connection string of this instance.
        /// </summary>
        public string ConnectionString => Connector?.ConnectionString;

        /// <summary>
        /// The connection timeout of this instance.
        /// </summary>
        public int ConnectionTimeout => Connector?.ConnectionTimeOut ?? 0;

        /// <summary>
        /// The state of this instance.
        /// </summary>
        public ConnectionState State => ConnectionState.Closed;

        /// <summary>
        /// Connects this instance.
        /// </summary>
        public abstract IBdoConnection Connect(IBdoLog log = null);

        /// <summary>
        /// Disconnects this instance.
        /// </summary>
        public abstract IBdoConnection Disconnect(IBdoLog log = null);

        #endregion

        // ------------------------------------------
        // IDISPOSABLE METHODS
        // ------------------------------------------

        #region IDisposable_Methods

        private bool _isDisposed = false;

        /// <summary>
        /// Disposes this instance. 
        /// </summary>
        /// <param key="isDisposing">Indicates whether this instance is disposing</param>
        protected override void Dispose(bool isDisposing)
        {
            if (_isDisposed)
            {
                return;
            }

            Connector?.Dispose();

            _isDisposed = true;

            base.Dispose(isDisposing);
        }

        #endregion
    }
}
