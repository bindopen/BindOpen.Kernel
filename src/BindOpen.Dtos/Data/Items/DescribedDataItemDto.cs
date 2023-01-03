using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace BindOpen.Data.Items
{
    /// <summary>
    /// This class represents described data item.
    /// </summary>
    [XmlType("DescribedDataItem", Namespace = "https://docs.bindopen.org/xsd")]
    [XmlRoot("describedDataItem", Namespace = "https://docs.bindopen.org/xsd", IsNullable = false)]
    public class DescribedDataItemDto : IDto
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// Description of this instance.
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
        /// Instantiates a new instance of the DescribedDataItemDto class.
        /// </summary>
        public DescribedDataItemDto()
        {
        }

        #endregion
    }
}
