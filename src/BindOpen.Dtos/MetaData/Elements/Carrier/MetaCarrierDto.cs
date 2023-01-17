using BindOpen.Extensions.Modeling;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace BindOpen.MetaData.Elements
{
    /// <summary>
    /// This class represents a carrier element.
    /// </summary>
    [XmlType("MetaCarrier", Namespace = "https://xsd.bindopen.org")]
    [XmlRoot(ElementName = "carrier", Namespace = "https://xsd.bindopen.org", IsNullable = false)]
    public class MetaCarrierDto : MetaElementDto
    {
        // --------------------------------------------------
        // PROPERTIES
        // --------------------------------------------------

        #region Properties

        /// <summary>
        /// The definition unique ID of this instance.
        /// </summary>
        [JsonPropertyName("definition")]
        [XmlElement("definition")]
        public string DefinitionUniqueId { get; set; }

        // --------------------------------------------------

        /// <summary>
        /// Carriers of this instance.
        /// </summary>
        [JsonPropertyName("items")]
        [XmlArray("items")]
        [XmlArrayItem("add")]
        public List<BdoCarrierConfigurationDto> Carriers { get; set; }

        // --------------------------------------------------

        /// <summary>
        /// The specification of this instance.
        /// </summary>
        [JsonPropertyName("specs")]
        [XmlArray("specs")]
        [XmlArrayItem("spec")]
        public List<MetaCarrierSpecDto> Specs { get; set; }

        #endregion

        // --------------------------------------------------
        // CONSTRUCTORS
        // --------------------------------------------------

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the CarrierElementDto class.
        /// </summary>
        public MetaCarrierDto()
        {
        }

        #endregion
    }
}

