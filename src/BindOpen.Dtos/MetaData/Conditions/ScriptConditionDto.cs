using BindOpen.MetaData.Items;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace BindOpen.MetaData.Conditions
{
    /// <summary>
    /// This class represents a script condition DTO.
    /// </summary>
    [XmlType("ScriptCondition", Namespace = "https://xsd.bindopen.org")]
    [XmlRoot(ElementName = "script.condition", Namespace = "https://xsd.bindopen.org", IsNullable = false)]
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