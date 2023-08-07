using BindOpen.System.Data;

namespace BindOpen.System.Scoping
{
    /// <summary>
    /// This static class provides methods to create objects.
    /// </summary>
    public static partial class BdoScoping
    {
        // Entities ------------------------------------------------

        /// <summary>
        /// Creates the instance of the specified definition.
        /// </summary>
        /// <param key="extensionDefinition">The extension definition DTO to consider.</param>
        /// <param key="name">The name of the definition DTO to consider.</param>
        public static BdoEntityDefinition NewEntityDefinition(
            this IBdoPackageDefinition extensionDefinition,
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
        /// <param key="extensionDefinition">The extension definition DTO to consider.</param>
        /// <param key="name">The name of the definition DTO to consider.</param>
        public static BdoConnectorDefinition NewConnectorDefinition(
            this IBdoPackageDefinition extensionDefinition,
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
        /// <param key="extensionDefinition">The extension definition DTO to consider.</param>
        /// <param key="name">The name of the definition DTO to consider.</param>
        public static BdoTaskDefinition NewTaskDefinition(
            this IBdoPackageDefinition extensionDefinition,
            string name = null)
        {
            var definition = new BdoTaskDefinition(name, extensionDefinition);
            definition.WithName(name);

            return definition;
        }
    }
}
