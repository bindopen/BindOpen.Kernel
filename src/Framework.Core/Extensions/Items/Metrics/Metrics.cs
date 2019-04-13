using BindOpen.Framework.Core.Data.Items;

namespace BindOpen.Framework.Core.Extensions.Items.Metrics
{
    /// <summary>
    /// This class represents an task.
    /// </summary>
    public abstract class Metrics : DataItem, IMetrics
    {
        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the Metrics class.
        /// </summary>
        protected Metrics() : base()
        {
        }

        #endregion
   }
}