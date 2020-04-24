using BindOpen.Data.Common;
using BindOpen.Data.Specification;
using BindOpen.System.Diagnostics;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace BindOpen.Data.Elements.Schema
{
    /// <summary>
    /// This class represents a schema element specification.
    /// </summary>
    [XmlType("SchemaElementSpec", Namespace = "https://bindopen.org/xsd")]
    [XmlRoot(ElementName = "specification", Namespace = "https://bindopen.org/xsd", IsNullable = false)]
    public class SchemaElementSpec : DataElementSpec
    {
        // --------------------------------------------------
        // PROPERTIES
        // --------------------------------------------------

        #region Properties

        // Schema ----------------------------------

        /// <summary>
        /// The entity unique name filter of this instance.
        /// </summary>
        [XmlElement("entityUniqueNameFilter")]
        public DataValueFilter SchemuniqueNameFilter { get; set; } = new DataValueFilter();

        /// <summary>
        /// Schema requirement level of this instance.
        /// </summary>
        [XmlElement("entityRequirementLevel")]
        public RequirementLevel SchemaRequirementLevel { get; set; } = RequirementLevel.None;

        /// <summary>
        /// The specification levels for entity specification of this instance.
        /// </summary>
        [XmlArray("entitySpecificationLevels")]
        [XmlArrayItem("add.level")]
        public List<SpecificationLevels> SchemaSpecificationLevels { get; set; } = new List<SpecificationLevels>();

        // Format ----------------------------------

        /// <summary>
        /// The format unique name filter of this instance.
        /// </summary>
        [XmlElement("formatUniqueNameFilter")]
        public DataValueFilter FormatUniqueNameFilter { get; set; } = new DataValueFilter();

        /// <summary>
        /// Format requirement level of this instance.
        /// </summary>
        [XmlElement("formatRequirementLevel")]
        public RequirementLevel FormatRequirementLevel { get; set; } = RequirementLevel.Optional;

        /// <summary>
        /// The specification levels for format specification of this instance.
        /// </summary>
        [XmlArray("formatSpecificationLevels")]
        [XmlArrayItem("add.level")]
        public List<SpecificationLevels> FormatSpecificationLevels { get; set; } = new List<SpecificationLevels>();

        #endregion

        // --------------------------------------------------
        // CONSTRUCTORS
        // --------------------------------------------------

        #region Constructors

        /// <summary>
        /// Initializes a new schema element specification.
        /// </summary>
        public SchemaElementSpec() : base(AccessibilityLevels.Public)
        {
        }

        /// <summary>
        /// Initializes a new schema element specification.
        /// </summary>
        /// <param name="accessibilityLevel">The accessibilty level of this instance.</param>
        /// <param name="specificationLevels">The specification levels of this instance.</param>
        public SchemaElementSpec(
            AccessibilityLevels accessibilityLevel = AccessibilityLevels.Public,
            SpecificationLevels[] specificationLevels = null)
            : base(accessibilityLevel, specificationLevels)
        {
        }

        #endregion

        // --------------------------------------------------
        // ACCESSORS
        // --------------------------------------------------

        #region Accessors

        /// <summary>
        /// Check the specified item.
        /// </summary>
        /// <param name="item">The item to consider.</param>
        /// <param name="dataElement">The element to consider.</param>
        /// <returns>The log of check log.</returns>
        public override IBdoLog CheckItem(
            object item,
            IDataElement dataElement = null)
        {
            return new BdoLog();
        }

        /// <summary>
        /// Check the specified item.
        /// </summary>
        /// <param name="dataElement">The element to consider.</param>
        /// <param name="specificationAreas">The specification areas to consider.</param>
        /// <returns>The log of check log.</returns>
        public override IBdoLog CheckElement(
            IDataElement dataElement,
            string[] specificationAreas = null)
        {
            return new BdoLog();
        }

        #endregion

        // --------------------------------------------------
        // CLONING
        // --------------------------------------------------

        #region Cloning

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>Returns a cloned instance.</returns>
        public override object Clone(params string[] areas)
        {
            SchemaElementSpec aSchemaElementSpec = base.Clone(areas) as SchemaElementSpec;
            if (this.SchemuniqueNameFilter != null)
                aSchemaElementSpec.SchemuniqueNameFilter = this.SchemuniqueNameFilter.Clone() as DataValueFilter;
            if (this.FormatUniqueNameFilter != null)
                aSchemaElementSpec.FormatUniqueNameFilter = this.FormatUniqueNameFilter.Clone() as DataValueFilter;
            return aSchemaElementSpec;
        }

        #endregion
    }
}
