using BindOpen.Data;

namespace BindOpen.Extensions.Scripting
{
    /// <summary>
    /// This class represents a task converter.
    /// </summary>
    public static class BdoScriptwordConverter
    {
        /// <summary>
        /// Converts to DTO.
        /// </summary>
        /// <param name="poco">The poco to consider.</param>
        /// <returns>The DTO object.</returns>
        public static BdoScriptwordConfigurationDto ToDto(this IBdoScriptword poco)
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
        public static T ToPoco<T>(this BdoScriptwordConfigurationDto dto) where T : class, IBdoScriptword, new()
        {
            if (dto == null) return null;

            T poco = new();
            poco
                .WithConfig(dto.ToPoco());

            return poco;
        }
    }
}
