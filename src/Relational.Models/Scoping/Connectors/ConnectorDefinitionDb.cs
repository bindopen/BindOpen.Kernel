using BindOpen.Data;

namespace BindOpen.Scoping.Connectors
{
    /// <summary>
    /// This class represents a connector definition database entity.
    /// </summary>
    public class ConnectorDefinitionDb : ExtensionDefinitionDb
    {
        // --------------------------------------------------
        // PROPERTIES
        // --------------------------------------------------

        #region Properties

        /// <summary>
        /// The data source kind of this instance.
        /// </summary>
        public DatasourceKind DatasourceKind { get; set; } = DatasourceKind.None;

        /// <summary>
        /// The item class of this instance.
        /// </summary>
        public string ItemClass { get; set; }

        #endregion

        // --------------------------------------------------
        // CONSTRUCTORS
        // --------------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the ConnectorDefinitionDb class.
        /// </summary>
        public ConnectorDefinitionDb()
        {
        }

        #endregion
    }

}
