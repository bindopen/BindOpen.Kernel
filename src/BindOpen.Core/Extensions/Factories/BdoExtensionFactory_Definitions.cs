using BindOpen.Extensions.Definition;

namespace BindOpen.Extensions
{
    /// <summary>
    /// This static class provides methods to create objects.
    /// </summary>
    public static partial class BdoExtensionFactory
    {
        // Carriers ------------------------------------------------

        /// <summary>
        /// Creates the instance of the specified definition.
        /// </summary>
        /// <param name="name">The name of the definition DTO to consider.</param>
        public static BdoCarrierDefinitionDto CreateCarrierDefinitionDto(string name)
        {
            var definition = new BdoCarrierDefinitionDto();
            definition.WithName(name);

            return definition;
        }

        // Connectors ------------------------------------------------

        /// <summary>
        /// Creates the instance of the specified definition DTO.
        /// </summary>
        /// <param name="name">The name of the definition DTO to consider.</param>
        public static BdoConnectorDefinitionDto CreateConnectorDefinitionDto(string name)
        {
            var definition = new BdoConnectorDefinitionDto();
            definition.WithName(name);

            return definition;
        }

        // Entities ------------------------------------------------

        /// <summary>
        /// Creates the instance of the specified definition DTO.
        /// </summary>
        /// <param name="name">The name of the definition DTO to consider.</param>
        public static BdoEntityDefinitionDto CreateEntityDefinitionDto(string name)
        {
            var definition = new BdoEntityDefinitionDto();
            definition.WithName(name);

            return definition;
        }

        // Tasks ------------------------------------------------

        /// <summary>
        /// Creates the instance of the specified definition.
        /// </summary>
        /// <param name="name">The name of the definition DTO to consider.</param>
        public static BdoTaskDefinitionDto CreateTaskDefinitionDto(string name)
        {
            var definition = new BdoTaskDefinitionDto();
            definition.WithName(name);

            return definition;
        }
    }
}
