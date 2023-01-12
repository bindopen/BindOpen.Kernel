using BindOpen.Meta.Specification;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace BindOpen.Meta.Elements
{
    /// <summary>
    /// This class represents a catalog element specification.
    /// </summary>
    [XmlType("CollectionElementSpec", Namespace = "https://docs.bindopen.org/xsd")]
    [XmlRoot(ElementName = "specification", Namespace = "https://docs.bindopen.org/xsd", IsNullable = false)]
    public class CollectionElementSpecDto : BdoElementSpecDto
    {
        // --------------------------------------------------
        // PROPERTIES
        // --------------------------------------------------

        #region Properties

        /// <summary>
        /// The elements of this instance.
        /// </summary>
        [JsonPropertyName("carriers")]
        [XmlArray("carriers")]
        [XmlArrayItem("add")]
        public List<CarrierElementSpecDto> CarrierElementSpecs { get; set; }

        /// <summary>
        /// The elements of this instance.
        /// </summary>
        [JsonPropertyName("objects")]
        [XmlArray("objects")]
        [XmlArrayItem("add")]
        public List<ObjectElementSpecDto> ObjectElementSpecs { get; set; }

        /// <summary>
        /// The elements of this instance.
        /// </summary>
        [JsonPropertyName("scalars")]
        [XmlArray("scalars")]
        [XmlArrayItem("add")]
        public List<ScalarElementSpecDto> ScalarElementSpecs { get; set; }

        /// <summary>
        /// The elements of this instance.
        /// </summary>
        [JsonPropertyName("sources")]
        [XmlArray("sources")]
        [XmlArrayItem("add")]
        public List<SourceElementSpecDto> SourceElementSpecs { get; set; }

        /// <summary>
        /// The elements of this instance.
        /// </summary>
        [JsonPropertyName("collections")]
        [XmlArray("collections")]
        [XmlArrayItem("add")]
        public List<CollectionElementSpecDto> CollectionElementSpecs { get; set; }

        // Entity ----------------------------------

        /// <summary>
        /// The class filter of this instance.
        /// </summary>
        [JsonPropertyName("class.filter")]
        [XmlElement("class.filter")]
        public DataValueFilterDto ClassFilter { get; set; }

        /// <summary>
        /// The definition filter of this instance.
        /// </summary>
        [JsonPropertyName("definition.filter")]
        [XmlElement("definition.filter")]
        public DataValueFilterDto DefinitionFilter { get; set; }

        #endregion

        // --------------------------------------------------
        // CONSTRUCTORS
        // --------------------------------------------------

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the CollectionElementSpecDto class.
        /// </summary>
        public CollectionElementSpecDto()
        {
        }

        #endregion
    }
}
