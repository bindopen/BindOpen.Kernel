using BindOpen.Data.Elements;
using System.Text.Json.Serialization;
using System.Xml;
using System.Xml.Serialization;

namespace BindOpen.Data.References
{
    /// <summary>
    /// This class represents a data reference DTO.
    /// </summary>
    [XmlType("DataReference", Namespace = "https://docs.bindopen.org/xsd")]
    [XmlRoot(ElementName = "data.reference", Namespace = "https://docs.bindopen.org/xsd", IsNullable = false)]
    public class DataReferenceDto
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// The data handler unique name of this instance.
        /// </summary>
        [JsonPropertyName("handler")]
        [XmlAttribute("handler")]
        public string DataHandlerUniqueName { get; set; }

        /// <summary>
        /// The path detail of this instance.
        /// </summary>
        [JsonPropertyName("path")]
        [XmlElement("path")]
        public BdoElementSetDto PathDetail { get; set; }

        /// <summary>
        /// Source element of this instance.
        /// </summary>
        [JsonPropertyName("carrier")]
        [XmlElement("carrier")]
        public CarrierElementDto CarrierElement { get; set; }

        /// <summary>
        /// Source element of this instance.
        /// </summary>
        [JsonPropertyName("source")]
        [XmlElement("source")]
        public SourceElementDto SourceElement { get; set; }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the DataReferenceDto class.
        /// </summary>
        public DataReferenceDto() : base()
        {
        }

        #endregion
    }
}