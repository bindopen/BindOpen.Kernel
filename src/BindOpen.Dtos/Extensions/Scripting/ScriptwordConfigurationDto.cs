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
    public class ScriptwordConfigurationDto
        : ExtensionItemConfigurationDto
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// Kind of this instance.
        /// </summary>
        /// <example>Script word, syntax, text...</example>
        [JsonPropertyName("wordKind")]
        [XmlAttribute("wordKind")]
        public ScriptItemKinds WordKind { get; set; } = ScriptItemKinds.None;

        // Tree ----------------------------------

        /// <summary>
        /// Sub script words of this instance.
        /// </summary>
        [JsonPropertyName("subWord")]
        [XmlElement("subWord")]
        public ScriptwordConfigurationDto SubScriptword { get; set; }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the ScriptwordConfigurationDto class.
        /// </summary>
        public ScriptwordConfigurationDto()
        {
        }

        #endregion
    }
}
