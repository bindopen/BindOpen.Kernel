using BindOpen.Data.Items;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace BindOpen.Data
{
    /// <summary>
    /// This class represents an identified data item.
    /// </summary>
    [XmlType("IdentifiedNamedTitledDescribedDataItemDto", Namespace = "https://xsd.bindopen.org")]
    [XmlRoot("IdentifiedNamedTitledDescribedDataItemDto", Namespace = "https://xsd.bindopen.org", IsNullable = false)]
    public class IdentifiedNamedTitledDescribedDataItemDto : IdentifiedNamedTitledDataItemDto
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// The description of this instance.
        /// </summary>
        [JsonPropertyName("description")]
        [XmlElement("description")]
        public DictionaryDto Description { get; set; }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the IdentifiedNamedTitledDescribedDataItemDto class.
        /// </summary>
        public IdentifiedNamedTitledDescribedDataItemDto()
        {
        }

        #endregion
    }
}
