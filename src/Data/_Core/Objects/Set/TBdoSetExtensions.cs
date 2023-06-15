using BindOpen.System.Logging;
using BindOpen.System.Data.Helpers;
using System.Linq;

namespace BindOpen.System.Data
{
    /// <summary>
    /// 
    /// </summary>
    public static class TBdoSetExtensions
    {
        // Add

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="Q"></typeparam>
        /// <typeparam name="T"></typeparam>
        /// <param key="list1"></param>
        /// <param key="list2"></param>
        /// <returns></returns>
        public static Q AddRange<Q, T>(
            this Q list1,
            ITBdoSet<T> list2)
            where Q : ITBdoSet<T>
            where T : IReferenced
        {
            list1?.Add(list2?.Items?
                .Cast<T>()
                .ToArray());
            return list1;
        }

        // With

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="Q"></typeparam>
        /// <typeparam name="T"></typeparam>
        /// <param key="list1"></param>
        /// <param key="list2"></param>
        /// <returns></returns>
        public static Q WithRange<Q, T>(
            this Q list1,
            ITBdoSet<T> list2)
            where Q : ITBdoSet<T>
            where T : IReferenced
        {
            list1?.With(list2?.Items?
                .Cast<T>()
                .ToArray());
            return list1;
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
            areas ??= new[] { nameof(DataAreaKind.Any) };
            updateModes ??= new[] { UpdateModes.Full };

            if (set != null)
            {
                if (updateModes.Has(UpdateModes.Full))
                {
                    set.With(set?.ToArray());
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
                                if (item is ITUpdatable<T> updatable)
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
                        if (set != null)
                        {
                            foreach (var setItem in set)
                            {
                                var item = set[setItem?.Key()];

                                if (item is IClonable clonable)
                                {
                                    var newSubItem = clonable.Clone().As<T>();

                                    if (newSubItem != null)
                                    {
                                        set.Add(newSubItem);
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return set;
        }
    }
}