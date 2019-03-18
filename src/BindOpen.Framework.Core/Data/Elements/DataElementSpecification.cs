using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Xml.Serialization;
using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.Data.Common;
using BindOpen.Framework.Core.Data.Elements.Carrier;
using BindOpen.Framework.Core.Data.Elements.Document;
using BindOpen.Framework.Core.Data.Elements.Entity;
using BindOpen.Framework.Core.Data.Elements.Scalar;
using BindOpen.Framework.Core.Data.Elements.Schema;
using BindOpen.Framework.Core.Data.Elements.Sets;
using BindOpen.Framework.Core.Data.Elements.Source;
using BindOpen.Framework.Core.Data.Helpers.Objects;
using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.Data.Specification;
using BindOpen.Framework.Core.Data.Specification.Constraints;
using BindOpen.Framework.Core.Data.Specification.Design;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Core.System.Scripting;

namespace BindOpen.Framework.Core.Data.Elements
{
    /// <summary>
    /// This class represents a data element specification.
    /// </summary>
    [Serializable()]
    [XmlType("DataElementSpecification", Namespace = "http://meltingsoft.com/bindopen/xsd")]
    [XmlRoot(ElementName = "specification", Namespace = "http://meltingsoft.com/bindopen/xsd", IsNullable = false)]
    [XmlInclude(typeof(CarrierElementSpecification))]
    [XmlInclude(typeof(DocumentElementSpecification))]
    [XmlInclude(typeof(EntityElementSpecification))]
    [XmlInclude(typeof(ScalarElementSpecification))]
    [XmlInclude(typeof(SchemaElementSpecification))]
    [XmlInclude(typeof(SourceElementSpecification))]
    public abstract class DataElementSpecification : DataSpecification
    {
        // --------------------------------------------------
        // CONSTANTS
        // --------------------------------------------------

        #region Constants

        /// <summary>
        /// Names of the attribute areas of the DataElementSpecification class.
        /// </summary>
        public static List<String> __Arenames = new List<String>()
        {
            DataAreaKind.Design.ToString(), 
            DataAreaKind.Constraints.ToString(),
            DataAreaKind.Properties.ToString(),
            DataElementAreaKind.Element.ToString(),
            DataAreaKind.Items.ToString()
        };

        #endregion

        // --------------------------------------------------
        // VARIABLES
        // --------------------------------------------------

        #region Variables

        // General ------------------------------------------

        private String _GroupId = null;
        private List<String> _Aliases = null;
        private DataValueType _ValueType = DataValueType.Any;
        private Boolean _IsAllocatable = false;
        private List<DataAreaSpecification> _AreaSpecifications = null;

        // Items ------------------------------

        private List<DataItemizationMode> _AvailableItemizationModes = null;
        private int _MinimumItemNumber = 1;
        private int _MaximumItemNumber = -1;

        private List<Object> _DefaultItems = null;
        private List<String> _DefaultStringItems = null;

        private List<SpecificationLevel> _ItemSpecificationLevels = null;

        // Constraints ------------------------

        private DataConstraintStatement _ConstraintStatement = null;

        // Properties ---------------------------------------

        private DataElementSpecificationSet _DetailSpecification = null;

        // Design -----------------------------

        private DataElementDesignStatement _DesignStatement = null;

        #endregion

        // --------------------------------------------------
        // PROPERTIES
        // --------------------------------------------------

        #region Properties

        // General ------------------------------------------

        /// <summary>
        /// ID of the group of this instance.
        /// </summary>
        [XmlElement("groupId")]
        public String GroupId
        {
            get { return this._GroupId ?? ""; }
            set { this._GroupId = value; }
        }

        /// <summary>
        /// Specification of the GroupId property of this instance.
        /// </summary>
        [XmlIgnore()]
        public Boolean GroupIdSpecified
        {
            get
            {
                return !String.IsNullOrEmpty(this._GroupId);
            }
        }

        /// <summary>
        /// The value type of this instance.
        /// </summary>
        [XmlIgnore()]
        public DataValueType ValueType
        {
            get { return this._ValueType; }
            set { this._ValueType = value; }
        }

        /// <summary>
        /// The aliases of the entry.
        /// </summary>
        [XmlArray("aliases")]
        [XmlArrayItem("alias")]
        public List<String> Aliases
        {
            get
            {
                if (this._Aliases == null) this._Aliases = new List<string>();
                return this._Aliases;
            }
            set
            {
                this._Aliases = value;
            }
        }

        /// <summary>
        /// Specification of the Aliases property of this instance.
        /// </summary>
        [XmlIgnore()]
        public Boolean AliasesSpecified
        {
            get
            {
                return this._Aliases != null && this._Aliases.Count > 1;
            }
        }

        /// <summary>
        /// The area specifications of this instance.
        /// </summary>
        [XmlElement("areaSpecifications")]
        public List<DataAreaSpecification> AreaSpecifications
        {
            get
            {
                 if (this._AreaSpecifications ==null) this._AreaSpecifications = new List<DataAreaSpecification>();
                return this._AreaSpecifications;
            }
            set
            {
                this._AreaSpecifications = value;
            }
        }

        /// <summary>
        /// Specification of the AreaSpecifications property of this instance.
        /// </summary>
        [XmlIgnore()]
        public Boolean AreaSpecificationsSpecified
        {
            get
            {
                return this._AreaSpecifications != null && this._AreaSpecifications.Count > 1;
            }
        }

        /// <summary>
        /// Indicates whether the instance can be allocated.
        /// </summary>
        [XmlElement("isAllocatable")]
        [DefaultValue(false)]
        public Boolean IsAllocatable
        {
            get
            {
                return this._IsAllocatable;
            }
            set { this._IsAllocatable = value; }
        }

        /// <summary>
        /// Specification of the IsAllocatable property of this instance.
        /// </summary>
        [XmlIgnore()]
        public Boolean IsAllocatableSpecified
        {
            get
            {
                return this.IsAllocatable;
            }
        }

        // Items ---------------------------------

        /// <summary>
        /// The available itemization modes of this instance.
        /// </summary>
        [XmlArray("availableItemizationModes")]
        [XmlArrayItem("add.mode")]
        public List<DataItemizationMode> AvailableItemizationModes
        {
            get {
                if (this._AvailableItemizationModes == null) this._AvailableItemizationModes = new List<DataItemizationMode>();
                return this._AvailableItemizationModes;
            }
            set
            {
                if ((value == null) || (value.Count==0) || (value.Contains(DataItemizationMode.Any)))
                    this._AvailableItemizationModes = new List<DataItemizationMode>() { DataItemizationMode.Any };
                else 
                    this._AvailableItemizationModes = value;
            }
        }

        /// <summary>
        /// Specification of the AvailableItemizationModes property of this instance.
        /// </summary>
        [XmlIgnore()]
        public Boolean AvailableItemizationModesSpecified
        {
            get
            {
                return this._AvailableItemizationModes !=null && this._AvailableItemizationModes.Count>0;
            }
        }

        /// <summary>
        /// Default string items of this instance.
        /// </summary>
        [XmlArray("defaultStringItems")]
        [XmlArrayItem("item")]
        public List<String> DefaultStringItems
        {
            get {
                 if (this._DefaultStringItems ==null) this._DefaultStringItems = new List<string>();
                return this._DefaultStringItems;
            }
            set { this._DefaultStringItems = value; }
        }

        /// <summary>
        /// Specification of the DefaultStringItems property of this instance.
        /// </summary>
        [XmlIgnore()]
        public Boolean DefaultStringItemsSpecified
        {
            get
            {
                return this._DefaultStringItems !=null && this._DefaultStringItems.Count>0;
            }
        }

        /// <summary>
        /// Default items of this instance.
        /// </summary>
        [XmlIgnore()]
        public List<Object> DefaultItems
        {
            get {
                if (this._DefaultItems == null) this._DefaultItems = new List<object>();
                return this._DefaultItems;
            }
            set { this._DefaultItems = value; }
        }

        /// <summary>
        /// Minimum item number of this instance.
        /// </summary>
        [XmlElement("minimumItemNumber")]
        [DefaultValue(1)]
        public int MinimumItemNumber
        {
            get { return this._MinimumItemNumber; }
            set { this._MinimumItemNumber = value; }
        }

        /// <summary>
        /// Maximum item number of this instance.
        /// </summary>
        [XmlElement("maximumItemNumber")]
        [DefaultValue(-1)]
        public int MaximumItemNumber
        {
            get { return this._MaximumItemNumber; }
            set { this._MaximumItemNumber = value; }
        }

        /// <summary>
        /// Indicates whether the value of this instance is a list.
        /// </summary>
        [XmlIgnore()]
        public Boolean IsValueList
        {
            get
            {
                return (this._MaximumItemNumber == -1) | (this._MaximumItemNumber > 1);
            }
        }

        /// <summary>
        /// The item requirement level of this instance.
        /// </summary>
        [XmlIgnore()]
        public RequirementLevel ItemRequirementLevel
        {
            get
            {
                RequirementLevel aItemRequirementLevel = RequirementLevel.None;
                if (this._MaximumItemNumber == 0)
                    aItemRequirementLevel = RequirementLevel.Forbidden;
                else if (this._MinimumItemNumber > 0)
                    aItemRequirementLevel = RequirementLevel.Required;
                else if (this._MinimumItemNumber <= 0)
                    aItemRequirementLevel = RequirementLevel.Optional;
                else
                    aItemRequirementLevel = RequirementLevel.None;
                return aItemRequirementLevel;
            }
        }

        /// <summary>
        /// Levels of specification of this instance.
        /// </summary>
        [XmlArray("itemSpecificationLevels")]
        [XmlArrayItem("itemSpecificationLevel")]
        public List<SpecificationLevel> ItemSpecificationLevels
        {
            get
            {
                if (this._ItemSpecificationLevels ==null) this._ItemSpecificationLevels  = new List<SpecificationLevel>();
                return this._ItemSpecificationLevels;
            }
            set { this._ItemSpecificationLevels = value; }
        }

        /// <summary>
        /// Specification of the SpecificationLevels property of this instance.
        /// </summary>
        [XmlIgnore()]
        public Boolean ItemSpecificationLevelsSpecified
        {
            get
            {
                return this._ItemSpecificationLevels != null && this._ItemSpecificationLevels.Count > 0 && !this._ItemSpecificationLevels.Contains(SpecificationLevel.All);
            }
        }

        // Constraints ---------------------------

        /// <summary>
        /// Constraint statement of this instance.
        /// </summary>
        [XmlElement("constraint.statement")]
        public DataConstraintStatement ConstraintStatement
        {
            get {
                if (this._ConstraintStatement == null) this._ConstraintStatement = new DataConstraintStatement();
                return this._ConstraintStatement;
            }
            set { this._ConstraintStatement = value; }
        }

        /// <summary>
        /// Specification of the ConstraintStatement property of this instance.
        /// </summary>
        [XmlIgnore()]
        public Boolean ConstraintStatementSpecified
        {
            get
            {
                return this._ConstraintStatement!=null && this._ConstraintStatement.Count >0;
            }
        }

        // Detail ---------------------------------

        /// <summary>
        /// Detail specification of this instance.
        /// </summary>
        [XmlElement("detail.specification")]
        public DataElementSpecificationSet DetailSpecification
        {
            get
            {
                if (this._DetailSpecification == null) this._DetailSpecification = new DataElementSpecificationSet();
                return this._DetailSpecification;
            }
            set { this._DetailSpecification = value; }
        }

        /// <summary>
        /// Specification of the DetailSpecification property of this instance.
        /// </summary>
        [XmlIgnore()]
        public Boolean DetailSpecificationSpecified
        {
            get
            {
                return this._DetailSpecification != null && this._DetailSpecification.Count > 0;
            }
        }

        // Design ----------------------------------

        /// <summary>
        /// Design statement of this instance.
        /// </summary>
        [XmlElement("design.statement")]
        public DataElementDesignStatement DesignStatement
        {
            get
            {
                if (this._DesignStatement == null) this._DesignStatement = new DataElementDesignStatement();
                return this._DesignStatement;
            }
            set { this._DesignStatement = value; }
        }

        /// <summary>
        /// Specification of the DesignStatement property of this instance.
        /// </summary>
        [XmlIgnore()]
        public Boolean DesignStatementSpecified
        {
            get
            {
                return this._DesignStatement!=null;
            }
        }

        #endregion

        // --------------------------------------------------
        // CONSTRUCTORS
        // --------------------------------------------------

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the DataElementSpecification class.
        /// </summary>
        protected DataElementSpecification() : this(AccessibilityLevel.Public)
        {
        }

        /// <summary>
        /// Initializes a new instance of the DataElementSpecification class.
        /// </summary>
        /// <param name="accessibilityLevel">The accessibilty level of this instance.</param>
        /// <param name="specificationLevels">The specification levels of this instance.</param>
        protected DataElementSpecification(
            AccessibilityLevel accessibilityLevel = AccessibilityLevel.Public,
            List<SpecificationLevel> specificationLevels = null) : base(accessibilityLevel, specificationLevels)
        {
            // we update the area specifications
            this.Repair();
        }

        #endregion

        // --------------------------------------------------
        // UPDATORS
        // --------------------------------------------------

        #region Updators

        /// <summary>
        /// Adds the specified default item of this instance.
        /// </summary>
        /// <remarks>Items of this instance must be allowed and must not be forbidden. Otherwise, the values will be the default ones..</remarks>
        /// <param name="aItem">The default item of this instance.</param>
        /// <returns>Returns True if the default item has well been Added.</returns>
        public Boolean AddDefaultItem(Object aItem)
        {
            if (!this.ConstraintStatement.CheckItem(aItem, null, false).HasErrorsOrExceptions())
            {
                this.DefaultItems.Add(aItem);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Sets the specified default item of this instance.
        /// </summary>
        /// <remarks>Items of this instance must be allowed and must not be forbidden. Otherwise, the values will be the default ones..</remarks>
        /// <param name="aItem">The default item of this instance.</param>
        /// <returns>Returns True if the default item has well been set.</returns>
        public Boolean SetDefaultItem(Object aItem)
        {
            this.DefaultItems.Clear();
            return this.AddDefaultItem(aItem);
        }

        /// <summary>
        /// Sets the specified default items of this instance.
        /// </summary>
        /// <remarks>Items of this instance must be allowed and must not be forbidden. Otherwise, the values will be the default ones..</remarks>
        /// <param name="defaultItems">The default items of this instance.</param>
        /// <returns>Returns True if the default items has well been set.</returns>
        public Boolean SetDefaultItem(List<Object> defaultItems)
        {
            Boolean aHasBeenSet = true;

            this.DefaultItems.Clear();
            if ((aHasBeenSet = (defaultItems==null || defaultItems.Count==0)) == true)
                foreach(Object aItem in defaultItems)
                    aHasBeenSet &= this.AddDefaultItem(aItem);

            return aHasBeenSet;
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
        public virtual DataElement NewElement(IAppScope appScope = null, DataElementSet detail = null)
        {
            return null;
        }

        /// <summary>
        /// Gets the area specification with the specified area name.
        /// </summary>
        /// <param name="areaName">The data specification to consider.</param>
        /// <returns>Returns the area specification.</returns>
        public DataAreaSpecification GetAreaSpecification(String areaName)
        {
            DataAreaSpecification areaSpecification = this._AreaSpecifications.FirstOrDefault(p=>p.AreaName.KeyEquals(areaName));

            return (areaSpecification !=null ? areaSpecification : new DataAreaSpecification() { AreaName=areaName });
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
        public virtual Log CheckItem(
            Object item,
            DataElement dataElement = null,
            IAppScope appScope = null,
            ScriptVariableSet scriptVariableSet = null)
        {
            Log log = new Log();
            if (item != null)
            {
                if (this._ConstraintStatement != null)
                {
                    log = this._ConstraintStatement.CheckItem(item, dataElement, true, appScope, scriptVariableSet);
                }
            }

            return log;
        }

        /// <summary>
        /// Check the specified item.
        /// </summary>
        /// <param name="dataElement">The element to consider.</param>
        /// <param name="specificationAreas">The specification areas to consider.</param>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to use.</param>
        /// <returns>The log of check log.</returns>
        public virtual Log CheckElement(
            DataElement dataElement,
            List<String> specificationAreas = null,
            IAppScope appScope = null,
            ScriptVariableSet scriptVariableSet = null)
        {
            Log log = new Log();

            if (dataElement == null)
                return log;

            if (specificationAreas == null)
                specificationAreas = new List<String>() { DataAreaKind.Any.ToString() };

            if (specificationAreas.Contains(DataAreaKind.Any.ToString()) || specificationAreas.Contains("element"))
            {
                if (!this._AvailableItemizationModes.Contains(DataItemizationMode.Any) && !this._AvailableItemizationModes.Contains(dataElement.ItemizationMode))
                {
                    log.AddError(
                        title: "Itemization mode not available",
                        description: "The itemization mode of this element is not available.");
                }
                else
                {
                    switch (dataElement.ItemizationMode)
                    {
                        case DataItemizationMode.Referenced:
                            if (dataElement.ItemReference == null)
                            {
                                log.AddWarning(
                                   title: "Item reference missing in element",
                                   description: "This element has no item reference where as it is in reference itemization mode.");
                            }

                            switch (this.GetAreaSpecification("item").RequirementLevel)
                            {
                                case RequirementLevel.OptionalExclusively:
                                    if ((String.IsNullOrEmpty(dataElement.ItemScript)) || (dataElement.Items.Count > 0))
                                        log.AddError(
                                            title: "Item script and items forbidden with reference",
                                            description: "No item reference.");
                                    break;
                                case RequirementLevel.Forbidden:
                                    if (dataElement.ItemReference != null)
                                        log.AddWarning(
                                            title: "Item reference forbidden",
                                            description: "No item reference.");
                                    break;
                                case RequirementLevel.Required:
                                    if (dataElement.ItemReference == null)
                                        log.AddError(
                                            title: "Item reference required",
                                            description: "The element requires a item reference.");
                                    break;
                            }
                            break;
                        case DataItemizationMode.Script:
                            if (String.IsNullOrEmpty(dataElement.ItemScript))
                                log.AddWarning(
                                    title: "Item script missing in element",
                                    description: "The element has no item script where as it is in script itemization mode.");

                            switch (this.GetAreaSpecification("item").RequirementLevel)
                            {
                                case RequirementLevel.OptionalExclusively:
                                    if ((String.IsNullOrEmpty(dataElement.ItemScript)) || (dataElement.Items.Count > 0))
                                        log.AddError(
                                            title: "Item script and items forbidden forbidden with reference",
                                            description: "No item reference.");
                                    break;
                                case RequirementLevel.Forbidden:
                                    if (dataElement.ItemReference != null)
                                        log.AddWarning(
                                            title: "Item reference forbidden",
                                            description: "No item reference.");
                                    break;
                                case RequirementLevel.Required:
                                    if (dataElement.ItemReference == null)
                                        log.AddError(
                                            title: "Item reference required",
                                            description: "The element requires a item reference.");
                                    break;
                            }
                            break;
                        case DataItemizationMode.Valued:
                            if ((!this.IsValueList) && (dataElement.Items.Count > 1))
                                log.AddWarning(
                                    title: "More than one item found in element",
                                    description: "The element has more than one item where as it is in single itemization mode.");

                            switch (this.GetAreaSpecification("item").RequirementLevel)
                            {
                                case RequirementLevel.OptionalExclusively:
                                    if ((String.IsNullOrEmpty(dataElement.ItemScript)) || (dataElement.Items.Count > 0))
                                        log.AddError(
                                            title: "Item script and items forbidden forbidden with reference",
                                            description: "No item reference.");
                                    break;
                                case RequirementLevel.Forbidden:
                                    if (dataElement.ItemReference != null)
                                        log.AddWarning(
                                            title: "Item specification allows reference forbidden",
                                            description: "No item reference.");
                                    break;
                                case RequirementLevel.Required:
                                    if ((dataElement.Items == null) || (dataElement.Items.Count == 0))
                                        log.AddError(
                                            title: "Items required",
                                            description: "The element requires items.");
                                    break;
                            }
                            break;
                    }
                }
            }

            if (specificationAreas.Contains(DataAreaKind.Any.ToString()) || specificationAreas.Contains(DataAreaKind.Items.ToString()))
            {
                if (this.IsValueList)
                {
                    if (this._MinimumItemNumber > dataElement.Items.Count)
                        log.AddError(
                            title: "Not enough items in element",
                            description: "The element has " + dataElement.Items.Count + " items where as the minimum was specified at " + this._MinimumItemNumber + ".");

                    if (this._MaximumItemNumber > -1 && this._MaximumItemNumber < dataElement.Items.Count)
                        log.AddError(
                            title: "Too many items in element",
                            description: "The element has " + dataElement.Items.Count + " items where as the maximum was specified at " + this._MaximumItemNumber + ".");
                }

                foreach (Object aItem in dataElement.Items)
                    log.AddEvents(this.CheckItem(aItem, dataElement, appScope, scriptVariableSet));
            }

            return log;
        }

        #endregion

        // --------------------------------------------------
        // UPDATE, CHECK, REPAIR
        // --------------------------------------------------

        #region Update_Check_Repair
        
        /// <summary>
        /// Repairs this instance with the specified definition.
        /// </summary>
        /// <param name="item">The item to consider.</param>
        /// <param name="specificationAreas">The specification areas to consider.</param>
        /// <param name="updateModes">The update modes to consider.</param>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to use.</param>
        /// <returns>Log of the operation.</returns>
        public override Log Repair<T>(
            T item = null,
            List<String> specificationAreas = null,
            List<UpdateMode> updateModes = null,
            IAppScope appScope = null,
            ScriptVariableSet scriptVariableSet = null)
        {
            Log log = new Log();

            if (this._MinimumItemNumber < 0) this._MinimumItemNumber = 0;
            if (this._MaximumItemNumber < 0) this._MaximumItemNumber = -1;
            if (this._MaximumItemNumber > -1 && this._MaximumItemNumber < this._MinimumItemNumber) this._MaximumItemNumber = this._MinimumItemNumber;

            // we update the area specifications
            this._AreaSpecifications.RemoveAll(p =>
                !CarrierElementSpecification.__Arenames.Any(q => q.KeyEquals(p.AreaName)));
            foreach (String arename in CarrierElementSpecification.__Arenames)
                if (this._AreaSpecifications.Any(p => p.AreaName.KeyEquals(arename)))
                    this._AreaSpecifications.Add(new DataAreaSpecification(arename));

            if (this._AvailableItemizationModes == null || this._AvailableItemizationModes.Count == 0)
                this._AvailableItemizationModes = new List<DataItemizationMode>() { DataItemizationMode.Valued };

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
            DataElementSpecification dataElementSpecification = base.Clone() as DataElementSpecification;

            if (this._Aliases != null)
                dataElementSpecification.Aliases = new List<String>(this._Aliases);
            if (this.ConstraintStatement != null)
                dataElementSpecification.ConstraintStatement = this.ConstraintStatement.Clone() as DataConstraintStatement;
            if (this.DesignStatement != null)
                dataElementSpecification.DesignStatement = this.DesignStatement.Clone() as DataElementDesignStatement;
            return dataElementSpecification;
        }

        #endregion
    }

}
