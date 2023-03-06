using BindOpen.Data.Meta;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace BindOpen.Runtime.Definitions
{
    /// <summary>
    /// This class represents a routine definition.
    /// </summary>
    [XmlType("RoutineDefinition", Namespace = "https://storage.bindopen.org/xsd/bindopen")]
    [XmlRoot(ElementName = "routine.definition", Namespace = "https://storage.bindopen.org/xsd/bindopen", IsNullable = false)]
    public class BdoRoutineDefinitionDto : ExtensionDefinitionDto
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
        /// The parameter statement of this instance.
        /// </summary>
        [JsonPropertyName("parameterStatement")]
        [XmlElement("parameterStatement")]
        public MetaSetDto ParameterStatement { get; set; }

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
