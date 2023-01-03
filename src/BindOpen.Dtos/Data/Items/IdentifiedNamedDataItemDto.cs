using System.ComponentModel;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace BindOpen.Data.Items
{
    /// <summary>
    /// This class represents an identified data item.
    /// </summary>
    [XmlType("IdentifiedNamedDataItemDto", Namespace = "https://docs.bindopen.org/xsd")]
    [XmlRoot("IdentifiedNamedDataItemDto", Namespace = "https://docs.bindopen.org/xsd", IsNullable = false)]
    public class IdentifiedNamedDataItemDto : IdentifiedDataItemDto
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// ID of this instance.
        /// </summary>
        [JsonPropertyName("name")]
        [XmlAttribute("name")]
        public string Name { get; set; }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the IdentifiedNamedDataItemDto class.
        /// </summary>
        public IdentifiedNamedDataItemDto()
        {
        }

        #endregion
    }
}
