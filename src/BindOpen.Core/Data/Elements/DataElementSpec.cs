using BindOpen.Data.Common;
using BindOpen.Data.Elements.Schema;
using BindOpen.Data.Helpers.Objects;
using BindOpen.Data.Items;
using BindOpen.Data.Specification;
using BindOpen.System.Diagnostics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Xml.Serialization;

namespace BindOpen.Data.Elements
{
    /// <summary>
    /// This class represents a data element specification.
    /// </summary>
    [XmlType("DataElementSpec", Namespace = "https://bindopen.org/xsd")]
    [XmlRoot(ElementName = "specification", Namespace = "https://bindopen.org/xsd", IsNullable = false)]
    [XmlInclude(typeof(CarrierElementSpec))]
    [XmlInclude(typeof(DocumentElementSpec))]
    [XmlInclude(typeof(ObjectElementSpec))]
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
        public static string[] __Arenames = new[]
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
        private List<DataAreaSpecification> _areaSpecifications = null;

        // Items ------------------------------

        private List<DataItemizationMode> _availableItemizationModes = null;
        private List<object> _defaultItems = null;
        private List<string> _defaultStringItems = null;

        private List<SpecificationLevels> _itemSpecificationLevels = null;

        // Constraints ------------------------

        private DataConstraintStatement _constraintStatement = null;

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
        [DefaultValue("")]
        public string GroupId
        {
            get => _groupId ?? "";
            set { _groupId = value; }
        }

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
        /// Indicates whether the instance can be allocated.
        /// </summary>
        [XmlElement("isAllocatable")]
        [DefaultValue(false)]
        public bool IsAllocatable { get; set; } = false;

        // Items ---------------------------------

        /// <summary>
        /// The script of this instance.
        /// </summary>
        [XmlAttribute("script")]
        [DefaultValue("")]
        public string ItemScript { get; set; } = null;

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
        /// Default string items of this instance.
        /// </summary>
        [XmlArray("defaultStringItems")]
        [XmlArrayItem("item")]
        public List<string> DtoDefaultItems
        {
            get => _defaultStringItems ?? (_defaultStringItems = new List<string>());
            set { _defaultStringItems = value; }
        }

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
        public List<SpecificationLevels> ItemSpecificationLevels
        {
            get => _itemSpecificationLevels ?? (_itemSpecificationLevels = new List<SpecificationLevels>());
            set { _itemSpecificationLevels = value; }
        }

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

        #endregion

        // --------------------------------------------------
        // CONSTRUCTORS
        // --------------------------------------------------

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the DataElementSpec class.
        /// </summary>
        protected DataElementSpec() : this(AccessibilityLevels.Public)
        {
        }

        /// <summary>
        /// Initializes a new instance of the DataElementSpec class.
        /// </summary>
        /// <param name="accessibilityLevel">The accessibilty level of this instance.</param>
        /// <param name="specificationLevels">The specification levels of this instance.</param>
        protected DataElementSpec(
            AccessibilityLevels accessibilityLevel = AccessibilityLevels.Public,
            SpecificationLevels[] specificationLevels = null) : base(accessibilityLevel, specificationLevels)
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
            DefaultItems.Clear();
            bool hasBeenSet;
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
        /// <param name="log">The log to populate.</param>
        /// <returns>Returns the item object.</returns>
        public object GetDefaultItemObject(
            IBdoLog log = null)
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
            IDataAreaSpecification areaSpecification = _areaSpecifications.Find(p => p.AreaName.KeyEquals(areaName));

            return areaSpecification ?? new DataAreaSpecification() { AreaName = areaName };
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
        /// <returns>Log of the operation.</returns>
        public override IBdoLog Repair<T>(
            T item = default,
            string[] specificationAreas = null,
            UpdateModes[] updateModes = null)
        {
            var log = new BdoLog();

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
        // CHECKING
        // --------------------------------------------------

        #region Checking

        /// <summary>
        /// Check the specified item.
        /// </summary>
        /// <param name="item">The item to consider.</param>
        /// <param name="dataElement">The element to consider.</param>
        /// <returns>The log of check log.</returns>
        public virtual IBdoLog CheckItem(
            object item,
            IDataElement dataElement = null)
        {
            var log = new BdoLog();
            if (item != null)
            {
                if (_constraintStatement != null)
                {
                    log.AddEvents(_constraintStatement.CheckItem(item, dataElement, true));
                }
            }

            return log;
        }

        /// <summary>
        /// Check the specified item.
        /// </summary>
        /// <param name="dataElement">The element to consider.</param>
        /// <param name="specificationAreas">The specification areas to consider.</param>
        /// <returns>The log of check log.</returns>
        public virtual IBdoLog CheckElement(
            IDataElement dataElement,
            string[] specificationAreas = null)
        {
            var log = new BdoLog();

            if (dataElement == null)
                return log;

            if (specificationAreas == null)
                specificationAreas = new[] { nameof(DataAreaKind.Any) };

            if (specificationAreas.Contains(nameof(DataAreaKind.Any)) || specificationAreas.Contains("element"))
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
                                    if ((string.IsNullOrEmpty(dataElement.ItemScript)) || (dataElement.Items.Count > 0))
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
                            if (string.IsNullOrEmpty(dataElement.ItemScript))
                                log.AddWarning(
                                    title: "Item script missing in element",
                                    description: "The element has no item script where as it is in script itemization mode.");

                            switch (GetAreaSpecification("item").RequirementLevel)
                            {
                                case RequirementLevel.OptionalExclusively:
                                    if ((string.IsNullOrEmpty(dataElement.ItemScript)) || (dataElement.Items.Count > 0))
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
                                    if ((string.IsNullOrEmpty(dataElement.ItemScript)) || (dataElement.Items.Count > 0))
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

            if (specificationAreas.Contains(nameof(DataAreaKind.Any)) || specificationAreas.Contains(nameof(DataAreaKind.Items)))
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

                foreach (object item in dataElement.Items)
                    log.AddEvents(CheckItem(item, dataElement));
            }

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

            return dataElementSpec;
        }

        #endregion

        // ------------------------------------------
        // IDISPOSABLE METHODS
        // ------------------------------------------

        #region IDisposable_Methods

        private bool _isDisposed = false;

        /// <summary>
        /// Disposes this instance. 
        /// </summary>
        /// <param name="isDisposing">Indicates whether this instance is disposing</param>
        protected override void Dispose(bool isDisposing)
        {
            if (_isDisposed)
            {
                return;
            }

            _constraintStatement?.Dispose();

            _isDisposed = true;

            if (isDisposing)
            {
                GC.SuppressFinalize(this);
            }

            base.Dispose(isDisposing);
        }

        #endregion
    }
}
