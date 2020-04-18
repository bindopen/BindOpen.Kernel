using BindOpen.Data.Connections;
using BindOpen.Extensions.Runtime;
using BindOpen.System.Diagnostics;
using System;

namespace BindOpen.Application.Services
{
    /// <summary>
    /// This class represents a connected service.
    /// </summary>
    public abstract class BdoConnectedService : BdoService, IBdoConnectedService
    {
        // ------------------------------------------
        // VARIABLES
        // ------------------------------------------

        #region Variables

        // Scope ----------------------

        /// <summary>
        /// The connector of this instance.
        /// </summary>
        protected IBdoConnector _connector = null;

        #endregion

        // ------------------------------------------
        // VARIABLES
        // ------------------------------------------

        #region Variables

        /// <summary>
        /// The connector of this instance.
        /// </summary>
        public IBdoConnector Connector
        {
            get { return _connector; }
        }

        #endregion

        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// Initializes a new instance of the BdoConnectedService class.
        /// </summary>
        protected BdoConnectedService()
        {
        }

        #endregion

        // ------------------------------------------
        // MUTATORS
        // ------------------------------------------

        #region Mutators

        /// <summary>
        /// Sets the specified connector.
        /// </summary>
        /// <param name="connector">The connector to consider.</param>
        /// <returns>Returns this instance.</returns>
        public IBdoConnectedService WithConnector(IBdoConnector connector)
        {
            _connector = connector;

            return this;
        }

        /// <summary>
        /// Executing the specified action during a new connection.
        /// </summary>
        /// <param name="action">The action to execute.</param>
        /// <param name="isAutoConnected">Indicates whether the connection must be automatically connected.</param>
        public virtual void UsingConnection(Action<IBdoConnection> action, bool isAutoConnected = true)
            => UsingConnection((p, l) => action?.Invoke(p), null, isAutoConnected);

        /// <summary>
        /// Executing the specified action during a new connection.
        /// </summary>
        /// <param name="action">The action to execute.</param>
        /// <param name="log">The log to consider.</param>
        /// <param name="isAutoConnected">Indicates whether the connection must be automatically connected.</param>
        public virtual void UsingConnection(Action<IBdoConnection, IBdoLog> action, IBdoLog log, bool isAutoConnected = true)
        {
            _connector?.UsingConnection(action, log, isAutoConnected);
        }

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

            if (isDisposing)
            {
                GC.SuppressFinalize(this);
            }

            base.Dispose(isDisposing);
        }

        #endregion
    }
}
