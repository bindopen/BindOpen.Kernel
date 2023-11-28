using BindOpen.Kernel.Data.Conditions;
using BindOpen.Kernel.Logging;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace BindOpen.Kernel.Data.Meta
{
    /// <summary>
    /// This class represents a data key value.
    /// </summary>
    [XmlType("constraint", Namespace = "https://storage.bindopen.org/xsd/bindopen/kernel")]
    [XmlRoot(ElementName = "constraint", Namespace = "https://storage.bindopen.org/xsd/bindopen/kernel", IsNullable = false)]
    public class ConstraintDto
    {
        // --------------------------------------------------
        // PROPERTIES
        // --------------------------------------------------

        #region Properties

        /// <summary>
        /// Identifier of this instance.
        /// </summary>
        [JsonPropertyName("id")]
        [XmlAttribute("id")]
        public string Id { get; set; }

        /// <summary>
        /// The group identifier of this instance.
        /// </summary>
        [JsonPropertyName("groupId")]
        [XmlElement("groupId")]
        public string GroupId { get; set; }

        /// <summary>
        /// Values of this instance.
        /// </summary>
        [JsonPropertyName("value")]
        [XmlElement("value")]
        public MetaScalarDto Value { get; set; }

        /// <summary>
        /// The reference of this instance.
        /// </summary>
        [JsonPropertyName("reference")]
        [XmlElement("reference")]
        public ReferenceDto Reference { get; set; }

        /// <summary>
        /// Default items of this instance.
        /// </summary>
        [JsonPropertyName("condition")]
        [XmlElement("condition", Type = typeof(BasicConditionDto))]
        [XmlElement("condition.composite", Type = typeof(CompositeConditionDto))]
        [XmlElement("condition.expression", Type = typeof(ExpressionConditionDto))]
        public ConditionDto Condition { get; set; }

        /// <summary>
        /// The result code of this instance.
        /// </summary>
        [JsonPropertyName("resultCode")]
        [XmlElement("resultCode")]
        public string ResultCode { get; set; }

        [JsonPropertyName("resultEventKind")]
        [XmlElement("resultEventKind")]
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
        /// Instantiates a new instance of ConstraintDto class.
        /// </summary>
        public ConstraintDto()
        {
        }

        #endregion
    }
}
