using System;

namespace BindOpen.Framework.Core.Extensions.Attributes
{
    /// <summary>
    /// This class represents an attribute of metrics.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class MetricsAttribute : AppExtensionItemAttribute
    {
        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the MetricsAttribute class.
        /// </summary>
        public MetricsAttribute() : base()
        {
        }

        #endregion
    }
}
