using BindOpen.Data.Meta;
using System.Text.Json.Serialization;
using System.Xml;
using System.Xml.Serialization;

namespace BindOpen.Data.References
{
    /// <summary>
    /// This class represents a data reference DTO.
    /// </summary>
    [XmlType("DataReference", Namespace = "https://xsd.bindopen.org")]
    [XmlRoot(ElementName = "data.reference", Namespace = "https://xsd.bindopen.org", IsNullable = false)]
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
        public MetaSetDto PathDetail { get; set; }

        /// <summary>
        /// Source element of this instance.
        /// </summary>
        [JsonPropertyName("source")]
        [XmlElement("source")]
        public MetaDataDto SourceMetaData { get; set; }

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