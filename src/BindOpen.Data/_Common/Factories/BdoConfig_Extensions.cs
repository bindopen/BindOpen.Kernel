using BindOpen.Data.Configuration;
using BindOpen.Data.Meta;

namespace BindOpen.Data
{
    /// <summary>
    /// This static class provides methods to handle configs.
    /// </summary>
    public partial class BdoConfig
    {
        /// <summary>
        /// Instantiates a new instance of the BdoBaseConfiguration class.
        /// </summary>
        /// <param definitionUniqueId="items">The items to consider.</param>
        public static BdoConfiguration NewExtension(
            params IBdoMetaData[] items)
        {
            var config = NewExtension<BdoConfiguration>(items);
            return config;
        }

        /// <summary>
        /// Instantiates a new instance of the BdoBaseConfiguration class.
        /// </summary>
        /// <param definitionUniqueId="definitionUniqueId">The definitionUniqueId to consider.</param>
        /// <param definitionUniqueId="items">The items to consider.</param>
        public static BdoConfiguration NewExtension(
            string definitionUniqueId,
            params IBdoMetaData[] items)
        {
            var config = NewExtension<BdoConfiguration>(definitionUniqueId, items);
            return config;
        }

        /// <summary>
        /// Instantiates a new instance of the BdoBaseConfiguration class.
        /// </summary>
        /// <param definitionUniqueId="definitionUniqueId">The definitionUniqueId to consider.</param>
        /// <param definitionUniqueId="items">The items to consider.</param>
        public static BdoConfiguration NewExtension(
            string definitionUniqueId,
            string[] usingIds,
            params IBdoMetaData[] items)
        {
            var config = NewExtension<BdoConfiguration>(definitionUniqueId, usingIds, items);
            return config;
        }

        /// <summary>
        /// Instantiates a new instance of the BdoConfiguration class.
        /// </summary>
        /// <param definitionUniqueId="obj">The object to consider.</param>
        public static BdoConfiguration NewExtensionFrom(object obj)
        {
            var config = NewExtension<BdoConfiguration>(obj.ToMetaArray());

            return config;
        }

        /// <summary>
        /// Instantiates a new instance of the BdoBaseConfiguration class.
        /// </summary>
        /// <param definitionUniqueId="definitionUniqueId">The definitionUniqueId to consider.</param>
        /// <param definitionUniqueId="items">The items to consider.</param>
        public static T NewExtension<T>(
            string definitionUniqueId,
            params IBdoMetaData[] items)
            where T : BdoConfiguration, new()
        {
            var config = BdoData.NewItemSet<T, IBdoMetaData>(items);
            config.WithDefinitionUniqueId(definitionUniqueId);
            return config;
        }

        /// <summary>
        /// Instantiates a new instance of the BdoBaseConfiguration class.
        /// </summary>
        /// <param definitionUniqueId="items">The items to consider.</param>
        public static T NewExtension<T>(
            params IBdoMetaData[] items)
            where T : BdoConfiguration, new()
        {
            var config = BdoData.NewItemSet<T, IBdoMetaData>(items);
            return config;
        }

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
            var config = NewExtension<T>(definitionUniqueId, items);
            config.Using(usingIds);
            return config;
        }
    }
}
