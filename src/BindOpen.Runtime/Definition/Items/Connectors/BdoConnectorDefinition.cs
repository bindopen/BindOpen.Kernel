using BindOpen.Data;
using BindOpen.Data.Meta;
using System;

namespace BindOpen.Runtime.Definition
{
    /// <summary>
    /// This class represents a DTO connector definition.
    /// </summary>
    public class BdoConnectorDefinition : BdoExtensionItemDefinition,
        IBdoConnectorDefinition
    {
        // --------------------------------------------------
        // PROPERTIES
        // --------------------------------------------------

        #region Properties

        /// <summary>
        /// Data source kind of this instance.
        /// </summary>
        public DatasourceKind DatasourceKind { get; set; } = DatasourceKind.None;

        /// <summary>
        /// Data constraint statement of this instance.
        /// </summary>
        public IBdoMetaSpecList DatasourceDetailSpec { get; set; } = new BdoMetaSpecList();

        /// <summary>
        /// Item class of this instance.
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

        // --------------------------------------------------
        // CONSTRUCTORS
        // --------------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the ConnectorDefinition class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="extensionDefinition">The extensition definition to consider.</param>
        public BdoConnectorDefinition(
            string name,
            IBdoExtensionDefinition extensionDefinition) : base(name, "connectorDef_", extensionDefinition)
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
