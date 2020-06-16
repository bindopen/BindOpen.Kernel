using BindOpen.Application.Scopes;
using BindOpen.Data.Common;
using BindOpen.Data.Expression;
using BindOpen.Data.Helpers.Objects;
using BindOpen.Data.Items;
using BindOpen.Data.References;
using BindOpen.Data.Specification;
using BindOpen.System.Diagnostics;
using BindOpen.System.Diagnostics.Events;
using BindOpen.System.Scripting;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;

namespace BindOpen.Data.Elements
{
    /// <summary>
    /// This class represents a data element.
    /// </summary>
    [XmlType("DataElement", Namespace = "https://docs.bindopen.org/xsd")]
    [XmlRoot(ElementName = "element", Namespace = "https://docs.bindopen.org/xsd", IsNullable = false)]
    [XmlInclude(typeof(CarrierElement))]
    [XmlInclude(typeof(SourceElement))]
    [XmlInclude(typeof(DocumentElement))]
    [XmlInclude(typeof(ObjectElement))]
    [XmlInclude(typeof(ScalarElement))]
    public abstract class DataElement : IndexedDataItem, IDataElement
    {
        // --------------------------------------------------
        // VARIABLES
        // --------------------------------------------------

        #region Variables

        private DataItemizationMode _itemizationMode = DataItemizationMode.Any;

        private EventKinds? _eventKind = null;

        #endregion

        // --------------------------------------------------
        // PROPERTIES
        // --------------------------------------------------

        #region Properties

        // Items --------------------------------------------

        /// <summary>
        /// The value type of this instance.
        /// </summary>
        [XmlAttribute("valueType")]
        [DefaultValue(DataValueTypes.Text)]
        public DataValueTypes ValueType { get; set; } = DataValueTypes.Any;

        /// <summary>
        /// The itemization mode of this instance.
        /// </summary>
        [XmlElement("itemizationMode")]
        [DefaultValue(DataItemizationMode.Valued)]
        public DataItemizationMode ItemizationMode
        {
            get
            {
                return _itemizationMode != DataItemizationMode.Any ? _itemizationMode :
                  (!string.IsNullOrEmpty(ItemScript) ? DataItemizationMode.Script :
                  (ItemReference != null ? DataItemizationMode.Referenced : DataItemizationMode.Valued));
            }
            set { _itemizationMode = value; }
        }

        /// <summary>
        /// Item reference of this instance.
        /// </summary>
        [XmlElement("itemReference")]
        public DataReferenceDto ItemReference { get; set; } = null;

        /// <summary>
        /// The script of this instance.
        /// </summary>
        [XmlAttribute("script")]
        [DefaultValue("")]
        public string ItemScript { get; set; } = null;

        /// <summary>
        /// Items of this instance.
        /// </summary>
        [XmlIgnore()]
        public List<object> Items { get; set; } = null;

        /// <summary>
        /// Converts from string.
        /// </summary>
        /// <param name="st">The string to consider.</param>
        public static explicit operator DataElement(string st)
        {
            return ElementFactory.CreateScalar(DataValueTypes.Any, st);
        }

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <param name="element">The element to consider.</param>
        public static explicit operator string(DataElement element)
        {
            return element?.GetValue() as string;
        }

        /// <summary>
        /// Returns the item with the specified indexed.
        /// </summary>
        [XmlIgnore()]
        public object this[int index] => Items == null ? null : (index >= 0 && index < Items.Count ? Items[index] : null);

        // Specification -------------------------------

        /// <summary>
        /// Specification of this instance.
        /// </summary>
        [XmlIgnore()]
        public IDataElementSpec Specification { get; set; } = null;

        // Properties -------------------------------

        /// <summary>
        /// Property detail of this instance.
        /// </summary>
        [XmlElement("propertyDetail")]
        public DataElementSet PropertyDetail { get; set; } = null;

        /// <summary>
        /// The event kind of this instance.
        /// </summary>
        [XmlElement("eventKind")]
        [DefaultValue(EventKinds.None)]
        public EventKinds EventKind
        {
            get => _eventKind ?? EventKinds.None;
            set { _eventKind = value; }
        }

        #endregion

        // --------------------------------------------------
        // CONSTRUCTORS
        // --------------------------------------------------

        #region Constructors

        /// <summary>
        /// Initializes a new data element.
        /// </summary>
        protected DataElement() : this(null)
        {
        }

        /// <summary>
        /// Initializes a new data element.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="namePreffix">The name preffix to consider.</param>
        /// <param name="id">The ID to consider.</param>
        protected DataElement(
            string name = null,
            string namePreffix = null,
            string id = null)
            : base(name, namePreffix, id)
        {
        }

        #endregion

        // --------------------------------------------------
        // ITEMS
        // --------------------------------------------------

        #region Items

        /// <summary>
        /// Indicates whether this instance is compatible with the specified item.
        /// </summary>
        /// <param name="item">The item to consider.</param>
        /// <returns></returns>
        public bool IsCompatibleWithItem(object item)
        {
            return (ValueType == DataValueTypes.Any || item.GetValueType().IsCompatibleWith(ValueType));
        }

        /// <summary>
        /// Returns the item object of this instance.
        /// </summary>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to use.</param>
        /// <param name="log">The log to populate.</param>
        /// <returns>Returns the items of this instance.</returns>
        public virtual object GetValue<T>(
            IBdoScope scope = null,
            IScriptVariableSet scriptVariableSet = null,
            IBdoLog log = null)
        {
            return (T)GetValue(scope, scriptVariableSet, log);
        }

        /// <summary>
        /// Returns the item object of this instance.
        /// </summary>
        /// <param name="log">The log to populate.</param>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to use.</param>
        /// <returns>Returns the items of this instance.</returns>
        public virtual object GetValue(
            IBdoScope scope = null,
            IScriptVariableSet scriptVariableSet = null,
            IBdoLog log = null)
        {
            object object1 = null;

            switch (ItemizationMode)
            {
                case DataItemizationMode.Valued:
                    return Items?.Count > 0 ? (Items.Count == 1 ? Items[0] : Items) : null;
                case DataItemizationMode.Referenced:
                    if (scope == null)
                    {
                        log?.AddError(title: "Application scope missing");
                    }
                    else if (scope.Interpreter == null)
                    {
                        log?.AddError(title: "Script interpreter missing");
                    }
                    else if (ItemReference == null)
                    {
                        log?.AddWarning(title: "Reference missing");
                    }
                    else
                    {
                        DataReference reference = new DataReference(ItemReference);

                        return reference.Get(scope, scriptVariableSet, log);
                    }
                    break;
                case DataItemizationMode.Script:
                    if (scope == null)
                    {
                        log?.AddWarning(title: "Application scope missing");
                        return ItemScript;
                    }
                    else if (scope.Interpreter == null)
                    {
                        log?.AddError(title: "Script interpreter missing");
                        return ItemScript;
                    }
                    else if (string.IsNullOrEmpty(ItemScript))
                    {
                        log?.AddWarning(title: "Script missing");
                        return ItemScript;
                    }
                    else
                    {
                        object1 = scope.Interpreter.Evaluate(ItemScript, DataExpressionKind.Script, scriptVariableSet, log);
                        if (object1 != null)
                        {
                            return object1.GetType().IsArray ? object1 as List<object> : object1;
                        }
                    }
                    break;
            }

            return null;
        }

        // Mutators ---------------------------

        // Clear

        /// <summary>
        /// Clears items of this instance.
        /// </summary>
        public IDataElement ClearItems()
        {
            Items = new List<object>();

            return this;
        }

        // New

        /// <summary>
        /// Creates a new specification of this instance.
        /// </summary>
        /// <returns>Returns True .</returns>
        public abstract IDataElementSpec NewSpecification();

        // Set

        /// <summary>
        /// Sets the specified single item of this instance.
        /// </summary>
        /// <param name="items">The items to apply to this instance.</param>
        /// <remarks>Items of this instance must be allowed and must not be forbidden. Otherwise, the values will be the default ones..</remarks>
        public IDataElement WithItems(params object[] items)
        {
            ClearItems();
            Add(items);

            return this;
        }

        // Add

        /// <summary>
        /// Adds a new single item of this instance.
        /// </summary>
        /// <param name="item">The string item of this instance.</param>
        /// <param name="log">The log to populate.</param>
        /// <remarks>Items of this instance must be allowed and must not be forbidden. Otherwise, the items will be the default ones..</remarks>
        /// <returns>Returns True if the specified has been well added.</returns>
        public virtual IDataElement Add(params object[] items)
        {
            if (items != null)
            {
                foreach (object item in items)
                {
                    Add(item);
                }
            }

            return this;
        }

        /// <summary>
        /// Adds a new single item of this instance.
        /// </summary>
        /// <param name="item">The string item of this instance.</param>
        /// <remarks>Items of this instance must be allowed and must not be forbidden. Otherwise, the items will be the default ones..</remarks>
        /// <returns>Returns True if the specified has been well added.</returns>
        protected virtual void Add(object item)
        {
            if (item != null)
            {
                if ((item.GetType().IsArray || item is IList) && item is IEnumerable && !(item is byte[]))
                {
                    foreach (object subItem in (item as IEnumerable))
                    {
                        Add(subItem);
                    }
                }
                else if (Specification == null
                    || (Specification.MaximumItemNumber == -1)
                    || ((Items?.Count ?? 0) < Specification.MaximumItemNumber))
                {
                    if (Specification == null && IsCompatibleWithItem(item))
                    {
                        if (Items == null)
                        {
                            Items = new List<object>();
                        }

                        if (this is CollectionElement)
                        {
                            Items?.RemoveAll(p => p.KeyEquals(item));
                        }

                        Items.Add(item);
                    }
                }
            }
        }

        // Switch

        /// <summary>
        /// Switches the single value value1 with the single value value2.
        /// </summary>
        /// <param name="value1">The first single value to switch.</param>
        /// <param name="value2">The second single value to switch.</param>
        public IDataElement SwitchItems(object value1, object value2)
        {
            object aTempValue = value1;

            if ((Items.IndexOf(value1) > -1) && (Items.IndexOf(value2) > -1))
            {
                Items[Items.IndexOf(value1)] = value2;
                Items[Items.IndexOf(value2)] = aTempValue;
            }

            return this;
        }

        /// <summary>
        /// Updates the value value1 with the value value2.
        /// </summary>
        /// <param name="item">The item to consider.</param>
        /// <param name="aNewItem">The new item.</param>
        public IDataElement UpdateItem(object item, object aNewItem)
        {
            if (Items.Contains(item))
            {
                Items[Items.IndexOf(item)] = aNewItem;
            }

            return this;
        }

        // Remove

        /// <summary>
        /// Removes the specified item.
        /// </summary>
        /// <param name="items">The items to delete.</param>
        public IDataElement Remove(params object[] items)
        {
            if (items != null)
            {
                foreach (var item in items)
                {
                    Items.RemoveAll(p => p == item);
                }
            }

            return this;
        }

        // Accessors --------------------------

        /// <summary>
        /// Returns a text node representing this instance.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "";
        }

        #endregion

        // --------------------------------------------------
        // CHECK, UPDATE, REPAIR
        // --------------------------------------------------

        #region Check_Update_Repair

        /// <summary>
        /// Updates this instance.
        /// </summary>
        /// <param name="item">The item to consider.</param>
        /// <param name="specificationAreas">The specification areas to consider.</param>
        /// <param name="updateModes">The update modes to consider.</param>
        /// <returns>ILog of the operation.</returns>
        /// <remarks>Put reference collections as null if you do not want to repair this instance.</remarks>
        public override IBdoLog Update<T>(
            T item = default,
            string[] specificationAreas = null,
            UpdateModes[] updateModes = null)
        {
            var log = new BdoLog();

            if (item is IDataElement element)
            {
                if (specificationAreas == null)
                    specificationAreas = new[] { nameof(DataAreaKind.Any) };

                if ((specificationAreas.Contains(nameof(DataAreaKind.Any)))
                    || (specificationAreas.Contains(nameof(DataAreaKind.Constraints))))
                {
                }

                if ((specificationAreas.Contains(nameof(DataAreaKind.Any)))
                    || (specificationAreas.Contains(nameof(DataAreaKind.Design))))
                {
                }

                if ((specificationAreas.Contains(nameof(DataAreaKind.Any)))
                    || (specificationAreas.Contains(nameof(DataElementAreaKind.Element))))
                {
                    Name = element.Name;
                    Title = element.Title;
                    Description = element.Description;
                    Index = element.Index;
                }

                if ((specificationAreas.Contains(nameof(DataAreaKind.Any)))
                    || (specificationAreas.Contains(nameof(DataAreaKind.Items))))
                {
                    if (element != null)
                    {
                        if (ValueType == DataValueTypes.None || ValueType == DataValueTypes.Any)
                        {
                            ValueType = element.ValueType;
                        }

                        ItemScript = element.ItemScript;

                        if (element.Items != null && element.Items.Count > 0)
                        {
                            Items = new List<object>();
                            foreach (object currentItem in element.Items)
                            {
                                Items.Add(currentItem);
                            }
                        }
                    }
                }

                if ((specificationAreas.Contains(nameof(DataAreaKind.Any)))
                    || (specificationAreas.Contains(nameof(DataAreaKind.Properties))))
                {
                }
            }

            return log;
        }

        /// <summary>
        /// Checks this instance.
        /// </summary>
        /// <param name="isExistenceChecked">Indicates whether the carrier existence is checked.</param>
        /// <param name="item">The item to consider.</param>
        /// <param name="specificationAreas">The specification areas to consider.</param>
        /// <returns>Returns the check log.</returns>
        public override IBdoLog Check<T>(
            bool isExistenceChecked = true,
            T item = default,
            string[] specificationAreas = null)
        {
            var log = new BdoLog();

            if (item is IDataElement element)
            {
                if (specificationAreas == null)
                    specificationAreas = new[] { nameof(DataAreaKind.Any) };

                if ((specificationAreas.Contains(nameof(DataAreaKind.Any))) ||
                    (specificationAreas.Contains(nameof(DataAreaKind.Constraints))))
                {
                }

                if ((specificationAreas.Contains(nameof(DataAreaKind.Any))) ||
                    (specificationAreas.Contains(nameof(DataAreaKind.Design))))
                {
                }

                if ((specificationAreas.Contains(nameof(DataAreaKind.Any))) ||
                    (specificationAreas.Contains(nameof(DataAreaKind.Items))))
                    if (Specification != null)
                    {
                        foreach (object subItem in element.Items)
                        {
                            log.AddEvents(Specification.ConstraintStatement.CheckItem(subItem, this, true));
                        }
                    }

                if ((specificationAreas.Contains(nameof(DataAreaKind.Any))) ||
                    (specificationAreas.Contains(nameof(DataAreaKind.Properties))))
                {
                }
            }

            return log;
        }

        /// <summary>
        /// Repairs this instance with the specified definition.
        /// </summary>
        /// <param name="item">The item to consider.</param>
        /// <param name="specificationAreas">The specification areas to consider.</param>
        /// <param name="updateModes">The update modes to consider.</param>
        /// <returns>ILog of the operation.</returns>
        public override IBdoLog Repair<T>(
            T item = default,
            string[] specificationAreas = null,
            UpdateModes[] updateModes = null)
        {
            var log = new BdoLog();

            if (item is IDataElement element)
            {
                if (specificationAreas == null)
                    specificationAreas = new[] { nameof(DataAreaKind.Any) };

                if ((specificationAreas.Contains(nameof(DataAreaKind.Any)))
                    || (specificationAreas.Contains(nameof(DataAreaKind.Constraints))))
                {
                }

                if ((specificationAreas.Contains(nameof(DataAreaKind.Any))) ||
                    (specificationAreas.Contains(nameof(DataAreaKind.Design))))
                {
                }

                if ((specificationAreas.Contains(nameof(DataAreaKind.Any))) ||
                    (specificationAreas.Contains(nameof(DataElementAreaKind.Element))))
                {
                    if (Specification != null)
                        if (Specification.AvailableItemizationModes.Count == 1)
                            ItemizationMode = Specification.AvailableItemizationModes[0];
                }

                if ((specificationAreas.Contains(nameof(DataAreaKind.Any))) ||
                    (specificationAreas.Contains(nameof(DataAreaKind.Items))))
                {
                    if (ValueType == DataValueTypes.None || ValueType == DataValueTypes.Any)
                    {
                        ValueType = element.ValueType;
                    }
                }

                if ((specificationAreas.Contains(nameof(DataAreaKind.Any))) ||
                    (specificationAreas.Contains(nameof(DataAreaKind.Properties))))
                {
                }
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
        public override object Clone(params string[] areas)
        {
            if (areas == null)
            {
                areas = new[] { nameof(DataAreaKind.Any) };
            }

            DataElement cloneDataElement = base.Clone(areas) as DataElement;

            if (ItemReference != null)
                cloneDataElement.ItemReference = ItemReference.Clone() as DataReferenceDto;
            if (Specification != null)
                cloneDataElement.Specification = Specification.Clone() as DataElementSpec;

            if (PropertyDetail != null)
                if (areas.Contains(nameof(DataAreaKind.Any)) || areas.Contains(nameof(DataAreaKind.Properties)))
                    cloneDataElement.PropertyDetail = PropertyDetail.Clone() as DataElementSet;

            return cloneDataElement;
        }

        #endregion

        // --------------------------------------------------
        // SERIALIZATION
        // --------------------------------------------------

        #region Serialization

        /// <summary>
        /// Updates information for storage.
        /// </summary>
        /// <param name="log">The log to update.</param>
        public override void UpdateStorageInfo(IBdoLog log = null)
        {
            base.UpdateStorageInfo(log);

            PropertyDetail?.UpdateStorageInfo(log);

            ItemReference?.UpdateStorageInfo(log);
        }

        /// <summary>
        /// Updates information for runtime.
        /// </summary>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="scriptVariableSet">The set of script variables to consider.</param>
        /// <param name="log">The log to update.</param>
        public override void UpdateRuntimeInfo(IBdoScope scope = null, IScriptVariableSet scriptVariableSet = null, IBdoLog log = null)
        {
            PropertyDetail?.UpdateRuntimeInfo(scope, scriptVariableSet, log);

            ItemReference?.UpdateRuntimeInfo(scope, scriptVariableSet, log);

            base.UpdateRuntimeInfo(scope, scriptVariableSet, log);
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

            PropertyDetail?.Dispose();

            _isDisposed = true;

            base.Dispose(isDisposing);
        }

        #endregion
    }
}
