using BindOpen.Data.Conditions;
using BindOpen.Logging;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace BindOpen.Data.Meta
{
    /// <summary>
    /// This class represents a data key value.
    /// </summary>
    [XmlType("rule", Namespace = "https://storage.bindopen.org/xsd/bindopen/kernel")]
    [XmlRoot(ElementName = "rule", Namespace = "https://storage.bindopen.org/xsd/bindopen/kernel", IsNullable = false)]
    public class SpecRuleDto
    {
        // --------------------------------------------------
        // PROPERTIES
        // --------------------------------------------------

        #region Properties

        /// <summary>
        /// Identifier of this instance.
        /// </summary>
        [Key]
        [Column("SpecRuleId")]
        [JsonPropertyName("id")]
        [XmlAttribute("id")]
        public string Identifier { get; set; }

        [JsonIgnore()]
        [XmlIgnore()]
        public string SpecId { get; set; }

        /// <summary>
        /// The kind of this instance.
        /// </summary>
        [JsonPropertyName("kind")]
        [XmlAttribute("kind")]
        [DefaultValue(BdoSpecRuleKinds.None)]
        public BdoSpecRuleKinds Kind { get; set; }

        /// <summary>
        /// The group identifier of this instance.
        /// </summary>
        [JsonPropertyName("groupId")]
        [XmlElement("groupId")]
        public string GroupId { get; set; }

        /// <summary>
        /// Values of this instance.
        /// </summary>
        [ForeignKey(nameof(ValueMetaDataId))]
        [JsonPropertyName("value")]
        [XmlElement("value")]
        public MetaScalarDto Value { get; set; }

        /// <summary>
        /// The class name of this instance.
        /// </summary>
        [JsonIgnore]
        [XmlIgnore]
        public string ValueMetaDataId { get; set; }

        /// <summary>
        /// The reference of this instance.
        /// </summary>
        [ForeignKey(nameof(ReferenceId))]
        [JsonPropertyName("reference")]
        [XmlElement("reference")]
        public ReferenceDto Reference { get; set; }

        /// <summary>
        /// The class name of this instance.
        /// </summary>
        [JsonIgnore]
        [XmlIgnore]
        public string ReferenceId { get; set; }

        /// <summary>
        /// Default items of this instance.
        /// </summary>
        [ForeignKey(nameof(ConditionId))]
        [JsonPropertyName("condition")]
        [XmlElement("condition", Type = typeof(BasicConditionDto))]
        [XmlElement("condition.composite", Type = typeof(CompositeConditionDto))]
        [XmlElement("condition.expression", Type = typeof(ExpressionConditionDto))]
        public ConditionDto Condition { get; set; }

        /// <summary>
        /// The class name of this instance.
        /// </summary>
        [JsonIgnore]
        [XmlIgnore]
        public string ConditionId { get; set; }

        /// <summary>
        /// The result code of this instance.
        /// </summary>
        [JsonPropertyName("resultCode")]
        [XmlElement("resultCode")]
        public string ResultCode { get; set; }

        [JsonPropertyName("resultEventKind")]
        [XmlElement("resultEventKind")]
        [DefaultValue(EventKinds.None)]
        public EventKinds ResultEventKind { get; set; }

        [JsonPropertyName("resultTitle")]
        [XmlElement("resultTitle")]
        public string ResultTitle { get; set; }

        [JsonPropertyName("resultDescription")]
        [XmlElement("resultDescription")]
        public string ResultDescription { get; set; }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of SpecRuleDto class.
        /// </summary>
        public SpecRuleDto()
        {
        }

        #endregion
    }
}
