using BindOpen.System.Scoping;
using BindOpen.System.Data;
using BindOpen.System.Scoping;

namespace BindOpen.System.Scoping
{
    /// <summary>
    /// This class represents a DTO connector definition.
    /// </summary>
    public class BdoConnectorDefinition : BdoEntityDefinition,
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

        #endregion

        // --------------------------------------------------
        // CONSTRUCTORS
        // --------------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the ConnectorDefinition class.
        /// </summary>
        /// <param key="name">The name to consider.</param>
        /// <param key="extensionDefinition">The extensition definition to consider.</param>
        public BdoConnectorDefinition(
            string name,
            IBdoPackageDefinition extensionDefinition)
            : base(name, extensionDefinition, "connectorDef_")
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
            return UniqueName;
        }

        #endregion
    }

}
