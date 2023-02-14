using BindOpen.Runtime.Dtos.Extensions;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace BindOpen.Extensions.Scripting
{
    /// <summary>
    /// This class represents a script word configuration.
    /// </summary>
    [XmlType("ScriptwordConfiguration", Namespace = "https://docs.bindopen.org/xsd")]
    [XmlRoot(ElementName = "scriptword", Namespace = "https://docs.bindopen.org/xsd", IsNullable = false)]
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
        [JsonPropertyName("kind")]
        [XmlElement("kind")]
        public ScriptItemKinds WordKind { get; set; } = ScriptItemKinds.None;

        // Tree ----------------------------------

        /// <summary>
        /// Sub script words of this instance.
        /// </summary>
        [JsonPropertyName("subScriptword")]
        [XmlElement("subScriptword")]
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
