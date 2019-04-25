using BindOpen.Framework.Core.Extensions.Definition.Metrics;

namespace BindOpen.Framework.Core.Extensions.Items.Metrics
{
    /// <summary>
    /// This class represents an task.
    /// </summary>
    public abstract class Metrics : TAppExtensionItem<IMetricsDefinition>, IMetrics
    {
        new public IMetricsDto Dto { get; }

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

        /// <summary>
        /// Instantiates a new instance of the Metrics class.
        /// </summary>
        /// <param name="dto">The DTO item of this instance.</param>
        protected Metrics(IMetricsDto dto)
        {
        }

        #endregion 
    }
}