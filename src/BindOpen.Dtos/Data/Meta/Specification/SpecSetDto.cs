using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace BindOpen.Data.Meta
{
    /// <summary>
    /// This class represents a set of data element specifications.
    /// </summary>
    [XmlType("SpecSet", Namespace = "https://storage.bindopen.org/xsd/bindopen")]
    [XmlRoot(ElementName = "spec.set", Namespace = "https://storage.bindopen.org/xsd/bindopen", IsNullable = false)]
    public class SpecSetDto : IDto
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// The elements of this instance.
        /// </summary>
        [JsonPropertyName("specs")]
        [XmlElement("spec")]
        public List<MetaDataDto> Specs { get; set; }

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
        /// Initializes a new instance of the MetaSpecSetDto class.
        /// </summary>
        public SpecSetDto() : base()
        {
        }

        #endregion
    }

}
