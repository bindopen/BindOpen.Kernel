using BindOpen.MetaData.Elements;
using BindOpen.Runtime.Definition;
using BindOpen.Runtime.Dtos.Extensions;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace BindOpen.Extensions.Scripting
{
    /// <summary>
    /// This class represents a script word configuration.
    /// </summary>
    [XmlType("ScriptwordConfiguration", Namespace = "https://xsd.bindopen.org")]
    [XmlRoot(ElementName = "scriptword", Namespace = "https://xsd.bindopen.org", IsNullable = false)]
    public class BdoScriptwordConfigurationDto
        : TBdoExtensionTitledItemConfigurationDto<BdoScriptwordDefinitionDto>
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// Kind of this instance.
        /// </summary>
        /// <example>Script word, syntax, text...</example>
        [JsonPropertyName("kind")]
        [XmlElement("kind")]
        public ScriptItemKinds WordKind { get; set; } = ScriptItemKinds.None;

        // Values ----------------------------------

        /// <summary>
        /// Parameter detail of this instance.
        /// </summary>
        [JsonPropertyName("parameters")]
        [XmlElement("parameters")]
        public BdoElementSetDto ParameterDetail { get; set; }

        // Tree ----------------------------------

        /// <summary>
        /// Sub script words of this instance.
        /// </summary>
        [JsonPropertyName("subScriptwords")]
        [XmlElement("subScriptwords")]
        public BdoScriptwordConfigurationDto SubScriptword { get; set; }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the BdoScriptwordConfigurationDto class.
        /// </summary>
        public BdoScriptwordConfigurationDto()
        {
        }

        #endregion
    }
}
