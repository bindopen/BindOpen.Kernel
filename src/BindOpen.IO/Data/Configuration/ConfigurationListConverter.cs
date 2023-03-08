using BindOpen.Data.Meta;

namespace BindOpen.Data.Configuration
{
    /// <summary>
    /// This class represents a Xml helper.
    /// </summary>
    public static class ConfigurationListConverter
    {
        /// <summary>
        /// Converts to DTO.
        /// </summary>
        /// <param key="poco">The poco to consider.</param>
        /// <returns>The DTO object.</returns>
        public static ConfigurationSetDto ToDto(this IBdoConfigurationSet poco)
        {
            if (poco == null) return null;

            ConfigurationSetDto dto = new()
            {
            };

            return dto;
        }

        /// <summary>
        /// Converts to POCO.
        /// </summary>
        /// <param key="dto">The dto to consider.</param>
        /// <returns>The POCO object.</returns>
        public static IBdoConfigurationSet ToPoco(
            this ConfigurationSetDto dto)
        {
            if (dto == null) return null;

            BdoConfigurationSet poco = new()
            {
            };

            return poco;
        }
    }
}
