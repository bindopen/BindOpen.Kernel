using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace BindOpen.Data.Meta
{

    /// <summary>
    /// This class represents a scalar element specification.
    /// </summary>
    [XmlType("ScalarSpec", Namespace = "https://xsd.bindopen.org")]
    [XmlRoot(ElementName = "scalar.spec", Namespace = "https://xsd.bindopen.org", IsNullable = false)]
    public class ScalarSpecDto : SpecDto
    {
        // --------------------------------------------------
        // PROPERTIES
        // --------------------------------------------------

        #region Properties

        /// <summary>
        /// Default items of this instance.
        /// </summary>
        [JsonPropertyName("default.items")]
        [XmlArray("default.items")]
        [XmlArrayItem("add")]
        public List<string> DefaultItems { get; set; }

        #endregion

        // --------------------------------------------------
        // CONSTRUCTORS
        // --------------------------------------------------

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the ScalarElementSpecDto class.
        /// </summary>
        public ScalarSpecDto()
        {
        }

        #endregion
    }

}
