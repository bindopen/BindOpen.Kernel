using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace BindOpen.System.Data.Meta
{
    /// <summary>
    /// This class represents a data element specification.
    /// </summary>
    [XmlType("CompositeSpecDto", Namespace = "https://storage.bindopen.org/xsd/bindopen")]
    [XmlRoot(ElementName = "composite.spec", Namespace = "https://storage.bindopen.org/xsd/bindopen", IsNullable = false)]
    public class CompositeSpecDto : SpecDto
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
        [XmlElement("spec.composite", Type = typeof(CompositeSpecDto))]
        public List<SpecDto> Children { get; set; }

        #endregion

        // --------------------------------------------------
        // CONSTRUCTORS
        // --------------------------------------------------

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the CompositeSpecDto class.
        /// </summary>
        public CompositeSpecDto()
        {
        }

        #endregion
    }
}
