using BindOpen.MetaData.Items;
using BindOpen.MetaData.References;
using System.ComponentModel;
using System.Text.Json.Serialization;
using System.Xml;
using System.Xml.Serialization;

namespace BindOpen.MetaData.Elements
{
    /// <summary>
    /// This class represents a data element.
    /// </summary>
    [XmlType("MetaElement", Namespace = "https://xsd.bindopen.org")]
    [XmlInclude(typeof(MetaCarrierDto))]
    [XmlInclude(typeof(MetaSourceDto))]
    [XmlInclude(typeof(MetaObjectDto))]
    [XmlInclude(typeof(MetaScalarDto))]
    [JsonDerivedType(typeof(MetaCarrierDto), "carrier")]
    [JsonDerivedType(typeof(MetaSourceDto), "source")]
    [JsonDerivedType(typeof(MetaObjectDto), "object")]
    [JsonDerivedType(typeof(MetaScalarDto), "scalar")]
    public class MetaElementDto : IDto
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

        /// <summary>
        /// Title of this instance.
        /// </summary>
        [JsonPropertyName("title")]
        [XmlElement("title")]
        public DictionaryDto Title { get; set; }

        /// <summary>
        /// Description of this instance.
        /// </summary>
        [JsonPropertyName("description")]
        [XmlElement("description")]
        public DictionaryDto Description { get; set; }

        // Items --------------------------------------------

        /// <summary>
        /// The itemization mode of this instance.
        /// </summary>
        [JsonPropertyName("itemizationMode")]
        [XmlElement("itemizationMode")]
        [DefaultValue(DataItemizationMode.Valued)]
        public DataItemizationMode ItemizationMode = DataItemizationMode.Valued;

        /// <summary>
        /// Item reference of this instance.
        /// </summary>
        [JsonPropertyName("itemReference")]
        [XmlElement("itemReference")]
        public DataReferenceDto ItemReference { get; set; }

        /// <summary>
        /// The script of this instance.
        /// </summary>
        [JsonPropertyName("script")]
        [XmlAttribute("script")]
        public string ItemScript { get; set; }

        /// <summary>
        /// The value type of this instance.
        /// </summary>
        [JsonPropertyName("valueType")]
        [XmlAttribute("valueType")]
        [DefaultValue(DataValueTypes.Text)]
        public DataValueTypes ValueType { get; set; } = DataValueTypes.Any;

        // Properties -------------------------------

        /// <summary>
        /// Property detail of this instance.
        /// </summary>
        [JsonPropertyName("propertyDetail")]
        [XmlElement("propertyDetail")]
        public BdoElementSetDto Detail { get; set; }

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
        public MetaElementDto()
        {
        }

        #endregion
    }
}
