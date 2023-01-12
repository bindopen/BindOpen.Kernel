using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace BindOpen.Meta.Elements
{
    /// <summary>
    /// This class represents a data element set.
    /// </summary>
    [XmlRoot(ElementName = "element.set", Namespace = "https://docs.bindopen.org/xsd", IsNullable = false)]
    public class BdoElementSetDto : IDto, IIdentified
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// The identifier of this instance.
        /// </summary>
        [JsonPropertyName("id")]
        [XmlAttribute("id")]
        public string Id { get; set; }

        /// <summary>
        /// The elements of this instance.
        /// </summary>
        [JsonPropertyName("carriers")]
        [XmlArray("carriers")]
        [XmlArrayItem("add")]
        public List<CarrierElementDto> CarrierElements { get; set; }

        /// <summary>
        /// Indicates whether the carriers property must be ignored.
        /// </summary>
        [JsonIgnore]
        [XmlIgnore]
        public bool CarrierElementsSpecified => CarrierElements?.Count > 0;

        /// <summary>
        /// The elements of this instance.
        /// </summary>
        [JsonPropertyName("objects")]
        [XmlArray("objects")]
        [XmlArrayItem("add")]
        public List<ObjectElementDto> ObjectElements { get; set; }

        /// <summary>
        /// Indicates whether the objects property must be ignored.
        /// </summary>
        [JsonIgnore]
        [XmlIgnore]
        public bool ObjectElementsSpecified => ObjectElements?.Count > 0;

        /// <summary>
        /// The elements of this instance.
        /// </summary>
        [JsonPropertyName("scalars")]
        [XmlArray("scalars")]
        [XmlArrayItem("add")]
        public List<ScalarElementDto> ScalarElements { get; set; }

        /// <summary>
        /// Indicates whether the scalars property must be ignored.
        /// </summary>
        [JsonIgnore]
        [XmlIgnore]
        public bool ScalarElementsSpecified => ScalarElements?.Count > 0;

        /// <summary>
        /// The elements of this instance.
        /// </summary>
        [JsonPropertyName("sources")]
        [XmlArray("sources")]
        [XmlArrayItem("add")]
        public List<SourceElementDto> SourceElements { get; set; }

        /// <summary>
        /// Indicates whether the sources property must be ignored.
        /// </summary>
        [JsonIgnore]
        [XmlIgnore]
        public bool SourceElementsSpecified => SourceElements?.Count > 0;

        /// <summary>
        /// The elements of this instance.
        /// </summary>
        [JsonPropertyName("collections")]
        [XmlArray("collections")]
        [XmlArrayItem("add")]
        public List<CollectionElementDto> CollectionElements { get; set; }

        /// <summary>
        /// Indicates whether the collections property must be ignored.
        /// </summary>
        [JsonIgnore]
        [XmlIgnore]
        public bool CollectionElementsSpecified => CollectionElements?.Count > 0;

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the BdoElementSetDto class.
        /// </summary>
        public BdoElementSetDto()
        {
        }

        #endregion
    }
}

