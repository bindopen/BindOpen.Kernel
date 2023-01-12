using BindOpen.Meta.Elements;
using BindOpen.Meta.Items;
using System;

namespace BindOpen.Runtime.Definition
{
    /// <summary>
    /// This class represents a DTO carrier definition.
    /// </summary>
    public class BdoCarrierDefinition : BdoExtensionItemDefinition, IBdoCarrierDefinition
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// The data source kind of this instance.
        /// </summary>
        public DatasourceKind DatasourceKind { get; set; } = DatasourceKind.None;

        /// <summary>
        /// Data constraint statement of this instance.
        /// </summary>
        public IBdoElementSpecSet DetailSpec { get; set; }

        /// <summary>
        /// The item class of this instance.
        /// </summary>
        public string ItemClass { get; set; }

        /// <summary>
        /// The runtime type of this instance.
        /// </summary>
        public Type RuntimeType { get; set; }

        /// <summary>
        /// The unique ID of this instance.
        /// </summary>
        public new string UniqueId { get => ExtensionDefinition?.UniqueId + "$" + Name; }

        #endregion


        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the CarrierDefinition class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="extensionDefinition">The extensition definition to consider.</param>
        public BdoCarrierDefinition(
            string name,
            IBdoExtensionDefinition extensionDefinition)
            : base(name, "carrierDef_", extensionDefinition)
        {
        }

        #endregion


        // ------------------------------------------
        // ACCESSORS
        // ------------------------------------------

        #region Accessors

        /// <summary>
        /// Returns the key of this instance.
        /// </summary>
        /// <returns></returns>
        public override string Key()
        {
            return UniqueId;
        }

        #endregion
    }
}
