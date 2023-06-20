
using BindOpen.System.Data.Meta;

namespace BindOpen.System.Data
{
    /// <summary>
    /// This static class provides methods to handle configs.
    /// </summary>
    public static partial class BdoData
    {
        // NewExtension

        /// <summary>
        /// Instantiates a new instance of the BdoBaseConfiguration class.
        /// </summary>
        /// <param definitionUniqueName="items">The items to consider.</param>
        public static BdoConfiguration NewExtensionConfig(
            params IBdoMetaData[] items)
            => NewExtensionConfig<BdoConfiguration>(items);

        /// <summary>
        /// Instantiates a new instance of the BdoBaseConfiguration class.
        /// </summary>
        /// <param definitionUniqueName="definitionUniqueName">The definitionUniqueName to consider.</param>
        /// <param definitionUniqueName="items">The items to consider.</param>
        public static BdoConfiguration NewExtensionConfig(
            string definitionUniqueName,
            params IBdoMetaData[] items)
            => NewExtensionConfig<BdoConfiguration>(definitionUniqueName, items);

        /// <summary>
        /// Instantiates a new instance of the BdoBaseConfiguration class.
        /// </summary>
        /// <param definitionUniqueName="definitionUniqueName">The definitionUniqueName to consider.</param>
        /// <param definitionUniqueName="items">The items to consider.</param>
        public static BdoConfiguration NewExtensionConfig(
            string definitionUniqueName,
            string[] usingIds,
            params IBdoMetaData[] items)
            => NewExtensionConfig<BdoConfiguration>(definitionUniqueName, usingIds, items);

        // NewExtension<T>

        /// <summary>
        /// Instantiates a new instance of the BdoBaseConfiguration class.
        /// </summary>
        /// <param definitionUniqueName="definitionUniqueName">The definitionUniqueName to consider.</param>
        /// <param definitionUniqueName="items">The items to consider.</param>
        public static T NewExtensionConfig<T>(
            string definitionUniqueName,
            params IBdoMetaData[] items)
            where T : BdoConfiguration, new()
            => NewExtensionConfig<T>(definitionUniqueName, null as string[], items);

        /// <summary>
        /// Instantiates a new instance of the BdoBaseConfiguration class.
        /// </summary>
        /// <param definitionUniqueName="items">The items to consider.</param>
        public static T NewExtensionConfig<T>(
            params IBdoMetaData[] items)
            where T : BdoConfiguration, new()
            => NewExtensionConfig<T>(null as string, null as string[], items);

        /// <summary>
        /// Instantiates a new instance of the BdoBaseConfiguration class.
        /// </summary>
        /// <param definitionUniqueName="definitionUniqueName">The definitionUniqueName to consider.</param>
        /// <param definitionUniqueName="items">The items to consider.</param>
        public static T NewExtensionConfig<T>(
            string definitionUniqueName,
            string[] usingIds,
            params IBdoMetaData[] items)
            where T : BdoConfiguration, new()
        {
            var config = NewConfig<T>(definitionUniqueName, items);
            config.WithDefinition(definitionUniqueName);
            config.Using(usingIds);
            return config;
        }
    }
}
