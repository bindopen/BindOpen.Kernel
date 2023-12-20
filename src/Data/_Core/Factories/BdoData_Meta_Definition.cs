using BindOpen.Data.Meta;

namespace BindOpen.Data
{
    /// <summary>
    /// This static class provides methods to handle configs.
    /// </summary>
    public static partial class BdoData
    {
        // New

        /// <summary>
        /// Instantiates a new instance of the BdoBaseDefinition class.
        /// </summary>
        /// <param key="items">The items to consider.</param>
        public static BdoDefinition NewDefinition(
            params IBdoSpec[] items)
        {
            var config = NewDefinition<BdoDefinition>(items);
            return config;
        }

        /// <summary>
        /// Instantiates a new instance of the BdoBaseDefinition class.
        /// </summary>
        /// <param key="name">The name to consider.</param>
        /// <param key="items">The items to consider.</param>
        public static BdoDefinition NewDefinition(
            string name,
            params IBdoSpec[] items)
        {
            var config = NewDefinition<BdoDefinition>(name, items);
            return config;
        }

        /// <summary>
        /// Instantiates a new instance of the BdoBaseDefinition class.
        /// </summary>
        /// <param key="name">The name to consider.</param>
        /// <param key="items">The items to consider.</param>
        public static BdoDefinition NewDefinition(
            string name,
            string[] usingIds,
            params IBdoSpec[] items)
        {
            var config = NewDefinition<BdoDefinition>(name, usingIds, items);
            return config;
        }

        // New<T>

        /// <summary>
        /// Instantiates a new instance of the BdoBaseDefinition class.
        /// </summary>
        /// <param key="name">The name to consider.</param>
        /// <param key="items">The items to consider.</param>
        public static T NewDefinition<T>(
            string name,
            params IBdoSpec[] items)
            where T : IBdoDefinition, new()
            => NewDefinition<T>(name, null as string[], items);

        /// <summary>
        /// Instantiates a new instance of the BdoBaseDefinition class.
        /// </summary>
        /// <param key="items">The items to consider.</param>
        public static T NewDefinition<T>(
            params IBdoSpec[] items)
            where T : IBdoDefinition, new()
            => NewDefinition<T>(null as string, null as string[], items);

        /// <summary>
        /// Instantiates a new instance of the BdoBaseDefinition class.
        /// </summary>
        /// <param key="name">The name to consider.</param>
        /// <param key="items">The items to consider.</param>
        public static T NewDefinition<T>(
            string name,
            string[] usingIds,
            params IBdoSpec[] items)
            where T : IBdoDefinition, new()
        {
            var config = NewItemSet<T, IBdoSpec>(items)
                .WithName(name)
                .Using(usingIds);
            return config;
        }
    }
}
