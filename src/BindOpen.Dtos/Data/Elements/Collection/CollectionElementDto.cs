using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Xml;
using System.Xml.Serialization;

namespace BindOpen.Data.Elements
{
    /// <summary>
    /// This class represents a catalog element that is an element whose elements are entities.
    /// </summary>
    [XmlType("CollectionElement", Namespace = "https://docs.bindopen.org/xsd")]
    [XmlRoot(ElementName = "collection", Namespace = "https://docs.bindopen.org/xsd", IsNullable = false)]
    public class CollectionElementDto : BdoElementDto
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
        [XmlArrayItem("carrier")]
        public List<CarrierElementDto> CarrierElements { get; set; }

        /// <summary>
        /// The elements of this instance.
        /// </summary>
        [JsonPropertyName("objects")]
        [XmlArray("objects")]
        [XmlArrayItem("object")]
        public List<ObjectElementDto> ObjectElements { get; set; }

        /// <summary>
        /// The elements of this instance.
        /// </summary>
        [JsonPropertyName("scalars")]
        [XmlArray("scalars")]
        [XmlArrayItem("scalar")]
        public List<ScalarElementDto> ScalarElements { get; set; }

        /// <summary>
        /// The elements of this instance.
        /// </summary>
        [JsonPropertyName("sources")]
        [XmlArray("sources")]
        [XmlArrayItem("source")]
        public List<SourceElementDto> SourceElements { get; set; }

        /// <summary>
        /// The elements of this instance.
        /// </summary>
        [JsonPropertyName("collections")]
        [XmlArray("collections")]
        [XmlArrayItem("collection")]
        public List<CollectionElementDto> CollectionElements { get; set; }

        // Specification -----------------------

        /// <summary>
        /// The specification of this instance.
        /// </summary>
        [JsonPropertyName("specification")]
        [XmlElement("specification")]
        public CollectionElementSpecDto Specification { get; set; }

        #endregion

        // --------------------------------------------------
        // CONSTRUCTORS
        // --------------------------------------------------

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the CollectionElementDto class.
        /// </summary>
        public CollectionElementDto()
        {
        }

        #endregion
    }
}
