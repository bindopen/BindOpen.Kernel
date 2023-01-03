namespace BindOpen.Runtime.Definition
{
    /// <summary>
    /// This class represents a DTO metrics dictionary.
    /// </summary>
    public class BdoMetricsDictionary : TBdoExtensionDictionary<IBdoMetricsDefinition>, IBdoMetricsDictionary
    {
        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the BdoMetricsDictionary class.
        /// </summary>
        public BdoMetricsDictionary()
        {
        }

        #endregion
    }
}
