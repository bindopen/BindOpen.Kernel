using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace BindOpen.MetaData
{
    /// <summary>
    /// This class represents indexed data item.
    /// </summary>
    [XmlType("IndexedDataItem", Namespace = "https://xsd.bindopen.org")]
    [XmlRoot("indexedDataItem", Namespace = "https://xsd.bindopen.org", IsNullable = false)]
    public class IndexedDataItemDto : IDto
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// The index of this instance.
        /// </summary>
        [JsonPropertyName("index")]
        [XmlAttribute("index")]
        public int Index { get; set; } = -1;

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the IndexedDataItemDto class.
        /// </summary>
        public IndexedDataItemDto()
        {
        }

        #endregion
    }
}
