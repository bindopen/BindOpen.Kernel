using BindOpen.Meta.Items;
using BindOpen.Meta.References;
using System.ComponentModel;
using System.Text.Json.Serialization;
using System.Xml;
using System.Xml.Serialization;

namespace BindOpen.Meta.Elements
{
    /// <summary>
    /// This class represents a data element.
    /// </summary>
    [XmlType("BdoElement", Namespace = "https://docs.bindopen.org/xsd")]
    [XmlRoot(ElementName = "element", Namespace = "https://docs.bindopen.org/xsd", IsNullable = false)]
    [XmlInclude(typeof(CarrierElementDto))]
    [XmlInclude(typeof(SourceElementDto))]
    [XmlInclude(typeof(ObjectElementDto))]
    [XmlInclude(typeof(ScalarElementDto))]
    public abstract class BdoElementDto : IdentifiedNamedTitledDescribedDataItemDto
    {
        // --------------------------------------------------
        // PROPERTIES
        // --------------------------------------------------

        #region Properties

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
        [DefaultValue("")]
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
        [DefaultValue(-1)]
        public int? Index { get; set; }

        #endregion

        // --------------------------------------------------
        // CONSTRUCTORS
        // --------------------------------------------------

        #region Constructors

        /// <summary>
        /// Initializes a new DTO data element.
        /// </summary>
        protected BdoElementDto()
        {
        }

        #endregion
    }
}
