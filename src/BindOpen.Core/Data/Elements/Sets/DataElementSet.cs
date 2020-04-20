using BindOpen.Application.Scopes;
using BindOpen.Data.Common;
using BindOpen.Data.Helpers.Objects;
using BindOpen.Data.Items;
using BindOpen.Data.Specification;
using BindOpen.System.Diagnostics;
using BindOpen.System.Scripting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace BindOpen.Data.Elements
{
    /// <summary>
    /// This class represents a data element set.
    /// </summary>
    [XmlRoot(ElementName = "element.set", Namespace = "https://bindopen.org/xsd", IsNullable = false)]
    public class DataElementSet : DataItemSet<IDataElement>, IDataElementSet
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
            get;
            set;
        }

        // Conversions -----------------------------

        /// <summary><<                    
        /// Converts from data element array.
        /// </summary>
        /// <param name="elements">The elements to consider.</param>
        public static implicit operator DataElementSet(DataElement[] elements)
        {
            return ElementFactory.CreateSet(elements);
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
        public IDataElementSet AddElement(
            IDataElement element,
            IDataElementSet referenceElementSet = null)
        {
            base.Add(element);

            return null;
        }

        // Remove an element.
        /// <summary>
        /// Removes the element with the specified name.
        /// </summary>
        /// <param name="key">The key of the element to remove.</param>
        public IDataElementSet RemoveElement(string key)
        {
            if (key != null && _items != null)
            {
                _items.RemoveAll(p => p.KeyEquals(key));
            }

            return this;
        }

        // Element items ------------------------

        /// <summary>
        /// Returns the item of this instance.
        /// </summary>
        /// <param name="elementKey">The element key to consider.</param>
        /// <param name="item">The item to consider.</param>
        /// <param name="log">The log to populate.</param>
        /// <returns>Indicates whether the item has been set.</returns>
        public virtual IDataElementSet Add(
            string elementKey,
            object item = null,
            IBdoLog log = null)
        {
            IDataElement element = Get(elementKey);
            if (element != null)
            {
                element.AddItem(item, log);
            }

            return this;
        }

        /// <summary>
        /// Returns the items of this instance.
        /// </summary>
        /// <param name="elementKey">The element key to consider.</param>
        /// <param name="items">The items to consider.</param>
        /// <param name="log">The log to populate.</param>
        /// <returns>Returns the items of this instance.</returns>
        public virtual IDataElementSet Add(
            string elementKey,
            object[] items = null,
            IBdoLog log = null)
        {
            IDataElement element = Get(elementKey);
            element?.AddItems(items, log);

            return this;
        }

        #endregion

        // ------------------------------------------
        // ACCESSORS
        // ------------------------------------------

        #region Accessors

        // Elements -----------------------------

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

            if (elementSet.Items != null)
            {
                foreach (IDataElement currentDataItem in elementSet.Items)
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
        /// Returns the item with the specified name and group ID.
        /// </summary>
        /// <param name="name">The name of the item to return.</param>
        /// <param name="groupId">The ID of the group of the item to return.</param>
        /// <returns>Returns the item with the specified name and group ID.</returns>
        public IDataElement GetWithGroup(string name = null, string groupId = null)
        {
            return _items?.FirstOrDefault(p =>
                (name == null || p.Name.KeyEquals(name))
                && (groupId == null || p.Specification?.GroupId.KeyEquals(groupId) != false));
        }

        // Groups -------------------------------

        /// <summary>
        /// Gets all the element groups IDs.
        /// </summary>
        /// <returns>Returns all the element group IDs.</returns>
        public List<string> GetGroupIds()
        {
            if (_items == null) return new List<string>();
            return _items.Select(p => p.Specification?.GroupId).Distinct().ToList();
        }

        // Element items ------------------------

        /// <summary>
        /// Returns the item object of this instance.
        /// </summary>
        /// <param name="elementKey">The element key to consider.</param>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to use.</param>
        /// <param name="log">The log to populate.</param>
        /// <returns>Returns the items of this instance.</returns>
        public virtual object GetValue(
            string elementKey,
            IBdoScope scope = null,
            IBdoScriptVariableSet scriptVariableSet = null,
            IBdoLog log = null)
        {
            IDataElement element = Get(elementKey);
            if (element != null)
            {
                return element.GetValue(scope, scriptVariableSet, log);
            }

            return null;
        }

        /// <summary>
        /// Returns the item object of this instance.
        /// </summary>
        /// <param name="elementKey">The element key to consider.</param>
        /// <param name="log">The log to populate.</param>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to use.</param>
        /// <returns>Returns the items of this instance.</returns>
        public virtual T GetValue<T>(
            string elementKey,
            IBdoScope scope = null,
            IBdoScriptVariableSet scriptVariableSet = null,
            IBdoLog log = null)
        {
            var aObject = GetValue(elementKey, scope, scriptVariableSet, log) ?? default(T);
            return (T)aObject;
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
            if (_items != null)
            {
                int[] indexes = _items.Select(q => q.Index).ToArray();
                availableIndexes.RemoveAll(p => indexes.Contains(p));
            }

            return availableIndexes;
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

            if (_items != null)
            {
                foreach (IDataElement currentElement in _items)
                {
                    if ((groupId == null)
                       || (currentElement.Specification != null && currentElement.Specification.GroupId == groupId))
                    {
                        int currentIndex = 0;
                        foreach (IDataElement sortedDataElement in _items)
                        {
                            if (currentElement.Index < sortedDataElement.Index)
                                break;
                            currentIndex++;
                        }
                        if (currentIndex > _items.Count - 1)
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
        // CLONING
        // --------------------------------------------------

        #region Cloning

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>Returns a cloned instance.</returns>
        public override object Clone()
        {
            DataElementSet elementSet = base.Clone() as DataElementSet;

            return elementSet;
        }

        #endregion

        // --------------------------------------------------
        // UPDATE, CHECK, REPAIR
        // --------------------------------------------------

        #region Update_Check_Repair

        /// <summary>
        /// Updates this instance.
        /// </summary>
        /// <param name="item">The item to consider.</param>
        /// <param name="specificationAreas">The specification areas to consider.</param>
        /// <param name="updateModes">The update modes to consider.</param>
        /// <returns>Log of the operation.</returns>
        /// <remarks>Put reference collections as null if you do not want to repair this instance.</remarks>
        public override IBdoLog Update<T1>(
                T1 item = default,
                string[] specificationAreas = null,
                UpdateModes[] updateModes = null)
        {
            var log = new BdoLog();

            if (specificationAreas == null)
                specificationAreas = new[] { nameof(DataAreaKind.Any) };

            if (updateModes == null)
                updateModes = new[] { UpdateModes.Incremental_AddItemsMissingInTarget };

            base.Update<T1>(item, specificationAreas, updateModes);

            if (item is IDataItemSet<DataElementSpec> referenceItem)
            {
                // we repair this instance if needed
                Repair(referenceItem, specificationAreas, updateModes.Excluding(UpdateModes.Incremental_UpdateCommonItems));

                // we update the common element values

                if ((specificationAreas.Contains(nameof(DataAreaKind.Any))) || (specificationAreas.Contains(nameof(DataAreaKind.Items))))
                {
                    if (Items != null)
                    {
                        foreach (var subItem in Items)
                        {
                            if (subItem != null)
                            {
                                var referenceSubItem = referenceItem.Get(subItem.Key());
                                if (referenceSubItem != null)
                                    subItem.Update(referenceSubItem, new[] { nameof(DataAreaKind.Items) });
                            }
                        }
                    }
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
        public override IBdoLog Check<T1>(
            bool isExistenceChecked = true,
            T1 item = default,
            string[] specificationAreas = null)
        {
            var log = new BdoLog();

            if (specificationAreas == null)
                specificationAreas = new[] { nameof(DataAreaKind.Any) };

            base.Check<T1>(isExistenceChecked, item, specificationAreas);

            if (item is IDataItemSet<DataElementSpec> referenceItem)
            {
                // we check that all the elements in this instance are in the specified item

                if (Items != null)
                {
                    foreach (var currentSubItem in Items)
                    {
                        if (!referenceItem.Items.Any(p => p.KeyEquals(currentSubItem)))
                        {
                            log.AddError("").ResultCode = "additionalItem:" + currentSubItem.Key();
                        }
                    }
                }

                // we check that all the elements in specified collections are in this instance

                foreach (var referenceSubItem in referenceItem.Items)
                {
                    var currentSubItem = Items.Find(p => p.KeyEquals(referenceSubItem));

                    if (currentSubItem == null)
                        log.AddError("").ResultCode = "MISSINGATTRIBUTE:" + referenceSubItem.Key();
                    else
                        log.AddEvents(currentSubItem.Check(isExistenceChecked, referenceSubItem, specificationAreas));
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
        /// <returns>Log of the operation.</returns>
        public override IBdoLog Repair<T1>(
            T1 item = default,
            string[] specificationAreas = null,
            UpdateModes[] updateModes = null)
        {
            BdoLog log = null;

            if (specificationAreas == null)
                specificationAreas = new[] { nameof(DataAreaKind.Any) };

            if (updateModes == null)
                updateModes = new[] { UpdateModes.Full };

            base.Repair<T1>(item, specificationAreas, updateModes);

            if (item is IDataItemSet<DataElementSpec> referenceItem)
            {
                // we check that all the elements in this instance are in the specified item

                if (updateModes.Has(UpdateModes.Incremental_RemoveItemsMissingInSource)
                    || updateModes.Has(UpdateModes.Incremental_UpdateCommonItems))
                {
                    int i = 0;

                    if (Items != null)
                    {
                        while (i < Items.Count)
                        {
                            var currentSubItem = Items[i];

                            var referenceSubItem = referenceItem.Items.Find(p => p.KeyEquals(currentSubItem));
                            if (referenceSubItem == null)
                            {
                                if (updateModes.Has(UpdateModes.Incremental_RemoveItemsMissingInSource))
                                {
                                    Items.RemoveAt(i);
                                    i--;
                                }
                            }
                            else if (updateModes.Has(UpdateModes.Incremental_UpdateCommonItems))
                            {
                                log.AddEvents(currentSubItem.Repair(referenceSubItem, specificationAreas));
                            }

                            i++;
                        }
                    }
                }

                // we check that all the elements in specified item are in this instance

                if (updateModes.Has(UpdateModes.Incremental_AddItemsMissingInTarget))
                {
                    if (referenceItem.Items != null)
                    {
                        foreach (var referenceSubItem in referenceItem.Items)
                        {
                            var currentSubItem = Items.Find(p => p.KeyEquals(referenceSubItem));

                            if (currentSubItem == null)
                                Add(ElementFactory.CreateFromSpec(referenceSubItem) as DataElement);
                        }
                    }
                }
            }

            return log;
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

            Elements = Items?.Cast<DataElement>().ToList();
        }

        /// <summary>
        /// Updates information for runtime.
        /// </summary>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="scriptVariableSet">The set of script variables to consider.</param>
        /// <param name="log">The log to update.</param>
        public override void UpdateRuntimeInfo(IBdoScope scope = null, IBdoScriptVariableSet scriptVariableSet = null, IBdoLog log = null)
        {
            Items = Elements?.Cast<IDataElement>().ToList();

            base.UpdateRuntimeInfo(scope, scriptVariableSet, log);
        }

        #endregion

    }
}

