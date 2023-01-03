using BindOpen.Data.Elements;
using BindOpen.Data.Items;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace BindOpen.Runtime.Definition
{
    /// <summary>
    /// This class represents a DTO connector definition.
    /// </summary>
    [XmlType("ConnectorDefinition", Namespace = "https://docs.bindopen.org/xsd")]
    [XmlRoot(ElementName = "connector.definition", Namespace = "https://docs.bindopen.org/xsd", IsNullable = false)]
    public class BdoConnectorDefinitionDto : BdoExtensionItemDefinitionDto
    {
        // --------------------------------------------------
        // PROPERTIES
        // --------------------------------------------------

        #region Properties

        /// <summary>
        /// Data source kind of this instance.
        /// </summary>
        [JsonPropertyName("dataSourceKind")]
        [XmlElement("dataSourceKind")]
        public DatasourceKind DatasourceKind { get; set; } = DatasourceKind.None;

        /// <summary>
        /// Item class of this instance.
        /// </summary>
        [JsonPropertyName("itemClass")]
        [XmlElement("itemClass")]
        public string ItemClass { get; set; }

        // DTO

        /// <summary>
        /// The data source detail specification of this instance.
        /// </summary>
        [JsonPropertyName("dataSource.specification")]
        [XmlElement("dataSource.specification")]
        public BdoElementSpecSetDto DatasourceDetailSpec { get; set; }

        #endregion

        // --------------------------------------------------
        // CONSTRUCTORS
        // --------------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the ConnectorDefinition class.
        /// </summary>
        public BdoConnectorDefinitionDto()
        {
        }

        #endregion
    }

}
