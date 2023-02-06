using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace BindOpen.Data.Meta
{
    /// <summary>
    /// This class represents a scalar element that is an element whose items are scalars.
    /// </summary>
    [XmlType("MetaScalar", Namespace = "https://xsd.bindopen.org")]
    [XmlRoot(ElementName = "scalar", Namespace = "https://xsd.bindopen.org", IsNullable = false)]
    public class MetaScalarDto : MetaDataDto
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
        [JsonPropertyName("specs")]
        [XmlArray("specs")]
        [XmlArrayItem("spec")]
        public List<MetaScalarSpecDto> Specs { get; set; }

        #endregion

        // --------------------------------------------------
        // CONSTRUCTORS
        // --------------------------------------------------

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the ScalarElementDto class.
        /// </summary>
        public MetaScalarDto()
        {
        }

        #endregion
    }
}
