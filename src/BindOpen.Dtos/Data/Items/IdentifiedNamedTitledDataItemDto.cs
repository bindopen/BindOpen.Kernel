using System.ComponentModel;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace BindOpen.Data.Items
{
    /// <summary>
    /// This class represents an identified data item.
    /// </summary>
    [XmlType("IdentifiedNamedTitledDataItemDto", Namespace = "https://docs.bindopen.org/xsd")]
    [XmlRoot("IdentifiedNamedTitledDataItemDto", Namespace = "https://docs.bindopen.org/xsd", IsNullable = false)]
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
        public DictionaryDataItemDto Title { get; set; }

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
