using BindOpen.MetaData.Items;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace BindOpen.MetaData
{
    /// <summary>
    /// This class represents described data item.
    /// </summary>
    [XmlType("DescribedDataItem", Namespace = "https://xsd.bindopen.org")]
    [XmlRoot("describedDataItem", Namespace = "https://xsd.bindopen.org", IsNullable = false)]
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
        public DictionaryDto Description { get; set; }

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
