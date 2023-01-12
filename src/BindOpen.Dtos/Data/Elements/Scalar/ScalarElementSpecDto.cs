using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace BindOpen.Meta.Elements
{

    /// <summary>
    /// This class represents a scalar element specification.
    /// </summary>
    [XmlType("ScalarElementSpec", Namespace = "https://docs.bindopen.org/xsd")]
    [XmlRoot(ElementName = "specification", Namespace = "https://docs.bindopen.org/xsd", IsNullable = false)]
    public class ScalarElementSpecDto : BdoElementSpecDto
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
        public ScalarElementSpecDto()
        {
        }

        #endregion
    }

}
