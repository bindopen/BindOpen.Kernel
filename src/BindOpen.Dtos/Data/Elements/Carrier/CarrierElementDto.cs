using BindOpen.Extensions.Modeling;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace BindOpen.Data.Elements
{
    /// <summary>
    /// This class represents a carrier element.
    /// </summary>
    [XmlType("CarrierElement", Namespace = "https://docs.bindopen.org/xsd")]
    [XmlRoot(ElementName = "carrier", Namespace = "https://docs.bindopen.org/xsd", IsNullable = false)]
    public class CarrierElementDto : BdoElementDto
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
        [JsonPropertyName("specification")]
        [XmlElement("specification")]
        public CarrierElementSpecDto Specification { get; set; }

        #endregion

        // --------------------------------------------------
        // CONSTRUCTORS
        // --------------------------------------------------

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the CarrierElementDto class.
        /// </summary>
        public CarrierElementDto()
        {
        }

        #endregion
    }
}

