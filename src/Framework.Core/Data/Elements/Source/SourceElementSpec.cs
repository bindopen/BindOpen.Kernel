using System;
using System.Xml.Serialization;
using BindOpen.Framework.Core.Data.Common;
using BindOpen.Framework.Core.Data.Items.Source;
using BindOpen.Framework.Core.Data.Specification.Filters;

namespace BindOpen.Framework.Core.Data.Elements.Source
{
    /// <summary>
    /// This class represents a data source element specification.
    /// </summary>
    [Serializable()]
    [XmlType("SourceElementSpec", Namespace = "http://meltingsoft.com/bindopen/xsd")]
    [XmlRoot(ElementName = "specification", Namespace = "http://meltingsoft.com/bindopen/xsd", IsNullable = false)]
    public class SourceElementSpec : DataElementSpec, ISourceElementSpec
    {
        // --------------------------------------------------
        // PROPERTIES
        // --------------------------------------------------

        #region Properties

        /// <summary>
        /// The data source kind of this instance.
        /// </summary>
        [XmlAttribute("kind")]
        public DataSourceKind DataSourceKind { get; set; } = DataSourceKind.Any;

        /// <summary>
        /// The definition filter of this instance.
        /// </summary>
        [XmlElement("definition.filter")]
        public DataValueFilter DefinitionFilter { get; set; } = new DataValueFilter();

        /// <summary>
        /// Specification of the DefinitionFilter property of this instance.
        /// </summary>
        [XmlIgnore()]
        public bool DefinitionFilterSpecified => DefinitionFilter != null
                    && (DefinitionFilter.AddedValues == null || DefinitionFilter.AddedValues.Count > 0) &&
                    (DefinitionFilter.RemovedValues == null || DefinitionFilter.RemovedValues.Count > 0);

        #endregion

        // --------------------------------------------------
        // CONSTRUCTORS
        // --------------------------------------------------

        #region Constructors

        /// <summary>
        /// Initializes a new data source element specification.
        /// </summary>
        public SourceElementSpec(): this(AccessibilityLevel.Public)
        {
        }

        /// <summary>
        /// Initializes a new data source element specification.
        /// </summary>
        /// <param name="accessibilityLevel">The accessibilty level of this instance.</param>
        /// <param name="specificationLevels">The specification levels of this instance.</param>
        public SourceElementSpec(
            AccessibilityLevel accessibilityLevel = AccessibilityLevel.Public,
            SpecificationLevel[] specificationLevels = null)
            : base(accessibilityLevel, specificationLevels)
        {
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
        public override object Clone()
        {
            SourceElementSpec specification = base.Clone() as SourceElementSpec;
            if (DefinitionFilter != null)
                specification.DefinitionFilter = DefinitionFilter.Clone() as DataValueFilter;
            return specification;
        }

        #endregion
    }
}
