using BindOpen.Data.Meta;
using BindOpen.Extensions.Connecting;
using BindOpen.Logging;

namespace BindOpen.Tests
{
    /// <summary>
    /// This class represents a test connector.
    /// </summary>
    [BdoConnector(Name = "tests.core$testConnector")]
    public class ConnectorFake : BdoConnector
    {
        /// <summary>
        /// The host of this instance.
        /// </summary>
        [BdoData(Name = "host")]
        public string Host { get; set; }

        /// <summary>
        /// Indicates whether this instance enables SSL.
        /// </summary>
        [BdoData(Name = "isSslEnabled")]
        public bool? IsSslEnabled { get; set; }

        /// <summary>
        /// The port of this instance.
        /// </summary>
        [BdoData(Name = "port")]
        public int? Port { get; set; }

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the ConnectorFake class.
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
        public override ConnectionFake NewConnection(IBdoLog log = null)
        {
            return new ConnectionFake(this);
        }

        #endregion
    }
}
