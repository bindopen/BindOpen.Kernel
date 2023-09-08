using BindOpen.Kernel.Logging;
using BindOpen.Kernel.Scoping;

namespace BindOpen.Kernel.Tests
{
    /// <summary>
    /// This class represents a database connection.
    /// </summary>
    public class ConnectionFake : BdoConnection
    {
        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the ConnectionFake class.
        /// </summary>
        /// <param key="connector">The connector to consider.</param>
        public ConnectionFake(ConnectorFake connector)
        {
            Connector = connector;
        }

        #endregion

        // ------------------------------------------
        // IBdoConnection_Methods METHODS
        // ------------------------------------------

        #region IBdoConnection_Methods

        /// <summary>
        /// Connects this instance.
        /// </summary>
        /// <returns>Returns the log of process.</returns>
        public override IBdoConnection Connect(IBdoLog log = null)
        {
            return this;
        }

        /// <summary>
        /// Disconnects this instance.
        /// </summary>
        /// <returns>Returns the log of process.</returns>
        public override IBdoConnection Disconnect(IBdoLog log = null)
        {
            return this;
        }

        #endregion
    }
}
