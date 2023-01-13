using BindOpen.MetaData;
using BindOpen.MetaData.Items;
using BindOpen.MetaData.Specification;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace BindOpen.MetaData.Elements
{
    /// <summary>
    /// This class represents a data element specification.
    /// </summary>
    [XmlType("BdoElementSpec", Namespace = "https://docs.bindopen.org/xsd")]
    [XmlRoot(ElementName = "specification", Namespace = "https://docs.bindopen.org/xsd", IsNullable = false)]
    [XmlInclude(typeof(CarrierElementSpecDto))]
    [XmlInclude(typeof(ObjectElementSpecDto))]
    [XmlInclude(typeof(ScalarElementSpecDto))]
    [XmlInclude(typeof(SourceElementSpecDto))]
    public abstract class BdoElementSpecDto : DataSpecificationDto
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
        public List<DataSpecificationDto> AreaSpecifications { get; set; }

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
        [JsonPropertyName("script")]
        [XmlAttribute("script")]
        [DefaultValue("")]
        public string ItemScript { get; set; }

        /// <summary>
        /// The available itemization modes of this instance.
        /// </summary>
        [JsonPropertyName("availableItemizationModes")]
        [XmlArray("availableItemizationModes")]
        [XmlArrayItem("add.mode")]
        public List<DataItemizationMode> AvailableItemizationModes { get; set; }

        /// <summary>
        /// Minimum item number of this instance.
        /// </summary>
        [JsonPropertyName("minimumItemNumber")]
        [XmlElement("minimumItemNumber")]
        [DefaultValue(1)]
        public int MinimumItemNumber { get; set; } = 1;

        /// <summary>
        /// Maximum item number of this instance.
        /// </summary>
        [JsonPropertyName("maximumItemNumber")]
        [XmlElement("maximumItemNumber")]
        [DefaultValue(-1)]
        public int MaximumItemNumber { get; set; } = -1;

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
        public DataConstraintStatementDto ConstraintStatement { get; set; }

        #endregion

        // --------------------------------------------------
        // CONSTRUCTORS
        // --------------------------------------------------

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the BdoElementSpecDto class.
        /// </summary>
        protected BdoElementSpecDto()
        {
        }

        #endregion
    }
}
