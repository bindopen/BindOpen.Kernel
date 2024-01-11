using BindOpen.Data.Helpers;
using BindOpen.Data.Meta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BindOpen.Data
{
    /// <summary>
    /// 
    /// </summary>
    public static class IBdoMetaSetExtensions
    {
        public static void Map(
            this IBdoMetaSet set,
            params (string Key, Action<IBdoMetaData> Action)[] pairs)
        {
            if (set != null)
            {
                foreach (var pair in pairs)
                {
                    var key = pair.Key;
                    var meta = set?[key];

                    if (meta != null)
                    {
                        pair.Action?.Invoke(meta);
                    }
                }
            }
        }

        public static IBdoMetaData GetOfGroup(
            this IBdoMetaSet set,
            string key,
            string groupId)
        {
            IBdoMetaData meta = null;

            if (set != null)
            {
                meta = set.FirstOrDefault(p =>
                    p.OfGroup(groupId)
                    && (key == null || p.BdoKeyEquals(key)));
            }

            return meta;
        }

        public static IEnumerable<IBdoMetaData> GetOfGroup(
            this IBdoMetaSet set,
            string groupId)
        {
            if (set != null)
            {
                return set.Where(p => p.OfGroup(groupId));
            }

            return Enumerable.Empty<IBdoMetaData>();
        }

        public static bool HasGroup(
            this IBdoMetaSet set,
            string groupId = null)
        {
            return set?.Any(p => p.OfGroup(groupId)) == true;
        }

        public static bool Has(
            this IBdoMetaSet set,
            string key,
            string groupId = StringHelper.__Star)
        {
            return set?.Any(p =>
                p.OfGroup(groupId)
                && p.BdoKeyEquals(key)) == true;
        }

        /// <summary>
        /// Adds the specified item.
        /// </summary>
        /// <param key="items">The items of the item to add.</param>
        /// <returns>Returns the new item that has been added.
        /// Returns null if the new item is null or else its name is null.</returns>
        /// <remarks>The new item must have a name.</remarks>
        public static T Add<T>(
           this T set,
           params IBdoMetaData[] items)
           where T : IBdoMetaSet
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
        /// Sets the specified single item of this instance.
        /// </summary>
        /// <param key="items">The items to apply to this instance.</param>
        /// <remarks>Items of this instance must be allowed and must not be forbidden. Otherwise, the values will be the default ones..</remarks>
        public static T With<T>(
           this T set,
           params IBdoMetaData[] items)
           where T : IBdoMetaSet
        {
            if (set != null)
            {
                set.Clear();
                set.Add(items);
            }

            return set;
        }

        /// <summary>
        /// Executes the specified action if the specified value is true.
        /// </summary>
        /// <param key="value">The value to consider.</param>
        /// <param key="action">The action to consider.</param>
        public static T Invoke<T>(this T obj, Predicate<T> fun, Action action)
        {
            if (obj != null && fun?.Invoke(obj) == true)
            {
                action?.Invoke();
            }

            return obj;
        }

        public static async Task<T> InvokeAsync<T>(this T obj, Predicate<T> fun, Func<Task> action)
        {
            if (obj != null && fun?.Invoke(obj) == true)
            {
                await action();
            }

            return obj;
        }
    }
}