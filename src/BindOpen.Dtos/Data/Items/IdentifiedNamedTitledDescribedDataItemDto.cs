using System.ComponentModel;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace BindOpen.MetaData.Items
{
    /// <summary>
    /// This class represents an identified data item.
    /// </summary>
    [XmlType("IdentifiedNamedTitledDescribedDataItemDto", Namespace = "https://docs.bindopen.org/xsd")]
    [XmlRoot("IdentifiedNamedTitledDescribedDataItemDto", Namespace = "https://docs.bindopen.org/xsd", IsNullable = false)]
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
        public DictionaryDataItemDto Description { get; set; }

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
