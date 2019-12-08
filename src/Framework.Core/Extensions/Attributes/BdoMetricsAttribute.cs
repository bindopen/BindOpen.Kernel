using BindOpen.Framework.Core.Data.Items;
using System;

namespace BindOpen.Framework.Core.Extensions.Attributes
{
    /// <summary>
    /// This class represents an attribute of metrics.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class BdoMetricsAttribute : DescribedDataItemAttribute
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
