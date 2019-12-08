using BindOpen.Framework.Core.Data.Connections;
using BindOpen.Framework.Core.Extensions.Runtime.Items;
using BindOpen.Framework.Databases.Extensions.Connectors;
using System.Data;
using System.Xml.Serialization;

namespace BindOpen.Framework.Databases.Data.Connections
{
    /// <summary>
    /// This class represents a database connection.
    /// </summary>
    [XmlType("DatabaseConnection", Namespace = "https://bindopen.org/xsd")]
    [XmlRoot(ElementName = "databaseConnection", Namespace = "https://bindopen.org/xsd", IsNullable = false)]
    public class BdoDbConnection : BdoConnection, IBdoDbConnection
    {
        // -----------------------------------------------
        // PROPERTIES
        // -----------------------------------------------

        #region Properties

        /// <summary>
        /// The connector of this instance.
        /// </summary>
        [XmlIgnore()]
        public new DatabaseConnector Connector
        {
            get
            {
                return base.Connector as DatabaseConnector;
            }
            set
            {
                SetConnector(value);
            }
        }

        /// <summary>
        /// Gets the database connection of this instance.
        /// </summary>
        /// <returns>Returns the connection of this instance.</returns>
        [XmlIgnore()]
        public IDbConnection DotNetDbConnection => Connector?.GetDotNetDbConnection();

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the DatabaseConnection class.
        /// </summary>
        public BdoDbConnection() : base()
        {
        }

        /// <summary>
        /// Instantiates a new instance of the DatabaseConnection class.
        /// </summary>
        /// <param name="connector">The connector to consider.</param>
        public BdoDbConnection(DatabaseConnector connector)
        {
            Connector = connector;
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
        public override void SetConnector(IBdoConnector connector)
        {
            base.SetConnector(connector);
        }

        #endregion
    }
}
