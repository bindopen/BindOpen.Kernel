using BindOpen.Framework.Core.Extensions.Attributes;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Runtime.Data.Connections;

namespace BindOpen.Framework.Runtime.Extensions.Connectors
{
    /// <summary>
    /// This class represents a file NFS connector.
    /// </summary>
    [BdoConnector(Name = "runtime$nfsConnector")]
    public class BdoNFSConnector : BdoRepoConnector
    {
        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the NFSConnector class.
        /// </summary>
        public BdoNFSConnector() : base()
        {
        }

        #endregion

        // ------------------------------------------
        // ACCESSORS
        // ------------------------------------------

        #region Accessors

        /// <summary>
        /// Creates a new connection.
        /// </summary>
        /// <param name="log">The log to consider.</param>
        public override BdoRepoConnection CreateConnection(IBdoLog log = null)
        {
            return new BdoNFSConnection(this);
        }

        #endregion
    }
}
