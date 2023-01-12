using BindOpen.Meta.Elements;
using BindOpen.Meta.Items;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace BindOpen.Runtime.Definition
{
    /// <summary>
    /// This class represents a routine definition.
    /// </summary>
    [XmlType("RoutineDefinition", Namespace = "https://docs.bindopen.org/xsd")]
    [XmlRoot(ElementName = "routine.definition", Namespace = "https://docs.bindopen.org/xsd", IsNullable = false)]
    public class BdoRoutineDefinitionDto : BdoExtensionItemDefinitionDto
    {
        // --------------------------------------------------
        // PROPERTIES
        // --------------------------------------------------

        #region Properties

        /// <summary>
        /// The item class of this instance.
        /// </summary>
        [JsonPropertyName("itemClass")]
        [XmlElement("itemClass")]
        public string ItemClass { get; set; }

        // DTO

        /// <summary>
        /// The output result codes of this instance.
        /// </summary>
        [JsonPropertyName("outputResultCodes")]
        [XmlArray("outputResultCodes")]
        [XmlArrayItem("add")]
        public List<DescribedDataItemDto> OutputResultCodes { get; set; }

        /// <summary>
        /// The parameter statement of this instance.
        /// </summary>
        [JsonPropertyName("parameterStatement")]
        [XmlElement("parameterStatement")]
        public BdoElementSetDto ParameterStatement { get; set; }

        #endregion

        // --------------------------------------------------
        // CONSTRUCTORS
        // --------------------------------------------------

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the BdoRoutineDefinitionDto class.
        /// </summary>
        public BdoRoutineDefinitionDto()
        {
        }

        #endregion
    }
}
