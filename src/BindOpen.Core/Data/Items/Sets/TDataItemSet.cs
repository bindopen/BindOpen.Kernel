using BindOpen.Application.Scopes;
using BindOpen.Data.Common;
using BindOpen.Data.Helpers.Objects;
using BindOpen.Data.Specification;
using BindOpen.System.Diagnostics;
using BindOpen.System.Scripting;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace BindOpen.Data.Items
{
    /// <summary>
    /// This class represents a data item set.
    /// </summary>
    /// <typeparam name="T">The class of the named data items.</typeparam>
    [XmlRoot(ElementName = "item.set", Namespace = "https://bindopen.org/xsd", IsNullable = false)]
    public class TDataItemSet<T> : IdentifiedDataItem, ITDataItemSet<T>
        where T : IIdentifiedDataItem
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// Items of this instance.
        /// </summary>
        [XmlIgnore()]
        public List<T> Items { get; set; }

        /// <summary>
        /// Returns the number of items.
        /// </summary>
        [XmlIgnore()]
        public int Count
        {
            get
            {
                return Items?.Count ?? 0;
            }
        }

        /// <summary>
        /// Returns the element with the specified indexed.
        /// </summary>
        [XmlIgnore()]
        public T this[int index]
            => Items != null && index >= 0 && index < Items.Count ? Items[index] : default;

        /// <summary>
        /// Returns the element with the specified key.
        /// </summary>
        [XmlIgnore()]
        public T this[string key]
        {
            get
            {
                if (key == null || Items == null) return default;
                return Items.Find(p => p.KeyEquals(key));
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
        public TDataItemSet() : base()
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
        public ITDataItemSet<T> ClearItems()
        {
            Items = null;

            return this;
        }

        /// <summary>
        /// Adds the specified item.
        /// </summary>
        /// <param name="items">The items of the item to add.</param>
        /// <returns>Returns the new item that has been added.
        /// Returns null if the new item is null or else its name is null.</returns>
        /// <remarks>The new item must have a name.</remarks>
        public ITDataItemSet<T> Add(params T[] items)
        {
            if (items != null)
            {
                foreach (T item in items)
                {
                    Add(item);
                }
            }

            return this;
        }

        /// <summary>
        /// Adds the specified item.
        /// </summary>
        /// <param name="item">The item of the item to add.</param>
        /// <returns>Returns the new item that has been added.
        /// Returns null if the new item is null or else its name is null.</returns>
        /// <remarks>The new item must have a name.</remarks>
        protected virtual void Add(T item)
        {
            var key = item?.Key();
            if (key != null)
            {
                if (Items == null)
                {
                    Items = new List<T>();
                }

                Remove(key);
                Items.Add(item);
            }
        }

        /// <summary>
        /// Sets the specified single item of this instance.
        /// </summary>
        /// <param name="items">The items to apply to this instance.</param>
        /// <remarks>Items of this instance must be allowed and must not be forbidden. Otherwise, the values will be the default ones..</remarks>
        public ITDataItemSet<T> WithItems(params T[] items)
        {
            ClearItems();
            Add(items);

            return this;
        }

        /// <summary>
        /// Removes the item with the specified name.
        /// </summary>
        /// <param name="keys">The keys of the item to remove.</param>
        public virtual ITDataItemSet<T> Remove(params string[] keys)
        {
            Items?.RemoveAll(p => keys.Any(q => p.KeyEquals(q)));

            return this;
        }

        #endregion

        // ------------------------------------------
        // ACCESSORS
        // ------------------------------------------

        #region Accessors

        /// <summary>
        /// Checks if this instance has an item with the specified name.
        /// </summary>
        /// <param name="key">The key of the item to check.</param>
        /// <returns>Returns true if the instance has an item with the specified name.</returns>
        public bool HasItem(string key = null)
        {
            if (key == null) return Items.Count > 0;
            return Items?.Any(p => p.KeyEquals(key)) == true;
        }

        /// <summary>
        /// Returns the specified item of this instance.
        /// </summary>
        /// <param name="key">The key to consider.</param>
        /// <returns>Returns the item of this instance.</returns>
        public virtual T Get(string key = null)
        {
            if (key == null) return this[0];
            return this[key];
        }

        /// <summary>
        /// Returns the specified item of this instance.
        /// </summary>
        /// <param name="key">The key to consider.</param>
        /// <returns>Returns the item of this instance.</returns>
        public virtual Q Get<Q>(string key = null) where Q : class, T
        {
            return Get(key) as Q;
        }

        /// <summary>
        /// Returns the specified item of this instance.
        /// </summary>
        /// <param name="index">The index to consider.</param>
        /// <returns>Returns the item of this instance.</returns>
        public virtual Q Get<Q>(int index) where Q : class, T
        {
            return this[index] as Q;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public T[] ToArray()
        {
            return Items?.ToArray();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<T> ToList()
        {
            return Items?.ToList();
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

            if (item is ITDataItemSet<T> referenceItem)
            {
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
                                T referenceSubItem = referenceItem[subItem.Key()];
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

            if (item is ITDataItemSet<T> referenceItem)
            {
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

            if (item is ITDataItemSet<T> referenceItem)
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
                                currentSubItem.Repair(referenceSubItem, specificationAreas).AddEventsTo(log);
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
                            T currentSubItem = Items == null ? default : Items.Find(p => p.KeyEquals(referenceSubItem));

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
        public override void UpdateStorageInfo(IBdoLog log = null)
        {
            base.UpdateStorageInfo(log);

            if (Items != null)
            {
                foreach (T item in Items)
                {
                    item.UpdateStorageInfo(log);
                }
            }
        }

        /// <summary>
        /// Updates information for runtime.
        /// </summary>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="scriptVariableSet">The set of script variables to consider.</param>
        /// <param name="log">The log to update.</param>
        public override void UpdateRuntimeInfo(IBdoScope scope = null, IScriptVariableSet scriptVariableSet = null, IBdoLog log = null)
        {
            if (Items != null)
            {
                foreach (T item in Items)
                {
                    item.UpdateRuntimeInfo(scope, scriptVariableSet, log);
                }
            }

            base.UpdateRuntimeInfo(scope, scriptVariableSet, log);
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
            TDataItemSet<T> dataItemSet = base.Clone(areas) as TDataItemSet<T>;
            dataItemSet.Items = Items?.Select(p => (T)p.Clone()).ToList();

            return dataItemSet;
        }

        #endregion
    }
}
