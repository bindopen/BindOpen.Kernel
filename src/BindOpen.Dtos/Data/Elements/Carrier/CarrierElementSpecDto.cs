using BindOpen.Extensions.Modeling;
using BindOpen.Meta.Specification;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace BindOpen.Meta.Elements
{
    /// <summary>
    /// This class represents a carrier element specification.
    /// </summary>
    [XmlType("CarrierElementSpec", Namespace = "https://docs.bindopen.org/xsd")]
    [XmlRoot(ElementName = "specification", Namespace = "https://docs.bindopen.org/xsd", IsNullable = false)]
    public class CarrierElementSpecDto : BdoElementSpecDto
    {
        // --------------------------------------------------
        // PROPERTIES
        // --------------------------------------------------

        #region Properties

        /// <summary>
        /// The definition filter of this instance.
        /// </summary>
        [JsonPropertyName("definition.filter")]
        [XmlElement("definition.filter")]
        public DataValueFilterDto DefinitionFilter { get; set; }

        /// <summary>
        /// Default items of this instance.
        /// </summary>
        [JsonPropertyName("default.values")]
        [XmlArray("default.values")]
        [XmlArrayItem("add")]
        public List<BdoCarrierConfigurationDto> DefaultItems { get; set; }

        #endregion

        // --------------------------------------------------
        // CONSTRUCTORS
        // --------------------------------------------------

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the CarrierElementSpecDto class.
        /// </summary>
        public CarrierElementSpecDto()
        {
        }

        #endregion
    }

}
