using BindOpen.Extensions.Scripting;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace BindOpen.Meta.Items
{
    /// <summary>
    /// This class represents a data exp that can contain a literal and script texts.
    /// </summary>
    [XmlType("DataExpression", Namespace = "https://docs.bindopen.org/xsd")]
    [XmlRoot(ElementName = "dataExpression", Namespace = "https://docs.bindopen.org/xsd", IsNullable = false)]
    public class DataExpressionDto
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// The value of this instance.
        /// </summary>
        [JsonPropertyName("text")]
        [XmlElement("text")]
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
        public BdoScriptwordConfigurationDto Word { get; set; }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of DataExpressionDto class.
        /// </summary>
        public DataExpressionDto()
        {
        }

        #endregion
    }
}