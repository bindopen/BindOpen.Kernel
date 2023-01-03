using BindOpen.Data.Elements;
using BindOpen.Data.Items;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace BindOpen.Runtime.Definition
{
    /// <summary>
    /// This class represents a DTO carrier definition.
    /// </summary>
    [XmlType("CarrierDefinition", Namespace = "https://docs.bindopen.org/xsd")]
    [XmlRoot(ElementName = "carrier.definition", Namespace = "https://docs.bindopen.org/xsd", IsNullable = false)]
    public class BdoCarrierDefinitionDto : BdoExtensionItemDefinitionDto
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// The data source kind of this instance.
        /// </summary>
        [JsonPropertyName("dataSourceKind")]
        [XmlElement("dataSourceKind")]
        public DatasourceKind DatasourceKind { get; set; } = DatasourceKind.None;

        /// <summary>
        /// The item class of this instance.
        /// </summary>
        [JsonPropertyName("itemClass")]
        [XmlElement("itemClass")]
        public string ItemClass { get; set; }

        // DTO

        /// <summary>
        /// The set of element specifications of this instance.
        /// </summary>
        [JsonPropertyName("detail.specification")]
        [XmlElement("detail.specification")]
        public BdoElementSpecSetDto DetailSpec { get; set; }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the CarrierDefinition class.
        /// </summary>
        public BdoCarrierDefinitionDto()
        {
        }

        #endregion
    }
}
