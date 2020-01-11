using BindOpen.Framework.Data.Items;
using BindOpen.Framework.Extensions.Runtime;
using BindOpen.Framework.System.Diagnostics;
using System.Data;
using System.Xml.Serialization;

namespace BindOpen.Framework.Data.Connections
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

        /// <summary>
        /// Disposes this instance. 
        /// </summary>
        protected override void Dispose(bool isDisposing)
        {
            Close();
            base.Dispose(isDisposing);

            if (isDisposing)
            {
                _connector?.Dispose();
            }
        }

        #endregion

        // ------------------------------------------
        // MANAGEMENT
        // ------------------------------------------

        #region Management

        // Open / Close -----------------------------

        /// <summary>
        /// Opens this instance.
        /// </summary>
        public abstract IBdoLog Open();

        /// <summary>
        /// Closes this instance.
        /// </summary>
        public abstract IBdoLog Close();

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
