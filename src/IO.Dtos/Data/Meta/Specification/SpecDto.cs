using BindOpen.System.Data.Assemblies;
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

        // General ------------------------------------------

        /// <summary>
        /// The value type of this instance.
        /// </summary>
        [JsonPropertyName("valueType")]
        [XmlElement("valueType")]
        [DefaultValue(DataValueTypes.Any)]
        public DataValueTypes ValueType { get; set; } = DataValueTypes.Any;

        /// <summary>
        /// The class reference of this instance.
        /// </summary>
        [JsonPropertyName("classRef")]
        [XmlElement("classRef")]
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
        [XmlArrayItem("add")]
        public List<string> DefaultItems { get; set; }

        /// <summary>
        /// The aliases of the entry.
        /// </summary>
        [JsonPropertyName("aliases")]
        [XmlArray("aliases")]
        [XmlArrayItem("add")]
        public List<string> Aliases { get; set; }

        /// <summary>
        /// The area specifications for this instance.
        /// </summary>
        [JsonPropertyName("areaSpecs")]
        [XmlArray("areaSpecs")]
        [XmlArrayItem("add")]
        public List<SpecDto> AreaSpecifications { get; set; }

        /// <summary>
        /// Indicates whether the instance can be allocated.
        /// </summary>
        [JsonPropertyName("isAllocatable")]
        [XmlElement("isAllocatable")]
        [DefaultValue(false)]
        public bool? IsAllocatable { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonIgnore()]
        [XmlIgnore()]
        public bool IsAllocatableSpecified => IsAllocatable != null;

        // Items ---------------------------------

        /// <summary>
        /// The script of this instance.
        /// </summary>
        [JsonPropertyName("itemExp")]
        [XmlElement("itemExp")]
        public ExpressionDto ItemExpression { get; set; }

        /// <summary>
        /// The available itemization modes of this instance.
        /// </summary>
        [JsonPropertyName("valueModes")]
        [XmlArray("valueModes")]
        [XmlArrayItem("add")]
        public List<DataMode> AvailableValueModes { get; set; }

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

        /// <summary>
        /// Levels of specification of this instance.
        /// </summary>
        [JsonPropertyName("spec.levels")]
        [XmlArray("spec.levels")]
        [XmlArrayItem("add")]
        public List<SpecificationLevels> SpecificationLevels { get; set; }

        /// <summary>
        /// Levels of specification of this instance.
        /// </summary>
        [JsonPropertyName("itemSpec.levels")]
        [XmlArray("itemSpec.levels")]
        [XmlArrayItem("add")]
        public List<SpecificationLevels> ItemSpecificationLevels { get; set; }

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
        public ExpressionDto RequirementExp { get; set; }

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
