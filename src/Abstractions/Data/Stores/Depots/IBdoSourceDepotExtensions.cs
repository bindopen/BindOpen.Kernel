using BindOpen.System.Data.Stores;

namespace BindOpen.System.Data
{
    /// <summary>
    /// 
    /// </summary>
    public static class IBdoSourceDepotExtensions
    {
        /// <summary>
        /// Adds the specified item.
        /// </summary>
        /// <param key="items">The items of the item to add.</param>
        /// <returns>Returns the new item that has been added.
        /// Returns null if the new item is null or else its name is null.</returns>
        /// <remarks>The new item must have a name.</remarks>
        public static T Add<T>(
            this T set,
            params IBdoDatasource[] items)
            where T : IBdoSourceDepot
        {
            set?.Add<T, IBdoDatasource>(items);

            return set;
        }

        /// <summary>
        /// Adds the specified item.
        /// </summary>
        /// <param key="items">The items of the item to add.</param>
        /// <returns>Returns the new item that has been added.
        /// Returns null if the new item is null or else its name is null.</returns>
        /// <remarks>The new item must have a name.</remarks>
        public static T AddRange<T>(
            this T set,
            ITBdoSet<IBdoDatasource> list)
            where T : IBdoSourceDepot
        {
            set?.AddRange<T, IBdoDatasource>(list);

            return set;
        }

        /// <summary>
        /// Sets the specified single item of this instance.
        /// </summary>
        /// <param key="items">The items to apply to this instance.</param>
        /// <remarks>Items of this instance must be allowed and must not be forbidden. Otherwise, the values will be the default ones..</remarks>
        public static T With<T>(
           this T set,
           params IBdoDatasource[] items)
           where T : IBdoSourceDepot
        {
            set?.With<T, IBdoDatasource>(items);

            return set;
        }
    }
}