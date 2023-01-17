using BindOpen.MetaData.Items;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace BindOpen.MetaData
{
    /// <summary>
    /// This class represents titled data item.
    /// </summary>
    [XmlType("TitledDataItem", Namespace = "https://xsd.bindopen.org")]
    [XmlRoot("titledDataItem", Namespace = "https://xsd.bindopen.org", IsNullable = false)]
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
        public DictionaryDto Title { get; set; }

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
