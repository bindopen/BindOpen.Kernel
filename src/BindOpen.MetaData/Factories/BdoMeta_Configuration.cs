using BindOpen.Abstractions.Meta.Configuration;
using BindOpen.Meta.Configuration;
using BindOpen.Meta.Elements;

namespace BindOpen.Meta
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
        public static BdoConfigurationBundle NewConfigurationBundle(
            params IBdoConfiguration[] items)
        {
            var config = BdoMeta.NewItemSet<BdoConfigurationBundle, IBdoConfiguration>(items);
            return config;
        }

        /// <summary>
        /// Instantiates a new instance of the BdoBaseConfiguration class.
        /// </summary>
        /// <param name="items">The items to consider.</param>
        public static BdoConfiguration NewConfiguration(
            params IBdoMetaElement[] items)
        {
            var config = NewConfiguration<BdoConfiguration>(items);
            return config;
        }

        /// <summary>
        /// Instantiates a new instance of the BdoBaseConfiguration class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="items">The items to consider.</param>
        public static BdoConfiguration NewConfiguration(
            string name,
            params IBdoMetaElement[] items)
        {
            var config = NewConfiguration<BdoConfiguration>(name, items);
            return config;
        }

        /// <summary>
        /// Instantiates a new instance of the BdoBaseConfiguration class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="items">The items to consider.</param>
        public static BdoConfiguration NewConfiguration(
            string name,
            string[] usingIds,
            params IBdoMetaElement[] items)
        {
            var config = NewConfiguration<BdoConfiguration>(name, usingIds, items);
            return config;
        }

        /// <summary>
        /// Instantiates a new instance of the BdoBaseConfiguration class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="items">The items to consider.</param>
        public static T NewConfiguration<T>(
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
        public static T NewConfiguration<T>(
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
        public static T NewConfiguration<T>(
            string name,
            string[] usingIds,
            params IBdoMetaElement[] items)
            where T : BdoConfiguration, new()
        {
            var config = NewConfiguration<T>(name, items);
            config.Using(usingIds);
            return config;
        }
    }
}
