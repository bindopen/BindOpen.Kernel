using BindOpen.System.Data.Assemblies;
using BindOpen.System.Data.Conditions;
using BindOpen.System.Scoping.Script;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace BindOpen.System.Data.Meta
{
    /// <summary>
    /// This class represents a data element specification.
    /// </summary>
    [XmlType("Spec", Namespace = "https://storage.bindopen.org/xsd/bindopen")]
    [XmlRoot(ElementName = "spec", Namespace = "https://storage.bindopen.org/xsd/bindopen", IsNullable = false)]
    [XmlInclude(typeof(SpecDto))]
    [JsonDerivedType(typeof(SpecDto), "spec")]
    [JsonDerivedType(typeof(AggregateSpecDto), "node")]
    public class SpecDto : IBdoDto, IIdentified
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
        /// The name of this instance.
        /// </summary>
        [JsonPropertyName("name")]
        [XmlAttribute("name")]
        public string Name { get; set; }

        // General ------------------------------------------

        /// <summary>
        /// Default items of this instance.
        /// </summary>
        [JsonPropertyName("condition")]
        [XmlElement("condition", Type = typeof(BasicConditionDto))]
        [XmlElement("condition.node", Type = typeof(CompositeConditionDto))]
        [XmlElement("condition.reference", Type = typeof(ExpressionConditionDto))]
        public ConditionDto Condition { get; set; }

        /// <summary>
        /// The expression of this instance.
        /// </summary>
        [JsonPropertyName("ref")]
        [XmlElement("ref")]
        public ReferenceDto DataReference { get; set; }

        /// <summary>
        /// The description DTO of this instance.
        /// </summary>
        [JsonPropertyName("description")]
        [XmlElement("description")]
        public DictionaryDto Description { get; set; }

        /// <summary>
        /// The description DTO of this instance.
        /// </summary>
        [JsonPropertyName("title")]
        [XmlElement("title")]
        public DictionaryDto Title { get; set; }

        /// <summary>
        /// ID of the group of this instance.
        /// </summary>
        [JsonPropertyName("detail")]
        [XmlElement("detail")]
        public MetaSetDto Detail { get; set; }

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
        [JsonPropertyName("class")]
        [XmlElement("class")]
        public ClassReferenceDto ClassReference { get; set; }

        /// <summary>
        /// ID of the group of this instance.
        /// </summary>
        [JsonPropertyName("groupId")]
        [XmlElement("groupId")]
        public string GroupId { get; set; }

        /// <summary>
        /// Default items of this instance.
        /// </summary>
        [JsonPropertyName("default.items")]
        [XmlArray("default.items")]
        [XmlArrayItem("node", Type = typeof(MetaNodeDto))]
        [XmlArrayItem("object", Type = typeof(MetaObjectDto))]
        [XmlArrayItem("scalar", Type = typeof(MetaScalarDto))]
        [XmlArrayItem("word", Type = typeof(ScriptwordDto))]
        public List<MetaDataDto> DefaultItems { get; set; }

        /// <summary>
        /// The aliases of the entry.
        /// </summary>
        [JsonPropertyName("aliases")]
        [XmlArray("aliases")]
        [XmlArrayItem("add")]
        public List<string> Aliases { get; set; }

        // Items ---------------------------------

        /// <summary>
        /// The available itemization modes of this instance.
        /// </summary>
        [JsonPropertyName("data.modes")]
        [XmlArray("data.modes")]
        [XmlArrayItem("add")]
        public List<DataMode> AvailableDataModes { get; set; }

        /// <summary>
        /// Minimum item number of this instance.
        /// </summary>
        [JsonPropertyName("items.minNumber")]
        [XmlElement("items.minNumber")]
        [DefaultValue(null)]
        public int? MinDataItemNumber { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonIgnore()]
        [XmlIgnore()]
        public bool MinDataItemNumberSpecified => MinDataItemNumber != null;

        /// <summary>
        /// Maximum item number of this instance.
        /// </summary>
        [JsonPropertyName("items.maxNumber")]
        [XmlElement("items.maxNumber")]
        [DefaultValue(null)]
        public int? MaxDataItemNumber { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonIgnore()]
        [XmlIgnore()]
        public bool MaxDataItemNumberSpecified => MaxDataItemNumber != null;

        // Data 

        /// <summary>
        /// Indicates whether the instance can be allocated.
        /// </summary>
        [JsonPropertyName("isAllocatable")]
        [XmlElement("isAllocatable")]
        [DefaultValue(null)]
        public bool? IsAllocatable { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonIgnore()]
        [XmlIgnore()]
        public bool IsAllocatableSpecified => IsAllocatable != null;

        [JsonPropertyName("isStatic")]
        [XmlElement("isStatic")]
        [DefaultValue(null)]
        public bool? IsStatic { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonIgnore()]
        [XmlIgnore()]
        public bool IsStaticSpecified => IsStatic != null;

        /// <summary>
        /// Indicates whether the instance can be allocated.
        /// </summary>
        [JsonPropertyName("label")]
        [XmlElement("label")]
        public string Label { get; set; }

        // Levels

        /// <summary>
        /// Levels of specification of this instance.
        /// </summary>
        [JsonPropertyName("spec.levels")]
        [XmlArray("spec.levels")]
        [XmlArrayItem("add")]
        public List<SpecificationLevels> SpecLevels { get; set; }

        /// <summary>
        /// Levels of specification of this instance.
        /// </summary>
        [JsonPropertyName("item.spec.levels")]
        [XmlArray("item.spec.levels")]
        [XmlArrayItem("add")]
        public List<SpecificationLevels> ItemSpecLevels { get; set; }

        /// <summary>
        /// Level of accessibility of this instance.
        /// </summary>
        [JsonPropertyName("accessibility.level")]
        [XmlElement("accessibility.level")]
        [DefaultValue(AccessibilityLevels.Public)]
        public AccessibilityLevels AccessibilityLevel { get; set; }

        /// <summary>
        /// The level of inheritance of this instance.
        /// </summary>
        [JsonPropertyName("inheritance.level")]
        [XmlElement("inheritance.level")]
        [DefaultValue(InheritanceLevels.None)]
        public InheritanceLevels InheritanceLevel { get; set; }

        /// <summary>
        /// The requirement level of this instance.
        /// </summary>
        [JsonPropertyName("requirement.level")]
        [XmlElement("requirement.level")]
        [DefaultValue(RequirementLevels.None)]
        public RequirementLevels RequirementLevel { get; set; }

        /// <summary>
        /// The requirement script of this instance.
        /// </summary>
        [JsonPropertyName("requirement.exp")]
        [XmlElement("requirement.exp")]
        public string RequirementExp { get; set; }

        /// <summary>
        /// The requirement level of this instance.
        /// </summary>
        [JsonPropertyName("item.requirement.level")]
        [XmlElement("item.requirement.level")]
        [DefaultValue(RequirementLevels.None)]
        public RequirementLevels ItemRequirementLevel { get; set; }

        [JsonPropertyName("item.requirement.exp")]
        [XmlElement("item.requirement.exp")]
        public string ItemRequirementExp { get; set; }

        #endregion

        // --------------------------------------------------
        // CONSTRUCTORS
        // --------------------------------------------------

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the SpecDto class.
        /// </summary>
        public SpecDto()
        {
        }

        #endregion
    }
}
