using BindOpen.System.Data.Meta;

namespace BindOpen.System.Data
{
    /// <summary>
    /// This static class provides methods to handle configs.
    /// </summary>
    public static partial class BdoData
    {
        // New

        /// <summary>
        /// Instantiates a new instance of the BdoBaseConfiguration class.
        /// </summary>
        /// <param key="items">The items to consider.</param>
        public static BdoConfiguration NewConfig(
            params IBdoMetaData[] items)
        {
            var config = NewConfig<BdoConfiguration>(items);
            return config;
        }

        /// <summary>
        /// Instantiates a new instance of the BdoBaseConfiguration class.
        /// </summary>
        /// <param key="name">The name to consider.</param>
        /// <param key="items">The items to consider.</param>
        public static BdoConfiguration NewConfig(
            string name,
            params IBdoMetaData[] items)
        {
            var config = NewConfig<BdoConfiguration>(name, items);
            return config;
        }

        /// <summary>
        /// Instantiates a new instance of the BdoBaseConfiguration class.
        /// </summary>
        /// <param key="name">The name to consider.</param>
        /// <param key="items">The items to consider.</param>
        public static BdoConfiguration NewConfig(
            string name,
            string[] usingIds,
            params IBdoMetaData[] items)
        {
            var config = NewConfig<BdoConfiguration>(name, usingIds, items);
            return config;
        }

        // New<T>

        /// <summary>
        /// Instantiates a new instance of the BdoBaseConfiguration class.
        /// </summary>
        /// <param key="name">The name to consider.</param>
        /// <param key="items">The items to consider.</param>
        public static T NewConfig<T>(
            string name,
            params IBdoMetaData[] items)
            where T : BdoConfiguration, new()
            => NewConfig<T>(name, null as string[], items);

        /// <summary>
        /// Instantiates a new instance of the BdoBaseConfiguration class.
        /// </summary>
        /// <param key="items">The items to consider.</param>
        public static T NewConfig<T>(
            params IBdoMetaData[] items)
            where T : BdoConfiguration, new()
            => NewConfig<T>(null as string, null as string[], items);

        /// <summary>
        /// Instantiates a new instance of the BdoBaseConfiguration class.
        /// </summary>
        /// <param key="name">The name to consider.</param>
        /// <param key="items">The items to consider.</param>
        public static T NewConfig<T>(
            string name,
            string[] usingIds,
            params IBdoMetaData[] items)
            where T : BdoConfiguration, new()
        {
            var config = BdoData.NewMetaSet<T>(items)
                .WithName(name)
                .Using(usingIds);
            return config;
        }
    }
}
