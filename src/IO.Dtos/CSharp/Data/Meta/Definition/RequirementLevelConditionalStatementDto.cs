using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace BindOpen.Kernel.Data.Meta
{
    /// <summary>
    /// This class represents a data key value.
    /// </summary>
    [XmlType("RequirementLevelConditionalStatement", Namespace = "https://storage.bindopen.org/xsd/bindopen")]
    [XmlRoot(ElementName = "conditional.statement", Namespace = "https://storage.bindopen.org/xsd/bindopen", IsNullable = false)]
    public class RequirementLevelConditionalStatementDto
    {
        // --------------------------------------------------
        // PROPERTIES
        // --------------------------------------------------

        #region Properties

        /// <summary>
        /// The ID of this instance.
        /// </summary>
        [JsonPropertyName("id")]
        [XmlElement("id")]
        public string Id { get; set; }

        /// <summary>
        /// Values of this instance.
        /// </summary>
        [JsonPropertyName("items")]
        [XmlElement("add")]
        public List<RequirementLevelConditionalStatementPairDto> Items { get; set; }


        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of RequirementLevelConditionalStatementDto class.
        /// </summary>
        public RequirementLevelConditionalStatementDto()
        {
        }

        #endregion
    }
}
