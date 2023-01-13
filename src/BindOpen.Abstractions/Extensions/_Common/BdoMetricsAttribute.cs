using BindOpen.MetaData.Items;
using System;

namespace BindOpen.Extensions
{
    /// <summary>
    /// This class represents an attribute of metrics.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class BdoMetricsAttribute : TitledDescribedDataItemAttribute
    {
        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the MetricsAttribute class.
        /// </summary>
        public BdoMetricsAttribute() : base()
        {
        }

        #endregion
    }
}
