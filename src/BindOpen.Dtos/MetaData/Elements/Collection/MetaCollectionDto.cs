using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Xml;
using System.Xml.Serialization;

namespace BindOpen.MetaData.Elements
{
    /// <summary>
    /// This class represents a catalog element that is an element whose elements are entities.
    /// </summary>
    [XmlType("MetaCollection", Namespace = "https://xsd.bindopen.org")]
    [XmlRoot(ElementName = "collection", Namespace = "https://xsd.bindopen.org", IsNullable = false)]
    public class MetaCollectionDto : MetaElementDto
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
        public List<MetaCarrierDto> CarrierElements { get; set; }

        /// <summary>
        /// The elements of this instance.
        /// </summary>
        [JsonPropertyName("objects")]
        [XmlArray("objects")]
        [XmlArrayItem("object")]
        public List<MetaObjectDto> ObjectElements { get; set; }

        /// <summary>
        /// The elements of this instance.
        /// </summary>
        [JsonPropertyName("scalars")]
        [XmlArray("scalars")]
        [XmlArrayItem("scalar")]
        public List<MetaScalarDto> ScalarElements { get; set; }

        /// <summary>
        /// The elements of this instance.
        /// </summary>
        [JsonPropertyName("sources")]
        [XmlArray("sources")]
        [XmlArrayItem("source")]
        public List<MetaSourceDto> SourceElements { get; set; }

        /// <summary>
        /// The elements of this instance.
        /// </summary>
        [JsonPropertyName("collections")]
        [XmlArray("collections")]
        [XmlArrayItem("collection")]
        public List<MetaCollectionDto> CollectionElements { get; set; }

        // Specification -----------------------

        /// <summary>
        /// The specification of this instance.
        /// </summary>
        [JsonPropertyName("specification")]
        [XmlElement("specification")]
        public MetaCollectionSpecDto Specification { get; set; }

        #endregion

        // --------------------------------------------------
        // CONSTRUCTORS
        // --------------------------------------------------

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the CollectionElementDto class.
        /// </summary>
        public MetaCollectionDto()
        {
        }

        #endregion
    }
}
