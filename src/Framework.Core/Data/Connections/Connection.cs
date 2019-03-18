using System;
using System.Xml.Serialization;
using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.Extensions.Runtime.Connectors;
using BindOpen.Framework.Core.System.Diagnostics;

namespace BindOpen.Framework.Core.Data.Connections
{
    /// <summary>
    /// This class represents a connector.
    /// </summary>
    [XmlType("Connection", Namespace = "http://meltingsoft.com/bindopen/xsd")]
    [XmlRoot(ElementName = "connection", Namespace = "http://meltingsoft.com/bindopen/xsd", IsNullable = false)]
    public abstract class Connection : DataItem, IConnection
    {
        // ------------------------------------------
        // VARIABLES
        // ------------------------------------------

        #region Variables

        private Connector _Connector = null;

        #endregion

        // -----------------------------------------------
        // PROPERTIES
        // -----------------------------------------------

        #region Properties

        /// <summary>
        /// The connector of this instance.
        /// </summary>
        [XmlIgnore()]
        public Connector Connector
        {
            get
            {
                return this._Connector;
            }
            set
            {
                this.SetConnector(value);
            }
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
        protected Connection(Connector connector)
        {
            this._Connector = connector;
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
            this.Close();
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
        protected virtual void SetConnector(Connector connector)
        {
            this._Connector = connector;
        }

        // Open / Close -----------------------------

        /// <summary>
        /// Opens this instance.
        /// </summary>
        public virtual Log Open()
        {
            return this._Connector?.Open() ?? new Log();
        }

        /// <summary>
        /// Closes this instance.
        /// </summary>
        public virtual Log Close()
        {
            return this._Connector?.Close() ?? new Log();
        }

        /// <summary>
        /// Indicates whether the instance is connected.
        /// </summary>
        public virtual Boolean IsConnected()
        {
            return this._Connector?.IsConnected() ?? false;
        }

        #endregion
    }
}
