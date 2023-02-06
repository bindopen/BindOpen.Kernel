using BindOpen.Data.Configuration;
using BindOpen.Data.Meta;

namespace BindOpen.Data
{
    /// <summary>
    /// This static class provides methods to handle configs.
    /// </summary>
    public partial class BdoConfig
    {

        // NewExtension

        /// <summary>
        /// Instantiates a new instance of the BdoBaseConfiguration class.
        /// </summary>
        /// <param definitionUniqueId="items">The items to consider.</param>
        public static BdoConfiguration NewExtension(
            params IBdoMetaData[] items)
        => NewExtension<BdoConfiguration>(items);

        /// <summary>
        /// Instantiates a new instance of the BdoBaseConfiguration class.
        /// </summary>
        /// <param definitionUniqueId="definitionUniqueId">The definitionUniqueId to consider.</param>
        /// <param definitionUniqueId="items">The items to consider.</param>
        public static BdoConfiguration NewExtension(
            string definitionUniqueId,
            params IBdoMetaData[] items)
        => NewExtension<BdoConfiguration>(definitionUniqueId, items);

        /// <summary>
        /// Instantiates a new instance of the BdoBaseConfiguration class.
        /// </summary>
        /// <param definitionUniqueId="definitionUniqueId">The definitionUniqueId to consider.</param>
        /// <param definitionUniqueId="items">The items to consider.</param>
        public static BdoConfiguration NewExtension(
            string definitionUniqueId,
            string[] usingIds,
            params IBdoMetaData[] items)
        => NewExtension<BdoConfiguration>(definitionUniqueId, usingIds, items);

        // NewExtension<T>

        /// <summary>
        /// Instantiates a new instance of the BdoBaseConfiguration class.
        /// </summary>
        /// <param definitionUniqueId="definitionUniqueId">The definitionUniqueId to consider.</param>
        /// <param definitionUniqueId="items">The items to consider.</param>
        public static T NewExtension<T>(
            string definitionUniqueId,
            params IBdoMetaData[] items)
            where T : BdoConfiguration, new()
        => NewExtension<T>(definitionUniqueId, null as string[], items);

        /// <summary>
        /// Instantiates a new instance of the BdoBaseConfiguration class.
        /// </summary>
        /// <param definitionUniqueId="items">The items to consider.</param>
        public static T NewExtension<T>(
            params IBdoMetaData[] items)
            where T : BdoConfiguration, new()
        => NewExtension<T>(null as string, null as string[], items);

        /// <summary>
        /// Instantiates a new instance of the BdoBaseConfiguration class.
        /// </summary>
        /// <param definitionUniqueId="definitionUniqueId">The definitionUniqueId to consider.</param>
        /// <param definitionUniqueId="items">The items to consider.</param>
        public static T NewExtension<T>(
            string definitionUniqueId,
            string[] usingIds,
            params IBdoMetaData[] items)
            where T : BdoConfiguration, new()
        {
            var config = New<T>(definitionUniqueId, items);
            config.WithDefinitionUniqueId(definitionUniqueId);
            config.Using(usingIds);
            return config;
        }
    }
}
