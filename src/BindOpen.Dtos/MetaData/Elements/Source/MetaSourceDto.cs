using BindOpen.Extensions.Connecting;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace BindOpen.MetaData.Elements
{
    /// <summary>
    /// This class represents a data source element.
    /// </summary>
    /// <remarks>A data source element can only have one item maximum.</remarks>
    [XmlType("MetaSource", Namespace = "https://xsd.bindopen.org")]
    [XmlRoot(ElementName = "source", Namespace = "https://xsd.bindopen.org", IsNullable = false)]
    public class MetaSourceDto : MetaElementDto
    {
        // --------------------------------------------------
        // PROPERTIES
        // --------------------------------------------------

        #region Properties

        /// <summary>
        /// The definition unique ID of this instance.
        /// </summary>
        [JsonPropertyName("definition")]
        [XmlAttribute("definition")]
        public string DefinitionUniqueId { get; set; }

        // --------------------------------------------------

        /// <summary>
        /// Connectors of this instance.
        /// </summary>
        [JsonPropertyName("items")]
        [XmlArray("items")]
        [XmlArrayItem("add")]
        public List<BdoConnectorConfigurationDto> Connectors { get; set; }

        // --------------------------------------------------

        /// <summary>
        /// The specification of this instance.
        /// </summary>
        [JsonPropertyName("specs")]
        [XmlArray("specs")]
        [XmlArrayItem("spec")]
        public List<MetaSourceSpecDto> Specs { get; set; }

        #endregion

        // --------------------------------------------------
        // CONSTRUCTORS
        // --------------------------------------------------

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the SourceElementDto class.
        /// </summary>
        public MetaSourceDto()
        {
        }

        #endregion
    }
}
