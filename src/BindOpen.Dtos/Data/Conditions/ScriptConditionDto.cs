using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace BindOpen.Data.Conditions
{
    /// <summary>
    /// This class represents a script condition DTO.
    /// </summary>
    [XmlType("ScriptCondition", Namespace = "https://storage.bindopen.org/xsd/bindopen")]
    [XmlRoot(ElementName = "script.condition", Namespace = "https://storage.bindopen.org/xsd/bindopen", IsNullable = false)]
    public class ScriptConditionDto : ConditionDto
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// Data exp DTO representing the condition.
        /// </summary>
        [JsonPropertyName("exp")]
        [XmlElement("exp")]
        public ExpressionDto Expression { get; set; }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the ScriptConditionDto class.
        /// </summary>
        public ScriptConditionDto()
        {
        }

        #endregion
    }
}