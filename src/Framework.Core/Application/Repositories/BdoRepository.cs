using BindOpen.Framework.Data.Items;
using BindOpen.Framework.Extensions.Runtime;

namespace BindOpen.Framework.Application.Repositories
{
    /// <summary>
    /// This class represents a repository.
    /// </summary>
    public abstract class BdoRepository : DataItem, IBdoRepository
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
        /// Initializes a new instance of the BdoRepository class.
        /// </summary>
        protected BdoRepository()
        {
        }

        /// <summary>
        /// Initializes a new instance of the BdoRepository class.
        /// </summary>
        /// <param name="connector">The connector to consider.</param>
        protected BdoRepository(IBdoConnector connector)
        {
            SetConnector(connector);
        }

        /// <summary>
        /// Sets the specified connector.
        /// </summary>
        /// <param name="connector">The connector to consider.</param>
        public void SetConnector(IBdoConnector connector)
        {
            _connector = connector;
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
