using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace BindOpen.Data
{
    /// <summary>
    /// This class represents a data exp that can contain a literal and script texts.
    /// </summary>
    [XmlType("Expression", Namespace = "https://storage.bindopen.org/xsd/bindopen")]
    [XmlRoot(ElementName = "expression", Namespace = "https://storage.bindopen.org/xsd/bindopen", IsNullable = false)]
    public class ExpressionDto : BdoItemDto
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// The value of this instance.
        /// </summary>
        [JsonPropertyName("text")]
        [XmlText()]
        public string Text { get; set; }

        /// <summary>
        /// The kind of this instance.
        /// </summary>
        [JsonPropertyName("kind")]
        [XmlAttribute("kind")]
        public BdoExpressionKind Kind { get; set; } = BdoExpressionKind.Auto;

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of DataExpressionDto class.
        /// </summary>
        public ExpressionDto()
        {
        }

        #endregion
    }
}