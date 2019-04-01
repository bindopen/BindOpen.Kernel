using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.Data.Common;
using BindOpen.Framework.Core.Data.Elements.Carrier;
using BindOpen.Framework.Core.Data.Elements.Complex;
using BindOpen.Framework.Core.Data.Elements.Document;
using BindOpen.Framework.Core.Data.Elements.Entity;
using BindOpen.Framework.Core.Data.Elements.Scalar;
using BindOpen.Framework.Core.Data.Elements.Sets;
using BindOpen.Framework.Core.Data.Elements.Source;
using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.Data.References;
using BindOpen.Framework.Core.Data.Specification;
using BindOpen.Framework.Core.Data.Specification.Design;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Core.System.Diagnostics.Events;
using BindOpen.Framework.Core.System.Scripting;

namespace BindOpen.Framework.Core.Data.Elements
{
    /// <summary>
    /// This class represents a data element.
    /// </summary>
    [Serializable()]
    [XmlType("DataElement", Namespace = "http://meltingsoft.com/bindopen/xsd")]
    [XmlRoot(ElementName = "element", Namespace = "http://meltingsoft.com/bindopen/xsd", IsNullable = false)]
    [XmlInclude(typeof(CarrierElement))]
    [XmlInclude(typeof(SourceElement))]
    [XmlInclude(typeof(DocumentElement))]
    [XmlInclude(typeof(EntityElement))]
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

        private IDataElementSet _propertyDetail = null;
        private EventKind? _eventKind = null;

        #endregion

        // --------------------------------------------------
        // PROPERTIES
        // --------------------------------------------------

        #region Properties

        // Items --------------------------------------------

        /// <summary>
        /// The value type of this instance.
        /// </summary>
        [XmlIgnore()]
        public DataValueType ValueType { get; set; } = DataValueType.Any;

        /// <summary>
        /// The itemization mode of this instance.
        /// </summary>
        [XmlElement("itemizationMode")]
        public DataItemizationMode ItemizationMode
        {
            get { return _itemizationMode != DataItemizationMode.Any ? _itemizationMode :
                    (!string.IsNullOrEmpty(ItemScript) ? DataItemizationMode.Script :
                    (ItemReference != null ? DataItemizationMode.Referenced : DataItemizationMode.Valued)); }
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
        public IDataReference ItemReference { get; set; } = null;

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
        /// Items of this instance.
        /// </summary>
        [XmlIgnore()]
        public object FirstItem
        {
            get
            {
                return Items.Count > 0 ? Items[0] : null;
            }
            set
            {
                _items = new List<object>() { value };
            }
        }

        // Serialization -------------------------------

        /// <summary>
        /// The item Xml element object of this instance.
        /// </summary>
        [XmlAnyElement]
        public XElement ItemXElement { get; set; } = null;

        /// <summary>
        /// Specification of the ItemXElement property of this instance.
        /// </summary>
        [XmlIgnore()]
        public bool ItemXElementSpecified => !ValueType.IsScalar() || _items?.Count > 1;

        // Specification -------------------------------

        /// <summary>
        /// Specification of this instance.
        /// </summary>
        [XmlIgnore()]
        public IDataElementSpec Specification { get; set; } = null;

        /// <summary>
        /// Specification of the Specification property of this instance.
        /// </summary>
        [XmlIgnore()]
        public bool SpecificationSpecified => Specification != null;

        /// <summary>
        /// Returns the element with the specified indexed.
        /// </summary>
        [XmlIgnore()]
        public object this[int index]
        {
            get
            {
                return index >= 0 && index < _items.Count ? _items[index] : null;
            }
        }

        /// <summary>
        /// Returns the element with the specified unique name.
        /// </summary>
        [XmlIgnore()]
        public object this[string name] => GetItem(name);

        // Properties -------------------------------

        /// <summary>
        /// Property detail of this instance.
        /// </summary>
        [XmlElement("propertyDetail")]
        public IDataElementSet PropertyDetail
        {
            get => _propertyDetail ?? (_propertyDetail = new DataElementSet());
            set { _propertyDetail = value; }
        }

        /// <summary>
        /// Specification of the PropertyDetail property of this instance.
        /// </summary>
        [XmlIgnore()]
        public bool PropertyDetailSpecified => _propertyDetail != null && (_propertyDetail.ElementsSpecified || _propertyDetail.DescriptionSpecified);

        /// <summary>
        /// The event kind of this instance.
        /// </summary>
        [XmlElement("eventKind")]
        public EventKind? EventKind
        {
            get => _eventKind ?? System.Diagnostics.Events.EventKind.None;
            set { _eventKind = value; }
        }

        /// <summary>
        /// Specification of the EventKind property of this instance.
        /// </summary>
        [XmlIgnore()]
        public bool EventKindSpecified => _eventKind != null && _eventKind == System.Diagnostics.Events.EventKind.None;

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

        // Mutators ---------------------------

        // Clear

        /// <summary>
        /// Clears items of this instance.
        /// </summary>
        public void ClearItems()
        {
            _items = new List<object>();
        }

        // Set

        /// <summary>
        /// Sets the specified single item of this instance.
        /// </summary>
        /// <param name="item">The item to apply to this instance.</param>
        /// <param name="appScope">The application scope to consider.</param>
        /// <remarks>Items of this instance must be allowed and must not be forbidden. Otherwise, the values will be the default ones..</remarks>
        public virtual void SetItem(
            object item,
            IAppScope appScope = null)
        {
            ClearItems();
            AddItem(item, appScope);
        }

        /// <summary>
        /// Sets the specified single item of this instance.
        /// </summary>
        /// <param name="items">The items to apply to this instance.</param>
        /// <remarks>Items of this instance must be allowed and must not be forbidden. Otherwise, the values will be the default ones..</remarks>
        public void SetItems(List<object> items)
        {
            ClearItems();
            AddItems(items);
        }

        // Add

        /// <summary>
        /// Adds a new single item of this instance.
        /// </summary>
        /// <param name="item">The string item of this instance.</param>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to use.</param>
        /// <param name="log">The log to populate.</param>
        /// <remarks>Items of this instance must be allowed and must not be forbidden. Otherwise, the items will be the default ones..</remarks>
        /// <returns>Returns True if the specified has been well added.</returns>
        public virtual bool AddItem(
            object item,
            IAppScope appScope = null,
            IScriptVariableSet scriptVariableSet = null,
            ILog log = null)
        {
            bool isAdded = false;

            if (item != null)
            {
                if ((item.GetType().IsArray || item is IList) && item is IEnumerable)
                {
                    foreach (object aSubItem in (item as IEnumerable))
                        AddItem(aSubItem);
                }
                else if (Specification == null ||
                    (Specification.MaximumItemNumber == -1)
                    || (_items.Count < Specification.MaximumItemNumber))
                {
                    if (Specification == null
                        && (ValueType == DataValueType.Any || item.GetValueType() == ValueType))
                    {
                        if (item?.GetType().IsEnum == true)
                            item = item.ToString();
                        else if (item is string && ValueType != DataValueType.Any && ValueType != DataValueType.Text)
                            item = GetObjectFromString(item as string, appScope, log);

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
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to use.</param>
        /// <param name="log">The log to populate.</param>
        /// <remarks>Items of this instance must be allowed and must not be forbidden. Otherwise, the items will be the default ones..</remarks>
        public void AddItems(
            List<object> items,
            IAppScope appScope = null,
            IScriptVariableSet scriptVariableSet = null,
            ILog log = null)
        {
            if (items != null)
            {
                foreach (object item in items)
                {
                    AddItem(item, appScope, scriptVariableSet, log);
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
        /// Gets a new item of this instance.
        /// </summary>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="log">The log to populate.</param>
        /// <returns>Returns a new object of this instance.</returns>
        public abstract object NewItem(IAppScope appScope = null, ILog log = null);

        /// <summary>
        /// Creates a new specification of this instance.
        /// </summary>
        /// <returns>Returns True .</returns>
        public abstract bool NewSpecification();

        /// <summary>
        /// Gets the items of this instance.
        /// </summary>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to use.</param>
        /// <param name="log">The log to populate.</param>
        /// <returns>Returns the items of this instance.</returns>
        public List<object> GetItems(
            IAppScope appScope = null,
            IScriptVariableSet scriptVariableSet = null,
            ILog log = null)
        {
            log= (log?? new Log());

            object object1 = null;
            List<object> items = new List<object>();
            switch(ItemizationMode)
            {
                case DataItemizationMode.Valued:
                    items = _items;
                    break;
                case DataItemizationMode.Referenced:
                    if (appScope == null)
                        log.AddError(title: "Application scope missing");
                    else if (appScope.ScriptInterpreter == null)
                        log.AddError(title: "Script interpreter missing");
                    else if (ItemReference == null)
                        log.AddWarning(title: "Reference missing");
                    else
                        SetItem(ItemReference.Get(appScope, scriptVariableSet, log));
                    break;
                case DataItemizationMode.Script:
                    if (appScope == null)
                        log.AddError(title: "Application scope missing");
                    else if (appScope.ScriptInterpreter == null)
                        log.AddError(title: "Script interpreter missing");
                    else if (string.IsNullOrEmpty(ItemScript))
                        log.AddWarning(title: "Script missing");
                    else
                    {
                        object1 = appScope.ScriptInterpreter.Interprete(ItemScript, scriptVariableSet, log);
                        if (object1 != null)
                            if (object1.GetType().IsArray)
                                items = object1 as List<object>;
                            else
                                items = new List<object>() { object1 };
                    }
                    break;
            }

            return items;
        }

        /// <summary>
        /// Returns the specified item of this instance.
        /// </summary>
        /// <param name="indexItem">The index item to consider.</param>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to use.</param>
        /// <param name="log">The log to populate.</param>
        /// <returns>Returns the specified item of this instance.</returns>
        public virtual object GetItem(
            object indexItem = null,
            IAppScope appScope = null,
            IScriptVariableSet scriptVariableSet = null,
            ILog log = null)
        {
            List<object> objects = GetItems(appScope, scriptVariableSet, log);

            if (objects == null)
            {
                return null;
            }
            else if (indexItem == null)
            {
                return objects.Count > 0 ? objects[0] : null;
            }
            else if (indexItem is int)
            {
                int index = (indexItem as int?).Value;
                return objects.Count > index ? objects[objects.Count - 1] : objects[index - 1];
            }

            return null;
        }

        /// <summary>
        /// Gets the item object of this instance.
        /// </summary>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to use.</param>
        /// <param name="log">The log to populate.</param>
        /// <returns>Returns the item object.</returns>
        public object GetItemObject(
            IAppScope appScope = null,
            IScriptVariableSet scriptVariableSet = null,
            ILog log = null)
        {
            if (Specification?.IsValueList != true)
                return GetItem(null, appScope, scriptVariableSet, log);
            else
                return GetItems(appScope, scriptVariableSet, log);
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

        // Conversion ---------------------------

        /// <summary>
        /// Returns the string value from an object based on this instance's specification.
        /// </summary>
        /// <param name="object1">The object value to convert.</param>
        /// <param name="log">The log to populate.</param>
        /// <returns>The result string.</returns>
        public virtual string GetStringFromObject(
            object object1,
            ILog log = null)
        {
            return "";
        }

        /// <summary>
        /// Returns the object value from a based on this instance's specification.
        /// </summary>
        /// <param name="stringValue">The string value to consider.</param>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="log">The log to populate.</param>
        /// <returns>The result object.</returns>
        public virtual object GetObjectFromString(
            string stringValue,
            IAppScope appScope = null,
            ILog log = null)
        {
            return null;
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
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to use.</param>
        /// <returns>ILog of the operation.</returns>
        /// <remarks>Put reference collections as null if you do not want to repair this instance.</remarks>
        public override ILog Update<T>(
            T item = default,
            string[] specificationAreas = null,
            UpdateMode[] updateModes = null,
            IAppScope appScope = null,
            IScriptVariableSet scriptVariableSet = null)
        {
            IILog log = new Log();

            if (item is DataElement)
            {
                DataElement element = item as DataElement;

                if (specificationAreas == null)
                    specificationAreas = new [] { nameof(DataAreaKind.Any) };

                if ((specificationAreas.Contains(nameof(DataAreaKind.Any))) ||
                    (specificationAreas.Contains(nameof(DataAreaKind.Constraints))))
                {
                }

                if ((specificationAreas.Contains(nameof(DataAreaKind.Any))) ||
                    (specificationAreas.Contains(nameof(DataAreaKind.Design))))
                {
                }

                if ((specificationAreas.Contains(nameof(DataAreaKind.Any))) ||
                    (specificationAreas.Contains(nameof(DataElementAreaKind.Element))))
                {
                    Name = element.Name;
                    Title = element.Title;
                    Description = element.Description;
                    Index = element.Index;
                }

                if ((specificationAreas.Contains(nameof(DataAreaKind.Any))) ||
                    (specificationAreas.Contains(nameof(DataAreaKind.Items))))
                {
                    if (element != null)
                    {
                        ItemScript = element.ItemScript;

                        _items = new List<object>();
                        foreach (object currentItem in element.Items)
                            _items.Add(currentItem);
                    }
                }

                if ((specificationAreas.Contains(nameof(DataAreaKind.Any))) ||
                    (specificationAreas.Contains(nameof(DataAreaKind.Properties))))
                {
                }
            }
            else
                _items = GetItems(appScope, scriptVariableSet, log);

            return log;
        }

        /// <summary>
        /// Checks this instance.
        /// </summary>
        /// <param name="isExistenceChecked">Indicates whether the carrier existence is checked.</param>
        /// <param name="item">The item to consider.</param>
        /// <param name="specificationAreas">The specification areas to consider.</param>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to use.</param>
        /// <returns>Returns the check log.</returns>
        public override ILog Check<T>(
            bool isExistenceChecked = true,
            T item = default,
            string[] specificationAreas = null,
            IAppScope appScope = null,
            IScriptVariableSet scriptVariableSet = null)
        {
            IILog log = new Log();

            if (item is DataElement)
            {
                if (specificationAreas == null)
                    specificationAreas = new [] { nameof(DataAreaKind.Any) };

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
                        foreach (object item in Items)
                        {
                            log.AddEvents(Specification.ConstraintStatement.CheckItem(aItem, this, true, appScope));
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
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to use.</param>
        /// <returns>ILog of the operation.</returns>
        public override ILog Repair<T>(
            T item = default,
            string[] specificationAreas = null,
            UpdateMode[] updateModes = null,
            IAppScope appScope = null,
            IScriptVariableSet scriptVariableSet = null)
        {
            IILog log = new Log();

            if (item is DataElement)
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
                elementSpecificationAreas = new [] { nameof(DataAreaKind.Any) };

            DataElement cloneDataElement = base.Clone() as DataElement;

            if (ItemReference != null)
                cloneDataElement.ItemReference = ItemReference.Clone() as IDataReference;
            if (Specification != null)
                cloneDataElement.Specification = Specification.Clone() as IDataElementSpec;

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
        public override void UpdateStorageInfo(ILog log = null)
        {
            _propertyDetail?.UpdateStorageInfo(log);

            ItemReference?.UpdateStorageInfo(log);

            // we serialize items

            const string root = "<?xml version=\"1.0\" encoding=\"utf-16\"?>";
            const string xsd = " xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"";
            const string xsi = " xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"";
            const string xmlns = " xmlns=\"http://www.w3.org/2001/bdo.xsd\"";

            if (Items?.Count > 0)
            {
                string st = "";
                if (ValueType.IsScalar())
                {
                    st = "<values>";
                    foreach (object item in Items)
                        st += "<add>" + (GetStringFromObject(item, log) ?? "") + "</add>";
                    st += "</values>";
                }
                else
                {
                    st = "<objects>";
                    foreach (object item in Items)
                        st += (GetStringFromObject(item, log) ?? "").Replace(root, "").Replace(xsd, "").Replace(xsi, "").Replace(xmlns, "");//.Replace(xmlnsEmpty,"");
                    st += "</objects>";
                }

                ItemXElement = XElement.Parse(st, LoadOptions.SetBaseUri);
                //XNamespace aXNamespace = "http://meltingsoft.com/bindopen/xsd";
                if (ItemXElement != null)
                    ItemXElement.Name = ItemXElement.Name.LocalName;
            }
        }

        /// <summary>
        /// Updates information for runtime.
        /// </summary>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="log">The log to update.</param>
        public override void UpdateRuntimeInfo(IAppScope appScope = null,  ILog log = null)
        {
                _propertyDetail?.UpdateRuntimeInfo(appScope, log);

                ItemReference?.UpdateRuntimeInfo(appScope, log);

            if (ItemXElement != null)
            {
                Items = new List<object>();
                foreach (XElement subXElement in ItemXElement.Elements())
                {
                    subXElement.Add(
                        new XAttribute(XNamespace.Xmlns + "xsd", "http://www.w3.org/2001/XMLSchema")
                        , new XAttribute(XNamespace.Xmlns + "xsi", "http://www.w3.org/2001/XMLSchema-instance"));
                    XNamespace aXNamespace = "http://meltingsoft.com/bindopen/xsd";
                    subXElement.Name = aXNamespace + subXElement.Name.LocalName;

                    string xElementString =
                        "<?xml version=\"1.0\" encoding=\"utf-8\"?>" + subXElement.ToString().Replace(" xmlns=\"\"", "");

                    AddItem(GetObjectFromString(xElementString, appScope, log));
                }
            }
            else if (Items != null)
            {
                for (int i = 0; i < Items.Count; i++)
                {
                    object item = Items[i];
                    DataValueType itemValueType = DataValueType.Any;
                    if (item != null && (itemValueType = item.GetValueType()) != ValueType)
                    {
                        if ((ValueType != DataValueType.Any)
                           && (itemValueType == DataValueType.Text || ValueType == DataValueType.Text))
                        {
                            if (itemValueType == DataValueType.Text)
                                item = GetObjectFromString(item as string, appScope, log);
                            else
                                item = item.ToString();
                            Items.RemoveAt(i);
                            Items.Insert(i, item);
                        }
                    }
                }
            }
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
            if (Items.Count>0)
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
