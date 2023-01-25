using BindOpen.Data;

namespace BindOpen.Extensions.Connecting
{
    /// <summary>
    /// This class represents a connector converter.
    /// </summary>
    public static class BdoConnectorConverter
    {
        /// <summary>
        /// Converts to DTO.
        /// </summary>
        /// <param name="poco">The poco to consider.</param>
        /// <returns>The DTO object.</returns>
        public static BdoConnectorConfigurationDto ToDto(this IBdoConnector poco)
        {
            if (poco == null) return null;

            var dto = poco.Config.ToDto();
            dto.Update(dto);

            return dto;
        }

        /// <summary>
        /// Converts to DTO.
        /// </summary>
        /// <param name="dto">The DTO to consider.</param>
        /// <returns>The DTO object.</returns>
        public static T ToPoco<T>(this BdoConnectorConfigurationDto dto) where T : class, IBdoConnector, new()
        {
            if (dto == null) return null;

            T poco = new();
            poco
                .WithConfig(dto.ToPoco());

            return poco;
        }
    }
}
