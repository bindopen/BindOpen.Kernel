using System.Xml.Serialization;
using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.Extensions.Items.Connectors;
using BindOpen.Framework.Core.System.Diagnostics;

namespace BindOpen.Framework.Core.Data.Connections
{
    /// <summary>
    /// This class represents a connector.
    /// </summary>
    [XmlType("Connection", Namespace = "https://bindopen.org/xsd")]
    [XmlRoot(ElementName = "connection", Namespace = "https://bindopen.org/xsd", IsNullable = false)]
    public abstract class Connection : DataItem, IConnection
    {
        // ------------------------------------------
        // VARIABLES
        // ------------------------------------------

        #region Variables

        private IConnector _connector = null;

        #endregion

        // -----------------------------------------------
        // PROPERTIES
        // -----------------------------------------------

        #region Properties

        /// <summary>
        /// The connector of this instance.
        /// </summary>
        [XmlIgnore()]
        public IConnector Connector
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
        protected Connection() : base()
        {
        }

        /// <summary>
        /// Instantiates a new instance of the Connection class.
        /// </summary>
        /// <param name="connector">The connector to consider.</param>
        protected Connection(IConnector connector)
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
        public virtual void SetConnector(IConnector connector)
        {
            _connector = connector;
        }

        // Open / Close -----------------------------

        /// <summary>
        /// Opens this instance.
        /// </summary>
        public virtual ILog Open()
        {
            return _connector?.Open() ?? new Log();
        }

        /// <summary>
        /// Closes this instance.
        /// </summary>
        public virtual ILog Close()
        {
            return _connector?.Close() ?? new Log();
        }

        /// <summary>
        /// Indicates whether the instance is connected.
        /// </summary>
        public virtual bool IsConnected()
        {
            return _connector?.IsConnected() ?? false;
        }

        #endregion
    }
}
