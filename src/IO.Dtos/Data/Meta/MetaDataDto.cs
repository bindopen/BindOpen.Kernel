using BindOpen.System.Data.Assemblies;
using BindOpen.System.Scoping.Script;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text.Json.Serialization;
using System.Xml;
using System.Xml.Serialization;

namespace BindOpen.System.Data.Meta
{
    /// <summary>
    /// This class represents a data element.
    /// </summary>
    [XmlType("MetaData", Namespace = "https://storage.bindopen.org/xsd/bindopen")]
    [XmlInclude(typeof(MetaCompositeDto))]
    [XmlInclude(typeof(MetaObjectDto))]
    [XmlInclude(typeof(MetaScalarDto))]
    [XmlInclude(typeof(ScriptwordDto))]
    [JsonDerivedType(typeof(MetaCompositeDto), "set")]
    [JsonDerivedType(typeof(MetaObjectDto), "object")]
    [JsonDerivedType(typeof(MetaScalarDto), "scalar")]
    [JsonDerivedType(typeof(ScriptwordDto), "scripword")]
    public class MetaDataDto : IBdoDto, IIdentified
    {
        // --------------------------------------------------
        // PROPERTIES
        // --------------------------------------------------

        #region Properties

        /// <summary>
        /// ID of this instance.
        /// </summary>
        [JsonPropertyName("id")]
        [XmlAttribute("id")]
        public string Id { get; set; }

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
        [JsonIgnore()]
        [XmlIgnore()]
        public bool IndexSpecified => Index != null;

        // Items

        /// <summary>
        /// The itemization mode of this instance.
        /// </summary>
        [JsonPropertyName("mode")]
        [XmlAttribute("mode")]
        [DefaultValue(DataMode.Value)]
        public DataMode DataMode = DataMode.Value;

        /// <summary>
        /// The expression of this instance.
        /// </summary>
        [JsonPropertyName("ref")]
        [XmlElement("ref")]
        public ReferenceDto DataReference { get; set; }

        /// <summary>
        /// The elements of this instance.
        /// </summary>
        [JsonPropertyName("specs")]
        [XmlElement("spec")]
        public List<SpecDto> Specs { get; set; }

        /// <summary>
        /// Indicates whether the entities property must be ignored.
        /// </summary>
        [JsonIgnore]
        [XmlIgnore]
        public bool SpecsSpecified => Specs?.Count > 0;

        /// <summary>
        /// The value type of this instance.
        /// </summary>
        [JsonPropertyName("valueType")]
        [XmlAttribute("valueType")]
        [DefaultValue(DataValueTypes.Any)]
        public DataValueTypes ValueType { get; set; } = DataValueTypes.Any;

        /// <summary>
        /// The class reference of this instance.
        /// </summary>
        [JsonPropertyName("class")]
        [XmlElement("class")]
        public ClassReferenceDto ClassReference { get; set; }

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
