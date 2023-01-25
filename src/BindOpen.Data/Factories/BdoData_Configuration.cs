using BindOpen.Data.Configuration;
using BindOpen.Data.Meta;

namespace BindOpen.Data
{
    /// <summary>
    /// This static class provides methods to handle configurations.
    /// </summary>
    public partial class BdoData
    {
        /// <summary>
        /// Instantiates a new instance of the BdoConfigurationBundle class.
        /// </summary>
        /// <param name="items">The items to consider.</param>
        public static BdoConfigurationBundle NewConfigBundle(
            params IBdoConfiguration[] items)
        {
            var config = BdoData.NewItemSet<BdoConfigurationBundle, IBdoConfiguration>(items);
            return config;
        }

        /// <summary>
        /// Instantiates a new instance of the BdoBaseConfiguration class.
        /// </summary>
        /// <param name="items">The items to consider.</param>
        public static BdoConfiguration NewConfig(
            params IBdoMetaData[] items)
        {
            var config = NewConfig<BdoConfiguration>(items);
            return config;
        }

        /// <summary>
        /// Instantiates a new instance of the BdoBaseConfiguration class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="items">The items to consider.</param>
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
        /// <param name="name">The name to consider.</param>
        /// <param name="items">The items to consider.</param>
        public static BdoConfiguration NewConfig(
            string name,
            string[] usingIds,
            params IBdoMetaData[] items)
        {
            var config = NewConfig<BdoConfiguration>(name, usingIds, items);
            return config;
        }

        /// <summary>
        /// Instantiates a new instance of the BdoConfiguration class.
        /// </summary>
        /// <param name="obj">The object to consider.</param>
        public static BdoConfiguration NewConfigFrom(object obj)
        {
            var config = NewConfig<BdoConfiguration>(obj.ToMetaArray());

            return config;
        }

        /// <summary>
        /// Instantiates a new instance of the BdoBaseConfiguration class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="items">The items to consider.</param>
        public static T NewConfig<T>(
            string name,
            params IBdoMetaData[] items)
            where T : BdoConfiguration, new()
        {
            var config = BdoData.NewItemSet<T, IBdoMetaData>(items);
            config.WithName(name);
            return config;
        }

        /// <summary>
        /// Instantiates a new instance of the BdoBaseConfiguration class.
        /// </summary>
        /// <param name="items">The items to consider.</param>
        public static T NewConfig<T>(
            params IBdoMetaData[] items)
            where T : BdoConfiguration, new()
        {
            var config = BdoData.NewItemSet<T, IBdoMetaData>(items);
            return config;
        }

        /// <summary>
        /// Instantiates a new instance of the BdoBaseConfiguration class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="items">The items to consider.</param>
        public static T NewConfig<T>(
            string name,
            string[] usingIds,
            params IBdoMetaData[] items)
            where T : BdoConfiguration, new()
        {
            var config = NewConfig<T>(name, items);
            config.Using(usingIds);
            return config;
        }
    }
}
