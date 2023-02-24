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
        [XmlText()]
        public string Item { get; set; }

        /// <summary>
        /// The values of this instance.
        /// </summary>
        [JsonPropertyName("items")]
        [XmlArray("items")]
        [XmlArrayItem("add")]
        public List<string> Items { get; set; }

        /// <summary>
        /// Indicates whether the entities property must be ignored.
        /// </summary>
        [JsonIgnore]
        [XmlIgnore]
        public bool ItemsSpecified => Items?.Count > 0;

        /// <summary>
        /// The specification of this instance.
        /// </summary>
        [JsonPropertyName("specs")]
        [XmlArray("specs")]
        [XmlArrayItem("spec")]
        public List<ScalarSpecDto> Specs { get; set; }

        /// <summary>
        /// Indicates whether the entities property must be ignored.
        /// </summary>
        [JsonIgnore]
        [XmlIgnore]
        public bool SpecsSpecified => Specs?.Count > 0;

        #endregion

        // --------------------------------------------------
        // CONSTRUCTORS
        // --------------------------------------------------

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the MetaScalarDto class.
        /// </summary>
        public MetaScalarDto()
        {
        }

        #endregion
    }
}
