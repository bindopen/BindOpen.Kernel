using System;
using BindOpen.Framework.Core.Data.Items.Source;
using BindOpen.Framework.Core.Extensions.Attributes;

namespace BindOpen.Framework.Core.Extensions.Carriers
{
    /// <summary>
    /// This class represents a carrier attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class CarrierAttribute : AppExtensionItemAttribute
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// The data source kind of this instance.
        /// </summary>
        public DataSourceKind DataSourceKind { get; set; } = DataSourceKind.None;

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the CarrierAttribute class.
        /// </summary>
        public CarrierAttribute() : base()
        {
        }

        #endregion
    }
}
