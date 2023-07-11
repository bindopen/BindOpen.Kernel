using System.Collections.Generic;
using System.Linq;

namespace BindOpen.System.Data
{
    /// <summary>
    /// 
    /// </summary>
    public static class ITBdoSetExtensions
    {
        /// <summary>
        /// Adds the specified item.
        /// </summary>
        /// <param key="items">The items of the item to add.</param>
        /// <returns>Returns the new item that has been added.
        /// Returns null if the new item is null or else its name is null.</returns>
        /// <remarks>The new item must have a name.</remarks>
        public static Q Insert<Q, T>(
            this Q set,
            T value)
            where Q : ITBdoSet<T>
            where T : IReferenced
        {
            set?.Insert(null, value);

            return set;
        }

        /// <summary>
        /// Adds the specified item.
        /// </summary>
        /// <param key="items">The items of the item to add.</param>
        /// <returns>Returns the new item that has been added.
        /// Returns null if the new item is null or else its name is null.</returns>
        /// <remarks>The new item must have a name.</remarks>
        public static Q Add<Q, T>(
            this Q set,
            params (string Key, T Value)[] items)
            where Q : ITBdoSet<T>
            where T : IReferenced
        {
            if (set != null && items != null)
            {
                foreach (var (Key, Value) in items)
                {
                    set.Insert(Key, Value);
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
        public static Q Add<Q, T>(
            this Q set,
            params T[] items)
            where Q : ITBdoSet<T>
            where T : IReferenced
        {
            set.Add(items.Select(q => ((string)null, q)).ToArray());

            return set;
        }

        /// <summary>
        /// Adds the specified item.
        /// </summary>
        /// <param key="items">The items of the item to add.</param>
        /// <returns>Returns the new item that has been added.
        /// Returns null if the new item is null or else its name is null.</returns>
        /// <remarks>The new item must have a name.</remarks>
        public static Q AddRange<Q, T>(
            this Q set,
            ITBdoSet<T> list)
            where Q : ITBdoSet<T>
            where T : IReferenced
        {
            if (set != null)
            {
                var items = list?.Items?.ToArray();
                set.Add(items);
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
        public static Q AddRange<Q, T>(
            this Q set,
            IList<(string Key, T Value)> list)
            where Q : ITBdoSet<T>
            where T : IReferenced
        {
            if (set != null)
            {
                var items = list?.ToArray();
                set.Add(items);
            }

            return set;
        }

        /// <summary>
        /// Sets the specified single item of this instance.
        /// </summary>
        /// <param key="items">The items to apply to this instance.</param>
        /// <remarks>Items of this instance must be allowed and must not be forbidden. Otherwise, the values will be the default ones..</remarks>
        public static Q With<Q, T>(
           this Q set,
           params (string Key, T Value)[] items)
           where Q : ITBdoSet<T>
           where T : IReferenced
        {
            if (set != null)
            {
                set.Clear();
                set.Add(items);
            }

            return set;
        }

        /// <summary>
        /// Sets the specified single item of this instance.
        /// </summary>
        /// <param key="items">The items to apply to this instance.</param>
        /// <remarks>Items of this instance must be allowed and must not be forbidden. Otherwise, the values will be the default ones..</remarks>
        public static Q With<Q, T>(
           this Q set,
           params T[] items)
           where Q : ITBdoSet<T>
           where T : IReferenced
        {
            set.With(items.Select(q => ((string)null, q)).ToArray());

            return set;
        }
    }
}