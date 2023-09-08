using System.Collections.Generic;
using System.Linq;

namespace BindOpen.Kernel.Data.Meta
{
    /// <summary>
    /// 
    /// </summary>
    public static partial class BdoSpecExtensions
    {
        // Add

        /// <summary>
        /// Adds the specified item.
        /// </summary>
        /// <param key="items">The items of the item to add.</param>
        /// <returns>Returns the new item that has been added.
        /// Returns null if the new item is null or else its name is null.</returns>
        /// <remarks>The new item must have a name.</remarks>
        public static T Add<T>(
           this T set,
           string key,
           DataValueTypes valueType)
           where T : IBdoSpecSet
        {
            set.Add((key, valueType));

            return set;
        }

        /// <summary>
        /// Adds a new value to this instance.
        /// </summary>
        /// <param key="pairs">The value to add.</param>
        public static T Add<T>(
            this T list,
            params KeyValuePair<string, DataValueTypes>[] pairs)
            where T : IBdoSpecSet
        {
            list.Add(pairs.Select(q => (q.Key, q.Value)).ToArray());

            return list;
        }

        /// <summary>
        /// Adds a new value to this instance with the specified key and text.
        /// </summary>
        /// <param key="text">The text to consider.</param>
        /// <returns>Returns the added data key value.</returns>
        public static T Add<T>(
            this T list,
            params (string Name, DataValueTypes ValueType)[] pairs)
            where T : IBdoSpecSet
        {
            if (list != null)
            {
                foreach (var (Name, ValueType) in pairs)
                {
                    list.Add(BdoData.NewSpec(Name, ValueType));
                }
            }

            return list;
        }

        // With

        /// <summary>
        /// Withs a new value to this instance.
        /// </summary>
        /// <param key="pairs">The value to add.</param>
        public static T With<T>(
            this T list,
            params KeyValuePair<string, DataValueTypes>[] pairs)
            where T : IBdoSpecSet
        {
            if (list != null)
            {
                list.Clear();
                list.Add(pairs);
            }

            return list;
        }

        /// <summary>
        /// Adds a new value to this instance with the specified key and text.
        /// </summary>
        /// <param key="text">The text to consider.</param>
        /// <returns>Returns the added data key value.</returns>
        public static T With<T>(
            this T list,
            params (string Name, DataValueTypes Value)[] pairs)
            where T : IBdoSpecSet
        {
            if (list != null)
            {
                list.Clear();
                list.Add(pairs);
            }

            return list;
        }
    }
}