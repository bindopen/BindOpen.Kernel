using BindOpen.Data.Meta;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace BindOpen.Scripting
{
    /// <summary>
    /// This class represents a script word configuration.
    /// </summary>
    [XmlType("Scriptword", Namespace = "https://storage.bindopen.org/xsd/bindopen")]
    [XmlRoot(ElementName = "scriptword", Namespace = "https://storage.bindopen.org/xsd/bindopen", IsNullable = false)]
    public class ScriptwordDto : MetaObjectDto
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
        [XmlAttribute("kind")]
        public ScriptItemKinds Kind { get; set; } = ScriptItemKinds.None;

        // Tree ----------------------------------

        /// <summary>
        /// Sub script words of this instance.
        /// </summary>
        [JsonPropertyName("child")]
        [XmlElement("child")]
        public ScriptwordDto Child { get; set; }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the ScriptwordConfigurationDto class.
        /// </summary>
        public ScriptwordDto()
        {
        }

        #endregion
    }
}
