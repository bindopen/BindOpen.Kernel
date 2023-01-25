using BindOpen.Data;
using BindOpen.Data.Meta;
using BindOpen.Extensions.Connecting;

namespace BindOpen.Extensions
{
    /// <summary>
    /// This static class provides methods to create extension items.
    /// </summary>
    public static partial class BdoExt
    {
        /// <summary>
        /// Creates the instance of the specified definition.
        /// </summary>
        /// <typeparam name="T">The connector class to return.</typeparam>
        /// <param name="config"></param>
        /// <returns>Returns the created connector.</returns>
        public static T NewConnector<T>(IBdoConnectorConfiguration config)
            where T : class, IBdoConnector, new()
        {
            T connector = new();
            connector.WithConfig(config);
            connector.UpdateFromElementSet<BdoDataAttribute>(config);

            return connector;
        }

        /// <summary>
        /// Creates the instance of the specified configuration.
        /// </summary>
        /// <param name="definitionUniqueId">The unique ID of the definition to consider.</param>
        /// <param name="elms">The configuration elms of the definition to consider.</param>
        public static BdoConnectorConfiguration NewConnectorConfig(
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
        public static BdoConnectorConfiguration NewConnectorConfig(
            params IBdoMetaData[] elms)
            => NewConnectorConfig(null, elms);
    }
}
