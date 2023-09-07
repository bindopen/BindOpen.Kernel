using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace BindOpen.System.Data.Conditions
{
    /// <summary>
    /// This class represents a script condition DTO.
    /// </summary>
    [XmlType("ExpressionCondition", Namespace = "https://storage.bindopen.org/xsd/bindopen")]
    [XmlRoot(ElementName = "condition.expression", Namespace = "https://storage.bindopen.org/xsd/bindopen", IsNullable = false)]
    public class ExpressionConditionDto : ConditionDto
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// Data exp DTO representing the condition.
        /// </summary>
        [JsonPropertyName("expression")]
        [XmlElement("expression")]
        public ExpressionDto Expression { get; set; }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the ExpressionConditionDto class.
        /// </summary>
        public ExpressionConditionDto()
        {
        }

        #endregion
    }
}