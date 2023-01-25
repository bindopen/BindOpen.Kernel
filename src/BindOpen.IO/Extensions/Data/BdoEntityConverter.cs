using BindOpen.Data;

namespace BindOpen.Extensions.Modeling
{
    /// <summary>
    /// This class represents a entity converter.
    /// </summary>
    public static class BdoEntityConverter
    {
        /// <summary>
        /// Converts to DTO.
        /// </summary>
        /// <param name="poco">The poco to consider.</param>
        /// <returns>The DTO object.</returns>
        public static BdoEntityConfigurationDto ToDto(this IBdoEntity poco)
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
        public static T ToPoco<T>(this BdoEntityConfigurationDto dto) where T : class, IBdoEntity, new()
        {
            if (dto == null) return null;

            T poco = new();
            poco
                .WithConfig(dto.ToPoco());

            return poco;
        }
    }
}
