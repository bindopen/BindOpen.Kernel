using BindOpen.Data;
using BindOpen.Data.Meta;
using BindOpen.Extensions.Connecting;
using BindOpen.Extensions.Modeling;
using BindOpen.Extensions.Processing;

namespace BindOpen.Runtime
{
    /// <summary>
    /// This static class provides methods to create objects.
    /// </summary>
    public static partial class BdoRtm
    {
        // Entities ------------------------------------------------

        /// <summary>
        /// Creates the instance of the specified configuration.
        /// </summary>
        /// <param name="definitionUniqueId">The unique ID of the definition to consider.</param>
        public static BdoEntityConfiguration NewEntityConfiguration(
            string definitionUniqueId,
            params IBdoMetaData[] items)
        {
            var config = new BdoEntityConfiguration(definitionUniqueId);
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
            params IBdoMetaData[] items)
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
            params IBdoMetaData[] items)
        {
            var config = new BdoTaskConfiguration(definitionUniqueId);
            config.WithItems(items);

            return config;
        }
    }
}
