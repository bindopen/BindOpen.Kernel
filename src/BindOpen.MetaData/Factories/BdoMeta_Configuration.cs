using BindOpen.MetaData.Configuration;
using BindOpen.MetaData.Elements;

namespace BindOpen.MetaData
{
    /// <summary>
    /// This static class provides methods to handle configurations.
    /// </summary>
    public partial class BdoMeta
    {
        /// <summary>
        /// Instantiates a new instance of the BdoConfigurationBundle class.
        /// </summary>
        /// <param name="items">The items to consider.</param>
        public static BdoConfigurationBundle NewConfigBundle(
            params IBdoConfiguration[] items)
        {
            var config = BdoMeta.NewItemSet<BdoConfigurationBundle, IBdoConfiguration>(items);
            return config;
        }

        /// <summary>
        /// Instantiates a new instance of the BdoBaseConfiguration class.
        /// </summary>
        /// <param name="items">The items to consider.</param>
        public static BdoConfiguration NewConfig(
            params IBdoMetaElement[] items)
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
            params IBdoMetaElement[] items)
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
            params IBdoMetaElement[] items)
        {
            var config = NewConfig<BdoConfiguration>(name, usingIds, items);
            return config;
        }

        /// <summary>
        /// Instantiates a new instance of the BdoBaseConfiguration class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="items">The items to consider.</param>
        public static T NewConfig<T>(
            string name,
            params IBdoMetaElement[] items)
            where T : BdoConfiguration, new()
        {
            var config = BdoMeta.NewItemSet<T, IBdoMetaElement>(items);
            config.WithName(name);
            return config;
        }

        /// <summary>
        /// Instantiates a new instance of the BdoBaseConfiguration class.
        /// </summary>
        /// <param name="items">The items to consider.</param>
        public static T NewConfig<T>(
            params IBdoMetaElement[] items)
            where T : BdoConfiguration, new()
        {
            var config = BdoMeta.NewItemSet<T, IBdoMetaElement>(items);
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
            params IBdoMetaElement[] items)
            where T : BdoConfiguration, new()
        {
            var config = NewConfig<T>(name, items);
            config.Using(usingIds);
            return config;
        }
    }
}
