using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace BindOpen.Data.Conditions
{
    /// <summary>
    /// This class represents a condition DTO.
    /// </summary>
    [XmlType("Condition", Namespace = "https://storage.bindopen.org/xsd/bindopen/kernel")]
    [XmlRoot(ElementName = "condition", Namespace = "https://storage.bindopen.org/xsd/bindopen/kernel", IsNullable = false)]
    [XmlInclude(typeof(BasicConditionDto))]
    [XmlInclude(typeof(CompositeConditionDto))]
    [XmlInclude(typeof(ExpressionConditionDto))]
    [JsonDerivedType(typeof(BasicConditionDto), "condition")]
    [JsonDerivedType(typeof(CompositeConditionDto), "composite")]
    [JsonDerivedType(typeof(ExpressionConditionDto), "reference")]
    public abstract class ConditionDto : IBdoDto, IIdentified
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
        /// The name of this instance.
        /// </summary>
        [JsonPropertyName("name")]
        [XmlAttribute("name")]
        public string Name { get; set; }

        /// <summary>
        /// The parent identifier of this instance.
        /// </summary>
        [JsonPropertyName("parentId")]
        [XmlAttribute("parentId")]
        public string ParentId { get; set; }

        /// <summary>
        /// The kind of this instance.
        /// </summary>
        [JsonPropertyName("kind")]
        [XmlAttribute("kind")]
        public BdoConditionKind Kind { get; set; }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the ConditionDto class.
        /// </summary>
        protected ConditionDto() : base()
        {
        }

        #endregion
    }
}