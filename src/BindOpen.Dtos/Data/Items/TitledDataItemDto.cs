using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace BindOpen.Data.Items
{
    /// <summary>
    /// This class represents titled data item.
    /// </summary>
    [XmlType("TitledDataItem", Namespace = "https://docs.bindopen.org/xsd")]
    [XmlRoot("titledDataItem", Namespace = "https://docs.bindopen.org/xsd", IsNullable = false)]
    public class TitledDataItemDto : IDto
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// Title of this instance.
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
        /// Instantiates a new instance of the TitledDataItemDto class.
        /// </summary>
        public TitledDataItemDto()
        {
        }

        #endregion
    }
}
