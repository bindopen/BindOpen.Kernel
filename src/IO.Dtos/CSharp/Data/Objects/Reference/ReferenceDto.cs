using BindOpen.Data.Meta;
using BindOpen.Scoping.Script;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace BindOpen.Data
{
    /// <summary>
    /// This class represents a reference DTO.
    /// </summary>
    [XmlType("Reference", Namespace = "https://storage.bindopen.org/xsd/bindopen/kernel")]
    [XmlRoot(ElementName = "reference", Namespace = "https://storage.bindopen.org/xsd/bindopen/kernel", IsNullable = false)]
    public class ReferenceDto : IBdoDto
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
        /// The kind of this instance.
        /// </summary>
        [JsonPropertyName("kind")]
        [XmlAttribute("kind")]
        [DefaultValue(BdoReferenceKind.Expression)]
        public BdoReferenceKind Kind { get; set; } = BdoReferenceKind.Expression;

        /// <summary>
        /// The expression of this instance.
        /// </summary>
        [JsonPropertyName("expression")]
        [XmlElement("expression")]
        public ExpressionDto Expression { get; set; }

        /// <summary>
        /// The meta data  of this instance.
        /// </summary>
        [ForeignKey(nameof(MetaDataId))]
        [JsonPropertyName("meta")]
        [XmlElement("set", Type = typeof(MetaNodeDto))]
        [XmlElement("object", Type = typeof(MetaObjectDto))]
        [XmlElement("scalar", Type = typeof(MetaScalarDto))]
        [XmlElement("scriptword", Type = typeof(ScriptwordDto))]
        public MetaDataDto MetaData { get; set; }

        /// <summary>
        /// The class name of this instance.
        /// </summary>
        [JsonIgnore]
        [XmlIgnore]
        public string MetaDataId { get; set; }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of ReferenceDto class.
        /// </summary>
        public ReferenceDto()
        {
        }

        #endregion
    }
}