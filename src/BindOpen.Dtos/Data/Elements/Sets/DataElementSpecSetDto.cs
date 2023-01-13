using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace BindOpen.MetaData.Elements
{
    /// <summary>
    /// This class represents a set of data element specifications.
    /// </summary>
    [XmlType("BdoElementSpecSet", Namespace = "https://docs.bindopen.org/xsd")]
    [XmlRoot(ElementName = "dataElementSpecSet", Namespace = "https://docs.bindopen.org/xsd", IsNullable = false)]
    public class BdoElementSpecSetDto
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

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

        #endregion

        // --------------------------------------------------
        // CONSTRUCTORS
        // --------------------------------------------------

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the BdoElementSpecSetDto class.
        /// </summary>
        public BdoElementSpecSetDto() : base()
        {
        }

        #endregion
    }

}
