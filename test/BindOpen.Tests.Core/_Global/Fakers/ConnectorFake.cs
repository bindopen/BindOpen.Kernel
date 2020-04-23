using BindOpen.Data.Connections;
using BindOpen.Extensions.Runtime;
using BindOpen.System.Diagnostics;

namespace BindOpen.Tests.Core.Fakers
{
    /// <summary>
    /// This class represents a test connector.
    /// </summary>
    [BdoConnector(Name = "tests.core$testConnector")]
    public class ConnectorFake : TBdoConnector<ConnectionFake>
    {
        /// <summary>
        /// The host of this instance.
        /// </summary>
        [DetailProperty(Name = "host")]
        public string Host { get; set; }

        /// <summary>
        /// Indicates whether this instance enables SSL.
        /// </summary>
        [DetailProperty(Name = "isSslEnabled")]
        public bool? IsSslEnabled { get; set; }

        /// <summary>
        /// The port of this instance.
        /// </summary>
        [DetailProperty(Name = "port")]
        public int? Port { get; set; }

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the TestConnector class.
        /// </summary>
        public ConnectorFake() : base()
        {
        }

        #endregion

        // ------------------------------------------
        // MUTATORS
        // ------------------------------------------

        #region Mutators

        /// <summary>
        /// Creates a new connection.
        /// </summary>
        /// <param name="log">The log to consider.</param>
        public override IBdoConnection CreateConnection(IBdoLog log = null)
        {
            return new ConnectionFake(this);
        }

        #endregion
    }
}
