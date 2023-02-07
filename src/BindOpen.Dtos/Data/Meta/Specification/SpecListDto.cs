using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace BindOpen.Data.Meta
{
    /// <summary>
    /// This class represents a set of data element specifications.
    /// </summary>
    [XmlType("SpecList", Namespace = "https://xsd.bindopen.org")]
    [XmlRoot(ElementName = "spec.list", Namespace = "https://xsd.bindopen.org", IsNullable = false)]
    public class SpecListDto : IDto
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// The elements of this instance.
        /// </summary>
        [JsonPropertyName("elementSpecs")]
        [XmlArray("elementSpecs")]
        [XmlArrayItem("objectSpec", Type = typeof(ObjectSpecDto))]
        [XmlArrayItem("scalarSpec", Type = typeof(ScalarSpecDto))]
        public List<MetaDataDto> ElementSpecs { get; set; }

        /// <summary>
        /// Indicates whether the entities property must be ignored.
        /// </summary>
        [JsonIgnore]
        [XmlIgnore]
        public bool ElementSpecsSpecified => ElementSpecs?.Count > 0;

        #endregion

        // --------------------------------------------------
        // CONSTRUCTORS
        // --------------------------------------------------

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the MetaSpecSetDto class.
        /// </summary>
        public SpecListDto() : base()
        {
        }

        #endregion
    }

}
