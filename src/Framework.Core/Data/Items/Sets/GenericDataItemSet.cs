using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;
using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.Data.Common;
using BindOpen.Framework.Core.Data.Helpers.Objects;
using BindOpen.Framework.Core.Data.Items.Dictionary;
using BindOpen.Framework.Core.Data.Specification;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Core.System.Scripting;

namespace BindOpen.Framework.Core.Data.Items.Sets
{

    /// <summary>
    /// This class represents a data item set.
    /// </summary>
    /// <typeparam name="T">The class of the named data items.</typeparam>
    [Serializable()]
    [XmlRoot(ElementName = "item.set", Namespace = "http://meltingsoft.com/bindopen/xsd", IsNullable = false)]
    public abstract class GenericDataItemSet<T> : DataItem, IGenericDataItemSet<T>
        where T: IStoredDataItem
    {
        // ------------------------------------------
        // VARIABLES
        // ------------------------------------------

        #region Variables

        /// <summary>
        /// The items of this instance.
        /// </summary>
        protected List<T> _items = null;

        #endregion

        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// Description of this instance.
        /// </summary>
        [XmlElement("description")]
        public IDictionaryDataItem Description { get; set; } = null;

        /// <summary>
        /// Specification of the Description property of this instance.
        /// </summary>
        [XmlIgnore()]
        public bool DescriptionSpecified => Description != null && (Description.AvailableKeysSpecified || Description.ValuesSpecified || Description.SingleValueSpecified);

        /// <summary>
        /// Returns the number of items.
        /// </summary>
        [XmlIgnore()]
        public int Count
        {
            get
            {
                return _items == null ? 0 :_items.Count;
            }
        }

        /// <summary>
        /// Returns the element with the specified indexed.
        /// </summary>
        [XmlIgnore()]
        public T this[int index]
            => _items != null && index >= 0 && index < _items.Count ? _items[index] : default(T);

        /// <summary>
        /// Returns the element with the specified key.
        /// </summary>
        [XmlIgnore()]
        public T this[String key]
        {
            get
            {
                return GetItem(key);
            }
        }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the GenericDataItemSet class.
        /// </summary>
        protected GenericDataItemSet()
        {
        }

        /// <summary>
        /// Instantiates a new instance of the GenericDataItemSet class.
        /// </summary>
        /// <param name="items">The items to consider.</param>
        protected GenericDataItemSet(params T[] items) : this(null, items)
        {
        }

        /// <summary>
        /// Instantiates a new instance of the GenericDataItemSet class.
        /// </summary>
        /// <param name="items">The items to consider.</param>
        /// <param name="description">The description to consider.</param>
        protected GenericDataItemSet(IDictionaryDataItem description, params T[] items)
        {
            _items = items?.ToList();
            Description = description;
        }

        #endregion

        // ------------------------------------------
        // MUTATORS
        // ------------------------------------------

        #region Mutators

        /// <summary>
        /// Clears the items of this instance.
        /// </summary>
        public void ClearItems()
        {
            _items = null;
        }

        /// <summary>
        /// Adds a new item.
        /// </summary>
        /// <param name="item">The new item to add.</param>
        /// <param name="referenceCollection">The reference collection to consider.</param>
        /// <returns>Returns the new item that has been added.
        /// Returns null if the new item is null or else its name is null.</returns>
        /// <remarks>The new item must have a name.</remarks>
        public virtual void Add(
            T item,
            GenericDataItemSet<T> referenceCollection = null)
        {
            if ((item == null) || (item.Key() == null))
                return;
            if (_items == null)
                _items = new List<T>();
            else
                _items.RemoveAll(p => p.KeyEquals(item));

            if (referenceCollection == null)
                _items.Add(item);
            else if (referenceCollection.HasItem(item.Key()))
                _items.Add(item);
        }

        /// <summary>
        /// Adds the specified item.
        /// </summary>
        /// <param name="items">The items of the item to add.</param>
        /// <param name="referenceCollection">The reference collection to consider.</param>
        /// <returns>Returns the new item that has been added.
        /// Returns null if the new item is null or else its name is null.</returns>
        /// <remarks>The new item must have a name.</remarks>
        public virtual void Add(
            List<T> items,
            GenericDataItemSet<T> referenceCollection = null)
        {
            if (items != null)
            {
                foreach (T dataItem in items)
                {
                    Add(dataItem);
                }
            }
        }

        /// <summary>
        /// Adds the specified item.
        /// </summary>
        /// <param name="items">The items of the item to add.</param>
        /// <returns>Returns the new item that has been added.
        /// Returns null if the new item is null or else its name is null.</returns>
        /// <remarks>The new item must have a name.</remarks>
        public virtual void Add(
            params T[] items)
        {
            if (items != null)
                Add(items.ToList());
        }

        /// <summary>
        /// Removes the item with the specified name.
        /// </summary>
        /// <param name="keys">The keys of the item to remove.</param>
        public virtual void Remove(params string[] keys)
        {
            if (keys == null || _items == null) return;
            _items.RemoveAll(p => keys.Any(q=> p.KeyEquals(q)));
        }

        #endregion

        // ------------------------------------------
        // ACCESSORS
        // ------------------------------------------

        #region Accessors

        /// <summary>
        /// Returns true if this instance has any item.
        /// </summary>
        /// <returns>Returns true if this instance has any item.</returns>
        public bool HasItems()
        {
            return _items?.Count > 0;
        }

        /// <summary>
        /// Checks if this instance has an item with the specified name.
        /// </summary>
        /// <param name="key">The key of the item to check.</param>
        /// <returns>Returns true if the instance has an item with the specified name.</returns>
        public bool HasItem(string key)
        {
            if (key == null) return false;
            return _items?.Any(p => p.KeyEquals(key)) == true;
        }

        /// <summary>
        /// Returns the item with the specified name.
        /// </summary>
        /// <param name="key">The key of the item to return.</param>
        /// <returns>Returns the item with the specified name.</returns>
        public virtual T GetItem(string key)
        {
            if (key == null || _items == null) return default(T);
            return _items.Find(p => p.KeyEquals(key));
        }

        /// <summary>
        /// Gets the common keys with the specified set of elements.
        /// </summary>
        /// <param name="itemSet">The set of items to consider.</param>
        /// <returns>The names of the common object items with the specified set of elements.</returns>
        public virtual List<string> GetCommonItemKeys(GenericDataItemSet<T> itemSet)
        {
            List<string> keys = new List<string>();
            if (itemSet != null && _items != null)
                keys = _items.Where(p => HasItem(p.Key())).Select(p => p.Key()).Distinct().ToList();

            return keys;
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
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to use.</param>
        /// <returns>Log of the operation.</returns>
        /// <remarks>Put reference collections as null if you do not want to repair this instance.</remarks>
        public override ILog Update<T1>(
            T1 item = default,
            string[] specificationAreas = null,
            UpdateMode[] updateModes = null,
            IAppScope appScope = null,
            IScriptVariableSet scriptVariableSet = null)
        {
            ILog log = new Log();

            if (specificationAreas == null)
                specificationAreas = new[] { nameof(DataAreaKind.Any) };

            if (updateModes == null)
                updateModes = new[] { UpdateMode.Full };

            if (item is GenericDataItemSet<T>)
            {
                GenericDataItemSet<T> referenceItem = item as GenericDataItemSet<T>;

                // we repair this instance if needed
                Repair(referenceItem, specificationAreas, updateModes.Excluding(UpdateMode.Incremental_UpdateCommonItems));

                // we update the common element values

                if ((specificationAreas.Contains(nameof(DataAreaKind.Any))) || (specificationAreas.Contains(nameof(DataAreaKind.Items))))
                {
                    if (_items != null)
                    {
                        foreach (T subItem in _items)
                        {
                            if (subItem != null)
                            {
                                T referenceSubItem = referenceItem.GetItem(subItem.Key());
                                if (referenceSubItem != null)
                                    subItem.Update(referenceSubItem, new [] { nameof(DataAreaKind.Items) });
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
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to use.</param>
        /// <returns>Returns the check log.</returns>
        public override ILog Check<T1>(
            bool isExistenceChecked = true,
            T1 item = default,
            string[] specificationAreas = null,
            IAppScope appScope = null,
            IScriptVariableSet scriptVariableSet = null)
        {
            ILog log = new Log();

            if (specificationAreas == null)
                specificationAreas = new [] { nameof(DataAreaKind.Any) };

            if (item is GenericDataItemSet<T>)
            {
                GenericDataItemSet<T> referenceItem = item as GenericDataItemSet<T>;

                // we check that all the elements in this instance are in the specified item

                if (_items != null)
                {
                    foreach (T currentSubItem in _items)
                    {
                        if (!referenceItem._items.Any(p => p.KeyEquals(currentSubItem)))
                        {
                            log.AddError("").ResultCode = "additionalItem:" + currentSubItem.Key();
                        }
                    }
                }

                // we check that all the elements in specified collections are in this instance

                foreach (T referenceSubItem in referenceItem._items)
                {
                    T currentSubItem = _items.Find(p => p.KeyEquals(referenceSubItem));

                    if (currentSubItem == null)
                        log.AddError("").ResultCode = "MISSINGATTRIBUTE:" + referenceSubItem.Key();
                    else
                        log.Append(currentSubItem.Check(isExistenceChecked,referenceSubItem, specificationAreas,appScope, scriptVariableSet));
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
        public override ILog Repair<T1>(
            T1 item = default,
            string[] specificationAreas = null,
            UpdateMode[] updateModes = null,
            IAppScope appScope = null,
            IScriptVariableSet scriptVariableSet = null)
        {
            Log log = null;

            if (specificationAreas == null)
                specificationAreas = new [] { nameof(DataAreaKind.Any) };

            if (updateModes == null)
                updateModes = new[] { UpdateMode.Full };

            if (item is GenericDataItemSet<T>)
            {
                GenericDataItemSet<T> referenceItem = item as GenericDataItemSet<T>;

                // we check that all the elements in this instance are in the specified item

                if (updateModes.Has(UpdateMode.Incremental_RemoveItemsMissingInSource)
                    || updateModes.Has(UpdateMode.Incremental_UpdateCommonItems))
                {
                    int i = 0;

                    if (_items != null)
                    {
                        while (i < _items.Count)
                        {
                            T currentSubItem = _items[i];

                            T referenceSubItem = referenceItem._items.Find(p => p.KeyEquals(currentSubItem));
                            if (referenceSubItem == null)
                            {
                                if (updateModes.Has(UpdateMode.Incremental_RemoveItemsMissingInSource))
                                {
                                    _items.RemoveAt(i);
                                    i--;
                                }
                            }
                            else if (updateModes.Has(UpdateMode.Incremental_UpdateCommonItems))
                            {
                                log.AddEvents(currentSubItem.Repair(referenceSubItem, specificationAreas));
                            }

                            i++;
                        }
                    }
                }

                // we check that all the elements in specified item are in this instance

                if (updateModes.Has(UpdateMode.Incremental_AddItemsMissingInTarget))
                {
                    if (referenceItem._items != null)
                    {
                        foreach (T referenceSubItem in referenceItem._items)
                        {
                            T currentSubItem = _items.Find(p => p.KeyEquals(referenceSubItem));

                            if (currentSubItem == null)
                                Add((T)referenceSubItem.Clone());
                        }
                    }
                }
            }

            return log;
        }

        #endregion

        // ------------------------------------------
        // SERIALIZATION / UNSERIALIZATION
        // ------------------------------------------

        #region Serialization_Unserialization

        /// <summary>
        /// Updates information for storage.
        /// </summary>
        /// <param name="log">The log to update.</param>
        public override void UpdateStorageInfo(ILog log = null)
        {
            if (_items != null)
            {
                foreach (T item in _items)
                {
                    item.UpdateStorageInfo(log);
                }
            }
        }

        /// <summary>
        /// Updates information for runtime.
        /// </summary>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="log">The log to update.</param>
        public override void UpdateRuntimeInfo(IAppScope appScope = null, ILog log = null)
        {
            if (_items != null)
                foreach (T item in _items)
                {
                    item.UpdateRuntimeInfo(appScope, log);
                }
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
            GenericDataItemSet<T> dataItemSet = base.Clone() as GenericDataItemSet<T>;
            if (Description != null)
                dataItemSet.Description = Description.Clone() as DictionaryDataItem;
            dataItemSet._items = _items?.Select(p => (T)p.Clone()).ToList();

            return dataItemSet;
        }

        #endregion

        // -----------------------------------------------------
        // INotifyPropertyChanged IMPLEMENTATION
        // -----------------------------------------------------

        #region INotifyPropertyChanged Implementation

        /// <summary>
        /// Occures when a property of this instance has changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Indicates that a property has changed.
        /// </summary>
        /// <param name="name">The name of the property that has changed.</param>
        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler aHandler = PropertyChanged;
            if (aHandler != null) aHandler(this, new PropertyChangedEventArgs(name));
        }

        #endregion
    }
}
