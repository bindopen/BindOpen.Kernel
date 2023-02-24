using BindOpen.Data.Configuration;
using BindOpen.Data.Items;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace BindOpen.Data.Meta
{
    /// <summary>
    /// This class represents a data element specification.
    /// </summary>
    [XmlType("Spec", Namespace = "https://xsd.bindopen.org")]
    [XmlInclude(typeof(ObjectSpecDto))]
    [XmlInclude(typeof(ScalarSpecDto))]
    public abstract class SpecDto : IDto
    {
        // --------------------------------------------------
        // PROPERTIES
        // --------------------------------------------------

        #region Properties

        // General ------------------------------------------

        /// <summary>
        /// The value type of this instance.
        /// </summary>
        [JsonPropertyName("valueType")]
        [XmlAttribute("valueType")]
        [DefaultValue(DataValueTypes.Text)]
        public DataValueTypes ValueType { get; set; } = DataValueTypes.Any;

        /// <summary>
        /// ID of the group of this instance.
        /// </summary>
        [JsonPropertyName("groupId")]
        [XmlElement("groupId")]
        [DefaultValue("")]
        public string GroupId { get; set; }

        /// <summary>
        /// The aliases of the entry.
        /// </summary>
        [JsonPropertyName("aliases")]
        [XmlArray("aliases")]
        [XmlArrayItem("alias")]
        public List<string> Aliases { get; set; }

        /// <summary>
        /// The area specifications for this instance.
        /// </summary>
        [JsonPropertyName("areaSpecifications")]
        [XmlArray("areaSpecifications")]
        [XmlArrayItem("add")]
        public List<SpecDto> AreaSpecifications { get; set; }

        /// <summary>
        /// Indicates whether the instance can be allocated.
        /// </summary>
        [JsonPropertyName("isAllocatable")]
        [XmlElement("isAllocatable")]
        [DefaultValue(false)]
        public bool IsAllocatable { get; set; } = false;

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
        [JsonPropertyName("availableValueModes")]
        [XmlArray("availableValueModes")]
        [XmlArrayItem("add.mode")]
        public List<DataMode> AvailableValueModes { get; set; }

        /// <summary>
        /// Minimum item number of this instance.
        /// </summary>
        [JsonPropertyName("minimumItemNumber")]
        [XmlElement("minimumItemNumber")]
        [DefaultValue(1)]
        public uint MinimumItemNumber { get; set; } = 1;

        /// <summary>
        /// Maximum item number of this instance.
        /// </summary>
        [JsonPropertyName("maximumItemNumber")]
        [XmlElement("maximumItemNumber")]
        [DefaultValue(null)]
        public uint? MaximumItemNumber { get; set; }

        /// <summary>
        /// Levels of specification of this instance.
        /// </summary>
        [JsonPropertyName("itemSpecificationLevels")]
        [XmlArray("itemSpecificationLevels")]
        [XmlArrayItem("itemSpecificationLevel")]
        public List<SpecificationLevels> ItemSpecificationLevels { get; set; }

        // Constraints ---------------------------

        /// <summary>
        /// Constraint statement of this instance.
        /// </summary>
        [JsonPropertyName("constraint.statement")]
        [XmlElement("constraint.statement")]
        public ConfigurationSetDto ConstraintStatement { get; set; }

        /// <summary>
        /// Level of accessibility of this instance.
        /// </summary>
        [JsonPropertyName("accessibilityLevel")]
        [XmlElement("accessibilityLevel")]
        [DefaultValue(AccessibilityLevels.Public)]
        public AccessibilityLevels AccessibilityLevel { get; set; } = AccessibilityLevels.Public;

        /// <summary>
        /// The level of inheritance of this instance.
        /// </summary>
        [JsonPropertyName("inheritanceLevel")]
        [XmlElement("inheritanceLevel")]
        [DefaultValue(InheritanceLevels.None)]
        public InheritanceLevels InheritanceLevel { get; set; } = InheritanceLevels.None;

        /// <summary>
        /// The requirement level of this instance.
        /// </summary>
        [JsonPropertyName("requirementLevel")]
        [XmlAttribute("requirementLevel")]
        [DefaultValue(RequirementLevels.None)]
        public RequirementLevels RequirementLevel { get; set; } = RequirementLevels.None;

        /// <summary>
        /// The requirement script of this instance.
        /// </summary>
        [JsonPropertyName("requirementExp")]
        [XmlElement("requirementExp")]
        public ExpressionDto RequirementExpression { get; set; }

        /// <summary>
        /// Levels of specification of this instance.
        /// </summary>
        [JsonPropertyName("specificationLevels")]
        [XmlArray("specificationLevels")]
        [XmlArrayItem("specificationLevel")]
        public List<SpecificationLevels> SpecificationLevels { get; set; }

        #endregion

        // --------------------------------------------------
        // CONSTRUCTORS
        // --------------------------------------------------

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the BdoElementSpecDto class.
        /// </summary>
        protected SpecDto()
        {
        }

        #endregion
    }
}
