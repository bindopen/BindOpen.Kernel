using BindOpen.Framework.Core.Data.Items;
using System;

namespace BindOpen.Framework.Core.Extensions.Attributes
{
    /// <summary>
    /// This class represents a carrier attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class BdoCarrierAttribute : DescribedDataItemAttribute
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
