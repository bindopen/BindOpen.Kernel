using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Runtime.Application.Options;
using BindOpen.Framework.Runtime.Application.Settings;

namespace BindOpen.Framework.Runtime.Application.Hosts
{
    /// <summary>
    /// This class represents a default application host.
    /// </summary>
    public class BdoDefaultHost : TBdoHost<BdoDefaultAppSettings>
    {
        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the BdoDefaultHost class.
        /// </summary>
        public BdoDefaultHost() : this(null)
        {
        }

        /// <summary>
        /// Instantiates a new instance of the BdoDefaultHost class.
        /// </summary>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="options">The options to consider.</param>
        public BdoDefaultHost(
            IBdoScope scope = null,
            ITBdoHostOptions<BdoDefaultAppSettings> options = null)
             : base(scope, options)
        {
        }

        #endregion
    }
}