using BindOpen.Data.Meta;
using BindOpen.Extensions.Connecting;
using BindOpen.Extensions.Modeling;
using BindOpen.Extensions.Processing;

namespace BindOpen.Extensions
{
    /// <summary>
    /// This static class provides methods to create extension items.
    /// </summary>
    public static partial class BdoConfig
    {
        /// <summary>
        /// Creates the instance of the specified configuration.
        /// </summary>
        /// <param name="definitionUniqueId">The unique ID of the definition to consider.</param>
        /// <param name="elms">The configuration elms of the definition to consider.</param>
        public static BdoConnectorConfiguration NewConnector(
            string definitionUniqueId,
            params IBdoMetaData[] elms)
        {
            var config = new BdoConnectorConfiguration(definitionUniqueId);
            config.WithItems(elms);

            return config;
        }

        /// <summary>
        /// Creates the instance of the specified configuration.
        /// </summary>
        /// <param name="elms">The configuration elms of the definition to consider.</param>
        public static BdoConnectorConfiguration NewConnector(
            params IBdoMetaData[] elms)
            => NewConnector(null, elms);

        /// <summary>
        /// Creates the instance of the specified configuration.
        /// </summary>
        /// <param name="definitionUniqueId">The unique ID of the definition to consider.</param>
        public static BdoEntityConfiguration NewEntity(
            string definitionUniqueId,
            params IBdoMetaData[] elms)
        {
            var config = new BdoEntityConfiguration(definitionUniqueId);
            config.WithItems(elms);

            return config;
        }

        /// <summary>
        /// Creates the instance of the specified configuration.
        /// </summary>
        /// <param name="definitionUniqueId">The unique ID of the definition to consider.</param>
        public static BdoEntityConfiguration NewEntity(
            params IBdoMetaData[] elms)
            => NewEntity(null, elms);

        /// <summary>
        /// Creates the instance of the specified configuration.
        /// </summary>
        /// <param name="definitionUniqueId">The unique ID of the definition to consider.</param>
        public static BdoTaskConfiguration NewTask(
            string definitionUniqueId,
            params IBdoMetaData[] items)
        {
            var config = new BdoTaskConfiguration(definitionUniqueId);
            config.WithItems(items);

            return config;
        }

        /// <summary>
        /// Creates the instance of the specified configuration.
        /// </summary>
        /// <param name="definitionUniqueId">The unique ID of the definition to consider.</param>
        public static BdoTaskConfiguration NewTask(
            params IBdoMetaData[] elms)
            => NewTask(null, elms);
    }
}
