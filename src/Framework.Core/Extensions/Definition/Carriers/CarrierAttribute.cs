using System;
using BindOpen.Framework.Core.Data.Items.Source;
using BindOpen.Framework.Core.Extensions.Attributes;

namespace BindOpen.Framework.Core.Extensions.Definition.Carriers
{
    /// <summary>
    /// This class represents an attribute of carrier definition.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class CarrierAttribute : ExtensionItemAttribute
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// The item class of this instance.
        /// </summary>
        public string ItemClass { get; set; } = null;

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
