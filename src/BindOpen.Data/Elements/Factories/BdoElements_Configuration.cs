using BindOpen.Data.Items;

namespace BindOpen.Data.Elements
{
    /// <summary>
    /// This static class provides methods to handle configurations.
    /// </summary>
    public partial class BdoElements
    {
        /// <summary>
        /// Instantiates a new instance of the BdoConfigurationBundle class.
        /// </summary>
        /// <param name="items">The items to consider.</param>
        public static BdoConfigurationBundle NewConfigurationBundle(
            params IBdoConfiguration[] items)
        {
            var config = BdoItems.NewSet<BdoConfigurationBundle, IBdoConfiguration>(items);
            return config;
        }

        /// <summary>
        /// Instantiates a new instance of the BdoBaseConfiguration class.
        /// </summary>
        /// <param name="items">The items to consider.</param>
        public static BdoConfiguration NewConfiguration(
            params IBdoElement[] items)
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
            params IBdoElement[] items)
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
            params IBdoElement[] items)
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
            params IBdoElement[] items)
            where T : BdoConfiguration, new()
        {
            var config = BdoItems.NewSet<T, IBdoElement>(items);
            config.WithName(name);
            return config;
        }

        /// <summary>
        /// Instantiates a new instance of the BdoBaseConfiguration class.
        /// </summary>
        /// <param name="items">The items to consider.</param>
        public static T NewConfiguration<T>(
            params IBdoElement[] items)
            where T : BdoConfiguration, new()
        {
            var config = BdoItems.NewSet<T, IBdoElement>(items);
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
            params IBdoElement[] items)
            where T : BdoConfiguration, new()
        {
            var config = NewConfiguration<T>(name, items);
            config.Using(usingIds);
            return config;
        }
    }
}
