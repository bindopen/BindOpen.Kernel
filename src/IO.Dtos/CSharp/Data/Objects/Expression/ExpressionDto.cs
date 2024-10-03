using BindOpen.Scoping.Script;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace BindOpen.Data
{
    /// <summary>
    /// This class represents an expression DTO.
    /// </summary>
    [XmlType("Expression", Namespace = "https://storage.bindopen.org/xsd/bindopen/kernel")]
    [XmlRoot(ElementName = "expression", Namespace = "https://storage.bindopen.org/xsd/bindopen/kernel", IsNullable = false)]
    public class ExpressionDto : IBdoDto
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// The expression of this instance.
        /// </summary>
        [Key]
        [Column("ItemId")]
        [JsonIgnore]
        [XmlIgnore]
        public string Identifier { get; set; }

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

        // Reference

        /// <summary>
        /// The reference of this instance.
        /// </summary>
        [ForeignKey(nameof(ReferenceIdentifier))]
        [JsonIgnore]
        [XmlIgnore]
        public ReferenceDto Reference { get; set; }

        /// <summary>
        /// The reference name of this instance.
        /// </summary>
        [JsonIgnore]
        [XmlIgnore]
        public string ReferenceIdentifier { get; set; }

        // Word

        /// <summary>
        /// The script word of this instance.
        /// </summary>
        [JsonPropertyName("word")]
        [XmlElement("word")]
        public ScriptwordDto Scriptword { get; set; }

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