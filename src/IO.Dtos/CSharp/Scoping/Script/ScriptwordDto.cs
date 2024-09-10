using BindOpen.Data;
using BindOpen.Data.Meta;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace BindOpen.Scoping.Script
{
    /// <summary>
    /// This class represents a script word DTO.
    /// </summary>
    [XmlType("Scriptword", Namespace = "https://storage.bindopen.org/xsd/bindopen/kernel")]
    [XmlRoot(ElementName = "word", Namespace = "https://storage.bindopen.org/xsd/bindopen/kernel", IsNullable = false)]
    public class ScriptwordDto : MetaObjectDto
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// The kind of this instance.
        /// </summary>
        /// <example>Script word, syntax, text...</example>
        [JsonPropertyName("tokenKind")]
        [XmlAttribute("tokenKind")]
        public ScriptTokenKinds TokenKind { get; set; } = ScriptTokenKinds.None;

        // Expression ----------------------------------

        /// <summary>
        /// The script word child of this instance.
        /// </summary>
        [ForeignKey(nameof(ExpressionId))]
        [JsonIgnore]
        [XmlIgnore]
        public ExpressionDto Expression { get; set; }

        /// <summary>
        /// The class name of this instance.
        /// </summary>
        [JsonIgnore]
        [XmlIgnore]
        public string ExpressionId { get; set; }

        // Tree ----------------------------------

        /// <summary>
        /// The script word child of this instance.
        /// </summary>
        [ForeignKey(nameof(ChildWordId))]
        [JsonPropertyName("child")]
        [XmlElement("child")]
        public ScriptwordDto Child { get; set; }

        /// <summary>
        /// The class name of this instance.
        /// </summary>
        [JsonIgnore]
        [XmlIgnore]
        public string ChildWordId { get; set; }

        // Expression

        /// <summary>
        /// The text of this instance.
        /// </summary>
        [JsonPropertyName("text")]
        [XmlText()]
        public string Text { get; set; }

        /// <summary>
        /// The kind of this instance.
        /// </summary>
        [JsonPropertyName("kind")]
        [XmlAttribute("kind")]
        [DefaultValue(BdoExpressionKind.Auto)]
        public BdoExpressionKind ExpressionKind { get; set; } = BdoExpressionKind.Auto;

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the ScriptwordDto class.
        /// </summary>
        public ScriptwordDto()
        {
        }

        #endregion
    }
}
