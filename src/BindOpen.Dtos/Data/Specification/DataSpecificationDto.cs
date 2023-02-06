﻿using BindOpen.Data.Items;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace BindOpen.Data.Specification
{
    /// <summary>
    /// This abstract class represents a data specification.
    /// </summary>
    [XmlType("DataSpecification", Namespace = "https://xsd.bindopen.org")]
    [XmlRoot(ElementName = "specification", Namespace = "https://xsd.bindopen.org", IsNullable = false)]
    public abstract class DataSpecificationDto
    {
        // --------------------------------------------------
        // PROPERTIES
        // --------------------------------------------------

        #region Properties

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
        /// Initializes a new insance of the DataSpecificationDto class.
        /// </summary>
        protected DataSpecificationDto()
        {
        }

        #endregion
    }
}