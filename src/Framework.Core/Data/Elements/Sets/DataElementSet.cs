using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.Data.Elements._Object;
using BindOpen.Framework.Core.Data.Elements.Carrier;
using BindOpen.Framework.Core.Data.Elements.Collection;
using BindOpen.Framework.Core.Data.Elements.Document;
using BindOpen.Framework.Core.Data.Elements.Meta;
using BindOpen.Framework.Core.Data.Elements.Scalar;
using BindOpen.Framework.Core.Data.Elements.Source;
using BindOpen.Framework.Core.Data.Helpers.Objects;
using BindOpen.Framework.Core.Data.Items.Dictionary;
using BindOpen.Framework.Core.Data.Items.Sets;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Core.System.Scripting;

namespace BindOpen.Framework.Core.Data.Elements.Sets
{
    /// <summary>
    /// This class represents a data element set.
    /// </summary>
    [Serializable()]
    [XmlRoot(ElementName = "element.set", Namespace = "https://bindopen.org/xsd", IsNullable = false)]
    public class DataElementSet : DataItemSet<DataElement>, IDataElementSet
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
        [XmlElement("object", typeof(ObjectElement))]
        [XmlElement("meta", typeof(MetaDataElement))]
        [XmlElement("scalar", typeof(ScalarElement))]
        [XmlElement("source", typeof(SourceElement))]
        [XmlElement("collection", typeof(CollectionElement))]
        [XmlArrayElement("elements")]
        public List<DataElement> Elements
        {
            get { return _items; }
            set { _items = value; }
        }

        /// <summary>
        /// Specification of the Elements property of this instance.
        /// </summary>
        [XmlIgnore()]
        public bool ElementsSpecified => _items?.Count > 0;

        /// <summary>
        /// Returns the element with the specified key.
        /// </summary>
        [XmlIgnore()]
        public new DataElement this[string key] => GetItem(key) as DataElement;

        // Conversions -----------------------------

        /// <summary>
        /// Converts from data element array.
        /// </summary>
        /// <param name="elements">The elements to consider.</param>
        public static implicit operator DataElementSet(DataElement[] elements)
        {
            return new DataElementSet(elements);
        }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the IDataElementSet class.
        /// </summary>
        public DataElementSet()
        {
        }

        /// <summary>
        /// Instantiates a new instance of the IDataElementSet class.
        /// </summary>
        /// <param name="elements">The elements to consider.</param>
        public DataElementSet(
            params IDataElement[] elements) : base(elements.Cast<DataElement>().ToArray())
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
        /// <param name="element">The new element to add.</param>
        /// <param name="referenceElementSet">The reference set of elements to consider.</param>
        /// <returns>Returns the new element that has been added. Returns null if the element has not been added.</returns>
        /// <remarks>The new element must have a name.</remarks>
        public IDataElement AddElement(
            IDataElement element,
            IDataElementSet referenceElementSet = null)
        {
            if ((element == null) || (element.Name == null))
                return null;

            if (_items == null)
                _items = new List<DataElement>();
            Elements.RemoveAll(p => p.KeyEquals(element));

            if (referenceElementSet?.HasItem(element.Key()) != false)
            {
                OnPropertyChanged("Elements");
                Elements.Add(element as DataElement);
                return element;
            }

            return null;
        }

        // Remove an element.
        /// <summary>
        /// Removes the element with the specified name.
        /// </summary>
        /// <param name="key">The key of the element to remove.</param>
        public void RemoveElement(string key)
        {
            if (key == null || Elements == null) return;

            if (Elements.RemoveAll(p => p.KeyEquals(key)) > 0)
                OnPropertyChanged("Elements");
        }

        // Element items ------------------------

        /// <summary>
        /// Returns the item of this instance.
        /// </summary>
        /// <param name="elementName">The element name to consider.</param>
        /// <param name="item">The item to consider.</param>
        /// <param name="log">The log to populate.</param>
        /// <returns>Indicates whether the item has been set.</returns>
        public virtual bool AddElementItem(
            string elementName,
            object item = null,
            ILog log = null)
        {
            IDataElement element = GetItem(elementName);
            if (element != null)
                return element.AddItem(item, log);

            return false;
        }

        /// <summary>
        /// Returns the items of this instance.
        /// </summary>
        /// <param name="elementName">The element name to consider.</param>
        /// <param name="items">The items to consider.</param>
        /// <param name="log">The log to populate.</param>
        /// <returns>Returns the items of this instance.</returns>
        public virtual List<object> AddElementItems(
            string elementName,
            object[] items = null,
            ILog log = null)
        {
            IDataElement element = GetItem(elementName);
            element?.AddItems(items, log);
            return new List<object>();
        }

        #endregion

        // ------------------------------------------
        // ACCESSORS
        // ------------------------------------------

        #region Accessors

        // Elements -----------------------------

        /// <summary>
        /// Checks if this instance has an element with the specified key.
        /// </summary>
        /// <param name="key">The key of the element to check.</param>
        /// <returns>Returns true if the instance has an element with the specified name.</returns>
        public new bool HasItem(string key)
        {
            return Elements?.Any(p => p.KeyEquals(key)) == true;
        }

        /// <summary>
        /// Gets the common keys with the specified set of elements.
        /// </summary>
        /// <param name="elementSet">The collection to consider.</param>
        /// <returns>The names of the common object elements with the specified set of elements.</returns>
        public List<string> GetCommonItemKeys(IDataElementSet elementSet)
        {
            List<string> fieldNames = new List<string>();
            if (elementSet == null)
                return fieldNames;

            // To repair

            if (elementSet.Elements != null)
            {
                foreach (IDataElement currentDataItem in elementSet.Elements)
                {
                    fieldNames = _items.Where(p => HasItem(p.Key())).Select(p => p.Key()).Distinct().ToList();

                    if (HasItem(currentDataItem.Key()))
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
        public List<IDataElement> GetElementsWithGroupId(string groupId)
        {
            return Elements?.Where(p => p.Specification?.GroupId.KeyEquals(groupId) == true).ToList<IDataElement>();
        }

        /// <summary>
        /// Returns the item with the specified name and group ID.
        /// </summary>
        /// <param name="name">The name of the item to return.</param>
        /// <param name="groupId">The ID of the group of the item to return.</param>
        /// <returns>Returns the item with the specified name and group ID.</returns>
        public IDataElement GetElement(string name, string groupId = null)
        {
            return Elements?.FirstOrDefault(p =>
                p.Name.KeyEquals(name)
                && (p.Specification?.GroupId.KeyEquals(groupId) != false));
        }

        // Groups -------------------------------

        /// <summary>
        /// Gets all the element groups IDs.
        /// </summary>
        /// <returns>Returns all the element group IDs.</returns>
        public List<string> GetGroupIds()
        {
            if (Elements == null) return new List<string>();
            return Elements.Select(p => p.Specification?.GroupId).Distinct().ToList();
        }

        // Element items ------------------------

        /// <summary>
        /// Returns the item object of this instance.
        /// </summary>
        /// <param name="elementName">The element name to consider.</param>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to use.</param>
        /// <param name="log">The log to populate.</param>
        /// <returns>Returns the items of this instance.</returns>
        public virtual object GetElementObject(
            string elementName = null,
            IAppScope appScope = null,
            IScriptVariableSet scriptVariableSet = null,
            ILog log = null)
        {
            IDataElement element = (elementName != null ? GetItem(elementName) : Elements[0]);
            if (element != null)
                return element.GetObject(appScope, scriptVariableSet, log);

            return null;
        }

        /// <summary>
        /// Returns the item object of this instance.
        /// </summary>
        /// <param name="elementName">The element name to consider.</param>
        /// <param name="log">The log to populate.</param>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to use.</param>
        /// <returns>Returns the items of this instance.</returns>
        public virtual T GetElementObject<T>(
            string elementName = null,
            IAppScope appScope = null,
            IScriptVariableSet scriptVariableSet = null,
            ILog log = null)
        {
            return (T)GetElementObject(elementName,appScope,scriptVariableSet, log);
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
            if (Elements != null)
            {
                int[] indexes = Elements.Select(q => q.Index).ToArray();
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
        public string GetTitleLabel(
            string key,
            string variantName = "*",
            string defaultVariantName = "*",
            string[] parameters = null)
        {
            string label = "";

            IDataElement element = GetItem(key);
            if (element != null)
            {
                label = element.GetTitle(variantName, defaultVariantName);
                if (parameters != null)
                {
                    for (int k = 0; k < parameters.Length; k++)
                    {
                        label = label.Replace("{" + k + "}", parameters[k]);
                    }
                }
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
        public string GetDescriptionLabel(
            string key,
            string variantName = "*",
            string defaultVariantName = "*",
            string[] parameters = null)
        {
            string label = "";

            IDataElement element = GetItem(key);
            if (element != null)
            {
                label = element.GetDescription(variantName, defaultVariantName);
                if (parameters != null)
                {
                    for (int k = 0; k < parameters.Length; k++)
                    {
                        label = label.Replace("{" + k + "}", parameters[k]);
                    }
                }
            }

            return label;
        }

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
        public string GetTextNode(
            string nodeName,
            string indent)
        {
            string st = "";

            st += indent + nodeName + "\n";
            st += "\t" + indent + nodeName + ":elements" + "\n";
            if (Elements != null)
                foreach (IDataElement dataItem in Elements)
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
        public List<IDataElement> Sort(string groupId = null)
        {
            List<IDataElement> sortedDataItems = new List<IDataElement>();

            if (Elements != null)
            {
                foreach (IDataElement currentElement in Elements)
                {
                    if ((groupId == null)
                       || (currentElement.Specification != null && currentElement.Specification.GroupId == groupId))
                    {
                        int currentIndex = 0;
                        foreach (IDataElement sortedDataElement in Elements)
                        {
                            if (currentElement.Index < sortedDataElement.Index)
                                break;
                            currentIndex++;
                        }
                        if (currentIndex > Elements.Count - 1)
                            sortedDataItems.Add(currentElement);
                        else
                            sortedDataItems.Insert(currentIndex, currentElement);
                    }
                }
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
        public override object Clone()
        {
            DataElementSet elementSet = MemberwiseClone() as DataElementSet;
            elementSet._items = Elements?.Select(p => p.Clone() as DataElement).ToList();

            return elementSet;
        }

        #endregion
    }
}

