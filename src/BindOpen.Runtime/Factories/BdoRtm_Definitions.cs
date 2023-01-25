using BindOpen.Runtime.Definition;

namespace BindOpen.Runtime
{
    /// <summary>
    /// This static class provides methods to create objects.
    /// </summary>
    public static partial class BdoRtm
    {
        // Entities ------------------------------------------------

        /// <summary>
        /// Creates the instance of the specified definition.
        /// </summary>
        /// <param name="extensionDefinition">The extension definition DTO to consider.</param>
        /// <param name="name">The name of the definition DTO to consider.</param>
        public static BdoEntityDefinition NewEntityDefinition(
            this IBdoExtensionDefinition extensionDefinition,
            string name = null)
        {
            var definition = new BdoEntityDefinition(name, extensionDefinition);
            definition.WithName(name);

            return definition;
        }

        // Connectors ------------------------------------------------

        /// <summary>
        /// Creates the instance of the specified definition DTO.
        /// </summary>
        /// <param name="extensionDefinition">The extension definition DTO to consider.</param>
        /// <param name="name">The name of the definition DTO to consider.</param>
        public static BdoConnectorDefinition NewConnectorDefinition(
            this IBdoExtensionDefinition extensionDefinition,
            string name = null)
        {
            var definition = new BdoConnectorDefinition(name, extensionDefinition);
            definition.WithName(name);

            return definition;
        }

        // Tasks ------------------------------------------------

        /// <summary>
        /// Creates the instance of the specified definition.
        /// </summary>
        /// <param name="extensionDefinition">The extension definition DTO to consider.</param>
        /// <param name="name">The name of the definition DTO to consider.</param>
        public static BdoTaskDefinition NewTaskDefinition(
            this IBdoExtensionDefinition extensionDefinition,
            string name = null)
        {
            var definition = new BdoTaskDefinition(name, extensionDefinition);
            definition.WithName(name);

            return definition;
        }
    }
}
