using System.ComponentModel;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace BindOpen.System.Data.Conditions
{
    /// <summary>
    /// This class represents a condition DTO.
    /// </summary>
    [XmlType("Condition", Namespace = "https://storage.bindopen.org/xsd/bindopen")]
    [XmlRoot(ElementName = "condition", Namespace = "https://storage.bindopen.org/xsd/bindopen", IsNullable = false)]
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
        /// ID of this instance.
        /// </summary>
        [JsonPropertyName("id")]
        [XmlAttribute("id")]
        public string Id { get; set; }

        /// <summary>
        /// The value that expresses that the condition is satisfied.
        /// </summary>
        [JsonPropertyName("trueValue")]
        [XmlAttribute("trueValue")]
        [DefaultValue(true)]
        public bool TrueValue { get; set; }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the BusinessCondition class.
        /// </summary>
        protected ConditionDto() : base()
        {
        }

        /// <summary>
        /// Instantiates a new instance of the BusinessCondition class.
        /// </summary>
        /// <param key="trueValue">The true value to consider.</param>
        protected ConditionDto(bool trueValue) : base()
        {
            this.TrueValue = trueValue;
        }

        #endregion
    }
}