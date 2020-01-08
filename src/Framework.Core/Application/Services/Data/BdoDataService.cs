using BindOpen.Framework.Core.Data.Connections;
using BindOpen.Framework.Core.Data.Items;

namespace BindOpen.Framework.Core.Application.Services.Data
{
    /// <summary>
    /// This class represents a BindOpen data service.
    /// </summary>
    public abstract class BdoDataService : DataItem, IBdoDataService
    {
        /// <summary>
        /// The connection of this instance.
        /// </summary>
        protected IBdoConnection _connection = null;

        /// <summary>
        /// The connection of this instance.
        /// </summary>
        public IBdoConnection Connection
        {
            get { return _connection; }
        }

        /// <summary>
        /// Initializes a new instance of the BdoDataService class.
        /// </summary>
        protected BdoDataService()
        {
        }

        /// <summary>
        /// Initializes a new instance of the BdoDataService class.
        /// </summary>
        /// <param name="connection">The connection to consider.</param>
        protected BdoDataService(IBdoConnection connection)
        {
            SetConnection(connection);
        }

        /// <summary>
        /// Sets the specified connection.
        /// </summary>
        /// <param name="connection">The connection to consider.</param>
        public void SetConnection(IBdoConnection connection)
        {
            _connection = connection;
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
                _connection?.Dispose();
            }
        }

        #endregion
    }
}
