using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace BindOpen.Data.Items
{
    /// <summary>
    /// This class represents a dictionary data item.
    /// </summary>
    /// <example>Titles, Descriptions.</example>
    /// <seealso cref="DataKeyValue"/>
    [XmlType("DictionaryDataItem", Namespace = "https://docs.bindopen.org/xsd")]
    [XmlRoot(ElementName = "item", Namespace = "https://docs.bindopen.org/xsd", IsNullable = false)]
    public class DictionaryDataItemDto : IDto
    {
        // --------------------------------------------------
        // PROPERTIES
        // --------------------------------------------------

        #region Properties

        /// <summary>
        /// The ID of this instance.
        /// </summary>
        [JsonPropertyName("id")]
        [XmlElement("id")]
        public string Id { get; set; }

        /// <summary>
        /// Values of this instance.
        /// </summary>
        [JsonPropertyName("values")]
        [XmlElement("add.value")]
        public List<DataKeyValueDto> Values { get; set; }

        #endregion

        // --------------------------------------------------
        // CONSTRUCTORS
        // --------------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the DictionaryDataItemDto class. 
        /// </summary>
        public DictionaryDataItemDto()
        {
        }

        #endregion
    }

}
