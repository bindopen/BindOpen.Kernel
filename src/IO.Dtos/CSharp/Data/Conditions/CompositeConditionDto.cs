using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace BindOpen.Kernel.Data.Conditions
{
    /// <summary>
    /// This class represents an advanced condition DTO.
    /// </summary>
    [XmlType("CompositeCondition", Namespace = "https://storage.bindopen.org/xsd/bindopen/kernel")]
    [XmlRoot(ElementName = "condition.composite", Namespace = "https://storage.bindopen.org/xsd/bindopen/kernel", IsNullable = false)]
    public class CompositeConditionDto : ConditionDto
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
        public CompositeConditionKind Kind { get; set; } = CompositeConditionKind.And;

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
        /// Instantiates a new instance of the CompositeCondition class.
        /// </summary>
        public CompositeConditionDto()
        {
        }

        #endregion
    }
}