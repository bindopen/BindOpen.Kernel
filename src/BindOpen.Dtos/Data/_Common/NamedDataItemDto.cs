using System.ComponentModel;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace BindOpen.Data
{
    /// <summary>
    /// This class represents a named data item.
    /// </summary>
    [XmlType("NamedDataItem", Namespace = "https://xsd.bindopen.org")]
    [XmlRoot("namedDataItem", Namespace = "https://xsd.bindopen.org", IsNullable = false)]
    public class NamedDataItemDto : IDto
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// Name of this instance.
        /// </summary>
        [JsonPropertyName("name")]
        [XmlAttribute("name")]
        [DefaultValue(null)]
        public string Name { get; set; }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the NamedDataItem class.
        /// </summary>
        public NamedDataItemDto()
        {
        }

        #endregion
    }
}
