using BindOpen.Extensions.Attributes;
using BindOpen.Extensions.Runtime;

namespace BindOpen.Extensions.Connectors
{
    /// <summary>
    /// This class represents a test connector.
    /// </summary>
    [BdoConnector(Name = "runtime$test")]
    public class TestConnector : BdoConnector
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
        public TestConnector() : base()
        {
        }

        #endregion
    }
}
