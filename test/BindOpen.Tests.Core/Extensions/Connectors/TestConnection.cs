using BindOpen.Data.Connections;
using BindOpen.System.Diagnostics;
using System.Xml.Serialization;

namespace BindOpen.Tests.Core.Extensions.Connectors
{
    /// <summary>
    /// This class represents a database connection.
    /// </summary>
    [XmlType("TestConnection", Namespace = "https://bindopen.org/xsd")]
    [XmlRoot(ElementName = "testConnection", Namespace = "https://bindopen.org/xsd", IsNullable = false)]
    public class TestConnection : BdoConnection
    {
        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the BdoDbConnection class.
        /// </summary>
        /// <param name="connector">The connector to consider.</param>
        public TestConnection(TestConnector connector)
        {
            _connector = connector;
        }

        #endregion

        // ------------------------------------------
        // TESTCONNECTION METHODS
        // ------------------------------------------

        #region IBdoConnection_Methods

        /// <summary>
        /// Connects this instance.
        /// </summary>
        /// <returns>Returns the log of process.</returns>
        public override IBdoLog Connect()
        {
            return new BdoLog();
        }

        /// <summary>
        /// Disconnects this instance.
        /// </summary>
        /// <returns>Returns the log of process.</returns>
        public override IBdoLog Disconnect()
        {
            return new BdoLog();
        }

        #endregion
    }
}
