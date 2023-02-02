using System.ComponentModel;
using System.Text.Json.Serialization;
using System.Xml;
using System.Xml.Serialization;

namespace BindOpen.Data.Meta
{
    /// <summary>
    /// This class represents a data element.
    /// </summary>
    [XmlType("MetaItemDto", Namespace = "https://xsd.bindopen.org")]
    [XmlInclude(typeof(MetaSetDto))]
    [XmlInclude(typeof(MetaObjectDto))]
    [XmlInclude(typeof(MetaScalarDto))]
    [JsonDerivedType(typeof(MetaSetDto), "set")]
    [JsonDerivedType(typeof(MetaObjectDto), "object")]
    [JsonDerivedType(typeof(MetaScalarDto), "scalar")]
    public class MetaItemDto : IDto
    {
        // --------------------------------------------------
        // PROPERTIES
        // --------------------------------------------------

        #region Properties

        /// <summary>
        /// ID of this instance.
        /// </summary>
        [JsonPropertyName("id")]
        [XmlAttribute("id")]
        public string Id { get; set; }

        /// <summary>
        /// Name of this instance.
        /// </summary>
        [JsonPropertyName("name")]
        [XmlAttribute("name")]
        [DefaultValue(null)]
        public string Name { get; set; }

        // IIndexedDataItem -------------------------------

        /// <summary>
        /// The index of this instance.
        /// </summary>
        [JsonPropertyName("index")]
        [XmlElement("index")]
        public int? Index { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonIgnore()]
        [XmlIgnore()]
        public bool IndexSpecified => Index != null;

        #endregion

        // --------------------------------------------------
        // CONSTRUCTORS
        // --------------------------------------------------

        #region Constructors

        /// <summary>
        /// Initializes a new DTO data element.
        /// </summary>
        public MetaItemDto()
        {
        }

        #endregion
    }
}
