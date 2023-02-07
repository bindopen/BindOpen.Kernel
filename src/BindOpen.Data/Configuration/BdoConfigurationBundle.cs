using BindOpen.Data.Items;

namespace BindOpen.Data.Configuration
{
    /// <summary>
    /// This class represents a config.
    /// </summary>
    public class BdoConfigurationSet :
        TBdoList<IBdoConfiguration>,
        IBdoConfigurationSet
    {
        // -------------------------------------------------------------
        // CONSTRUCTORS
        // -------------------------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the BdoConfigurationSet class.
        /// </summary>
        public BdoConfigurationSet() : base()
        {
        }

        #endregion
    }
}
