namespace BindOpen.Data.Meta
{
    /// <summary>
    /// This class represents a config.
    /// </summary>
    public class BdoConfigurationSet :
        TBdoSet<IBdoConfiguration>,
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
