using BindOpen.Framework.Core.Extensions.Attributes;
using BindOpen.Framework.Core.Extensions.Items.Connectors;

namespace BindOpen.Framework.Runtime.Extensions.Connectors
{
    /// <summary>
    /// This class represents a test connector.
    /// </summary>
    [Connector(Name = "runtime$test")]
    public class TestConnector : Connector
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
