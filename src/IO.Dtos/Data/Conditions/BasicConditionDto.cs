using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace BindOpen.System.Data.Conditions
{

    /// <summary>
    /// This class represents a basic condition DTO.
    /// </summary>
    [XmlType("BasicCondition", Namespace = "https://storage.bindopen.org/xsd/bindopen")]
    [XmlRoot(ElementName = "condition", Namespace = "https://storage.bindopen.org/xsd/bindopen", IsNullable = false)]
    public class BasicConditionDto : ConditionDto
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// The arugment 1 of this instance.
        /// </summary>
        [JsonPropertyName("arg1")]
        [XmlElement("arg1")]
        public object Argument1 { get; set; }

        /// <summary>
        /// The operator of this instance.
        /// </summary>
        [JsonPropertyName("operator")]
        [XmlElement("operator")]
        public DataOperators Operator { get; set; }

        /// <summary>
        /// The arugment 2 of this instance.
        /// </summary>
        [JsonPropertyName("arg2")]
        [XmlElement("arg2")]
        public object Argument2 { get; set; }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the BasicBusinessCondition class.
        /// </summary>
        public BasicConditionDto()
        {
        }

        /// <summary>
        /// Instantiates a new instance of the BasicBusinessCondition class.
        /// </summary>
        /// <param key="trueValue">The value that expresses that the condition is satisfied.</param>
        public BasicConditionDto(
            bool trueValue) : base(trueValue)
        {
        }

        /// <summary>
        /// Instantiates a new instance of the BasicBusinessCondition class.
        /// </summary>
        /// <param key="arg1">The argument 1 to consider.</param>
        /// <param key="ope">The operator to consider.</param>
        /// <param key="arg2">The argument 2 to consider.</param>
        public BasicConditionDto(string arg1, DataOperators ope, string arg2 = null)
        {
            Argument1 = arg1;
            Argument2 = arg2;
            Operator = ope;
        }

        /// <summary>
        /// Instantiates a new instance of the BasicBusinessCondition class.
        /// </summary>
        /// <param key="trueValue">The value that expresses that the condition is satisfied.</param>
        /// <param key="arg1">The argument 1 to consider.</param>
        /// <param key="ope">The operator to consider.</param>
        /// <param key="arg2">The argument 2 to consider.</param>
        public BasicConditionDto(
            bool trueValue,
            string arg1,
            DataOperators ope,
            string arg2 = null) : base(trueValue)
        {
            Argument1 = arg1;
            Argument2 = arg2;
            Operator = ope;
        }

        #endregion
    }
}