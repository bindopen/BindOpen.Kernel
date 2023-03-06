using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace BindOpen.Data.Conditions
{
    /// <summary>
    /// This class represents an advanced condition DTO.
    /// </summary>
    [XmlType("AdvancedCondition", Namespace = "https://storage.bindopen.org/xsd/bindopen")]
    [XmlRoot(ElementName = "advanced.condition", Namespace = "https://storage.bindopen.org/xsd/bindopen", IsNullable = false)]
    public class AdvancedConditionDto : ConditionDto
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// Kind of this instance.
        /// </summary>
        [JsonPropertyName("kind")]
        [XmlElement("kind")]
        public AdvancedConditionKind Kind { get; set; } = AdvancedConditionKind.And;

        /// <summary>
        /// Conditions of this instance.
        /// </summary>
        [JsonPropertyName("conditions")]
        [XmlArray("conditions")]
        [XmlArrayItem("condition")]
        public List<ConditionDto> Conditions { get; set; }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the AdvancedCondition class.
        /// </summary>
        public AdvancedConditionDto()
        {
        }

        #endregion
    }
}