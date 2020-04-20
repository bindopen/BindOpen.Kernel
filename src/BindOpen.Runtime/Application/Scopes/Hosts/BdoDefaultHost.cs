using BindOpen.Application.Settings;

namespace BindOpen.Application.Scopes
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
        public BdoDefaultHost() : base()
        {
        }

        #endregion
    }
}