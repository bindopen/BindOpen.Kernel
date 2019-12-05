using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Xml.Serialization;
using BindOpen.Framework.Core.Data.Common;
using BindOpen.Framework.Core.Data.Elements;
using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.System.Diagnostics;

namespace BindOpen.Framework.Core.Data.Specification
{
    /// <summary>
    /// This abstract class represents a data specification.
    /// </summary>
    [Serializable()]
    [XmlType("DataSpecification", Namespace = "https://bindopen.org/xsd")]
    [XmlRoot(ElementName = "specification", Namespace = "https://bindopen.org/xsd", IsNullable = false)]
    public abstract class DataSpecification : IndexedDataItem, IDataSpecification
    {
        // --------------------------------------------------
        // VARIABLES
        // --------------------------------------------------

        #region Variables

        private List<SpecificationLevels> _specificationLevels = null;

        #endregion

        // --------------------------------------------------
        // PROPERTIES
        // --------------------------------------------------

        #region Properties

        /// <summary>
        /// The requirement level of this instance.
        /// </summary>
        [XmlAttribute("requirementLevel")]
        [DefaultValue(RequirementLevel.None)]
        public RequirementLevel RequirementLevel { get; set; } = RequirementLevel.None;

        /// <summary>
        /// The requirement script of this instance.
        /// </summary>
        [XmlElement("requirementScript")]
        public string RequirementScript { get; set; } = null;

        /// <summary>
        /// Specification of the RequirementScript property of this instance.
        /// </summary>
        [XmlIgnore()]
        public bool RequirementScriptSpecified => !string.IsNullOrEmpty(RequirementScript);

        /// <summary>
        /// The level of inheritance of this instance.
        /// </summary>
        [XmlElement("inheritanceLevel")]
        [DefaultValue(Common.InheritanceLevel.None)]
        public Common.InheritanceLevel InheritanceLevel { get; set; } = Common.InheritanceLevel.None;

        /// <summary>
        /// Levels of specification of this instance.
        /// </summary>
        [XmlArray("specificationLevels")]
        [XmlArrayItem("specificationLevel")]
        public List<SpecificationLevels> SpecificationLevels
        {
            get => _specificationLevels ?? (_specificationLevels = new List<SpecificationLevels>());
            set { _specificationLevels = value; }
        }

        /// <summary>
        /// Specification of the SpecificationLevels property of this instance.
        /// </summary>
        [XmlIgnore()]
        public bool SpecificationLevelsSpecified => _specificationLevels?.Count > 0 && !_specificationLevels.Contains(Common.SpecificationLevels.All);

        /// <summary>
        /// Level of accessibility of this instance.
        /// </summary>
        [XmlElement("accessibilityLevel")]
        [DefaultValue(Common.AccessibilityLevels.Public)]
        public AccessibilityLevels AccessibilityLevel { get; set; } = AccessibilityLevels.Public;

        #endregion

        // --------------------------------------------------
        // CONSTRUCTORS
        // --------------------------------------------------

        #region Constructors

        /// <summary>
        /// Initializes a new insance of the DataSpecification class.
        /// </summary>
        protected DataSpecification()
        {
        }

        /// <summary>
        /// Initializes a new insance of the DataSpecification class.
        /// </summary>
        /// <param name="accessibilityLevel">The accessibilty level of this instance.</param>
        /// <param name="specificationLevels">The specification levels of this instance.</param>
        protected DataSpecification(
            AccessibilityLevels accessibilityLevel = AccessibilityLevels.Public,
            SpecificationLevels[] specificationLevels = null)
        {
            AccessibilityLevel = accessibilityLevel;
            _specificationLevels = specificationLevels?.ToList() ?? new List<SpecificationLevels>() { Common.SpecificationLevels.All };
        }

        #endregion

        // --------------------------------------------------
        // ACCESSORS
        // --------------------------------------------------

        #region Accessors

        /// <summary>
        /// Indicates whether this instance is compatible with the specified data item.
        /// </summary>
        /// <param name="item">The data item to consider.</param>
        /// <returns>True if this instance is compatible with the specified data item.</returns>
        public virtual bool IsCompatibleWith(IDataItem item)
        {
            if (item == null)
                return true;

            return true;
        }

        #endregion

        // --------------------------------------------------
        // UPDATE, CHECK, REPAIR
        // --------------------------------------------------

        #region Update_Check_Repair
        /// <summary>
        /// Update this instance with the specified collections.
        /// </summary>
        /// <param name="referenceSpecification">The reference specification to consider.</param>
        /// <param name="specificationAreas">The specification areas to consider.</param>
        /// <returns>Returns the log of the operation.</returns>
        /// <remarks>Put reference collections as null if you do not want to repair this instance.</remarks>
        public virtual IBdoLog Update(
            IDataElementSpec referenceSpecification = null,
            string[] specificationAreas = null)
        {
            return new BdoLog();
        }

        /// <summary>
        /// Check this instance with the specified collections on the specific element areas.
        /// </summary>
        /// <param name="referenceSpecification">The reference specification to consider.</param>
        /// <returns>Returns the log of the operation.</returns>
        public virtual IBdoLog Check(
            IDataSpecification referenceSpecification = null)
        {
            IBdoLog log = new BdoLog();

            if (referenceSpecification == null)
                return log;

            return log;
        }

        /// <summary>
        /// Update this instance with the specified collections.
        /// </summary>
        /// <param name="referenceSpecification">The reference specification to consider.</param>
        /// <returns>Returns the log of the operation.</returns>
        public virtual IBdoLog Repair(
            IDataSpecification referenceSpecification = null)
        {
            IBdoLog log = new BdoLog();

            return log;
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
            DataSpecification specification = base.Clone() as DataSpecification;
            return specification;
        }

        #endregion
    }
}
