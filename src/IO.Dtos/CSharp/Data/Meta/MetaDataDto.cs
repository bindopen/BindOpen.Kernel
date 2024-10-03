using BindOpen.Data.Assemblies;
using BindOpen.Scoping.Script;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using System.Xml;
using System.Xml.Serialization;

namespace BindOpen.Data.Meta
{
    /// <summary>
    /// This class represents a data element.
    /// </summary>
    [XmlType("MetaData", Namespace = "https://storage.bindopen.org/xsd/bindopen/kernel")]
    [XmlInclude(typeof(MetaNodeDto))]
    [XmlInclude(typeof(MetaObjectDto))]
    [XmlInclude(typeof(MetaScalarDto))]
    [XmlInclude(typeof(ScriptwordDto))]
    [JsonPolymorphic(TypeDiscriminatorPropertyName = "metaKind")]
    [JsonDerivedType(typeof(MetaNodeDto), "Node")]
    [JsonDerivedType(typeof(MetaObjectDto), "Object")]
    [JsonDerivedType(typeof(MetaScalarDto), "Scalar")]
    [JsonDerivedType(typeof(ScriptwordDto), "Scripword")]
    public class MetaDataDto : IBdoDto, IIdentified
    {
        // --------------------------------------------------
        // PROPERTIES
        // --------------------------------------------------

        #region Properties

        /// <summary>
        /// ID of this instance.
        /// </summary>
        [Key]
        [Column("MetaDataId")]
        [JsonPropertyName("id")]
        [XmlElement("id")]
        public string Identifier { get; set; }

        [JsonIgnore()]
        [XmlIgnore()]
        public List<MetaSetDto> Supers { get; set; }

        /// <summary>
        /// Name of this instance.
        /// </summary>
        [JsonPropertyName("name")]
        [XmlAttribute("name")]
        [DefaultValue(null)]
        public string Name { get; set; }

        // IIndexedDataItem -------------------------------

        /// <summary>
        /// The index of this instance.
        /// </summary>
        [JsonPropertyName("index")]
        [XmlElement("index")]
        public int? Index { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [NotMapped]
        [JsonIgnore()]
        [XmlIgnore()]
        public bool IndexSpecified => Index != null;

        // Items

        /// <summary>
        /// The expression of this instance.
        /// </summary>
        [ForeignKey(nameof(ReferenceId))]
        [JsonPropertyName("ref")]
        [XmlElement("ref")]
        public ReferenceDto Reference { get; set; }

        /// <summary>
        /// The class name of this instance.
        /// </summary>
        [JsonIgnore]
        [XmlIgnore]
        public string ReferenceId { get; set; }

        /// <summary>
        /// The elements of this instance.
        /// </summary>
        [ForeignKey(nameof(SpecId))]
        [JsonPropertyName("spec")]
        [XmlElement("spec")]
        public SpecDto Spec { get; set; }

        /// <summary>
        /// The class name of this instance.
        /// </summary>
        [JsonIgnore]
        [XmlIgnore]
        public string SpecId { get; set; }

        /// <summary>
        /// The value type of this instance.
        /// </summary>
        [JsonPropertyName("valueType")]
        [XmlAttribute("valueType")]
        [DefaultValue(DataValueTypes.Any)]
        public DataValueTypes ValueType { get; set; } = DataValueTypes.Any;

        /// <summary>
        /// Creation date of this instance.
        /// </summary>
        [JsonPropertyName("definition")]
        [XmlAttribute("definition")]
        public string DefinitionUniqueName { get; set; }

        /// <summary>
        /// The class reference of this instance.
        /// </summary>
        [ForeignKey(nameof(ClassReferenceId))]
        [JsonPropertyName("class")]
        [XmlElement("class")]
        public ClassReferenceDto ClassReference { get; set; }

        /// <summary>
        /// The class name of this instance.
        /// </summary>
        [JsonIgnore]
        [XmlIgnore]
        public string ClassReferenceId { get; set; }

        #endregion

        // --------------------------------------------------
        // CONSTRUCTORS
        // --------------------------------------------------

        #region Constructors

        /// <summary>
        /// Initializes a new DTO data element.
        /// </summary>
        public MetaDataDto()
        {
        }

        #endregion
    }
}
