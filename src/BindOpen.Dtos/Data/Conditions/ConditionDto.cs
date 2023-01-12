using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace BindOpen.Meta.Conditions
{
    /// <summary>
    /// This class represents a condition DTO.
    /// </summary>
    [XmlType("Condition", Namespace = "https://docs.bindopen.org/xsd")]
    [XmlRoot(ElementName = "condition", Namespace = "https://docs.bindopen.org/xsd", IsNullable = false)]
    [XmlInclude(typeof(AdvancedConditionDto))]
    [XmlInclude(typeof(BasicConditionDto))]
    [XmlInclude(typeof(ScriptConditionDto))]
    public abstract class ConditionDto : IDto
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// The value that expresses that the condition is satisfied.
        /// </summary>
        [JsonPropertyName("trueValue")]
        [XmlElement("trueValue")]
        public bool TrueValue { get; set; } = true;

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
        /// <param name="trueValue">The true value to consider.</param>
        protected ConditionDto(bool trueValue) : base()
        {
            this.TrueValue = trueValue;
        }

        #endregion
    }
}