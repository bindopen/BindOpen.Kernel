using BindOpen.Data.Configuration;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace BindOpen.Data.Items
{
    /// <summary>
    /// This class represents a data exp that can contain a literal and script texts.
    /// </summary>
    [XmlType("Expression", Namespace = "https://xsd.bindopen.org")]
    [XmlRoot(ElementName = "expression", Namespace = "https://xsd.bindopen.org", IsNullable = false)]
    public class ExpressionDto
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
        [XmlElement("kind")]
        public BdoExpressionKind Kind { get; set; } = BdoExpressionKind.Auto;

        /// <summary>
        /// The script word of this instance.
        /// </summary>
        [JsonPropertyName("word")]
        [XmlElement("word")]
        public ConfigurationDto Word { get; set; }

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