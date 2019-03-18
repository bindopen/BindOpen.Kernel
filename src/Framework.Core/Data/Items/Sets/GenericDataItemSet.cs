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
    public abstract class GenericDataItemSet<T> : DataItem, INotifyPropertyChanged
        where T: StoredDataItem
    {
        // ------------------------------------------
        // VARIABLES
        // ------------------------------------------

        #region Variables

        private DictionaryDataItem _Description = null;

        /// <summary>
        /// The items of this instance.
        /// </summary>
        protected List<T> _Items = null;

        #endregion

        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// Description of this instance.
        /// </summary>
        [XmlElement("description")]
        public DictionaryDataItem Description
        {
            get {
                return this._Description;
            }
            set {
                this._Description = value;
            }
        }

        /// <summary>
        /// Specification of the Description property of this instance.
        /// </summary>
        [XmlIgnore()]
        public Boolean DescriptionSpecified
        {
            get
            {
                return this._Description != null && (this._Description.AvailableKeysSpecified || this._Description.ValuesSpecified || this._Description.SingleValueSpecified);
            }
        }

        /// <summary>
        /// Returns the number of items.
        /// </summary>
        [XmlIgnore()]
        public int Count
        {
            get
            {
                return this._Items == null ? 0 :this._Items.Count;
            }
        }

        /// <summary>
        /// Returns the element with the specified indexed.
        /// </summary>
        [XmlIgnore()]
        public T this[int index]
        {
            get
            {
                return (this._Items!=null && index >= 0 && index < this._Items.Count ? this._Items[index] : null);
            }
        }

        /// <summary>
        /// Returns the element with the specified key.
        /// </summary>
        [XmlIgnore()]
        public T this[String key]
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
        /// Instantiates a new instance of the GenericDataItemSet class.
        /// </summary>
        protected GenericDataItemSet()
        {
        }

        /// <summary>
        /// Instantiates a new instance of the GenericDataItemSet class.
        /// </summary>
        /// <param name="items">The items to consider.</param>
        /// <param name="description">The description to consider.</param>
        protected GenericDataItemSet(DictionaryDataItem description, params T[] items)
        {
            this._Items = items?.ToList();
            this._Description = description;
        }

        /// <summary>
        /// Instantiates a new instance of the GenericDataItemSet class.
        /// </summary>
        /// <param name="items">The items to consider.</param>
        protected GenericDataItemSet(params T[] items) : this(null, items)
        {
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
            this._Items = null;
        }

        /// <summary>
        /// Adds a new item.
        /// </summary>
        /// <param name="storedDataItem">The new item to add.</param>
        /// <param name="referenceCollection">The reference collection to consider.</param>
        /// <returns>Returns the new item that has been added.
        /// Returns null if the new item is null or else its name is null.</returns>
        /// <remarks>The new item must have a name.</remarks>
        public virtual void Add(
            T storedDataItem,
            GenericDataItemSet<T> referenceCollection = null)
        {
            if ((storedDataItem == null) || (storedDataItem.Key() == null))
                return;
            if (this._Items == null)
                this._Items = new List<T>();
            else
                this._Items.RemoveAll(p => p.KeyEquals(storedDataItem));

            if (referenceCollection == null)
                this._Items.Add(storedDataItem);
            else if (referenceCollection.HasItem(storedDataItem.Key()))
                this._Items.Add(storedDataItem);
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
                    this.Add(dataItem);
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
                this.Add(items.ToList());
        }

        /// <summary>
        /// Removes the item with the specified name.
        /// </summary>
        /// <param name="keys">The keys of the item to remove.</param>
        public virtual void Remove(params String[] keys)
        {
            if (keys == null || this._Items == null) return;
            this._Items.RemoveAll(p => keys.Any(q=> p.KeyEquals(q)));
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
        public Boolean HasItems()
        {
            return _Items?.Count > 0;
        }

        /// <summary>
        /// Checks if this instance has an item with the specified name.
        /// </summary>
        /// <param name="key">The key of the item to check.</param>
        /// <returns>Returns true if the instance has an item with the specified name.</returns>
        public Boolean HasItem(string key)
        {
            if (key == null) return false;
            return _Items?.Any(p => p.KeyEquals(key)) == true;
        }

        /// <summary>
        /// Returns the item with the specified name.
        /// </summary>
        /// <param name="key">The key of the item to return.</param>
        /// <returns>Returns the item with the specified name.</returns>
        public virtual T GetItem(string key)
        {
            if (key == null || this._Items == null) return null;
            return this._Items.Find(p => p.KeyEquals(key));
        }

        /// <summary>
        /// Gets the common keys with the specified set of elements.
        /// </summary>
        /// <param name="dataItemSet">The set of elements to consider.</param>
        /// <returns>The names of the common object items with the specified set of elements.</returns>
        public virtual List<string> GetCommonItemKeys(GenericDataItemSet<T> dataItemSet)
        {
            List<string> keys = new List<string>();
            if (dataItemSet != null && this._Items != null)
                keys = this._Items.Where(p => this.HasItem(p.Key())).Select(p => p.Key()).Distinct().ToList();

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
        public override Log Update<T1>(
            T1 item = null,
            List<string> specificationAreas = null,
            List<UpdateMode> updateModes = null,
            IAppScope appScope = null,
            ScriptVariableSet scriptVariableSet = null)
        {
            Log log = new Log();

            if (specificationAreas == null)
                specificationAreas = new List<string>() { DataAreaKind.Any.ToString() };

            if (updateModes == null)
                updateModes = new List<UpdateMode>() { UpdateMode.Full };

            if (item is GenericDataItemSet<T>)
            {
                GenericDataItemSet<T> referenceItem = item as GenericDataItemSet<T>;

                // we repair this instance if needed
                this.Repair(referenceItem, specificationAreas, updateModes.Excluding(UpdateMode.Incremental_UpdateCommonItems));

                // we update the common element values

                if ((specificationAreas.Contains(DataAreaKind.Any.ToString())) || (specificationAreas.Contains(nameof(DataAreaKind.Items))))
                {
                    if (this._Items != null)
                    {
                        foreach (T currentSubItem in this._Items)
                        {
                            if (currentSubItem != null)
                            {
                                T referenceSubItem = referenceItem.GetItem(currentSubItem.Key());
                                if (referenceSubItem != null)
                                    currentSubItem.Update(referenceSubItem, new List<string>() { nameof(DataAreaKind.Items) });
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
        public override Log Check<T1>(
            Boolean isExistenceChecked = true,
            T1 item = null,
            List<string> specificationAreas = null,
            IAppScope appScope = null,
            ScriptVariableSet scriptVariableSet = null)
        {
            Log log = new Log();

            if (specificationAreas == null)
                specificationAreas = new List<string>() { DataAreaKind.Any.ToString() };

            if (item is GenericDataItemSet<T>)
            {
                GenericDataItemSet<T> referenceItem = item as GenericDataItemSet<T>;

                // we check that all the elements in this instance are in the specified item

                if (this._Items != null)
                {
                    foreach (T currentSubItem in this._Items)
                    {
                        if (!referenceItem._Items.Any(p => p.KeyEquals(currentSubItem)))
                        {
                            log.AddError("").ResultCode = "additionalItem:" + currentSubItem.Key();
                        }
                    }
                }

                // we check that all the elements in specified collections are in this instance

                foreach (T referenceSubItem in referenceItem._Items)
                {
                    T currentSubItem = this._Items.FirstOrDefault(p => p.KeyEquals(referenceSubItem));

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
        public override Log Repair<T1>(
            T1 item = null,
            List<string> specificationAreas = null,
            List<UpdateMode> updateModes = null,
            IAppScope appScope = null,
            ScriptVariableSet scriptVariableSet = null)
        {
            Log log = null;

            if (specificationAreas == null)
                specificationAreas = new List<string>() { DataAreaKind.Any.ToString() };

            if (updateModes == null)
                updateModes = new List<UpdateMode>() { UpdateMode.Full };

            if (item is GenericDataItemSet<T>)
            {
                GenericDataItemSet<T> referenceItem = item as GenericDataItemSet<T>;

                // we check that all the elements in this instance are in the specified item

                if (updateModes.Has(UpdateMode.Incremental_RemoveItemsMissingInSource) || updateModes.Has(UpdateMode.Incremental_UpdateCommonItems))
                {
                    int i = 0;

                    if (this._Items != null)
                    {
                        while (i < this._Items.Count)
                        {
                            T currentSubItem = this._Items[i];

                            T referenceSubItem = referenceItem._Items.FirstOrDefault(p => p.KeyEquals(currentSubItem));
                            if (referenceSubItem == null)
                            {
                                if (updateModes.Has(UpdateMode.Incremental_RemoveItemsMissingInSource))
                                {
                                    this._Items.RemoveAt(i);
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
                    if (referenceItem._Items != null)
                    {
                        foreach (T referenceSubItem in referenceItem._Items)
                        {
                            T currentSubItem = this._Items?.FirstOrDefault(p => p.KeyEquals(referenceSubItem));

                            if (currentSubItem == null)
                                this.Add(referenceSubItem.Clone() as T);
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
        public override void UpdateStorageInfo(Log log = null)
        {
            if (this._Items != null)
            {
                foreach (T item in this._Items)
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
        public override void UpdateRuntimeInfo(IAppScope appScope = null, Log log = null)
        {
            if (this._Items != null)
                foreach (T item in this._Items)
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
        public override Object Clone()
        {
            GenericDataItemSet<T> dataItemSet = base.Clone() as GenericDataItemSet<T>;
            if (this._Description != null)
                dataItemSet.Description = this._Description.Clone() as DictionaryDataItem;
            dataItemSet._Items = this._Items?.Select(p => p.Clone() as T).ToList();

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
            PropertyChangedEventHandler aHandler = this.PropertyChanged;
            if (aHandler != null) aHandler(this, new PropertyChangedEventArgs(name));
        }

        #endregion
    }
}
