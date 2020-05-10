using BindOpen.Data.Common;
using BindOpen.Data.Items;
using BindOpen.Data.Specification;
using System.Xml.Serialization;

namespace BindOpen.Data.Elements
{
    /// <summary>
    /// This class represents a data source element specification.
    /// </summary>
    [XmlType("SourceElementSpec", Namespace = "https://docs.bindopen.org/xsd")]
    [XmlRoot(ElementName = "specification", Namespace = "https://docs.bindopen.org/xsd", IsNullable = false)]
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
        public DatasourceKind DatasourceKind { get; set; } = DatasourceKind.Any;

        /// <summary>
        /// The definition filter of this instance.
        /// </summary>
        [XmlElement("definition.filter")]
        public DataValueFilter DefinitionFilter { get; set; } = new DataValueFilter();

        #endregion

        // --------------------------------------------------
        // CONSTRUCTORS
        // --------------------------------------------------

        #region Constructors

        /// <summary>
        /// Initializes a new data source element specification.
        /// </summary>
        public SourceElementSpec() : this(AccessibilityLevels.Public)
        {
        }

        /// <summary>
        /// Initializes a new data source element specification.
        /// </summary>
        /// <param name="accessibilityLevel">The accessibilty level of this instance.</param>
        /// <param name="specificationLevels">The specification levels of this instance.</param>
        public SourceElementSpec(
            AccessibilityLevels accessibilityLevel = AccessibilityLevels.Public,
            SpecificationLevels[] specificationLevels = null)
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
        public override object Clone(params string[] areas)
        {
            SourceElementSpec specification = base.Clone(areas) as SourceElementSpec;
            if (DefinitionFilter != null)
                specification.DefinitionFilter = DefinitionFilter.Clone() as DataValueFilter;
            return specification;
        }

        #endregion
    }
}
