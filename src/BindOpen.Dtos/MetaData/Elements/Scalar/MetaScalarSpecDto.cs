using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace BindOpen.MetaData.Elements
{

    /// <summary>
    /// This class represents a scalar element specification.
    /// </summary>
    [XmlType("MetaScalarSpecDto", Namespace = "https://xsd.bindopen.org")]
    [XmlRoot(ElementName = "spec", Namespace = "https://xsd.bindopen.org", IsNullable = false)]
    public class MetaScalarSpecDto : MetaElementSpecDto
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
        public MetaScalarSpecDto()
        {
        }

        #endregion
    }

}
