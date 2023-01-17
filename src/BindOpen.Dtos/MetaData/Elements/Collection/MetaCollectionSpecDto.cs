using BindOpen.MetaData.Specification;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace BindOpen.MetaData.Elements
{
    /// <summary>
    /// This class represents a catalog element specification.
    /// </summary>
    [XmlType("MetaCollectionSpec", Namespace = "https://xsd.bindopen.org")]
    [XmlRoot(ElementName = "spec", Namespace = "https://xsd.bindopen.org", IsNullable = false)]
    public class MetaCollectionSpecDto : MetaElementSpecDto
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
        public List<MetaCarrierSpecDto> CarrierElementSpecs { get; set; }

        /// <summary>
        /// The elements of this instance.
        /// </summary>
        [JsonPropertyName("objects")]
        [XmlArray("objects")]
        [XmlArrayItem("add")]
        public List<MetaObjectSpecDto> ObjectElementSpecs { get; set; }

        /// <summary>
        /// The elements of this instance.
        /// </summary>
        [JsonPropertyName("scalars")]
        [XmlArray("scalars")]
        [XmlArrayItem("add")]
        public List<MetaScalarSpecDto> ScalarElementSpecs { get; set; }

        /// <summary>
        /// The elements of this instance.
        /// </summary>
        [JsonPropertyName("sources")]
        [XmlArray("sources")]
        [XmlArrayItem("add")]
        public List<MetaSourceSpecDto> SourceElementSpecs { get; set; }

        /// <summary>
        /// The elements of this instance.
        /// </summary>
        [JsonPropertyName("collections")]
        [XmlArray("collections")]
        [XmlArrayItem("add")]
        public List<MetaCollectionSpecDto> CollectionElementSpecs { get; set; }

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
        public MetaCollectionSpecDto()
        {
        }

        #endregion
    }
}
