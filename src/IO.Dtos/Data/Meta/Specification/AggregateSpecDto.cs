using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace BindOpen.System.Data.Meta
{
    /// <summary>
    /// This class represents a data element specification.
    /// </summary>
    [XmlType("AggregateSpecDto", Namespace = "https://storage.bindopen.org/xsd/bindopen")]
    [XmlRoot(ElementName = "spec.aggregate", Namespace = "https://storage.bindopen.org/xsd/bindopen", IsNullable = false)]
    public class AggregateSpecDto : SpecDto
    {
        // --------------------------------------------------
        // PROPERTIES
        // --------------------------------------------------

        #region Properties

        /// <summary>
        /// THe children of this instance.
        /// </summary>
        [JsonPropertyName("spec")]
        [XmlElement("spec", Type = typeof(SpecDto))]
        [XmlElement("spec.aggregate", Type = typeof(AggregateSpecDto))]
        public List<SpecDto> Children { get; set; }

        #endregion

        // --------------------------------------------------
        // CONSTRUCTORS
        // --------------------------------------------------

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the AggregateSpecDto class.
        /// </summary>
        public AggregateSpecDto()
        {
        }

        #endregion
    }
}
