using BindOpen.System.Data.Conditions;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace BindOpen.System.Data
{
    /// <summary>
    /// This class represents a data key value.
    /// </summary>
    [XmlType("RequirementLevelConditionalStatementPair", Namespace = "https://storage.bindopen.org/xsd/bindopen")]
    [XmlRoot(ElementName = "add", Namespace = "https://storage.bindopen.org/xsd/bindopen", IsNullable = false)]
    public class RequirementLevelConditionalStatementPairDto
    {
        // --------------------------------------------------
        // PROPERTIES
        // --------------------------------------------------

        #region Properties

        /// <summary>
        /// Key of this instance.
        /// </summary>
        [JsonPropertyName("item")]
        [XmlAttribute("item")]
        public RequirementLevels Item { get; set; }

        /// <summary>
        /// Default items of this instance.
        /// </summary>
        [JsonPropertyName("condition")]
        [XmlElement("condition", Type = typeof(BasicConditionDto))]
        [XmlElement("condition.composite", Type = typeof(CompositeConditionDto))]
        [XmlElement("condition.expression", Type = typeof(ExpressionConditionDto))]
        public ConditionDto Condition { get; set; }


        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of TConditionalStatementPairDto class.
        /// </summary>
        public RequirementLevelConditionalStatementPairDto()
        {
        }

        #endregion
    }
}
