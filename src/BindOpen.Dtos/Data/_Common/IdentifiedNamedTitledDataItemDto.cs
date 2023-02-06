using BindOpen.Data.Items;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace BindOpen.Data
{
    /// <summary>
    /// This class represents an identified data item.
    /// </summary>
    [XmlType("IdentifiedNamedTitledDataItemDto", Namespace = "https://xsd.bindopen.org")]
    [XmlRoot("IdentifiedNamedTitledDataItemDto", Namespace = "https://xsd.bindopen.org", IsNullable = false)]
    public class IdentifiedNamedTitledDataItemDto : IdentifiedNamedDataItemDto
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// The title of this instance.
        /// </summary>
        [JsonPropertyName("title")]
        [XmlElement("title")]
        public DictionaryDto Title { get; set; }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the IdentifiedNamedTitledDataItemDto class.
        /// </summary>
        public IdentifiedNamedTitledDataItemDto()
        {
        }

        #endregion
    }
}
