using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace BindOpen.System.Data.Conditions
{
    /// <summary>
    /// This class represents a script condition DTO.
    /// </summary>
    [XmlType("ReferenceCondition", Namespace = "https://storage.bindopen.org/xsd/bindopen")]
    [XmlRoot(ElementName = "condition.reference", Namespace = "https://storage.bindopen.org/xsd/bindopen", IsNullable = false)]
    public class ReferenceConditionDto : ConditionDto
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// Data exp DTO representing the condition.
        /// </summary>
        [JsonPropertyName("reference")]
        [XmlElement("reference")]
        public ReferenceDto Reference { get; set; }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the ReferenceConditionDto class.
        /// </summary>
        public ReferenceConditionDto()
        {
        }

        #endregion
    }
}