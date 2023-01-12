using BindOpen.Meta.Elements;
using BindOpen.Extensions.Connecting;
using BindOpen.Extensions.Modeling;
using BindOpen.Extensions.Processing;

namespace BindOpen.Runtime
{
    /// <summary>
    /// This static class provides methods to create objects.
    /// </summary>
    public static partial class BdoRuntime
    {
        // Carriers ------------------------------------------------

        /// <summary>
        /// Creates the instance of the specified configuration.
        /// </summary>
        /// <param name="definitionUniqueId">The unique ID of the definition to consider.</param>
        public static BdoCarrierConfiguration NewCarrierConfiguration(
            string definitionUniqueId,
            params IBdoMetaElement[] items)
        {
            var config = new BdoCarrierConfiguration(definitionUniqueId);
            config.WithItems(items);

            return config;
        }

        // Connectors ------------------------------------------------

        /// <summary>
        /// Creates the instance of the specified configuration.
        /// </summary>
        /// <param name="definitionUniqueId">The unique ID of the definition to consider.</param>
        public static BdoConnectorConfiguration NewConnectorConfiguration(
            string definitionUniqueId,
            params IBdoMetaElement[] items)
        {
            var config = new BdoConnectorConfiguration(definitionUniqueId);
            config.WithItems(items);

            return config;
        }

        // Tasks ------------------------------------------------

        /// <summary>
        /// Creates the instance of the specified configuration.
        /// </summary>
        /// <param name="definitionUniqueId">The unique ID of the definition to consider.</param>
        public static BdoTaskConfiguration NewTaskConfiguration(
            string definitionUniqueId,
            params IBdoMetaElement[] items)
        {
            var config = new BdoTaskConfiguration(definitionUniqueId);
            config.WithItems(items);

            return config;
        }
    }
}
