using BindOpen.Data;

namespace BindOpen.Extensions.Modeling
{
    /// <summary>
    /// This class represents a carrier converter.
    /// </summary>
    public static class BdoCarrierConverter
    {
        /// <summary>
        /// Converts to DTO.
        /// </summary>
        /// <param name="poco">The poco to consider.</param>
        /// <returns>The DTO object.</returns>
        public static BdoCarrierConfigurationDto ToDto(this IBdoCarrier poco)
        {
            if (poco == null) return null;

            var dto = poco.Configuration.ToDto();
            dto.Update(dto);

            return dto;
        }

        /// <summary>
        /// Converts to DTO.
        /// </summary>
        /// <param name="dto">The DTO to consider.</param>
        /// <returns>The DTO object.</returns>
        public static T ToPoco<T>(this BdoCarrierConfigurationDto dto) where T : class, IBdoCarrier, new()
        {
            if (dto == null) return null;

            T poco = new();
            poco
                .WithConfiguration(dto.ToPoco());

            return poco;
        }
    }
}
