using BindOpen.Framework.Core.Data.Common;
using BindOpen.Framework.Core.Data.Elements;
using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.System.Diagnostics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;

namespace BindOpen.Framework.Core.Data.Specification
{

    /// <summary>
    /// This abstract class represents a data specification.
    /// </summary>
    [Serializable()]
    [XmlType("DataSpecification", Namespace = "http://meltingsoft.com/bindopen/xsd")]
    [XmlRoot(ElementName = "specification", Namespace = "http://meltingsoft.com/bindopen/xsd", IsNullable = false)]
    public abstract class DataSpecification : IndexedDataItem
    {

        // --------------------------------------------------
        // VARIABLES
        // --------------------------------------------------

        #region Variables

        private RequirementLevel _RequirementLevel = RequirementLevel.None;
        private String _RequirementScript = null;
        private Common.InheritanceLevel _InheritanceLevel = Common.InheritanceLevel.None;
        private List<SpecificationLevel> _SpecificationLevels = null;
        private AccessibilityLevel _AccessibilityLevel = AccessibilityLevel.Public;

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
        public RequirementLevel RequirementLevel
        {
            get
            {
                return this._RequirementLevel;
            }
            set { this._RequirementLevel = value; }
        }

        /// <summary>
        /// The requirement script of this instance.
        /// </summary>
        [XmlElement("requirementScript")]
        public String RequirementScript
        {
            get
            {
                return this._RequirementScript;
            }
            set { this._RequirementScript = value; }
        }

        /// <summary>
        /// Specification of the RequirementScript property of this instance.
        /// </summary>
        [XmlIgnore()]
        public Boolean RequirementScriptSpecified
        {
            get
            {
                return !String.IsNullOrEmpty(this.RequirementScript);
            }
        }

        /// <summary>
        /// The level of inheritance of this instance.
        /// </summary>
        [XmlElement("inheritanceLevel")]
        [DefaultValue(Common.InheritanceLevel.None)]
        public Common.InheritanceLevel InheritanceLevel
        {
            get
            {
                return this._InheritanceLevel;
            }
            set { this._InheritanceLevel = value; }
        }

        /// <summary>
        /// Levels of specification of this instance.
        /// </summary>
        [XmlArray("specificationLevels")]
        [XmlArrayItem("specificationLevel")]
        public List<SpecificationLevel> SpecificationLevels
        {
            get
            {
                if (this._SpecificationLevels == null) this._SpecificationLevels = new List<SpecificationLevel>();
                return this._SpecificationLevels;
            }
            set { this._SpecificationLevels = value; }
        }

        /// <summary>
        /// Specification of the SpecificationLevels property of this instance.
        /// </summary>
        [XmlIgnore()]
        public Boolean SpecificationLevelsSpecified
        {
            get
            {
                return this._SpecificationLevels != null && this._SpecificationLevels.Count > 0 && !this._SpecificationLevels.Contains(SpecificationLevel.All);
            }
        }

        /// <summary>
        /// Level of accessibility of this instance.
        /// </summary>
        [XmlElement("accessibilityLevel")]
        [DefaultValue(Common.AccessibilityLevel.Public)]
        public AccessibilityLevel AccessibilityLevel
        {
            get
            {
                return this._AccessibilityLevel;
            }
            set { this._AccessibilityLevel = value; }
        }

        #endregion


        // --------------------------------------------------
        // CONSTRUCTORS
        // --------------------------------------------------

        #region Constructors

        /// <summary>
        /// Initializes a new data specification.
        /// </summary>
        public DataSpecification()
        {
        }

        /// <summary>
        /// Initializes a new data element specification.
        /// </summary>
        /// <param name="accessibilityLevel">The accessibilty level of this instance.</param>
        /// <param name="specificationLevels">The specification levels of this instance.</param>
        public DataSpecification(
            AccessibilityLevel accessibilityLevel = AccessibilityLevel.Public,
            List<SpecificationLevel> specificationLevels = null)
        {
            this._AccessibilityLevel = accessibilityLevel;
            this._SpecificationLevels = (specificationLevels ?? new List<SpecificationLevel>() { SpecificationLevel.All }); 
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
        public virtual Boolean IsCompatibleWith(DataItem item)
        {
            if (item == null)
                return true;

            //private RequirementLevel _RequirementLevel = RequirementLevel..None;
            //private InheritanceLevel _InheritanceLevel = InheritanceLevel.None;
            //private List<SpecificationLevel> _SpecificationLevels = new List<SpecificationLevel>();
            //private AccessibilityLevel _AccessibilityLevel = AccessibilityLevel.Public;

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
        public virtual Log Update(
            DataElementSpec referenceSpecification = null,
            List<String> specificationAreas = null)
        {
            return new Log();
        }

        /// <summary>
        /// Check this instance with the specified collections on the specific element areas.
        /// </summary>
        /// <param name="referenceSpecification">The reference specification to consider.</param>
        /// <returns>Returns the log of the operation.</returns>
        public virtual Log Check(
            DataSpecification referenceSpecification = null)
        {
            Log log = new Log();

            if (referenceSpecification == null)
                return log;


            return log;
        }

        /// <summary>
        /// Update this instance with the specified collections.
        /// </summary>
        /// <param name="referenceSpecification">The reference specification to consider.</param>
        /// <returns>Returns the log of the operation.</returns>
        public virtual Log Repair(
            DataSpecification referenceSpecification = null)
        {
            Log log = new Log();

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
        public override Object Clone()
        {
            DataSpecification dataSpecification = this.MemberwiseClone() as DataSpecification;
            return dataSpecification;
        }

        #endregion

    }

}
