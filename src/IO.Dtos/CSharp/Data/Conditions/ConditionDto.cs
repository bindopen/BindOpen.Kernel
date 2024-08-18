using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace BindOpen.Data.Conditions
{
    /// <summary>
    /// This class represents a condition DTO.
    /// </summary>
    [XmlType("Condition", Namespace = "https://storage.bindopen.org/xsd/bindopen/kernel")]
    [XmlRoot(ElementName = "condition", Namespace = "https://storage.bindopen.org/xsd/bindopen/kernel", IsNullable = false)]
    [XmlInclude(typeof(BasicConditionDto))]
    [XmlInclude(typeof(CompositeConditionDto))]
    [XmlInclude(typeof(ExpressionConditionDto))]
    [JsonPolymorphic(TypeDiscriminatorPropertyName = "kind")]
    [JsonDerivedType(typeof(BasicConditionDto), "Condition")]
    [JsonDerivedType(typeof(CompositeConditionDto), "Composite")]
    [JsonDerivedType(typeof(ExpressionConditionDto), "Reference")]
    public abstract class ConditionDto : IBdoDto, IIdentified
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// The identifier of this instance.
        /// </summary>
        [Key]
        [Column("ConditionId")]
        [JsonPropertyName("id")]
        [XmlAttribute("id")]
        public string Identifier { get; set; }

        /// <summary>
        /// The name of this instance.
        /// </summary>
        [JsonPropertyName("name")]
        [XmlAttribute("name")]
        public string Name { get; set; }

        /// <summary>
        /// The parent identifier of this instance.
        /// </summary>
        [JsonPropertyName("parentId")]
        [XmlAttribute("parentId")]
        public string ParentId { get; set; }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the ConditionDto class.
        /// </summary>
        protected ConditionDto() : base()
        {
        }

        #endregion
    }
}