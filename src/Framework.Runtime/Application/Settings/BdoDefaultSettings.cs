using BindOpen.Framework.Core.Application.Configuration;
using BindOpen.Framework.Core.Application.Scopes;

namespace BindOpen.Framework.Runtime.Application.Settings
{
    /// <summary>
    /// This class represents a base settings.
    /// </summary>
    public class BdoDefaultSettings : TBdoSettings<BdoBaseConfiguration>
    {
        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the BdoDefaultSettings class.
        /// </summary>
        public BdoDefaultSettings()
        {
            _configuration = new BdoBaseConfiguration();
        }

        /// <summary>
        /// Instantiates a new instance of the BdoDefaultSettings class.
        /// </summary>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="configuration">The configuration to consider.</param>
        public BdoDefaultSettings(IBdoScope scope, BdoBaseConfiguration configuration) : base(scope, configuration)
        {
        }

        #endregion
    }
}
