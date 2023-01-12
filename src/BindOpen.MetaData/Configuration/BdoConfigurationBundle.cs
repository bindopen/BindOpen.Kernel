using BindOpen.Abstractions.Meta.Configuration;
using BindOpen.Meta.Items;

namespace BindOpen.Meta.Configuration
{
    /// <summary>
    /// This class represents a configuration.
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
