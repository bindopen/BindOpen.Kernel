using BindOpen.Meta.Items;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace BindOpen.Meta.Conditions
{
    /// <summary>
    /// This class represents a script condition DTO.
    /// </summary>
    [XmlType("ScriptCondition", Namespace = "https://docs.bindopen.org/xsd")]
    [XmlRoot(ElementName = "script.condition", Namespace = "https://docs.bindopen.org/xsd", IsNullable = false)]
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
        public DataExpressionDto Expression { get; set; }

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