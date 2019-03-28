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
    [XmlType("DataElementSpec", Namespace = "http://meltingsoft.com/bindopen/xsd")]
    [XmlRoot(ElementName = "specification", Namespace = "http://meltingsoft.com/bindopen/xsd", IsNullable = false)]
    [XmlInclude(typeof(CarrierElementSpec))]
    [XmlInclude(typeof(DocumentElementSpec))]
    [XmlInclude(typeof(EntityElementSpec))]
    [XmlInclude(typeof(ScalarElementSpec))]
    [XmlInclude(typeof(SchemaElementSpec))]
    [XmlInclude(typeof(SourceElementSpec))]
    public abstract class DataElementSpec : DataSpecification
    {
        // --------------------------------------------------
        // CONSTANTS
        // --------------------------------------------------

        #region Constants

        /// <summary>
        /// Names of the attribute areas of the DataElementSpec class.
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

        private String _groupId = null;
        private List<String> _aliases = null;
        private List<DataAreaSpecification> _areaSpecifications = null;

        // Items ------------------------------

        private List<DataItemizationMode> _availableItemizationModes = null;
        private List<Object> _defaultItems = null;
        private List<String> _defaultStringItems = null;

        private List<SpecificationLevel> _itemSpecificationLevels = null;

        // Constraints ------------------------

        private DataConstraintStatement _constraintStatement = null;

        // Properties ---------------------------------------

        private DataElementSpecSet _detailSpecification = null;

        // Design -----------------------------

        private DataElementDesignStatement _designStatement = null;

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
            get => _groupId ?? "";
            set { _groupId = value; }
        }

        /// <summary>
        /// Specification of the GroupId property of this instance.
        /// </summary>
        [XmlIgnore()]
        public Boolean GroupIdSpecified => !string.IsNullOrEmpty(_groupId);

        /// <summary>
        /// The value type of this instance.
        /// </summary>
        [XmlIgnore()]
        public DataValueType ValueType { get; set; } = DataValueType.Any;

        /// <summary>
        /// The aliases of the entry.
        /// </summary>
        [XmlArray("aliases")]
        [XmlArrayItem("alias")]
        public List<String> Aliases
        {
            get => _aliases ?? (_aliases = new List<string>());
            set
            {
                _aliases = value;
            }
        }

        /// <summary>
        /// Specification of the Aliases property of this instance.
        /// </summary>
        [XmlIgnore()]
        public Boolean AliasesSpecified => _aliases?.Count > 1;

        /// <summary>
        /// The area specifications of this instance.
        /// </summary>
        [XmlElement("areaSpecifications")]
        public List<DataAreaSpecification> AreaSpecifications
        {
            get => _areaSpecifications ?? (_areaSpecifications = new List<DataAreaSpecification>());
            set
            {
                _areaSpecifications = value;
            }
        }

        /// <summary>
        /// Specification of the AreaSpecifications property of this instance.
        /// </summary>
        [XmlIgnore()]
        public Boolean AreaSpecificationsSpecified => _areaSpecifications?.Count > 1;

        /// <summary>
        /// Indicates whether the instance can be allocated.
        /// </summary>
        [XmlElement("isAllocatable")]
        [DefaultValue(false)]
        public Boolean IsAllocatable { get; set; } = false;

        /// <summary>
        /// Specification of the IsAllocatable property of this instance.
        /// </summary>
        [XmlIgnore()]
        public Boolean IsAllocatableSpecified => IsAllocatable;

        // Items ---------------------------------

        /// <summary>
        /// The script of this instance.
        /// </summary>
        [XmlAttribute("script")]
        public string ItemScript { get; set; } = null;

        /// <summary>
        /// Specification of the ItemScript property of this instance.
        /// </summary>
        [XmlIgnore()]
        public Boolean ItemScriptSpecified => !string.IsNullOrEmpty(ItemScript);

        /// <summary>
        /// The available itemization modes of this instance.
        /// </summary>
        [XmlArray("availableItemizationModes")]
        [XmlArrayItem("add.mode")]
        public List<DataItemizationMode> AvailableItemizationModes
        {
            get => _availableItemizationModes ?? (_availableItemizationModes = new List<DataItemizationMode>());
            set
            {
                if ((value == null) || (value.Count == 0) || (value.Contains(DataItemizationMode.Any)))
                    _availableItemizationModes = new List<DataItemizationMode>() { DataItemizationMode.Any };
                else
                    _availableItemizationModes = value;
            }
        }

        /// <summary>
        /// Specification of the AvailableItemizationModes property of this instance.
        /// </summary>
        [XmlIgnore()]
        public Boolean AvailableItemizationModesSpecified => _availableItemizationModes?.Count > 0;

        /// <summary>
        /// Default string items of this instance.
        /// </summary>
        [XmlArray("defaultStringItems")]
        [XmlArrayItem("item")]
        public List<String> DefaultStringItems
        {
            get => _defaultStringItems ?? (_defaultStringItems = new List<string>());
            set { _defaultStringItems = value; }
        }

        /// <summary>
        /// Specification of the DefaultStringItems property of this instance.
        /// </summary>
        [XmlIgnore()]
        public Boolean DefaultStringItemsSpecified => _defaultStringItems?.Count > 0;

        /// <summary>
        /// Default items of this instance.
        /// </summary>
        [XmlIgnore()]
        public List<Object> DefaultItems
        {
            get => _defaultItems ?? (_defaultItems = new List<object>());
            set { _defaultItems = value; }
        }

        /// <summary>
        /// Minimum item number of this instance.
        /// </summary>
        [XmlElement("minimumItemNumber")]
        [DefaultValue(1)]
        public int MinimumItemNumber { get; set; } = 1;

        /// <summary>
        /// Maximum item number of this instance.
        /// </summary>
        [XmlElement("maximumItemNumber")]
        [DefaultValue(-1)]
        public int MaximumItemNumber { get; set; } = -1;

        /// <summary>
        /// Indicates whether the value of this instance is a list.
        /// </summary>
        [XmlIgnore()]
        public Boolean IsValueList => (MaximumItemNumber == -1) || (MaximumItemNumber > 1);

        /// <summary>
        /// The item requirement level of this instance.
        /// </summary>
        [XmlIgnore()]
        public RequirementLevel ItemRequirementLevel
        {
            get
            {
                if (MaximumItemNumber == 0)
                {
                    return RequirementLevel.Forbidden;
                }
                else if (MinimumItemNumber > 0)
                {
                    return RequirementLevel.Required;
                }
                else if (MinimumItemNumber <= 0)
                {
                    return RequirementLevel.Optional;
                }
                else
                {
                    return RequirementLevel.None;
                }
            }
        }

        /// <summary>
        /// Levels of specification of this instance.
        /// </summary>
        [XmlArray("itemSpecificationLevels")]
        [XmlArrayItem("itemSpecificationLevel")]
        public List<SpecificationLevel> ItemSpecificationLevels
        {
            get => _itemSpecificationLevels ?? (_itemSpecificationLevels = new List<SpecificationLevel>());
            set { _itemSpecificationLevels = value; }
        }

        /// <summary>
        /// Specification of the SpecificationLevels property of this instance.
        /// </summary>
        [XmlIgnore()]
        public Boolean ItemSpecificationLevelsSpecified => _itemSpecificationLevels?.Count > 0 && !_itemSpecificationLevels.Contains(SpecificationLevel.All);

        // Constraints ---------------------------

        /// <summary>
        /// Constraint statement of this instance.
        /// </summary>
        [XmlElement("constraint.statement")]
        public DataConstraintStatement ConstraintStatement
        {
            get => _constraintStatement ?? (_constraintStatement = new DataConstraintStatement());
            set { _constraintStatement = value; }
        }

        /// <summary>
        /// Specification of the ConstraintStatement property of this instance.
        /// </summary>
        [XmlIgnore()]
        public Boolean ConstraintStatementSpecified => _constraintStatement?.Count > 0;

        // Detail ---------------------------------

        /// <summary>
        /// Detail specification of this instance.
        /// </summary>
        [XmlElement("detail.specification")]
        public DataElementSpecSet DetailSpecification
        {
            get => _detailSpecification ?? (_detailSpecification = new DataElementSpecSet());
            set { _detailSpecification = value; }
        }

        /// <summary>
        /// Specification of the DetailSpecification property of this instance.
        /// </summary>
        [XmlIgnore()]
        public Boolean DetailSpecificationSpecified => _detailSpecification?.Count > 0;

        // Design ----------------------------------

        /// <summary>
        /// Design statement of this instance.
        /// </summary>
        [XmlElement("design.statement")]
        public DataElementDesignStatement DesignStatement
        {
            get => _designStatement ?? (_designStatement = new DataElementDesignStatement());
            set { _designStatement = value; }
        }

        /// <summary>
        /// Specification of the DesignStatement property of this instance.
        /// </summary>
        [XmlIgnore()]
        public Boolean DesignStatementSpecified => _designStatement != null;

        #endregion

        // --------------------------------------------------
        // CONSTRUCTORS
        // --------------------------------------------------

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the DataElementSpec class.
        /// </summary>
        protected DataElementSpec() : this(AccessibilityLevel.Public)
        {
        }

        /// <summary>
        /// Initializes a new instance of the DataElementSpec class.
        /// </summary>
        /// <param name="accessibilityLevel">The accessibilty level of this instance.</param>
        /// <param name="specificationLevels">The specification levels of this instance.</param>
        protected DataElementSpec(
            AccessibilityLevel accessibilityLevel = AccessibilityLevel.Public,
            List<SpecificationLevel> specificationLevels = null) : base(accessibilityLevel, specificationLevels)
        {
            // we update the area specifications
            Repair();
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
            if (!ConstraintStatement.CheckItem(aItem, null, false).HasErrorsOrExceptions())
            {
                DefaultItems.Add(aItem);
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
            DefaultItems.Clear();
            return AddDefaultItem(aItem);
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

            DefaultItems.Clear();
            if ((aHasBeenSet = (defaultItems==null || defaultItems.Count==0)) == true)
            {
                foreach (Object aItem in defaultItems)
                {
                    aHasBeenSet &= AddDefaultItem(aItem);
                }
            }

            return aHasBeenSet;
        }

        #endregion

        // --------------------------------------------------
        // ACCESSORS
        // --------------------------------------------------

        #region Accessors

        /// <summary>
        /// Gets the item object of this instance.
        /// </summary>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to use.</param>
        /// <param name="log">The log to populate.</param>
        /// <returns>Returns the item object.</returns>
        public Object GetItemObject(
            IAppScope appScope = null,
            ScriptVariableSet scriptVariableSet = null,
            Log log = null)
        {
            return !IsValueList ? this.DefaultItems.FirstOrDefault() : this.DefaultItems;
        }

        // Elements -----------------------------------------

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
            DataAreaSpecification areaSpecification = _areaSpecifications.FirstOrDefault(p=>p.AreaName.KeyEquals(areaName));

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
                if (_constraintStatement != null)
                {
                    log = _constraintStatement.CheckItem(item, dataElement, true, appScope, scriptVariableSet);
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
                if (!_availableItemizationModes.Contains(DataItemizationMode.Any) && !_availableItemizationModes.Contains(dataElement.ItemizationMode))
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

                            switch (GetAreaSpecification("item").RequirementLevel)
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

                            switch (GetAreaSpecification("item").RequirementLevel)
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
                            if ((!IsValueList) && (dataElement.Items.Count > 1))
                                log.AddWarning(
                                    title: "More than one item found in element",
                                    description: "The element has more than one item where as it is in single itemization mode.");

                            switch (GetAreaSpecification("item").RequirementLevel)
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
                if (IsValueList)
                {
                    if (MinimumItemNumber > dataElement.Items.Count)
                        log.AddError(
                            title: "Not enough items in element",
                            description: "The element has " + dataElement.Items.Count + " items where as the minimum was specified at " + MinimumItemNumber + ".");

                    if (MaximumItemNumber > -1 && MaximumItemNumber < dataElement.Items.Count)
                        log.AddError(
                            title: "Too many items in element",
                            description: "The element has " + dataElement.Items.Count + " items where as the maximum was specified at " + MaximumItemNumber + ".");
                }

                foreach (Object aItem in dataElement.Items)
                    log.AddEvents(CheckItem(aItem, dataElement, appScope, scriptVariableSet));
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

            if (MinimumItemNumber < 0) MinimumItemNumber = 0;
            if (MaximumItemNumber < 0) MaximumItemNumber = -1;
            if (MaximumItemNumber > -1 && MaximumItemNumber < MinimumItemNumber) MaximumItemNumber = MinimumItemNumber;

            // we update the area specifications
            _areaSpecifications.RemoveAll(p =>
                !CarrierElementSpec.__Arenames.Any(q => q.KeyEquals(p.AreaName)));
            foreach (String arename in CarrierElementSpec.__Arenames)
                if (_areaSpecifications.Any(p => p.AreaName.KeyEquals(arename)))
                    _areaSpecifications.Add(new DataAreaSpecification(arename));

            if (_availableItemizationModes == null || _availableItemizationModes.Count == 0)
                _availableItemizationModes = new List<DataItemizationMode>() { DataItemizationMode.Valued };

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
            DataElementSpec dataElementSpec = base.Clone() as DataElementSpec;

            if (_aliases != null)
                dataElementSpec.Aliases = new List<String>(_aliases);
            if (ConstraintStatement != null)
                dataElementSpec.ConstraintStatement = ConstraintStatement.Clone() as DataConstraintStatement;
            if (DesignStatement != null)
                dataElementSpec.DesignStatement = DesignStatement.Clone() as DataElementDesignStatement;
            return dataElementSpec;
        }

        #endregion
    }

}
