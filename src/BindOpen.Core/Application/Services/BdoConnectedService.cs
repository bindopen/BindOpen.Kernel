using BindOpen.Data.Items;
using BindOpen.Extensions.Runtime;

namespace BindOpen.Application.Services
{
    /// <summary>
    /// This class represents a connected service.
    /// </summary>
    public abstract class BdoConnectedService : DataItem, IBdoConnectedService
    {
        /// <summary>
        /// The connector of this instance.
        /// </summary>
        protected IBdoConnector _connector = null;

        /// <summary>
        /// The connector of this instance.
        /// </summary>
        public IBdoConnector Connector
        {
            get { return _connector; }
        }

        /// <summary>
        /// Initializes a new instance of the BdoConnectedService class.
        /// </summary>
        protected BdoConnectedService()
        {
        }

        /// <summary>
        /// Initializes a new instance of the BdoConnectedService class.
        /// </summary>
        /// <param name="connector">The connector to consider.</param>
        protected BdoConnectedService(IBdoConnector connector)
        {
            WithConnector(connector);
        }

        /// <summary>
        /// Sets the specified connector.
        /// </summary>
        /// <param name="connector">The connector to consider.</param>
        public IBdoConnectedService WithConnector(IBdoConnector connector)
        {
            _connector = connector;

            return this;
        }

        // ------------------------------------------
        // IDISPOSABLE METHODS
        // ------------------------------------------

        #region IDisposable_Methods

        /// <summary>
        /// Disposes this instance. 
        /// </summary>
        protected override void Dispose(bool isDisposing)
        {
            base.Dispose(isDisposing);

            if (isDisposing)
            {
                _connector?.Dispose();
            }
        }

        #endregion
    }
}
