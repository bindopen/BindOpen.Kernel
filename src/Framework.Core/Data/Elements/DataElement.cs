using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.Data.Common;
using BindOpen.Framework.Core.Data.Elements._Object;
using BindOpen.Framework.Core.Data.Elements.Carrier;
using BindOpen.Framework.Core.Data.Elements.Complex;
using BindOpen.Framework.Core.Data.Elements.Document;
using BindOpen.Framework.Core.Data.Elements.Entity;
using BindOpen.Framework.Core.Data.Elements.Scalar;
using BindOpen.Framework.Core.Data.Elements.Schema;
using BindOpen.Framework.Core.Data.Elements.Sets;
using BindOpen.Framework.Core.Data.Elements.Source;
using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.Data.References;
using BindOpen.Framework.Core.Data.Specification;
using BindOpen.Framework.Core.Data.Specification.Design;
using BindOpen.Framework.Core.Extensions.Configuration.Routines;
using BindOpen.Framework.Core.Extensions.Definition.Carriers;
using BindOpen.Framework.Core.Extensions.Definition.Connectors;
using BindOpen.Framework.Core.Extensions.Definition.Entities;
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
    public abstract class DataElement : IndexedDataItem
    {
        // --------------------------------------------------
        // VARIABLES
        // --------------------------------------------------

        #region Variables

        private DataItemizationMode _itemizationMode = DataItemizationMode.Any;

        private DataReference _ItemReference = null;

        private List<Object> _Items = null;
        private XElement _ItemXElement = null;

        // Specification ------------------------------------

        private DataElementSpec _Specification = null;

        // Properties ---------------------------------------

        private DataElementSet _PropertyDetail = null;
        private EventKind? _EventKind = null;

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
            get { return (this._itemizationMode != DataItemizationMode.Any ? this._itemizationMode :
                    (!String.IsNullOrEmpty(this.ItemScript) ? DataItemizationMode.Script :
                    (this._ItemReference!=null ? DataItemizationMode.Referenced : DataItemizationMode.Valued))); }
            set { this._itemizationMode = value; }
        }

        /// <summary>
        /// Specification of the ItemizationMode property of this instance.
        /// </summary>
        [XmlIgnore()]
        public Boolean ItemizationModeSpecified
        {
            get
            {
                return this.ItemizationMode != DataItemizationMode.Valued;
            }
        }

  //      /// <summary>
  //      /// The full class name of this intance.
  //      /// </summary>
  //      [XmlIgnore()]
  //      public String ClassFullName
        //{
  //          get
  //          {
  //              return (this is EntityElement ? (this as EntityElement).EntityUniqueName : "");
  //          }
  //      }

        /// <summary>
        /// Item reference of this instance.
        /// </summary>
        [XmlElement("itemReference")]
        public DataReference ItemReference
        {
            get { return this._ItemReference; }
            set
            {
                this._ItemReference = value;
            }
        }

        /// <summary>
        /// Specification of the ItemReference property of this instance.
        /// </summary>
        [XmlIgnore()]
        public Boolean ItemReferenceSpecified
        {
            get
            {
                return this._ItemReference != null;
            }
        }

        /// <summary>
        /// The script of this instance.
        /// </summary>
        [XmlAttribute("script")]
        public string ItemScript { get; set; } = null;

        /// <summary>
        /// Specification of the ItemScript property of this instance.
        /// </summary>
        [XmlIgnore()]
        public Boolean ItemScriptSpecified => !string.IsNullOrEmpty(this.ItemScript);

        /// <summary>
        /// Items of this instance.
        /// </summary>
        [XmlIgnore()]
        public List<Object> Items
        {
            get
            {
                return this._Items ?? (this._Items = new List<Object>());
            }
            set
            {
                this._Items = value;
            }
        }

        /// <summary>
        /// Items of this instance.
        /// </summary>
        [XmlIgnore()]
        public Object FirstItem
        {
            get
            {
                return (this.Items.Count > 0 ? this.Items[0] : null);
            }
            set
            {
                this._Items = new List<object>() { value };
            }
        }

        // Serialization -------------------------------

        /// <summary>
        /// The item Xml element object of this instance.
        /// </summary>
        [XmlAnyElement]
        public XElement ItemXElement
        {
            get
            {
                return this._ItemXElement;
            }
            set
            {
                this._ItemXElement = value;
            }
        }

        /// <summary>
        /// Specification of the ItemXElement property of this instance.
        /// </summary>
        [XmlIgnore()]
        public Boolean ItemXElementSpecified
        {
            get
            {
                return !this.ValueType.IsScalar() || //this._ValueType != DataValueType.Entity || this._ValueType == DataValueType.Text) &&
                    this._Items != null && this._Items.Count > 1;
            }
        }

        // Specification -------------------------------

        /// <summary>
        /// Specification of this instance.
        /// </summary>
        [XmlIgnore()]
        public DataElementSpec Specification
        {
            get {
                //if (this._Specification == null) this.NewSpecification();
                return this._Specification;
            }
            set { this._Specification = value; }
        }

        /// <summary>
        /// Specification of the Specification property of this instance.
        /// </summary>
        [XmlIgnore()]
        public Boolean SpecificationSpecified
        {
            get
            {
                return this._Specification != null;
            }
        }

        /// <summary>
        /// Returns the element with the specified indexed.
        /// </summary>
        [XmlIgnore()]
        public Object this[int index]
        {
            get
            {
                return (index >= 0 && index < this._Items.Count ? this._Items[index] : null);
            }
        }

        /// <summary>
        /// Returns the element with the specified unique name.
        /// </summary>
        [XmlIgnore()]
        public Object this[String name]
        {
            get
            {
                return this.GetItem(name);
            }
        }

        // Properties -------------------------------

        /// <summary>
        /// Property detail of this instance.
        /// </summary>
        [XmlElement("propertyDetail")]
        public DataElementSet PropertyDetail
        {
            get
            {
                if (this._PropertyDetail == null) this._PropertyDetail = new DataElementSet();
                return this._PropertyDetail;
            }
            set { this._PropertyDetail = value; }
        }

        /// <summary>
        /// Specification of the PropertyDetail property of this instance.
        /// </summary>
        [XmlIgnore()]
        public Boolean PropertyDetailSpecified
        {
            get
            {
                return this._PropertyDetail!=null && (this._PropertyDetail.ElementsSpecified || this._PropertyDetail.DescriptionSpecified);
            }
        }

        /// <summary>
        /// The event kind of this instance.
        /// </summary>
        [XmlElement("eventKind")]
        public EventKind? EventKind
        {
            get { return _EventKind == null ? System.Diagnostics.Events.EventKind.None : this._EventKind.Value; }
            set { this._EventKind = value; }
        }

        /// <summary>
        /// Specification of the EventKind property of this instance.
        /// </summary>
        [XmlIgnore()]
        public Boolean EventKindSpecified
        {
            get
            {
                return this._EventKind != null && this._EventKind == System.Diagnostics.Events.EventKind.None;
            }
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
        protected DataElement(String name = null,
            String namePreffix = null,
            String id = null)
            : base(name, namePreffix, id)
        {
        }

        #endregion

        // --------------------------------------------------
        // ACCESSORS
        // --------------------------------------------------

        #region Accessors

        // Static creators -------------------------

        /// <summary>
        /// Creates a data element of the specified kind.
        /// </summary>
        /// <param name="objects">The objets to consider.</param>
        /// <param name="dataValueType">The data value type to consider.</param>
        /// <param name="name">The name to consider.</param>
        public static DataElement Create(
            List<Object> objects,
            DataValueType dataValueType = DataValueType.Any,
            String name = null)
        {
            if (objects == null) return null;

            if (dataValueType == DataValueType.Any)
                dataValueType = objects.GetValueType();

            DataElement dataElement = null;
            if (dataValueType != DataValueType.Any)
            {
                dataElement = DataElement.Create(dataValueType, name);
                if (dataElement != null)
                    dataElement.SetItems(objects);
            }

            return dataElement;
        }

        /// <summary>
        /// Creates a data element of the specified kind.
        /// </summary>
        /// <param name="object1">The objet to consider.</param>
        /// <param name="dataValueType">The data value type to consider.</param>
        /// <param name="name">The name to consider.</param>
        public static DataElement Create(
            Object object1 = null,
            DataValueType dataValueType = DataValueType.Any,
            String name = null)
        {
            if (dataValueType == DataValueType.Any)
                dataValueType = object1.GetValueType();

            DataElement dataElement = DataElement.Create(dataValueType, name);
            if (dataElement != null && object1 != null)
                dataElement.SetItem(object1);

            return dataElement;
        }

        /// <summary>
        /// Creates a data element of the specified kind.
        /// </summary>
        /// <param name="valueType">The value type to consider.</param>
        /// <param name="name">The name to consider.</param>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="specification">The specification to consider.</param>
        public static DataElement Create(
            DataValueType valueType,
            string name = null,
            IAppScope appScope = null,
            DataElementSpec specification = null)
        {
            DataElement dataElement = null;

            if (valueType.IsScalar())
            {
                dataElement = new ScalarElement(name, "", valueType, specification as ScalarElementSpec);
            }
            else
            {
                string definitionUniqueName = null;
                switch (valueType)
                {
                    case DataValueType.CarrierConfiguration:
                        definitionUniqueName = (specification as CarrierElementSpec)?.DefinitionFilter.GetValues(
                            appScope?.AppExtension.GetItemDefinitionUniqueNames<CarrierDefinition>()).FirstOrDefault();
                        dataElement = new CarrierElement(
                            name, "", definitionUniqueName,
                            specification as CarrierElementSpec);
                        break;
                    case DataValueType.DataSource:
                        definitionUniqueName = (specification as SourceElementSpec)?.ConnectorFilter.GetValues(
                            appScope?.AppExtension.GetItemDefinitionUniqueNames<ConnectorDefinition>()).FirstOrDefault();
                        dataElement = new SourceElement(name, null);
                        break;
                    case DataValueType.Dictionary:
                        //dataElement = new SourceElement(name, namePreffix);
                        break;
                    case DataValueType.Document:
                        dataElement = new DocumentElement(name, null as CarrierElement, null);
                        break;
                    case DataValueType.Entity:
                        definitionUniqueName = (specification as EntityElementSpec)?.EntityFilter.GetValues(
                            appScope?.AppExtension.GetItemDefinitionUniqueNames<EntityDefinition>()).FirstOrDefault();
                        dataElement = new EntityElement(name, "", definitionUniqueName, specification as EntityElementSpec);
                        break;
                    case DataValueType.Object:
                        definitionUniqueName = (specification as ObjectElementSpec)?.ClassFilter.GetValues().FirstOrDefault();
                        dataElement = new ObjectElement(name, "", definitionUniqueName, specification as ObjectElementSpec);
                        break;
                    case DataValueType.Schema:
                        dataElement = new SchemaElement(name);
                        break;
                    case DataValueType.SchemaZone:
                        dataElement = new SchemaZoneElement(name);
                        break;
                    case DataValueType.StringValued:
                        //dataElement = new StringValuedElement(name, namePreffix);
                        break;
                }
            }

            return dataElement;
        }

        /// <summary>
        /// Creates a data element of the specified kind.
        /// </summary>
        /// <param name="type">The value type to consider.</param>
        /// <param name="name">The name to consider.</param>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="specification">The specification to consider.</param>
        public static DataElement Create(
            Type type,
            String name = null,
            IAppScope appScope = null,
            DataElementSpec specification = null)
        {
            if (type == null) return null;

            DataElement dataElement = DataElement.Create(type.GetValueType(), name, appScope, specification);

            if (dataElement?.Specification != null)
            {
                dataElement.Specification.DesignStatement.ControlType = type.GetDefaultControlType();

                if (type.IsArray)
                {
                    dataElement.Specification.MaximumItemNumber = -1;
                }
                else if (type.IsEnum)
                {
                    dataElement.Specification.ConstraintStatement.AddConstraint(
                       null, "standard$" + BasicRoutineKind.ItemMustBeInList, new DataElementSet(
                           DataElement.Create(type.GetFields().Select(p => p.Name).ToList().Cast<Object>(), DataValueType.Text)));
                }
            }

            return dataElement;
        }

        // Specification ---------------------

        /// <summary>
        /// Gets a new specification.
        /// </summary>
        /// <returns>Returns the new specifcation.</returns>
        public virtual DataElementSpec CreateSpecification()
        {
            return null;
        }

        /// <summary>
        /// Creates a new specification of this instance.
        /// </summary>
        /// <returns>Returns True .</returns>
        public Boolean NewSpecification()
        {
            return (this.Specification = this.CreateSpecification())!=null;
        }

        // General ---------------------------

        /// <summary>
        /// Indicates whether this instance is compatible with the specified element.
        /// </summary>
        /// <param name="specification">The data element specification to consider.</param>
        /// <returns>True if this instance is compatible with the specified data elements.</returns>
        public Boolean IsCompatibleWith(DataElementSpec specification)
        {
            if (specification == null)
                return true;

            return specification.IsCompatibleWith(this);
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
            this._Items = new List<Object>();
        }

        // Set

        /// <summary>
        /// Sets the specified single item of this instance.
        /// </summary>
        /// <param name="item">The item to apply to this instance.</param>
        /// <param name="appScope">The application scope to consider.</param>
        /// <remarks>Items of this instance must be allowed and must not be forbidden. Otherwise, the values will be the default ones..</remarks>
        public virtual void SetItem(
            Object item,
            IAppScope appScope = null)
        {
            this.ClearItems();
            this.AddItem(item, appScope);
        }

        /// <summary>
        /// Sets the specified single item of this instance.
        /// </summary>
        /// <param name="items">The items to apply to this instance.</param>
        /// <remarks>Items of this instance must be allowed and must not be forbidden. Otherwise, the values will be the default ones..</remarks>
        public void SetItems(List<Object> items)
        {
            this.ClearItems();
            this.AddItems(items);
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
        public virtual Boolean AddItem(
            Object item,
            IAppScope appScope = null,
            ScriptVariableSet scriptVariableSet = null,
            Log log = null)
        {
            Boolean isAdded = false;

            if (item != null)
            {
                if ((item.GetType().IsArray || item is IList) && item is IEnumerable)
                {
                    foreach (Object aSubItem in (item as IEnumerable))
                        this.AddItem(aSubItem);
                }
                else if (this._Specification == null ||
                    (this._Specification.MaximumItemNumber == -1)
                    || (this._Items.Count < this._Specification.MaximumItemNumber))
                {
                    Log subLog = (this._Specification == null ? null :
                        this._Specification.CheckItem(item, this, appScope, scriptVariableSet));
                    if ((this._Specification == null
                        && (this.ValueType == DataValueType.Any || item.GetValueType() == this.ValueType)) ||
                        (subLog == null || !subLog.HasErrorsOrExceptions()))
                    {
                        if (item != null && item.GetType().IsEnum)
                            item = item.ToString();
                        else if (item is String && this.ValueType != DataValueType.Any && this.ValueType != DataValueType.Text)
                            item = this.GetObjectFromString(item as String, appScope, log);

                        (this._Items ?? (this._Items = new List<object>())).Add(item);
                        isAdded = true;
                    }

                    if (log != null && subLog != null)
                        log.Append(subLog);
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
            List<Object> items,
            IAppScope appScope = null,
            ScriptVariableSet scriptVariableSet = null,
            Log log = null)
        {
            if (items != null)
                foreach (Object aItem in items)
                {
                    this.AddItem(aItem, appScope, scriptVariableSet, log);
                }
        }

        // Switch

        /// <summary>
        /// Switches the single value value1 with the single value value2.
        /// </summary>
        /// <param name="value1">The first single value to switch.</param>
        /// <param name="value2">The second single value to switch.</param>
        public void SwitchItems(Object value1, Object value2)
        {
            Object aTempValue = value1;

            if ((this._Items.IndexOf(value1) > -1) & (this._Items.IndexOf(value2) > -1))
            {
                this._Items[this._Items.IndexOf(value1)] = value2;
                this._Items[this._Items.IndexOf(value2)] = aTempValue;
            }
        }

        /// <summary>
        /// Updates the value value1 with the value value2.
        /// </summary>
        /// <param name="aItem">The item to consider.</param>
        /// <param name="aNewItem">The new item.</param>
        public void UpdateItem(Object aItem, Object aNewItem)
        {
            if (this._Items.Contains(aItem))
                this._Items[this._Items.IndexOf(aItem)] = aNewItem;
        }

        // Remove

        /// <summary>
        /// Removes the specified item.
        /// </summary>
        /// <param name="aItem">The item </param>
        public Boolean RemoveItem(Object aItem)
        {
            return this._Items.RemoveAll(p => p == aItem) > 0;
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
        public virtual Object NewItem(IAppScope appScope = null, Log log = null)
        {
            return null;
        }

        /// <summary>
        /// Gets the items of this instance.
        /// </summary>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to use.</param>
        /// <param name="log">The log to populate.</param>
        /// <returns>Returns the items of this instance.</returns>
        public List<Object> GetItems(
            IAppScope appScope = null,
            ScriptVariableSet scriptVariableSet = null,
            Log log = null)
        {
            log= (log?? new Log());

            Object object1 = null;
            List<Object> items = new List<Object>();
            switch(this.ItemizationMode)
            {
                case DataItemizationMode.Valued:
                    items = this._Items;
                    break;
                case DataItemizationMode.Referenced:
                    if (appScope == null)
                        log.AddError(title: "Application scope missing");
                    else if (appScope.ScriptInterpreter == null)
                        log.AddError(title: "Script interpreter missing");
                    else if (this._ItemReference == null)
                        log.AddWarning(title: "Reference missing");
                    else
                        this.SetItem(this._ItemReference.Get(appScope, scriptVariableSet, log));
                    break;
                case DataItemizationMode.Script:
                    if (appScope == null)
                        log.AddError(title: "Application scope missing");
                    else if (appScope.ScriptInterpreter == null)
                        log.AddError(title: "Script interpreter missing");
                    else if (String.IsNullOrEmpty(this.ItemScript))
                        log.AddWarning(title: "Script missing");
                    else
                    {
                        object1 = appScope.ScriptInterpreter.Interprete(this.ItemScript, scriptVariableSet, log);
                        if (object1 != null)
                            if (object1.GetType().IsArray)
                                items = object1 as List<Object>;
                            else
                                items = new List<Object>() { object1 };
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
        public virtual Object GetItem(
            Object indexItem = null,
            IAppScope appScope = null,
            ScriptVariableSet scriptVariableSet = null,
            Log log = null)
        {
            List<Object> objects = this.GetItems(appScope, scriptVariableSet, log);

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
        public Object GetItemObject(
            IAppScope appScope = null,
            ScriptVariableSet scriptVariableSet = null,
            Log log = null)
        {
            if (this._Specification?.IsValueList != true)
                return this.GetItem(null, appScope, scriptVariableSet, log);
            else
                return this.GetItems(appScope, scriptVariableSet, log);
        }

        /// <summary>
        /// Indicates whether this instance contains the specified scalar item or the specified entity name.
        /// </summary>
        /// <param name="indexItem">The index item to consider.</param>
        /// <param name="isCaseSensitive">Indicates whether the verification is case sensitive.</param>
        /// <returns>Returns true if this instance contains the specified scalar item or the specified entity name.</returns>
        public virtual Boolean HasItem(Object indexItem, Boolean isCaseSensitive = false)
        {
            return false;
        }

        /// <summary>
        /// Returns a text node representing this instance.
        /// </summary>
        /// <returns></returns>
        public override String ToString()
        {
            return "";
        }

        /// <summary>
        /// Check the specified item.
        /// </summary>
        /// <param name="aItem">The item to consider.</param>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to use.</param>
        /// <returns>The log of check log.</returns>
        public Log CheckItem(
            Object aItem = null,
            IAppScope appScope = null,
            ScriptVariableSet scriptVariableSet = null)
        {
            Log log = new Log();

            if ((aItem == null) && (this._Specification != null) && (this._Specification.IsValueList))
                aItem = this.GetItem();

            if (this._Specification != null)
                log = this._Specification.CheckItem(aItem, this, appScope, scriptVariableSet);

            return log;
        }

        // Conversion ---------------------------

        /// <summary>
        /// Returns the string value from an object based on this instance's specification.
        /// </summary>
        /// <param name="object1">The object value to convert.</param>
        /// <param name="log">The log to populate.</param>
        /// <returns>The result string.</returns>
        public virtual String GetStringFromObject(
            Object object1,
            Log log = null)
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
        public virtual Object GetObjectFromString(
            String stringValue,
            IAppScope appScope = null,
            Log log = null)
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
        /// <returns>Log of the operation.</returns>
        /// <remarks>Put reference collections as null if you do not want to repair this instance.</remarks>
        public override Log Update<T>(
            T item = null,
            List<String> specificationAreas = null,
            List<UpdateMode> updateModes = null,
            IAppScope appScope = null,
            ScriptVariableSet scriptVariableSet = null)
        {
            Log log = new Log();

            if (item is DataElement)
            {
                DataElement element = item as DataElement;

                if (specificationAreas == null)
                    specificationAreas = new List<String>() { DataAreaKind.Any.ToString() };

                if ((specificationAreas.Contains(DataAreaKind.Any.ToString())) ||
                    (specificationAreas.Contains(DataAreaKind.Constraints.ToString())))
                {
                }

                if ((specificationAreas.Contains(DataAreaKind.Any.ToString())) ||
                    (specificationAreas.Contains(DataAreaKind.Design.ToString())))
                {
                }

                if ((specificationAreas.Contains(DataAreaKind.Any.ToString())) ||
                    (specificationAreas.Contains(DataElementAreaKind.Element.ToString())))
                {
                    this.Name = element.Name;
                    this.Title = element.Title;
                    this.Description = element.Description;
                    this.Index = element.Index;
                }

                if ((specificationAreas.Contains(DataAreaKind.Any.ToString())) ||
                    (specificationAreas.Contains(DataAreaKind.Items.ToString())))
                    if (element != null)
                    {
                        this.ItemScript = element.ItemScript;

                        this._Items = new List<object>();
                        foreach (Object currentItem in element.Items)
                            this._Items.Add(currentItem);
                    }

                if ((specificationAreas.Contains(DataAreaKind.Any.ToString())) ||
                    (specificationAreas.Contains(DataAreaKind.Properties.ToString())))
                {
                }
            }
            else
                this._Items = this.GetItems(appScope, scriptVariableSet, log);

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
        public override Log Check<T>(
            Boolean isExistenceChecked = true,
            T item = null,
            List<String> specificationAreas = null,
            IAppScope appScope = null,
            ScriptVariableSet scriptVariableSet = null)
        {
            Log log = new Log();

            if (item is DataElement)
            {
                if (specificationAreas == null)
                    specificationAreas = new List<String>() { DataAreaKind.Any.ToString() };

                if ((specificationAreas.Contains(DataAreaKind.Any.ToString())) ||
                    (specificationAreas.Contains(DataAreaKind.Constraints.ToString())))
                {
                }

                if ((specificationAreas.Contains(DataAreaKind.Any.ToString())) ||
                    (specificationAreas.Contains(DataAreaKind.Design.ToString())))
                {
                }

                if ((specificationAreas.Contains(DataAreaKind.Any.ToString())) ||
                    (specificationAreas.Contains(DataAreaKind.Items.ToString())))
                    if (this._Specification != null)
                        foreach (Object aItem in this.Items)
                            log.AddEvents(this._Specification.ConstraintStatement.CheckItem(aItem, this, true, appScope));

                if ((specificationAreas.Contains(DataAreaKind.Any.ToString())) ||
                    (specificationAreas.Contains(DataAreaKind.Properties.ToString())))
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
        /// <returns>Log of the operation.</returns>
        public override Log Repair<T>(
            T item = null,
            List<String> specificationAreas = null,
            List<UpdateMode> updateModes = null,
            IAppScope appScope = null,
            ScriptVariableSet scriptVariableSet = null)
        {
            Log log = new Log();

            if (item is DataElement)
            {
                if ((specificationAreas.Contains(DataAreaKind.Any.ToString())) ||
                (specificationAreas.Contains(DataAreaKind.Constraints.ToString())))
                {
                }

                if ((specificationAreas.Contains(DataAreaKind.Any.ToString())) ||
                    (specificationAreas.Contains(DataAreaKind.Design.ToString())))
                {
                }

                if ((specificationAreas.Contains(DataAreaKind.Any.ToString())) ||
                    (specificationAreas.Contains(DataElementAreaKind.Element.ToString())))
                {
                    if (this._Specification != null)
                        if (this._Specification.AvailableItemizationModes.Count == 1)
                            this.ItemizationMode = this._Specification.AvailableItemizationModes[0];
                }

                if ((specificationAreas.Contains(DataAreaKind.Any.ToString())) ||
                    (specificationAreas.Contains(DataAreaKind.Items.ToString())))
                {
                }

                if ((specificationAreas.Contains(DataAreaKind.Any.ToString())) ||
                    (specificationAreas.Contains(DataAreaKind.Properties.ToString())))
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
        public override Object Clone()
        {
            return this.Clone(null);
        }

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <param name="elementSpecificationAreas">The data element areas to consider.</param>
        /// <returns>Returns a cloned instance.</returns>
        public virtual Object Clone(List<String> elementSpecificationAreas = null)
        {
            if (elementSpecificationAreas == null)
                elementSpecificationAreas = new List<String>() { DataAreaKind.Any.ToString() };

            DataElement cloneDataElement = base.Clone() as DataElement;

            if (this._ItemReference != null)
                cloneDataElement.ItemReference = this._ItemReference.Clone() as DataReference;
            if (this._Specification != null)
                cloneDataElement.Specification = this._Specification.Clone() as DataElementSpec;

            if (this._PropertyDetail != null)
                if (elementSpecificationAreas.Contains(DataAreaKind.Any.ToString()) || elementSpecificationAreas.Contains(DataAreaKind.Properties.ToString()))
                cloneDataElement.PropertyDetail = this._PropertyDetail.Clone() as DataElementSet;

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
        public override void UpdateStorageInfo(Log log = null)
        {
            if (this._PropertyDetail != null)
                this._PropertyDetail.UpdateStorageInfo(log);

            if (this._ItemReference != null)
                this._ItemReference.UpdateStorageInfo(log);

            // we serialize items

            const string root = "<?xml version=\"1.0\" encoding=\"utf-16\"?>";
            const string xsd = " xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"";
            const string xsi = " xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"";
            const string xmlns = " xmlns=\"http://www.w3.org/2001/bdo.xsd\"";

            if (this.Items?.Count > 0)
            {
                String st = "";
                if (this.ValueType.IsScalar())
                {
                    st = "<values>";
                    foreach (Object item in this.Items)
                        st += "<add>" + (this.GetStringFromObject(item, log) ?? "") + "</add>";
                    st += "</values>";
                }
                else
                {
                    st = "<objects>";
                    foreach (Object item in this.Items)
                        st += (this.GetStringFromObject(item, log) ?? "").Replace(root, "").Replace(xsd, "").Replace(xsi, "").Replace(xmlns, "");//.Replace(xmlnsEmpty,"");
                    st += "</objects>";
                }

                this._ItemXElement = XElement.Parse(st, LoadOptions.SetBaseUri);
                //XNamespace aXNamespace = "http://meltingsoft.com/bindopen/xsd";
                if (this._ItemXElement != null)
                    this._ItemXElement.Name = this._ItemXElement.Name.LocalName;
            }
        }

        /// <summary>
        /// Updates information for runtime.
        /// </summary>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="log">The log to update.</param>
        public override void UpdateRuntimeInfo(IAppScope appScope = null,  Log log = null)
        {
                this._PropertyDetail?.UpdateRuntimeInfo(appScope, log);

                this._ItemReference?.UpdateRuntimeInfo(appScope, log);

            if (this._ItemXElement != null)
            {
                this.Items = new List<object>();
                foreach (XElement subXElement in this._ItemXElement.Elements())
                {
                    subXElement.Add(
                        new XAttribute(XNamespace.Xmlns + "xsd", "http://www.w3.org/2001/XMLSchema")
                        , new XAttribute(XNamespace.Xmlns + "xsi", "http://www.w3.org/2001/XMLSchema-instance"));
                    XNamespace aXNamespace = "http://meltingsoft.com/bindopen/xsd";
                    subXElement.Name = aXNamespace + subXElement.Name.LocalName;

                    String xElementString = 
                        "<?xml version=\"1.0\" encoding=\"utf-8\"?>" + subXElement.ToString().Replace(" xmlns=\"\"", "");

                    this.AddItem(this.GetObjectFromString(xElementString, appScope, log));
                }
            }
            else if (this.Items != null)
                for(int i = 0;i<this.Items.Count;i++)
                {
                    object item = this.Items[i];
                    DataValueType itemValueType = DataValueType.Any;
                    if (item!=null && (itemValueType = item.GetValueType()) != this.ValueType)
                        if ((this.ValueType!= DataValueType.Any) &&
                            (itemValueType == DataValueType.Text || this.ValueType == DataValueType.Text))
                        {
                            if (itemValueType == DataValueType.Text)
                                item = this.GetObjectFromString(item as string, appScope, log);
                            else
                                item = item.ToString();
                            this.Items.RemoveAt(i);
                            this.Items.Insert(i, item);
                        }
                }

        }

        /// <summary>
        /// Gets a text node representing this instance.
        /// </summary>
        /// <param name="nodeName">Name of the tex node.</param>
        /// <param name="indent">Tabulation indent to include in the text.</param>
        /// <returns>Returns the text node.</returns>
        public String GetTextNode(String nodeName, String indent)
        {
            String st = "";

            st += indent + nodeName + "\n";
            st += "\t" + indent + nodeName + ":name=\"" + this.Key() + "\"\n";
            st += "\t" + indent + nodeName + ":valueType=\"" + this.ValueType.ToString() + "\"\n";
            if (this.Items.Count>0)
            {
                st += "\t" + indent + nodeName + ":items\n";
                foreach (String aItemString in this.Items)
                    st += "\t\t" + indent + nodeName + ":items:item=\"" + aItemString + "\"\n";
            }

            return st;
        }

        #endregion
    }
}
