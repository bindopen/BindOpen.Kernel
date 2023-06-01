﻿using System.Collections.Generic;
using System.ComponentModel;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace BindOpen.Data.Meta
{
    /// <summary>
    /// This class represents a data element specification.
    /// </summary>
    [XmlType("Spec", Namespace = "https://storage.bindopen.org/xsd/bindopen")]
    [XmlRoot(ElementName = "spec", Namespace = "https://storage.bindopen.org/xsd/bindopen", IsNullable = false)]
    public class SpecDto : IDto
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
        public AccessibilityLevels AccessibilityLevel { get; set; } = AccessibilityLevels.Public;

        /// <summary>
        /// The level of inheritance of this instance.
        /// </summary>
        [JsonPropertyName("inheritance.level")]
        [XmlElement("inheritance.level")]
        [DefaultValue(InheritanceLevels.None)]
        public InheritanceLevels InheritanceLevel { get; set; } = InheritanceLevels.None;

        /// <summary>
        /// The requirement level of this instance.
        /// </summary>
        [JsonPropertyName("requirement.level")]
        [XmlAttribute("requirement.level")]
        [DefaultValue(RequirementLevels.None)]
        public RequirementLevels RequirementLevel { get; set; } = RequirementLevels.None;

        /// <summary>
        /// The requirement script of this instance.
        /// </summary>
        [JsonPropertyName("requirement.exp")]
        [XmlElement("requirement.exp")]
        public ExpressionDto RequirementExp { get; set; }

        // Constraints ---------------------------

        /// <summary>
        /// Constraint statement of this instance.
        /// </summary>
        [JsonPropertyName("constraints")]
        [XmlElement("constraints")]
        public ConfigurationSetDto ConstraintStatement { get; set; }

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
