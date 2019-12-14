using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Runtime.Application.Configuration;

namespace BindOpen.Framework.Runtime.Application.Settings.Hosts
{
    /// <summary>
    /// This class represents a BindOpen host settings.
    /// </summary>
    public class TBdoAppSettings<Q> : TBdoSettings<Q>, ITBdoSettings<Q>, IBdoAppSettings
        where Q : class, IBdoAppConfiguration, new()
    {
        // -------------------------------------------------------
        // PROPERTIES
        // -------------------------------------------------------

        #region Properties

        /// <summary>
        /// The host configuration of this instance.
        /// </summary>
        public new Q Configuration => base.Configuration as Q;

        /// <summary>
        /// The host configuration of this instance.
        /// </summary>
        public IBdoAppConfiguration HostConfiguration => Configuration;

        #endregion

        // -------------------------------------------------------------
        // CONSTRUCTORS
        // -------------------------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the TBdoHostSettings class.
        /// </summary>
        public TBdoAppSettings()
            : base()
        {
        }

        /// <summary>
        /// Instantiates a new instance of the TBdoHostSettings class.
        /// </summary>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="configuration">The configuration to consider.</param>
        public TBdoAppSettings(IBdoScope scope, Q configuration)
            : base(scope, configuration)
        {
        }

        #endregion
    }
}
