using System.ComponentModel;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace BindOpen.Data.Items
{
    /// <summary>
    /// This class represents indexed data item.
    /// </summary>
    [XmlType("IndexedDataItem", Namespace = "https://docs.bindopen.org/xsd")]
    [XmlRoot("indexedDataItem", Namespace = "https://docs.bindopen.org/xsd", IsNullable = false)]
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
