using BindOpen.Data.Elements;
using BindOpen.Extensions.Runtime;

namespace BindOpen.Extensions
{
    /// <summary>
    /// This static class provides methods to create objects.
    /// </summary>
    public static partial class BdoExtensionFactory
    {
        // Carriers ------------------------------------------------

        /// <summary>
        /// Creates the instance of the specified configuration.
        /// </summary>
        /// <param name="definitionUniqueId">The unique ID of the definition to consider.</param>
        public static BdoCarrierConfiguration CreateCarrierConfiguration(
            string definitionUniqueId,
            params IDataElement[] items)
        {
            var config = new BdoCarrierConfiguration()
            {
                DefinitionUniqueId = definitionUniqueId
            };
            config.WithItems(items);

            return config;
        }

        // Connectors ------------------------------------------------

        /// <summary>
        /// Creates the instance of the specified configuration.
        /// </summary>
        /// <param name="definitionUniqueId">The unique ID of the definition to consider.</param>
        public static BdoConnectorConfiguration CreateConnectorConfiguration(
            string definitionUniqueId,
            params IDataElement[] items)
        {
            var config = new BdoConnectorConfiguration()
            {
                DefinitionUniqueId = definitionUniqueId
            };
            config.WithItems(items);

            return config;
        }

        // Entities ------------------------------------------------

        /// <summary>
        /// Creates the instance of the specified configuration.
        /// </summary>
        /// <param name="definitionUniqueId">The unique ID of the definition to consider.</param>
        public static BdoEntityConfiguration CreateEntityConfiguration(
            string definitionUniqueId,
            params IDataElement[] items)
        {
            var config = new BdoEntityConfiguration()
            {
                DefinitionUniqueId = definitionUniqueId
            };
            config.WithItems(items);

            return config;
        }

        // Tasks ------------------------------------------------

        /// <summary>
        /// Creates the instance of the specified configuration.
        /// </summary>
        /// <param name="definitionUniqueId">The unique ID of the definition to consider.</param>
        public static BdoTaskConfiguration CreateTaskConfiguration(
            string definitionUniqueId,
            params IDataElement[] items)
        {
            var config = new BdoTaskConfiguration()
            {
                DefinitionUniqueId = definitionUniqueId
            };
            config.WithItems(items);

            return config;
        }
    }
}
