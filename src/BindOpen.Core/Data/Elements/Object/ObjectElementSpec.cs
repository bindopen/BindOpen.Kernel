using BindOpen.Data.Common;
using BindOpen.Data.Specification;
using BindOpen.System.Diagnostics;
using System.Xml.Serialization;

namespace BindOpen.Data.Elements
{
    /// <summary>
    /// This class represents an object element specification.
    /// </summary>
    [XmlType("ObjectElementSpec", Namespace = "https://docs.bindopen.org/xsd")]
    [XmlRoot(ElementName = "specification", Namespace = "https://docs.bindopen.org/xsd", IsNullable = false)]
    public class ObjectElementSpec : DataElementSpec, IObjectElementSpec
    {
        // --------------------------------------------------
        // PROPERTIES
        // --------------------------------------------------

        #region Properties

        // Entity ----------------------------------

        /// <summary>
        /// The class filter of this instance.
        /// </summary>
        [XmlElement("class.filter")]
        public DataValueFilter ClassFilter { get; set; } = null;

        /// <summary>
        /// The entity filter of this instance.
        /// </summary>
        [XmlElement("definition.filter")]
        public DataValueFilter DefinitionFilter { get; set; } = null;

        #endregion

        // --------------------------------------------------
        // CONSTRUCTORS
        // --------------------------------------------------

        #region Constructors

        /// <summary>
        /// Initializes a new object element specification.
        /// </summary>
        public ObjectElementSpec() : base()
        {
        }

        /// <summary>
        /// Initializes a new object element specification.
        /// </summary>
        /// <param name="accessibilityLevel">The accessibilty level of this instance.</param>
        /// <param name="specificationLevels">The specification levels of this instance.</param>
        public ObjectElementSpec(
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
            // we check that the entity unique name is available

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
            ObjectElementSpec specification = base.Clone(areas) as ObjectElementSpec;
            if (ClassFilter != null)
                specification.ClassFilter = ClassFilter.Clone() as DataValueFilter;
            //if (FormatUniqueNameFilter != null)
            //    entityElementSpec.FormatUniqueNameFilter = FormatUniqueNameFilter.Clone() as DataValueFilter;
            return specification;
        }

        #endregion
    }
}
