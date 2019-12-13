using BindOpen.Framework.Core.Data.Common;
using BindOpen.Framework.Core.Data.Specification;
using BindOpen.Framework.Core.System.Diagnostics;
using System.Xml.Serialization;

namespace BindOpen.Framework.Core.Data.Elements
{
    /// <summary>
    /// This class represents an object element specification.
    /// </summary>
    [XmlType("ObjectElementSpec", Namespace = "https://bindopen.org/xsd")]
    [XmlRoot(ElementName = "specification", Namespace = "https://bindopen.org/xsd", IsNullable = false)]
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
        /// Specification of the ClassFilter property of this instance.
        /// </summary>
        [XmlIgnore()]
        public bool ClassFilterSpecified => ClassFilter != null
            && (ClassFilter.AddedValues == null || ClassFilter.AddedValues.Count > 0)
            && (ClassFilter.RemovedValues == null || ClassFilter.RemovedValues.Count > 0);

        /// <summary>
        /// The entity filter of this instance.
        /// </summary>
        [XmlElement("definition.filter")]
        public DataValueFilter DefinitionFilter { get; set; } = null;

        /// <summary>
        /// Specification of the ClassFilter property of this instance.
        /// </summary>
        [XmlIgnore()]
        public bool DefinitionFilterSpecified => this.DefinitionFilter != null
                    && (this.DefinitionFilter.AddedValues == null || this.DefinitionFilter.AddedValues.Count > 0)
                    && (this.DefinitionFilter.RemovedValues == null || this.DefinitionFilter.RemovedValues.Count > 0);

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
        public override object Clone()
        {
            ObjectElementSpec specification = base.Clone() as ObjectElementSpec;
            if (ClassFilter != null)
                specification.ClassFilter = ClassFilter.Clone() as DataValueFilter;
            //if (FormatUniqueNameFilter != null)
            //    entityElementSpec.FormatUniqueNameFilter = FormatUniqueNameFilter.Clone() as DataValueFilter;
            return specification;
        }

        #endregion
    }
}
