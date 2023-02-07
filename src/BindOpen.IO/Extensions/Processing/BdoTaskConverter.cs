using BindOpen.Data;
using BindOpen.Data.Configuration;

namespace BindOpen.Extensions.Processing
{
    /// <summary>
    /// This class represents a task converter.
    /// </summary>
    public static class BdoTaskConverter
    {
        /// <summary>
        /// Converts to DTO.
        /// </summary>
        /// <param name="poco">The poco to consider.</param>
        /// <returns>The DTO object.</returns>
        public static ConfigurationDto ToDto(this IBdoTask poco)
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
        public static T ToPoco<T>(this ConfigurationDto dto) where T : class, IBdoTask, new()
        {
            if (dto == null) return null;

            T poco = new();
            poco
                .WithConfig(dto.ToPoco());

            return poco;
        }
    }
}
