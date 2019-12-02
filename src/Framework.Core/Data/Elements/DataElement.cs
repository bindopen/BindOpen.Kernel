using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.Data.Common;
using BindOpen.Framework.Core.Data.Elements._Object;
using BindOpen.Framework.Core.Data.Elements.Carrier;
using BindOpen.Framework.Core.Data.Elements.Collection;
using BindOpen.Framework.Core.Data.Elements.Document;
using BindOpen.Framework.Core.Data.Elements.Meta;
using BindOpen.Framework.Core.Data.Elements.Scalar;
using BindOpen.Framework.Core.Data.Elements.Sets;
using BindOpen.Framework.Core.Data.Elements.Source;
using BindOpen.Framework.Core.Data.Helpers.Objects;
using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.Data.References;
using BindOpen.Framework.Core.Data.Specification;
using BindOpen.Framework.Core.Data.Specification.Design;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Core.System.Diagnostics.Events;
using BindOpen.Framework.Core.System.Scripting;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;

namespace BindOpen.Framework.Core.Data.Elements
{
    /// <summary>
    /// This class represents a data element.
    /// </summary>
    [Serializable()]
    [XmlType("DataElement", Namespace = "https://bindopen.org/xsd")]
    [XmlRoot(ElementName = "element", Namespace = "https://bindopen.org/xsd", IsNullable = false)]
    [XmlInclude(typeof(CarrierElement))]
    [XmlInclude(typeof(SourceElement))]
    [XmlInclude(typeof(DocumentElement))]
    [XmlInclude(typeof(ObjectElement))]
    [XmlInclude(typeof(MetaDataElement))]
    [XmlInclude(typeof(ScalarElement))]
    public abstract class DataElement : IndexedDataItem, IDataElement
    {
        // --------------------------------------------------
        // VARIABLES
        // --------------------------------------------------

        #region Variables

        private DataItemizationMode _itemizationMode = DataItemizationMode.Any;

        private List<object> _items = null;

        // Properties ---------------------------------------

        private DataElementSet _propertyDetail = null;
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
        [DefaultValue(DataValueType.Text)]
        public DataValueType ValueType { get; set; } = DataValueType.Any;

        /// <summary>
        /// Specification of the ValueType property of this instance.
        /// </summary>
        [XmlIgnore()]
        public bool ValueTypeSpecified => ValueType != DataValueType.Any;

        /// <summary>
        /// The itemization mode of this instance.
        /// </summary>
        [XmlElement("itemizationMode")]
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
        /// Specification of the ItemizationMode property of this instance.
        /// </summary>
        [XmlIgnore()]
        public bool ItemizationModeSpecified => ItemizationMode != DataItemizationMode.Valued;

        /// <summary>
        /// Item reference of this instance.
        /// </summary>
        [XmlElement("itemReference")]
        public DataReferenceDto ItemReference { get; set; } = null;

        /// <summary>
        /// Specification of the ItemReference property of this instance.
        /// </summary>
        [XmlIgnore()]
        public bool ItemReferenceSpecified => ItemReference != null;

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
        /// Items of this instance.
        /// </summary>
        [XmlIgnore()]
        public List<object> Items
        {
            get => _items ?? (_items = new List<object>());
            set => _items = value;
        }

        /// <summary>
        /// Converts from string.
        /// </summary>
        /// <param name="st">The string to consider.</param>
        public static explicit operator DataElement(string st)
        {
            return ElementFactory.CreateScalar(DataValueType.Any, st);
        }

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <param name="element">The element to consider.</param>
        public static explicit operator string(DataElement element)
        {
            return element?.GetObject() as string;
        }

        // Specification -------------------------------

        /// <summary>
        /// Specification of this instance.
        /// </summary>
        [XmlIgnore()]
        public DataElementSpec Specification { get; set; } = null;

        /// <summary>
        /// Specification of the Specification property of this instance.
        /// </summary>
        [XmlIgnore()]
        public bool SpecificationSpecified => Specification != null;

        /// <summary>
        /// Returns the item with the specified indexed.
        /// </summary>
        [XmlIgnore()]
        public object this[int index] => index >= 0 && index < _items.Count ? _items[index] : null;

        /// <summary>
        /// Returns the item with the specified unique name.
        /// </summary>
        [XmlIgnore()]
        public object this[string name] => _items.Find(p => p.KeyEquals(name));

        /// <summary>
        /// Returns the first item.
        /// </summary>
        [XmlIgnore()]
        public object First => this[0];

        // Properties -------------------------------

        /// <summary>
        /// Property detail of this instance.
        /// </summary>
        [XmlElement("propertyDetail")]
        public DataElementSet PropertyDetail
        {
            get => _propertyDetail ?? (_propertyDetail = new DataElementSet());
            set { _propertyDetail = value; }
        }

        /// <summary>
        /// Specification of the PropertyDetail property of this instance.
        /// </summary>
        [XmlIgnore()]
        public bool PropertyDetailSpecified => _propertyDetail != null && (_propertyDetail.ElementsSpecified);

        /// <summary>
        /// The event kind of this instance.
        /// </summary>
        [XmlElement("eventKind")]
        public EventKinds? EventKind
        {
            get => _eventKind ?? System.Diagnostics.Events.EventKinds.None;
            set { _eventKind = value; }
        }

        /// <summary>
        /// Specification of the EventKind property of this instance.
        /// </summary>
        [XmlIgnore()]
        public bool EventKindSpecified => _eventKind != null && _eventKind == System.Diagnostics.Events.EventKinds.None;

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
        /// Returns the item object of this instance.
        /// </summary>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to use.</param>
        /// <param name="log">The log to populate.</param>
        /// <returns>Returns the items of this instance.</returns>
        public virtual object GetObject<T>(
            IBdoScope scope = null,
            IBdoScriptVariableSet scriptVariableSet = null,
            IBdoLog log = null)
        {
            return (T)GetObject(scope, scriptVariableSet, log);
        }

        /// <summary>
        /// Returns the item object of this instance.
        /// </summary>
        /// <param name="log">The log to populate.</param>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to use.</param>
        /// <returns>Returns the items of this instance.</returns>
        public virtual object GetObject(
            IBdoScope scope = null,
            IBdoScriptVariableSet scriptVariableSet = null,
            IBdoLog log = null)
        {
            object object1 = null;

            switch (ItemizationMode)
            {
                case DataItemizationMode.Valued:
                    return _items.Count == 0 ? null : (_items.Count == 1 ? _items[0] : _items);
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
                        log?.AddError(title: "Application scope missing");
                    }
                    else if (scope.Interpreter == null)
                    {
                        log?.AddError(title: "Script interpreter missing");
                    }
                    else if (string.IsNullOrEmpty(ItemScript))
                    {
                        log?.AddWarning(title: "Script missing");
                    }
                    else
                    {
                        object1 = scope.Interpreter.Interprete(ItemScript, scriptVariableSet, log);
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
        public void ClearItems()
        {
            _items = new List<object>();
        }

        // New

        /// <summary>
        /// Creates a new specification of this instance.
        /// </summary>
        /// <returns>Returns True .</returns>
        public abstract DataElementSpec NewSpecification();

        // Set

        /// <summary>
        /// Sets the specified single item of this instance.
        /// </summary>
        /// <param name="item">The item to apply to this instance.</param>
        /// <remarks>Items of this instance must be allowed and must not be forbidden. Otherwise, the values will be the default ones..</remarks>
        public virtual void SetItem(
            object item)
        {
            ClearItems();
            AddItem(item);
        }

        /// <summary>
        /// Sets the specified single item of this instance.
        /// </summary>
        /// <param name="items">The items to apply to this instance.</param>
        /// <remarks>Items of this instance must be allowed and must not be forbidden. Otherwise, the values will be the default ones..</remarks>
        public void SetItems(object[] items)
        {
            ClearItems();
            AddItems(items);
        }

        // Add

        /// <summary>
        /// Adds a new single item of this instance.
        /// </summary>
        /// <param name="item">The string item of this instance.</param>
        /// <param name="log">The log to populate.</param>
        /// <remarks>Items of this instance must be allowed and must not be forbidden. Otherwise, the items will be the default ones..</remarks>
        /// <returns>Returns True if the specified has been well added.</returns>
        public virtual bool AddItem(
            object item,
            IBdoLog log = null)
        {
            bool isAdded = false;

            if (item != null)
            {
                if ((item.GetType().IsArray || item is IList) && item is IEnumerable)
                {
                    foreach (object subItem in (item as IEnumerable))
                        AddItem(subItem);
                }
                else if (Specification == null
                    || (Specification.MaximumItemNumber == -1)
                    || (_items.Count < Specification.MaximumItemNumber))
                {
                    if (Specification == null
                        && (ValueType == DataValueType.Any || item.GetValueType().IsCompatibleWith(ValueType)))
                    {
                        if (this is CollectionElement)
                        {
                            _items?.RemoveAll(p => p.KeyEquals(item));
                        }

                        (_items ?? (_items = new List<object>())).Add(item);
                        isAdded = true;
                    }
                }
            }

            return isAdded;
        }

        /// <summary>
        /// Adds items to this instance.
        /// </summary>
        /// <param name="items">The items of this instance.</param>
        /// <param name="log">The log to populate.</param>
        /// <remarks>Items of this instance must be allowed and must not be forbidden. Otherwise, the items will be the default ones..</remarks>
        public void AddItems(
            object[] items,
            IBdoLog log = null)
        {
            if (items != null)
            {
                foreach (object item in items)
                {
                    AddItem(item, log);
                }
            }
        }

        // Switch

        /// <summary>
        /// Switches the single value value1 with the single value value2.
        /// </summary>
        /// <param name="value1">The first single value to switch.</param>
        /// <param name="value2">The second single value to switch.</param>
        public void SwitchItems(object value1, object value2)
        {
            object aTempValue = value1;

            if ((_items.IndexOf(value1) > -1) && (_items.IndexOf(value2) > -1))
            {
                _items[_items.IndexOf(value1)] = value2;
                _items[_items.IndexOf(value2)] = aTempValue;
            }
        }

        /// <summary>
        /// Updates the value value1 with the value value2.
        /// </summary>
        /// <param name="item">The item to consider.</param>
        /// <param name="aNewItem">The new item.</param>
        public void UpdateItem(object item, object aNewItem)
        {
            if (_items.Contains(item))
                _items[_items.IndexOf(item)] = aNewItem;
        }

        // Remove

        /// <summary>
        /// Removes the specified item.
        /// </summary>
        /// <param name="item">The item </param>
        public bool RemoveItem(object item)
        {
            return _items.RemoveAll(p => p == item) > 0;
        }

        // Accessors --------------------------

        /// <summary>
        /// Gets the default control type of this instance.
        /// </summary>
        /// <returns>Returns the default control type of this instance.</returns>
        public virtual DesignControlType GetDefaultControlType()
        {
            return DesignControlType.None;
        }

        /// <summary>
        /// Indicates whether this instance contains the specified scalar item or the specified entity name.
        /// </summary>
        /// <param name="indexItem">The index item to consider.</param>
        /// <param name="isCaseSensitive">Indicates whether the verification is case sensitive.</param>
        /// <returns>Returns true if this instance contains the specified scalar item or the specified entity name.</returns>
        public virtual bool HasItem(object indexItem, bool isCaseSensitive = false)
        {
            return false;
        }

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
            IBdoLog log = new BdoLog();

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
                        ItemScript = element.ItemScript;

                        _items = new List<object>();
                        foreach (object currentItem in element.Items)
                            _items.Add(currentItem);
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
            IBdoLog log = new BdoLog();

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
            IBdoLog log = new BdoLog();

            if (item is IDataElement)
            {
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
        public override object Clone()
        {
            return Clone(null);
        }

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <param name="elementSpecificationAreas">The data element areas to consider.</param>
        /// <returns>Returns a cloned instance.</returns>
        public virtual object Clone(string[] elementSpecificationAreas = null)
        {
            if (elementSpecificationAreas == null)
                elementSpecificationAreas = new[] { nameof(DataAreaKind.Any) };

            DataElement cloneDataElement = base.Clone() as DataElement;

            if (ItemReference != null)
                cloneDataElement.ItemReference = ItemReference.Clone() as DataReferenceDto;
            if (Specification != null)
                cloneDataElement.Specification = Specification.Clone() as DataElementSpec;

            if (_propertyDetail != null)
                if (elementSpecificationAreas.Contains(nameof(DataAreaKind.Any)) || elementSpecificationAreas.Contains(nameof(DataAreaKind.Properties)))
                    cloneDataElement.PropertyDetail = _propertyDetail.Clone() as DataElementSet;

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
            _propertyDetail?.UpdateStorageInfo(log);

            ItemReference?.UpdateStorageInfo(log);
        }

        /// <summary>
        /// Updates information for runtime.
        /// </summary>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="scriptVariableSet">The set of script variables to consider.</param>
        /// <param name="log">The log to update.</param>
        public override void UpdateRuntimeInfo(IBdoScope scope = null, IBdoScriptVariableSet scriptVariableSet = null, IBdoLog log = null)
        {
            _propertyDetail?.UpdateRuntimeInfo(scope, scriptVariableSet, log);

            ItemReference?.UpdateRuntimeInfo(scope, scriptVariableSet, log);
        }

        /// <summary>
        /// Gets a text node representing this instance.
        /// </summary>
        /// <param name="nodeName">Name of the tex node.</param>
        /// <param name="indent">Tabulation indent to include in the text.</param>
        /// <returns>Returns the text node.</returns>
        public string GetTextNode(string nodeName, string indent)
        {
            string st = "";

            st += indent + nodeName + "\n";
            st += "\t" + indent + nodeName + ":name=\"" + Key() + "\"\n";
            st += "\t" + indent + nodeName + ":valueType=\"" + ValueType.ToString() + "\"\n";
            if (Items.Count > 0)
            {
                st += "\t" + indent + nodeName + ":items\n";
                foreach (string aItemstring in Items)
                    st += "\t\t" + indent + nodeName + ":items:item=\"" + aItemstring + "\"\n";
            }

            return st;
        }

        #endregion
    }
}
