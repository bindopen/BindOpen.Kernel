using BindOpen.Framework.Core.Data.Items.Datasources;
using BindOpen.Framework.Core.Extensions.Attributes;
using System;

namespace BindOpen.Framework.Core.Extensions.Carriers
{
    /// <summary>
    /// This class represents a carrier attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class BdoCarrierAttribute : BdoExtensionItemAttribute
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// The data source kind of this instance.
        /// </summary>
        public DatasourceKind DatasourceKind { get; set; } = DatasourceKind.None;

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the CarrierAttribute class.
        /// </summary>
        public BdoCarrierAttribute() : base()
        {
        }

        #endregion
    }
}
