using BindOpen.Meta;
using BindOpen.Meta.Elements;
using BindOpen.Extensions.Connecting;

namespace BindOpen.Extensions
{
    /// <summary>
    /// This static class provides methods to create extension items.
    /// </summary>
    public static partial class BdoExtensions
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
            connector.WithConfiguration(config);
            connector.UpdateFromElementSet<BdoMetaAttribute>(config);

            return connector;
        }

        /// <summary>
        /// Creates the instance of the specified configuration.
        /// </summary>
        /// <param name="definitionUniqueId">The unique ID of the definition to consider.</param>
        /// <param name="elements">The configuration elements of the definition to consider.</param>
        public static BdoConnectorConfiguration NewConnectorConfiguration(
            string definitionUniqueId,
            params IBdoMetaElement[] elements)
        {
            var config = new BdoConnectorConfiguration(definitionUniqueId);
            config.WithItems(elements);

            return config;
        }
    }
}
