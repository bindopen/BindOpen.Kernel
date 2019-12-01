using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.Extensions.Runtime.Items;
using BindOpen.Framework.Core.System.Diagnostics;
using System.Xml.Serialization;

namespace BindOpen.Framework.Core.Data.Connections
{
    /// <summary>
    /// This class represents a connection.
    /// </summary>
    [XmlType("BdoConnection", Namespace = "https://bindopen.org/xsd")]
    [XmlRoot(ElementName = "connection", Namespace = "https://bindopen.org/xsd", IsNullable = false)]
    public abstract class BdoConnection : DataItem, IBdoConnection
    {
        // ------------------------------------------
        // VARIABLES
        // ------------------------------------------

        #region Variables

        private IBdoConnector _connector = null;

        #endregion

        // -----------------------------------------------
        // PROPERTIES
        // -----------------------------------------------

        #region Properties

        /// <summary>
        /// The connector of this instance.
        /// </summary>
        [XmlIgnore()]
        public IBdoConnector Connector
        {
            get => _connector;
        }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the Connection class.
        /// </summary>
        protected BdoConnection() : base()
        {
        }

        /// <summary>
        /// Instantiates a new instance of the Connection class.
        /// </summary>
        /// <param name="connector">The connector to consider.</param>
        protected BdoConnection(IBdoConnector connector)
        {
            _connector = connector;
        }

        #endregion

        // ------------------------------------------
        // IDISPOSABLE METHODS
        // ------------------------------------------

        #region IDisposable_Methods

        /// <summary>
        /// Disposes this instance. 
        /// </summary>
        public override void Dispose()
        {
            Close();
            base.Dispose();
        }

        #endregion

        // ------------------------------------------
        // MANAGEMENT
        // ------------------------------------------

        #region Management

        /// <summary>
        /// Sets the connector of this instance.
        /// </summary>
        /// <param name="connector">The database connector to consider.</param>
        public virtual void SetConnector(IBdoConnector connector)
        {
            _connector = connector;
        }

        // Open / Close -----------------------------

        /// <summary>
        /// Opens this instance.
        /// </summary>
        public virtual IBdoLog Open()
        {
            return _connector?.Open();
        }

        /// <summary>
        /// Closes this instance.
        /// </summary>
        public virtual IBdoLog Close()
        {
            return _connector?.Close();
        }

        /// <summary>
        /// Indicates whether the instance is connected.
        /// </summary>
        public virtual bool IsConnected()
        {
            return _connector?.IsConnected() == true;
        }

        #endregion
    }
}
