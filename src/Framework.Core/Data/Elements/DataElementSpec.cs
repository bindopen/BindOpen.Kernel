﻿using System;
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
    public abstract class DataElementSpec : DataSpecification, IDataElementSpec
    {
        // --------------------------------------------------
        // CONSTANTS
        // --------------------------------------------------

        #region Constants

        /// <summary>
        /// Names of the attribute areas of the DataElementSpec class.
        /// </summary>
        public static string[] __Arenames = new []
        {
            nameof(DataAreaKind.Design),
            nameof(DataAreaKind.Constraints),
            nameof(DataAreaKind.Properties),
            nameof(DataElementAreaKind.Element),
            nameof(DataAreaKind.Items)
        };

        #endregion

        // --------------------------------------------------
        // VARIABLES
        // --------------------------------------------------

        #region Variables

        // General ------------------------------------------

        private string _groupId = null;
        private List<string> _aliases = null;
        private List<IDataAreaSpecification> _areaSpecifications = null;

        // Items ------------------------------

        private List<DataItemizationMode> _availableItemizationModes = null;
        private List<object> _defaultItems = null;
        private List<string> _defaultStringItems = null;

        private List<SpecificationLevel> _itemSpecificationLevels = null;

        // Constraints ------------------------

        private IDataConstraintStatement _constraintStatement = null;

        // Design -----------------------------

        private IDataDesignStatement _designStatement = null;

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
        public string GroupId
        {
            get => _groupId ?? "";
            set { _groupId = value; }
        }

        /// <summary>
        /// Specification of the GroupId property of this instance.
        /// </summary>
        [XmlIgnore()]
        public bool GroupIdSpecified => !string.IsNullOrEmpty(_groupId);

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
        public List<string> Aliases
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
        public bool AliasesSpecified => _aliases?.Count > 1;

        /// <summary>
        /// The area specifications of this instance.
        /// </summary>
        [XmlElement("areaSpecifications")]
        public List<IDataAreaSpecification> AreaSpecifications
        {
            get => _areaSpecifications ?? (_areaSpecifications = new List<IDataAreaSpecification>());
            set
            {
                _areaSpecifications = value;
            }
        }

        /// <summary>
        /// Specification of the AreaSpecifications property of this instance.
        /// </summary>
        [XmlIgnore()]
        public bool AreaSpecificationsSpecified => _areaSpecifications?.Count > 1;

        /// <summary>
        /// Indicates whether the instance can be allocated.
        /// </summary>
        [XmlElement("isAllocatable")]
        [DefaultValue(false)]
        public bool IsAllocatable { get; set; } = false;

        /// <summary>
        /// Specification of the IsAllocatable property of this instance.
        /// </summary>
        [XmlIgnore()]
        public bool IsAllocatableSpecified => IsAllocatable;

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
        public bool ItemScriptSpecified => !string.IsNullOrEmpty(ItemScript);

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
        public bool AvailableItemizationModesSpecified => _availableItemizationModes?.Count > 0;

        /// <summary>
        /// Default string items of this instance.
        /// </summary>
        [XmlArray("defaultStringItems")]
        [XmlArrayItem("item")]
        public List<string> DefaultStringItems
        {
            get => _defaultStringItems ?? (_defaultStringItems = new List<string>());
            set { _defaultStringItems = value; }
        }

        /// <summary>
        /// Specification of the DefaultStringItems property of this instance.
        /// </summary>
        [XmlIgnore()]
        public bool DefaultStringItemsSpecified => _defaultStringItems?.Count > 0;

        /// <summary>
        /// Default items of this instance.
        /// </summary>
        [XmlIgnore()]
        public List<object> DefaultItems
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
        public bool IsValueList => (MaximumItemNumber == -1) || (MaximumItemNumber > 1);

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
        public bool ItemSpecificationLevelsSpecified => _itemSpecificationLevels?.Count > 0 && !_itemSpecificationLevels.Contains(SpecificationLevel.All);

        // Constraints ---------------------------

        /// <summary>
        /// Constraint statement of this instance.
        /// </summary>
        [XmlElement("constraint.statement")]
        public IDataConstraintStatement ConstraintStatement
        {
            get => _constraintStatement ?? (_constraintStatement = new DataConstraintStatement());
            set { _constraintStatement = value; }
        }

        /// <summary>
        /// Specification of the ConstraintStatement property of this instance.
        /// </summary>
        [XmlIgnore()]
        public bool ConstraintStatementSpecified => _constraintStatement?.Count > 0;

        // Design ----------------------------------

        /// <summary>
        /// Design statement of this instance.
        /// </summary>
        [XmlElement("design.statement")]
        public IDataDesignStatement DesignStatement
        {
            get => _designStatement ?? (_designStatement = new DataDesignStatement());
            set { _designStatement = value; }
        }

        /// <summary>
        /// Specification of the DesignStatement property of this instance.
        /// </summary>
        [XmlIgnore()]
        public bool DesignStatementSpecified => _designStatement != null;

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
            SpecificationLevel[] specificationLevels = null) : base(accessibilityLevel, specificationLevels)
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
        /// <param name="item">The default item of this instance.</param>
        /// <returns>Returns True if the default item has well been Added.</returns>
        public bool AddDefaultItem(object item)
        {
            DefaultItems.Add(item);
            return true;
        }

        /// <summary>
        /// Sets the specified default item of this instance.
        /// </summary>
        /// <remarks>Items of this instance must be allowed and must not be forbidden. Otherwise, the values will be the default ones..</remarks>
        /// <param name="item">The default item of this instance.</param>
        /// <returns>Returns True if the default item has well been set.</returns>
        public bool SetDefaultItem(object item)
        {
            DefaultItems.Clear();
            return AddDefaultItem(item);
        }

        /// <summary>
        /// Sets the specified default items of this instance.
        /// </summary>
        /// <remarks>Items of this instance must be allowed and must not be forbidden. Otherwise, the values will be the default ones..</remarks>
        /// <param name="defaultItems">The default items of this instance.</param>
        /// <returns>Returns True if the default items has well been set.</returns>
        public bool SetDefaultItem(List<object> defaultItems)
        {
            bool hasBeenSet = true;

            DefaultItems.Clear();
            if (hasBeenSet = (defaultItems == null || defaultItems.Count == 0))
            {
                foreach (object item in defaultItems)
                {
                    hasBeenSet &= AddDefaultItem(item);
                }
            }

            return hasBeenSet;
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
        public object GetDefaultItemObject(
            IAppScope appScope = null,
            IScriptVariableSet scriptVariableSet = null,
            ILog log = null)
        {
            return !IsValueList ? this.DefaultItems.FirstOrDefault() : this.DefaultItems;
        }

        // Elements -----------------------------------------

        /// <summary>
        /// Gets the area specification with the specified area name.
        /// </summary>
        /// <param name="areaName">The data specification to consider.</param>
        /// <returns>Returns the area specification.</returns>
        public IDataAreaSpecification GetAreaSpecification(string areaName)
        {
            IDataAreaSpecification areaSpecification = _areaSpecifications.Find(p=>p.AreaName.KeyEquals(areaName));

            return areaSpecification ?? new DataAreaSpecification() { AreaName = areaName };
        }

        /// <summary>
        /// Indicates whether this instance is compatible with the specified data item.
        /// </summary>
        /// <param name="item">The data item to consider.</param>
        /// <returns>True if this instance is compatible with the specified data item.</returns>
        public override bool IsCompatibleWith(IDataItem item)
        {
            bool isCompatible = base.IsCompatibleWith(item);

            if (isCompatible)
            {
            }

            return isCompatible;
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
        public override ILog Repair<T>(
            T item = default,
            string[] specificationAreas = null,
            UpdateMode[] updateModes = null,
            IAppScope appScope = null,
            IScriptVariableSet scriptVariableSet = null)
        {
            ILog log = new Log();

            if (MinimumItemNumber < 0) MinimumItemNumber = 0;
            if (MaximumItemNumber < 0) MaximumItemNumber = -1;
            if (MaximumItemNumber > -1 && MaximumItemNumber < MinimumItemNumber) MaximumItemNumber = MinimumItemNumber;

            // we update the area specifications
            _areaSpecifications.RemoveAll(p =>
                !CarrierElementSpec.__Arenames.Any(q => q.KeyEquals(p.AreaName)));
            foreach (string arename in CarrierElementSpec.__Arenames)
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
        public override object Clone()
        {
            DataElementSpec dataElementSpec = base.Clone() as DataElementSpec;

            if (_aliases != null)
                dataElementSpec.Aliases = new List<string>(_aliases);
            if (ConstraintStatement != null)
                dataElementSpec.ConstraintStatement = ConstraintStatement.Clone() as DataConstraintStatement;
            if (DesignStatement != null)
                dataElementSpec.DesignStatement = DesignStatement.Clone() as DataDesignStatement;
            return dataElementSpec;
        }

        #endregion
    }
}
