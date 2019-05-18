using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Xml.Serialization;
using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.Data.Common;
using BindOpen.Framework.Core.Data.Helpers.Objects;
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
    [XmlRoot(ElementName = "item.set", Namespace = "https://bindopen.org/xsd", IsNullable = false)]
    public class DataItemSet<T> : IdentifiedDataItem, IDataItemSet<T>
        where T : IdentifiedDataItem
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
        /// Items of this instance.
        /// </summary>
        [XmlIgnore()]
        public List<T> Items
        {
            get => _items ?? (_items = new List<T>());
            set
            {
                _items = value;
            }
        }

        /// <summary>
        /// Specification of the Items property of this instance.
        /// </summary>
        [XmlIgnore()]
        public bool ItemsSpecified => _items?.Count > 0;

        /// <summary>
        /// Returns the number of items.
        /// </summary>
        [XmlIgnore()]
        public int Count
        {
            get
            {
                return _items?.Count ?? 0;
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
        public T this[string key]
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
        /// Instantiates a new instance of the DataItemSet class.
        /// </summary>
        public DataItemSet()
        {
        }

        /// <summary>
        /// Instantiates a new instance of the DataItemSet class.
        /// </summary>
        /// <param name="items">The items to consider.</param>
        public DataItemSet(params T[] items)
        {
            _items = items?.ToList();
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
        /// Adds the specified item.
        /// </summary>
        /// <param name="items">The items of the item to add.</param>
        /// <returns>Returns the new item that has been added.
        /// Returns null if the new item is null or else its name is null.</returns>
        /// <remarks>The new item must have a name.</remarks>
        public virtual void Add(params T[] items)
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
        /// Removes the item with the specified name.
        /// </summary>
        /// <param name="keys">The keys of the item to remove.</param>
        public virtual void Remove(params string[] keys)
        {
            if (keys == null || _items == null) return;
            _items.RemoveAll(p => keys.Any(q => p.KeyEquals(q)));
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
            if (key == null || _items == null) return default;
            return _items.Find(p => p.KeyEquals(key));
        }

        /// <summary>
        /// Gets the common keys with the specified set of elements.
        /// </summary>
        /// <param name="itemSet">The set of items to consider.</param>
        /// <returns>The names of the common object items with the specified set of elements.</returns>
        public virtual List<string> GetCommonItemKeys(IDataItemSet<T> itemSet)
        {
            List<string> keys = new List<string>();
            if (itemSet != null && _items != null)
                keys = _items.Where(p => HasItem(p.Key())).Select(p => p.Key()).Distinct().ToList();

            return keys;
        }

        #endregion

        // ------------------------------------------
        // MUTATORS
        // ------------------------------------------

        #region Mutators

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
            IDataItemSet<T> referenceCollection = null)
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
            IDataItemSet<T> referenceCollection = null)
        {
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
        public override ILog Update<T1>(
                T1 item = default,
                string[] specificationAreas = null,
                UpdateModes[] updateModes = null)
        {
            ILog log = new Log();

            if (specificationAreas == null)
                specificationAreas = new[] { nameof(DataAreaKind.Any) };

            if (updateModes == null)
                updateModes = new[] { UpdateModes.Incremental_AddItemsMissingInTarget };

            if (item is IDataItemSet<T>)
            {
                IDataItemSet<T> referenceItem = item as DataItemSet<T>;

                // we repair this instance if needed
                Repair(referenceItem, specificationAreas, updateModes.Excluding(UpdateModes.Incremental_UpdateCommonItems));

                // we update the common element values

                if ((specificationAreas.Contains(nameof(DataAreaKind.Any))) || (specificationAreas.Contains(nameof(DataAreaKind.Items))))
                {
                    if (Items != null)
                    {
                        foreach (T subItem in Items)
                        {
                            if (subItem != null)
                            {
                                T referenceSubItem = referenceItem.GetItem(subItem.Key());
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
        public override ILog Check<T1>(
            bool isExistenceChecked = true,
            T1 item = default,
            string[] specificationAreas = null)
        {
            ILog log = new Log();

            if (specificationAreas == null)
                specificationAreas = new[] { nameof(DataAreaKind.Any) };

            if (item is IDataItemSet<T>)
            {
                IDataItemSet<T> referenceItem = item as DataItemSet<T>;

                // we check that all the elements in this instance are in the specified item

                if (Items != null)
                {
                    foreach (T currentSubItem in Items)
                    {
                        if (!referenceItem.Items.Any(p => p.KeyEquals(currentSubItem)))
                        {
                            log.AddError("").ResultCode = "additionalItem:" + currentSubItem.Key();
                        }
                    }
                }

                // we check that all the elements in specified collections are in this instance

                foreach (T referenceSubItem in referenceItem.Items)
                {
                    T currentSubItem = Items.Find(p => p.KeyEquals(referenceSubItem));

                    if (currentSubItem == null)
                        log.AddError("").ResultCode = "MISSINGATTRIBUTE:" + referenceSubItem.Key();
                    else
                        log.Append(currentSubItem.Check(isExistenceChecked, referenceSubItem, specificationAreas));
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
        public override ILog Repair<T1>(
            T1 item = default,
            string[] specificationAreas = null,
            UpdateModes[] updateModes = null)
        {
            Log log = null;

            if (specificationAreas == null)
                specificationAreas = new[] { nameof(DataAreaKind.Any) };

            if (updateModes == null)
                updateModes = new[] { UpdateModes.Full };

            if (item is IDataItemSet<T>)
            {
                IDataItemSet<T> referenceItem = item as DataItemSet<T>;

                // we check that all the elements in this instance are in the specified item

                if (updateModes.Has(UpdateModes.Incremental_RemoveItemsMissingInSource)
                    || updateModes.Has(UpdateModes.Incremental_UpdateCommonItems))
                {
                    int i = 0;

                    if (Items != null)
                    {
                        while (i < Items.Count)
                        {
                            T currentSubItem = Items[i];

                            T referenceSubItem = referenceItem.Items.Find(p => p.KeyEquals(currentSubItem));
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
                        foreach (T referenceSubItem in referenceItem.Items)
                        {
                            T currentSubItem = Items.Find(p => p.KeyEquals(referenceSubItem));

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
        /// <param name="log">The log to update.</param>
        public override void UpdateRuntimeInfo(IAppScope appScope = null, IScriptVariableSet scriptVariableSet = null, ILog log = null)
        {
            if (_items != null)
            {
                foreach (T item in _items)
                {
                    item.UpdateRuntimeInfo(appScope, scriptVariableSet, log);
                }
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
            DataItemSet<T> dataItemSet = base.Clone() as DataItemSet<T>;
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
