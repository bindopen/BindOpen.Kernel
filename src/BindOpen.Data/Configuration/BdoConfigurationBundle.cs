using BindOpen.Data.Items;

namespace BindOpen.Data.Configuration
{
    /// <summary>
    /// This class represents a config.
    /// </summary>
    public class BdoConfigurationBundle :
        TBdoItemSet<IBdoConfiguration>,
        IBdoConfigurationBundle
    {
        // -------------------------------------------------------------
        // CONSTRUCTORS
        // -------------------------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the BdoConfigurationBundle class.
        /// </summary>
        public BdoConfigurationBundle() : base()
        {
        }

        #endregion
    }
}
