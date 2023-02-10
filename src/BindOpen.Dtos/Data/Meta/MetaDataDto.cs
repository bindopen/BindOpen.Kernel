using BindOpen.Data.Items;
using System.ComponentModel;
using System.Text.Json.Serialization;
using System.Xml;
using System.Xml.Serialization;

namespace BindOpen.Data.Meta
{
    /// <summary>
    /// This class represents a data element.
    /// </summary>
    [XmlType("MetaData", Namespace = "https://xsd.bindopen.org")]
    [XmlInclude(typeof(MetaListDto))]
    [XmlInclude(typeof(MetaObjectDto))]
    [XmlInclude(typeof(MetaScalarDto))]
    [JsonDerivedType(typeof(MetaListDto), "set")]
    [JsonDerivedType(typeof(MetaObjectDto), "object")]
    [JsonDerivedType(typeof(MetaScalarDto), "scalar")]
    public class MetaDataDto : IDto
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

        // Items

        /// <summary>
        /// The itemization mode of this instance.
        /// </summary>
        [JsonPropertyName("valueMode")]
        [XmlElement("valueMode")]
        [DefaultValue(DataValueMode.Value)]
        public DataValueMode ValueMode = DataValueMode.Value;

        /// <summary>
        /// Item reference of this instance.
        /// </summary>
        [JsonPropertyName("reference")]
        [XmlElement("reference")]
        public ExpressionDto DataReference { get; set; }

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
        [JsonPropertyName("propertySet")]
        [XmlElement("propertySet")]
        public MetaListDto PropertySet { get; set; }

        #endregion

        // --------------------------------------------------
        // CONSTRUCTORS
        // --------------------------------------------------

        #region Constructors

        /// <summary>
        /// Initializes a new DTO data element.
        /// </summary>
        public MetaDataDto()
        {
        }

        #endregion
    }
}
