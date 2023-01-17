using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace BindOpen.MetaData
{
    /// <summary>
    /// This class represents an identified data item.
    /// </summary>
    [XmlType("IdentifiedDataItem", Namespace = "https://xsd.bindopen.org")]
    [XmlRoot("identifiedDataItem", Namespace = "https://xsd.bindopen.org", IsNullable = false)]
    public class IdentifiedDataItemDto : IDto
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// ID of this instance.
        /// </summary>
        [JsonPropertyName("id")]
        [XmlAttribute("id")]
        public string Id { get; set; }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the IdentifiedDataItemDto class.
        /// </summary>
        public IdentifiedDataItemDto()
        {
        }

        #endregion
    }
}
