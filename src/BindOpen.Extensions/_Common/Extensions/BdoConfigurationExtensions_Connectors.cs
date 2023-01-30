using BindOpen.Data.Configuration;

namespace BindOpen.Data
{
    /// <summary>
    /// 
    /// </summary>
    public static partial class BdoConfigurationExtensions
    {
        /// <summary>
        /// Adds the specified item.
        /// </summary>
        /// <param name="items">The items of the item to add.</param>
        /// <returns>Returns the new item that has been added.
        /// Returns null if the new item is null or else its name is null.</returns>
        /// <remarks>The new item must have a name.</remarks>
        public static string GetConnectionString<T>(this T obj)
            where T : IBdoConfiguration
        {
            return obj?.GetItem<string>("connectionString");
        }

        /// <summary>
        /// Sets the specified single item of this instance.
        /// </summary>
        /// <param name="items">The items to apply to this instance.</param>
        /// <remarks>Items of this instance must be allowed and must not be forbidden. Otherwise, the values will be the default ones..</remarks>
        public static T WithConnectionString<T>(
            this T obj,
            string connectionString)
            where T : IBdoConfiguration
        {
            obj?.Add(BdoMeta.NewScalar("connectionString", connectionString));

            return obj;
        }
    }
}