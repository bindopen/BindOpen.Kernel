using BindOpen.System.Data.Meta;
using BindOpen.System.Scoping.Script;
using System.ComponentModel;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace BindOpen.System.Data
{
    /// <summary>
    /// This class represents a data exp that can contain a literal and script texts.
    /// </summary>
    [XmlType("Reference", Namespace = "https://storage.bindopen.org/xsd/bindopen")]
    [XmlRoot(ElementName = "reference", Namespace = "https://storage.bindopen.org/xsd/bindopen", IsNullable = false)]
    public class ReferenceDto : IBdoDto
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// The kind of this instance.
        /// </summary>
        [JsonPropertyName("kind")]
        [XmlAttribute("kind")]
        [DefaultValue(BdoReferenceKind.Expression)]
        public BdoReferenceKind Kind { get; set; } = BdoReferenceKind.Expression;

        /// <summary>
        /// The identifier of this instance.
        /// </summary>
        [JsonPropertyName("identifier")]
        [XmlElement("identifier")]
        public string Identifier { get; set; }

        /// <summary>
        /// The value of this instance.
        /// </summary>
        [JsonPropertyName("text")]
        [XmlText()]
        public string Text { get; set; }

        /// <summary>
        /// The kind of this instance.
        /// </summary>
        [JsonPropertyName("expKind")]
        [XmlAttribute("expKind")]
        [DefaultValue(BdoExpressionKind.Auto)]
        public BdoExpressionKind ExpressionKind { get; set; } = BdoExpressionKind.Auto;

        /// <summary>
        /// The script word of this instance.
        /// </summary>
        [JsonPropertyName("meta")]
        [XmlElement("set", Type = typeof(MetaNodeDto))]
        [XmlElement("object", Type = typeof(MetaObjectDto))]
        [XmlElement("scalar", Type = typeof(MetaScalarDto))]
        [XmlElement("scriptword", Type = typeof(ScriptwordDto))]
        public MetaDataDto MetaData { get; set; }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of DataReferenceDto class.
        /// </summary>
        public ReferenceDto()
        {
        }

        #endregion
    }
}