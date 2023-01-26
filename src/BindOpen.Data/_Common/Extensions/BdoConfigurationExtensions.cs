using BindOpen.Data.Configuration;
using BindOpen.Data.Meta;

namespace BindOpen.Data
{
    /// <summary>
    /// 
    /// </summary>
    public static class IBdoConfigurationExtensions
    {
        /// <summary>
        /// Adds the specified item.
        /// </summary>
        /// <param name="items">The items of the item to add.</param>
        /// <returns>Returns the new item that has been added.
        /// Returns null if the new item is null or else its name is null.</returns>
        /// <remarks>The new item must have a name.</remarks>
        public static T Add<T>(
            this T obj,
            params IBdoMetaData[] items)
            where T : BdoConfiguration
        {
            ITBdoItemSetExtensions.Add(obj, items);

            return obj;
        }

        /// <summary>
        /// Sets the specified single item of this instance.
        /// </summary>
        /// <param name="items">The items to apply to this instance.</param>
        /// <remarks>Items of this instance must be allowed and must not be forbidden. Otherwise, the values will be the default ones..</remarks>
        public static T WithItems<T>(
            this T obj,
            IBdoMetaData[] items)
            where T : BdoConfiguration
        {
            ITBdoItemSetExtensions.WithItems(obj, items);

            return obj;
        }
    }
}