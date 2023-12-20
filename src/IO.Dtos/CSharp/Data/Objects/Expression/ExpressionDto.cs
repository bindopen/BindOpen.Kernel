using BindOpen.Scoping.Script;
using System.ComponentModel;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace BindOpen.Data
{
    /// <summary>
    /// This class represents an expression DTO.
    /// </summary>
    [XmlType("Expression", Namespace = "https://storage.bindopen.org/xsd/bindopen/kernel")]
    [XmlRoot(ElementName = "expression", Namespace = "https://storage.bindopen.org/xsd/bindopen/kernel", IsNullable = false)]
    public class ExpressionDto : BdoItemDto
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// The text of this instance.
        /// </summary>
        [JsonPropertyName("text")]
        [XmlText()]
        public string Text { get; set; }

        /// <summary>
        /// The script word of this instance.
        /// </summary>
        [JsonPropertyName("word")]
        [XmlElement("word")]
        public ScriptwordDto Word { get; set; }

        /// <summary>
        /// The kind of this instance.
        /// </summary>
        [JsonPropertyName("kind")]
        [XmlAttribute("kind")]
        [DefaultValue(BdoExpressionKind.Auto)]
        public BdoExpressionKind Kind { get; set; } = BdoExpressionKind.Auto;

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of ExpressionDto class.
        /// </summary>
        public ExpressionDto()
        {
        }

        #endregion
    }
}