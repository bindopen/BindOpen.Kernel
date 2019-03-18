using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Xml.Serialization;
using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.Data.Common;
using BindOpen.Framework.Core.Data.Elements._Object;
using BindOpen.Framework.Core.Data.Elements.Carrier;
using BindOpen.Framework.Core.Data.Elements.Complex;
using BindOpen.Framework.Core.Data.Elements.Document;
using BindOpen.Framework.Core.Data.Elements.Entity;
using BindOpen.Framework.Core.Data.Elements.Scalar;
using BindOpen.Framework.Core.Data.Elements.Source;
using BindOpen.Framework.Core.Data.Helpers.Objects;
using BindOpen.Framework.Core.Data.Items.Dictionary;
using BindOpen.Framework.Core.Data.Items.Sets;
using BindOpen.Framework.Core.Extensions.Attributes;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Core.System.Scripting;

namespace BindOpen.Framework.Core.Data.Elements.Sets
{

    /// <summary>
    /// This class represents a data element set.
    /// </summary>
    [Serializable()]
    [XmlRoot(ElementName = "element.set", Namespace = "http://meltingsoft.com/bindopen/xsd", IsNullable = false)]
    public class DataElementSet : GenericDataItemSet<DataElement>
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// Elements of this instance.
        /// </summary>
        [XmlElement("carrier", typeof(CarrierElement))]
        [XmlElement("document", typeof(DocumentElement))]
        [XmlElement("entity", typeof(EntityElement))]
        [XmlElement("object", typeof(ObjectElement))]
        [XmlElement("meta", typeof(MetaDataElement))]
        [XmlElement("scalar", typeof(ScalarElement))]
        [XmlElement("source", typeof(SourceElement))]
        public List<DataElement> Elements
        {
            get { return this._Items; }
            set { this._Items = value; }
        }

        /// <summary>
        /// Specification of the Elements property of this instance.
        /// </summary>
        [XmlIgnore()]
        public Boolean ElementsSpecified
        {
            get
            {
                return this._Items != null && this._Items.Count > 0;
            }
        }

        /// <summary>
        /// Returns the element with the specified key.
        /// </summary>
        [XmlIgnore()]
        public new DataElement this[String key]
        {
            get
            {
                return this.GetItem(key);
            }
        }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the DataElementSet class.
        /// </summary>
        public DataElementSet()
        {
        }

        /// <summary>
        /// Instantiates a new instance of the DataElementSet class.
        /// </summary>
        /// <param name="description">The description to consider.</param>
        /// <param name="items">The items to consider.</param>
        public DataElementSet(DictionaryDataItem description, params DataElement[] items) : base(description, items)
        {
        }

        /// <summary>
        /// Instantiates a new instance of the DataElementSet class.
        /// </summary>
        /// <param name="items">The items to consider.</param>
        public DataElementSet(params DataElement[] items) : base(items)
        {
        }

        #endregion

        // ------------------------------------------
        // MUTATORS
        // ------------------------------------------

        #region Mutators

        /// <summary>
        /// Adds a new element.
        /// </summary>
        /// <param name="dataElement">The new element to add.</param>
        /// <param name="referenceElementSet">The reference set of elements to consider.</param>
        /// <returns>Returns the new element that has been added. Returns null if the element has not been added.</returns>
        /// <remarks>The new element must have a name.</remarks>
        public DataElement AddElement(
            DataElement dataElement,
            DataElementSet referenceElementSet = null)
        {
            if ((dataElement == null) || (dataElement.Name == null))
                return null;

            if (this._Items == null)
                this._Items = new List<DataElement>();
            this.Elements.RemoveAll(p => p.KeyEquals(dataElement));

            if (referenceElementSet?.HasItem(dataElement.Key()) != false)
            {
                this.OnPropertyChanged("Elements");
                this.Elements.Add(dataElement);
                return dataElement;
            }

            return null;
        }

        /// <summary>
        /// Adds the specified element.
        /// </summary>
        /// <param name="elementUniqueName">The key of the element to add.</param>
        /// <param name="item">The item of the element to add.</param>
        /// <param name="valueType">The value type of the element to add.</param>
        /// <param name="referenceElementSet">The reference set of elements to consider.</param>
        /// <returns>Returns the new element that has been added.
        /// Returns null if the new element is null or else its name is null.</returns>
        /// <remarks>The new element must have a name.</remarks>
        public DataElement AddElement(
            String elementUniqueName,
            Object item,
            DataValueType valueType = DataValueType.Any,
            DataElementSet referenceElementSet = null)
        {
            return this.AddElement(elementUniqueName, new List<Object> { item }, valueType, referenceElementSet);
        }

        /// <summary>
        /// Adds the specified element.
        /// </summary>
        /// <param name="elementUniqueName">The key of the element to add.</param>
        /// <param name="items">The items of the element to add.</param>
        /// <param name="valueType">The value type of the element to add.</param>
        /// <param name="referenceElementSet">The reference set of elements to consider.</param>
        /// <returns>Returns the new element that has been added.
        /// Returns null if the new element is null or else its name is null.</returns>
        /// <remarks>The new element must have a name.</remarks>
        public DataElement AddElement(
            String elementUniqueName,
            List<Object> items,
            DataValueType valueType = DataValueType.Any,
            DataElementSet referenceElementSet = null)
        {
            DataElement dataElement = null;
            if (referenceElementSet == null)
            {
                this.AddElement(dataElement = DataElement.Create(items, valueType, elementUniqueName));
            }
            else
            {
                DataElement referenceDataElement = referenceElementSet.GetItem(elementUniqueName);
                if (referenceDataElement is DataElement)
                {
                    dataElement = referenceDataElement.Clone() as DataElement;
                    dataElement.SetItems(items);
                }
            }

            return dataElement;
        }

        // Remove an element.
        /// <summary>
        /// Removes the element with the specified name.
        /// </summary>
        /// <param name="key">The key of the element to remove.</param>
        public void RemoveElement(String key)
        {
            if (key == null || this.Elements == null) return;

            if (this.Elements.RemoveAll(p => p.KeyEquals(key)) > 0)
                this.OnPropertyChanged("Elements");
        }

        // Element items ------------------------

        /// <summary>
        /// Returns the item of this instance.
        /// </summary>
        /// <param name="elementName">The element name to consider.</param>
        /// <param name="item">The item to consider.</param>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to use.</param>
        /// <param name="log">The log to populate.</param>
        /// <returns>Indicates whether the item has been set.</returns>
        public virtual Boolean AddElementItem(
            String elementName,
            Object item = null,
            IAppScope appScope = null,
            ScriptVariableSet scriptVariableSet = null,
            Log log = null)
        {
            DataElement dataElement = this.GetItem(elementName);
            if (dataElement != null)
                return dataElement.AddItem(item, appScope, scriptVariableSet, log);

            return false;
        }

        /// <summary>
        /// Returns the items of this instance.
        /// </summary>
        /// <param name="elementName">The element name to consider.</param>
        /// <param name="items">The items to consider.</param>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to use.</param>
        /// <param name="log">The log to populate.</param>
        /// <returns>Returns the items of this instance.</returns>
        public virtual List<Object> AddElementItems(
            String elementName,
            List<Object> items = null,
            IAppScope appScope = null,
            ScriptVariableSet scriptVariableSet = null,
            Log log = null)
        {
            DataElement dataElement = this.GetItem(elementName);
            dataElement?.AddItems(items, appScope, scriptVariableSet, log);
            return new List<Object>();
        }

        #endregion

        // ------------------------------------------
        // ACCESSORS
        // ------------------------------------------

        #region Accessors

        // Creations --------------------------------

        /// <summary>
        /// Instantiates a new instance of the DataElementSet class.
        /// </summary>
        /// <param name="detail">The detail table.</param>
        public static DataElementSet Create(String[][] detail)
        {
            DataElementSet dataElementSet = new DataElementSet();
            if (detail != null)
            {
                foreach (String[] strings in detail)
                {
                    dataElementSet.AddElement(strings[0], strings[1], DataValueType.Text);
                }
            }

            return dataElementSet;
        }

        /// <summary>
        /// Creates a new instance of the DataElementSet class.
        /// </summary>
        /// <param name="stringObject">The string to consider.</param>
        /// <returns>The collection.</returns>
        public static DataElementSet Create(string stringObject)
        {
            DataElementSet dataElementSet = new DataElementSet();
            if (stringObject != null)
            {
                foreach (String aSubString in stringObject.Split('|'))
                    if (aSubString.IndexOf("=") > 0)
                    {
                        int i = aSubString.IndexOf("=");
                        dataElementSet.AddElement(
                            new ScalarElement(
                                aSubString.Substring(0, i),
                                DataValueType.Text,
                                aSubString.Substring(i + 1)));
                    }
            }
            return dataElementSet;
        }

        /// <summary>
        /// Creates a data element set from a dynamic object.
        /// </summary>
        /// <param name="dynamicObject">The objet to consider.</param>
        public static DataElementSet Create(DynamicObject dynamicObject)
        {
            DataElementSet dataElementSet = new DataElementSet();
            if (dynamicObject != null)
                foreach (PropertyInfo updatePropertyInfo in dynamicObject.GetType().GetProperties())
                    //if (updatePropertyInfo.PropertyType.GetValueType().IsScalar())
                    {
                        String propertyName = updatePropertyInfo.Name;
                        Object propertyValue = updatePropertyInfo.GetValue(dynamicObject);

                        dataElementSet.AddElement(DataElement.Create(propertyValue, DataValueType.Any, propertyName));
                    }

            return dataElementSet;
        }

        /// <summary>
        /// Creates a data element set from a dynamic object.
        /// </summary>
        /// <param name="aObject">The objet to consider.</param>
        public static DataElementSet Create<T>(Object aObject) where T : DataElementAttribute
        {
            DataElementSet dataElementSet = new DataElementSet();
            if (aObject != null)
                foreach (PropertyInfo propertyInfo in aObject.GetType().GetProperties())
                //if (updatePropertyInfo.PropertyType.GetValueType().IsScalar())
                {
                    DataElementAttribute attribute = propertyInfo.GetCustomAttribute(typeof(T)) as DetailPropertyAttribute;

                    if (attribute != null)
                    {
                        String name = attribute.Name;

                        String propertyName = propertyInfo.Name;
                        Object propertyValue = propertyInfo.GetValue(aObject);

                        dataElementSet.AddElement(DataElement.Create(propertyValue, DataValueType.Any, propertyName));
                    }
                }

            return dataElementSet;
        }

        // Elements -----------------------------

        /// <summary>
        /// Checks if this instance has an element with the specified key.
        /// </summary>
        /// <param name="key">The key of the element to check.</param>
        /// <returns>Returns true if the instance has an element with the specified name.</returns>
        public new Boolean HasItem(String key)
        {
            return Elements?.Any(p => p.KeyEquals(key)) == true;
        }

        /// <summary>
        /// Returns the item with the specified key.
        /// </summary>
        /// <param name="key">The key of the element to return.</param>
        /// <returns>Returns the element with the specified key.</returns>
        public override DataElement GetItem(String key)
        {
            return this.Elements?.FirstOrDefault(p =>
                p.KeyEquals(key)
                || (p.Specification != null && p.Specification.Aliases != null
                    && p.Specification.Aliases.Any(q => q.KeyEquals(key))));
        }

        /// <summary>
        /// Gets the common keys with the specified set of elements.
        /// </summary>
        /// <param name="dataElementSet">The collection to consider.</param>
        /// <returns>The names of the common object elements with the specified set of elements.</returns>
        public List<String> GetCommonItemKeys(DataElementSet dataElementSet)
        {
            List<String> fieldNames = new List<String>();
            if (dataElementSet == null)
                return fieldNames;

            // To repair

            if (dataElementSet.Elements != null)
            {
                foreach (DataElement currentDataItem in dataElementSet.Elements)
                {
                    fieldNames = this._Items.Where(p => this.HasItem(p.Key())).Select(p => p.Key()).Distinct().ToList();

                    if (this.HasItem(currentDataItem.Key()))
                    {
                        fieldNames.Add(currentDataItem.Name);
                    }
                }
            }

            return fieldNames;
        }

        /// <summary>
        /// Gets the items with the specified group ID.
        /// </summary>
        /// <param name="groupId">The ID of the item group.</param>
        /// <returns>Returns items with the specified group ID.</returns>
        public List<DataElement> GetItemsWithGroupId(string groupId)
        {
            return this.Elements?.Where(p => p.Specification?.GroupId.KeyEquals(groupId) == true).ToList();
        }

        /// <summary>
        /// Returns the item with the specified name and group ID.
        /// </summary>
        /// <param name="key">The key of the item to return.</param>
        /// <param name="groupId">The ID of the group of the item to return.</param>
        /// <returns>Returns the item with the specified name and group ID.</returns>
        public DataElement GetItem(string key, string groupId)
        {
            return Elements?.FirstOrDefault(p =>
                p.KeyEquals(key)
                && (p.Specification?.GroupId.KeyEquals(groupId) != false));
        }

        // Groups -------------------------------

        /// <summary>
        /// Gets all the element groups IDs.
        /// </summary>
        /// <returns>Returns all the element group IDs.</returns>
        public List<String> GetGroupIds()
        {
            if (this.Elements == null) return new List<String>();
            return this.Elements.Select(p => p.Specification?.GroupId).Distinct().ToList();
        }

        // Element items ------------------------

        /// <summary>
        /// Returns the item of this instance.
        /// </summary>
        /// <param name="elementName">The element name to consider.</param>
        /// <param name="indexItem">The index item to consider.</param>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to use.</param>
        /// <param name="log">The log to populate.</param>
        /// <returns>Returns the items of this instance.</returns>
        public virtual Object GetElementItem(
            String elementName = null,
            Object indexItem = null,
            IAppScope appScope = null,
            ScriptVariableSet scriptVariableSet = null,
            Log log = null)
        {
            DataElement dataElement = (elementName != null ? this.GetItem(elementName) : this.Elements[0]);
            if (dataElement != null)
                return dataElement.GetItem(indexItem, appScope, scriptVariableSet);
            return null;
        }

        /// <summary>
        /// Returns the items of this instance.
        /// </summary>
        /// <param name="elementName">The element name to consider.</param>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to use.</param>
        /// <param name="log">The log to populate.</param>
        /// <returns>Returns the items of this instance.</returns>
        public virtual List<Object> GetElementItems(
            String elementName,
            IAppScope appScope = null,
            ScriptVariableSet scriptVariableSet = null,
            Log log = null)
        {
            DataElement dataElement = this.GetItem(elementName);
            if (dataElement != null)
                return dataElement.GetItems(appScope, scriptVariableSet);
            return new List<Object>();
        }

        /// <summary>
        /// Returns the element item object of the specified element of this instance.
        /// </summary>
        /// <param name="elementName">The element name to consider.</param>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to use.</param>
        /// <param name="log">The log to populate.</param>
        /// <returns>Returns the items of this instance.</returns>
        public virtual Object GetElementItemObject(
            String elementName,
            IAppScope appScope = null,
            ScriptVariableSet scriptVariableSet = null,
            Log log = null)
        {
            DataElement dataElement = (elementName != null ? this.GetItem(elementName) :
                (this.Elements.Count > 0 ? this.Elements[0] : null));
            if (dataElement != null)
                return dataElement.GetItemObject(appScope, scriptVariableSet, log);

            return null;
        }

        /// <summary>
        /// Returns the item objects of all the elements of this instance.
        /// </summary>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to use.</param>
        /// <param name="log">The log to populate.</param>
        /// <returns>Returns the items of this instance.</returns>
        public virtual List<Object> GetElementItemObjects(
            IAppScope appScope = null,
            ScriptVariableSet scriptVariableSet = null,
            Log log = null)
        {
            if (this.Elements == null) return new List<Object>();

            return this.Elements.Select(p => p.GetItemObject()).ToList();
        }

        // General ------------------------------

        /// <summary>
        /// Gets the available indexes.
        /// </summary>
        /// <param name="maxIndex">The maximum index to consider.</param>
        /// <returns>Returns the avaible indexes.</returns>
        public List<int> GetAvailableIndexes(int maxIndex)
        {
            List<int> availableIndexes = new List<int>();
            for (int i = 1; i <= maxIndex; i++)
                availableIndexes.Add(i);
            if (this.Elements != null)
            {
                int[] indexes = this.Elements.Select(q => q.Index).ToArray();
                availableIndexes.RemoveAll(p => indexes.Contains(p));
            }

            return availableIndexes;
        }

        /// <summary>
        /// Gets the specified title.
        /// </summary>
        /// <param name="key">The key to consider.</param>
        /// <param name="variantName">The variant variant name to consider.</param>
        /// <param name="defaultVariantName">The default variant name to consider.</param>
        /// <param name="parameters">The parameters to consider.</param>
        /// <returns>Returns the specified label.</returns>
        public String GetTitleLabel(
            String key,
            String variantName = "*",
            String defaultVariantName = "*",
            String[] parameters = null)
        {
            String label = "";

            DataElement dataElement = this.GetItem(key);
            if (dataElement != null)
            {
                label = dataElement.GetTitleText(variantName, defaultVariantName);
                if (parameters != null)
                    for (int k = 0; k < parameters.Length; k++)
                        label = label.Replace("{" + k + "}", parameters[k]);
            }

            return label;
        }

        /// <summary>
        /// Gets the specified description.
        /// </summary>
        /// <param name="key">The key to consider.</param>
        /// <param name="variantName">The variant variant name to consider.</param>
        /// <param name="defaultVariantName">The default variant name to consider.</param>
        /// <param name="parameters">The parameters to consider.</param>
        /// <returns>Returns the specified label.</returns>
        public String GetDescriptionLabel(
            String key,
            String variantName = "*",
            String defaultVariantName = "*",
            String[] parameters = null)
        {
            String label = "";

            DataElement dataElement = this.GetItem(key);
            if (dataElement != null)
            {
                label = dataElement.GetDescriptionText(variantName, defaultVariantName);
                if (parameters != null)
                    for (int k = 0; k < parameters.Length; k++)
                        label = label.Replace("{" + k + "}", parameters[k]);
            }

            return label;
        }

        ///// <summary>
        ///// Gets the event kind of the element with the specified name.
        ///// </summary>
        ///// <param name="key">The name of the element to return.</param>
        ///// <returns>Returns the criticity of the element with the specified name.</returns>
        //public EventKind GetElementEventKind(String key)
        //{
        //    DataElement dataElement = this.GetItemWithName(key);
        //    return (dataElement == null ? EventKind.None : dataElement.EventKind);
        //}

        #endregion

        // --------------------------------------------------
        // EXPORTING
        // --------------------------------------------------

        #region Exporting

        /// <summary>
        /// Returns a text node representing this instance.
        /// </summary>
        /// <param name="nodeName">Name of the text node.</param>
        /// <param name="indent">Tabulation indent to include in the text.</param>
        /// <returns></returns>
        public String GetTextNode(
            String nodeName,
            String indent)
        {
            String st = "";

            st += indent + nodeName + "\n";
            st += "\t" + indent + nodeName + ":elements" + "\n";
            if (this.Elements != null)
                foreach (DataElement dataItem in this.Elements)
                    st += dataItem.GetTextNode(nodeName + ":elements:element", "\t\t" + indent);
            return st;
        }

        #endregion

        // --------------------------------------------------
        // SORTING
        // --------------------------------------------------

        #region Sorting

        /// <summary>
        /// Returns a text node representing this instance.
        /// </summary>
        /// <param name="groupId">ID of the group to consider. Null if all.</param>
        /// <returns>Returns the sorted list of data elements.</returns>
        public List<DataElement> Sort(String groupId = null)
        {
            List<DataElement> sortedDataItems = new List<DataElement>();

            if (this.Elements != null)
                foreach (DataElement currentDataElement in this.Elements)
                    if ((groupId == null) ||
                        (currentDataElement.Specification != null && currentDataElement.Specification.GroupId == groupId))
                    {
                        int currentIndex = 0;
                        foreach (DataElement currentSortedDataElement in this.Elements)
                        {
                            if (currentDataElement.Index < currentSortedDataElement.Index)
                                break;
                            currentIndex += 1;
                        }
                        if (currentIndex > this.Elements.Count - 1)
                            sortedDataItems.Add(currentDataElement);
                        else
                            sortedDataItems.Insert(currentIndex, currentDataElement);
                    }
            return sortedDataItems;
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
            DataElementSet dataElementSet = this.MemberwiseClone() as DataElementSet;
            if (this.Description != null)
                dataElementSet.Description = this.Description.Clone() as DictionaryDataItem;
            dataElementSet._Items = this.Elements?.Select(p => p.Clone() as DataElement).ToList();

            return dataElementSet;
        }

        #endregion
    }
}

