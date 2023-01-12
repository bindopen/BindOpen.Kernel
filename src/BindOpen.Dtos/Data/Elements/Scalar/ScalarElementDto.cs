using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace BindOpen.Meta.Elements
{
    /// <summary>
    /// This class represents a scalar element that is an element whose items are scalars.
    /// </summary>
    [XmlType("ScalarElement", Namespace = "https://docs.bindopen.org/xsd")]
    [XmlRoot(ElementName = "scalar", Namespace = "https://docs.bindopen.org/xsd", IsNullable = false)]
    public class ScalarElementDto : BdoElementDto
    {
        // --------------------------------------------------
        // PROPERTIES
        // --------------------------------------------------

        #region Properties

        /// <summary>
        /// The value of this instance.
        /// </summary>
        [JsonPropertyName("item")]
        [XmlAttribute("item")]
        public string Item { get; set; }

        /// <summary>
        /// The values of this instance.
        /// </summary>
        [JsonPropertyName("items")]
        [XmlArray("items")]
        [XmlArrayItem("add")]
        public List<string> Items { get; set; }

        /// <summary>
        /// The specification of this instance.
        /// </summary>
        [JsonPropertyName("specification")]
        [XmlElement("specification")]
        public ScalarElementSpecDto Specification { get; set; }

        #endregion

        // --------------------------------------------------
        // CONSTRUCTORS
        // --------------------------------------------------

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the ScalarElementDto class.
        /// </summary>
        public ScalarElementDto()
        {
        }

        #endregion
    }
}
