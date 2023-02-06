using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace BindOpen.Data
{
    /// <summary>
    /// This class represents an identified data item.
    /// </summary>
    [XmlType("IdentifiedNamedDataItemDto", Namespace = "https://xsd.bindopen.org")]
    [XmlRoot("IdentifiedNamedDataItemDto", Namespace = "https://xsd.bindopen.org", IsNullable = false)]
    public class IdentifiedNamedDataItemDto : IdentifiedDataItemDto
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// ID of this instance.
        /// </summary>
        [JsonPropertyName("name")]
        [XmlAttribute("name")]
        public string Name { get; set; }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the IdentifiedNamedDataItemDto class.
        /// </summary>
        public IdentifiedNamedDataItemDto()
        {
        }

        #endregion
    }
}
