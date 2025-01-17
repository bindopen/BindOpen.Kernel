﻿using BindOpen.Data.Helpers;
using BindOpen.Logging;

namespace BindOpen.Data
{
    /// <summary>
    /// 
    /// </summary>
    public static class TBdoSetExtensions
    {
        /// <summary>
        /// Adds the specified item.
        /// </summary>
        /// <param key="items">The items of the item to add.</param>
        /// <returns>Returns the new item that has been added.
        /// Returns null if the new item is null or else its name is null.</returns>
        /// <remarks>The new item must have a name.</remarks>
        public static ITBdoSet<T> Add<T>(
            this ITBdoSet<T> set,
            params T[] items)
            where T : IReferenced
        {
            if (set != null && items != null)
            {
                foreach (var item in items)
                {
                    set.Insert(item);
                }
            }

            return set;
        }

        /// <summary>
        /// Adds the specified item.
        /// </summary>
        /// <param key="items">The items of the item to add.</param>
        /// <returns>Returns the new item that has been added.
        /// Returns null if the new item is null or else its name is null.</returns>
        /// <remarks>The new item must have a name.</remarks>
        public static ITBdoSet<T> AddRange<T>(
            this ITBdoSet<T> set,
            ITBdoSet<T> list)
            where T : IReferenced
        {
            set?.Add(list?.Items?.ToArray());

            return set;
        }

        /// <summary>
        /// Sets the specified single item of this instance.
        /// </summary>
        /// <param key="items">The items to apply to this instance.</param>
        /// <remarks>Items of this instance must be allowed and must not be forbidden. Otherwise, the values will be the default ones..</remarks>
        public static ITBdoSet<T> With<T>(
            this ITBdoSet<T> set,
            params T[] items)
            where T : IReferenced
        {
            if (set != null)
            {
                set.Clear();
                set.Add(items);
            }

            return set;
        }

        // Update

        public static ITBdoSet<T> Update<T>(
            this ITBdoSet<T> set,
            ITBdoSet<T> refSet = default,
            UpdateModes[] updateModes = null,
            string[] areas = null,
            IBdoLog log = null)
            where T : IReferenced
        {
            areas ??= [nameof(DataAreaKind.Any)];
            updateModes ??= [UpdateModes.Incremental_AddMissingInTarget, UpdateModes.Incremental_UpdateCommon];

            if (set != null)
            {
                if (updateModes.Has(UpdateModes.Full))
                {
                    set.With(refSet?.ToArray());
                }
                else
                {
                    // we check that all the elems in this instance are in the specified item

                    if (updateModes.Has(
                            UpdateModes.Incremental_RemoveMissingInSource,
                            UpdateModes.Incremental_UpdateCommon))
                    {
                        int i = 0;

                        while (i < set.Count)
                        {
                            var item = set[i];
                            var setItem = refSet[item?.Key()];

                            if (setItem == null)
                            {
                                if (updateModes.Has(UpdateModes.Incremental_RemoveMissingInSource))
                                {
                                    set.Items?.RemoveAt(i);
                                    i--;
                                }
                            }
                            else if (updateModes.Has(UpdateModes.Incremental_UpdateCommon))
                            {
                                if (item is IUpdatable updatable)
                                {
                                    updatable.Update(setItem, areas, updateModes, log);
                                }
                            }

                            i++;
                        }
                    }

                    // we check that all the elems in specified item are in this instance

                    if (updateModes.Has(UpdateModes.Incremental_AddMissingInTarget))
                    {
                        if (refSet != null)
                        {
                            foreach (var refSetItem in refSet)
                            {
                                var key = refSetItem?.Key();

                                var item = set[key];

                                if (item == null)
                                {
                                    if (refSetItem is IClonable clonable)
                                    {
                                        var newSubItem = clonable.Clone().As<T>();
                                        set.Add(newSubItem);
                                    }
                                    else
                                    {
                                        set.Add(refSetItem);
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return set;
        }

        public static ITBdoSet<T> Update<T>(
            this ITBdoSet<T> set,
            T item = default,
            UpdateModes[] updateModes = null,
            string[] areas = null,
            IBdoLog log = null)
            where T : IReferenced
        {
            areas ??= [nameof(DataAreaKind.Any)];
            updateModes ??= [UpdateModes.Incremental_AddMissingInTarget, UpdateModes.Incremental_UpdateCommon];

            if (set != null)
            {
                if (updateModes.Has(UpdateModes.Full))
                {
                    set.With(item);
                }
                else
                {
                    var setItem = set[item?.Key()];

                    // we check that all the elems in this instance are in the specified item

                    if (setItem != null)
                    {
                        if (updateModes.Has(UpdateModes.Incremental_UpdateCommon))
                        {
                            if (setItem is IUpdatable updatable)
                            {
                                updatable.Update(item, areas, updateModes, log);
                            }
                        }
                    }
                    else
                    {
                        // we check that all the elems in specified item are in this instance

                        if (updateModes.Has(UpdateModes.Incremental_AddMissingInTarget))
                        {
                            if (item is IClonable clonable)
                            {
                                var newSubItem = clonable.Clone().As<T>();
                                set.Add(newSubItem);
                            }
                            else
                            {
                                set.Add(item);
                            }
                        }
                    }
                }
            }

            return set;
        }
    }
}