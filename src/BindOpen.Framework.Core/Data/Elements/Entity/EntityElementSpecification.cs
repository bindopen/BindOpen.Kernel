using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.Data.Common;
using BindOpen.Framework.Core.Data.Elements.Sets;
using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.Data.Specification.Filters;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Core.System.Scripting;

namespace BindOpen.Framework.Core.Data.Elements.Entity
{

    /// <summary>
    /// This class represents an entity element specification.
    /// </summary>
    [Serializable()]
    [XmlType("EntityElementSpecification", Namespace = "http://meltingsoft.com/bindopen/xsd")]
    [XmlRoot(ElementName = "specification", Namespace = "http://meltingsoft.com/bindopen/xsd", IsNullable = false)]
    public class EntityElementSpecification : DataElementSpecification
    {

        // --------------------------------------------------
        // VARIABLES
        // --------------------------------------------------

        #region Variables

        // Entity ----------------------------------

        private DataValueFilter _EntityFilter = null;
        //private RequirementLevel _EntityRequirementLevel = RequirementLevel.None;
        //private List<SpecificationLevel> _EntitySpecificationLevels = new List<SpecificationLevel>();

        //// Format ----------------------------------

        //private DataValueFilter _FormatUniqueNameFilter = new DataValueFilter();
        //private RequirementLevel _FormatRequirementLevel = RequirementLevel.Optional;
        //private List<SpecificationLevel> _FormatSpecificationLevels = new List<SpecificationLevel>();

        #endregion


        // --------------------------------------------------
        // PROPERTIES
        // --------------------------------------------------

        #region Properties

        // Entity ----------------------------------

        /// <summary>
        /// The entity filter of this instance.
        /// </summary>
        [XmlElement("entity.filter")]
        public DataValueFilter EntityFilter
        {
            get {
                //if (this._EntityFilter == null) this._EntityFilter = new DataValueFilter();
                return this._EntityFilter;
            }
            set { this._EntityFilter = value; }
        }

        /// <summary>
        /// Specification of the ClassFilter property of this instance.
        /// </summary>
        [XmlIgnore()]
        public Boolean ClassFilterSpecified
        {
            get
            {
                return this._EntityFilter != null && 
                    (this._EntityFilter.AddedValues==null || this._EntityFilter.AddedValues.Count > 0) &&
                    (this._EntityFilter.RemovedValues == null || this._EntityFilter.RemovedValues.Count > 0);
            }
        }

        ///// <summary>
        ///// Entity requirement level of this instance.
        ///// </summary>
        //[XmlElement("entityRequirementLevel")]
        //public RequirementLevel EntityRequirementLevel
        //{
        //    get
        //    {
        //        return this._EntityRequirementLevel;
        //    }
        //    set { this._EntityRequirementLevel = value; }
        //}

        ///// <summary>
        ///// The specification levels for entity specification of this instance.
        ///// </summary>
        //[XmlArray("entitySpecificationLevels")]
        //[XmlArrayItem("add.level")]
        //public List<SpecificationLevel> EntitySpecificationLevels
        //{
        //    get { return this._EntitySpecificationLevels; }
        //    set { this._EntitySpecificationLevels = value; }
        //}

        //// Format ----------------------------------

        ///// <summary>
        ///// The format unique name filter of this instance.
        ///// </summary>
        //[XmlElement("formatUniqueNameFilter")]
        //public DataValueFilter FormatUniqueNameFilter
        //{
        //    get { return this._FormatUniqueNameFilter; }
        //    set { this._FormatUniqueNameFilter = value; }
        //}

        ///// <summary>
        ///// Format requirement level of this instance.
        ///// </summary>
        //[XmlElement("formatRequirementLevel")]
        //public RequirementLevel FormatRequirementLevel
        //{
        //    get
        //    {
        //        return this._FormatRequirementLevel;
        //    }
        //    set { this._FormatRequirementLevel = value; }
        //}

        ///// <summary>
        ///// The specification levels for format specification of this instance.
        ///// </summary>
        //[XmlArray("formatSpecificationLevels")]
        //[XmlArrayItem("add.level")]
        //public List<SpecificationLevel> FormatSpecificationLevels
        //{
        //    get { return this._FormatSpecificationLevels; }
        //    set { this._FormatSpecificationLevels = value; }
        //}

        #endregion


        // --------------------------------------------------
        // CONSTRUCTORS
        // --------------------------------------------------

        #region Constructors

        /// <summary>
        /// Initializes a new entity element specification.
        /// </summary>
        public EntityElementSpecification() : base()
        {
        }

        /// <summary>
        /// Initializes a new entity element specification.
        /// </summary>
        /// <param name="accessibilityLevel">The accessibilty level of this instance.</param>
        /// <param name="specificationLevels">The specification levels of this instance.</param>
        public EntityElementSpecification(
            AccessibilityLevel accessibilityLevel = AccessibilityLevel.Public,
            List<SpecificationLevel> specificationLevels = null)
            : base(accessibilityLevel, specificationLevels)
        {
        }

        #endregion


        // --------------------------------------------------
        // ACCESSORS
        // --------------------------------------------------

        #region Accessors

        /// <summary>
        /// Creates a new data element respecting this instance.
        /// </summary>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="detail">The detail to consider.</param>
        /// <returns>Returns a new data element respecting this instance.</returns>
        public override DataElement NewElement(IAppScope appScope = null, DataElementSet detail = null)
        {
            return new EntityElement(this.Name);
        }

        /// <summary>
        /// Indicates whether this instance is compatible with the specified data item.
        /// </summary>
        /// <param name="item">The data item to consider.</param>
        /// <returns>True if this instance is compatible with the specified data item.</returns>
        public override Boolean IsCompatibleWith(DataItem item)
        {
            Boolean isCompatible = base.IsCompatibleWith(item);

            if (isCompatible)
            {

            }

            return isCompatible;
        }


        /// <summary>
        /// Check the specified item.
        /// </summary>
        /// <param name="item">The item to consider.</param>
        /// <param name="dataElement">The element to consider.</param>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to use.</param>
        /// <returns>The log of check log.</returns>
        public override Log CheckItem(
            Object item,
            DataElement dataElement = null,
            IAppScope appScope = null,
            ScriptVariableSet scriptVariableSet = null)
        {
            return new Log();
        }

        /// <summary>
        /// Check the specified item.
        /// </summary>
        /// <param name="dataElement">The element to consider.</param>
        /// <param name="specificationAreas">The specification areas to consider.</param>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to use.</param>
        /// <returns>The log of check log.</returns>
        public override Log CheckElement(
            DataElement dataElement,
            List<String> specificationAreas = null,
            IAppScope appScope = null,
            ScriptVariableSet scriptVariableSet = null)
        {
            // we check that the entity unique name is available
            // we check that the format unique name is available
            // we check that the schema unique name is available

            return new Log();
        }

        #endregion


        // --------------------------------------------------
        // UPDATE, CHECK, REPAIR
        // --------------------------------------------------

        #region Update_Check_Repair


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
            EntityElementSpecification specification = base.Clone() as EntityElementSpecification;
            if (this.EntityFilter != null)
                specification.EntityFilter = this.EntityFilter.Clone() as DataValueFilter;
            //if (this.FormatUniqueNameFilter != null)
            //    entityElementSpecification.FormatUniqueNameFilter = this.FormatUniqueNameFilter.Clone() as DataValueFilter;
            return specification;
        }

        #endregion

    }

}
