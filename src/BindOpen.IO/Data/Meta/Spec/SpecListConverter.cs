using BindOpen.Logging;
using BindOpen.Runtime.Scopes;

namespace BindOpen.Data.Meta
{
    /// <summary>
    /// This class represents a Xml helper.
    /// </summary>
    public static class SpecListConverter
    {
        /// <summary>
        /// Converts to DTO.
        /// </summary>
        /// <param name="poco">The poco to consider.</param>
        /// <returns>The DTO object.</returns>
        public static SpecListDto ToDto(this IBdoSpecList poco)
        {
            if (poco == null) return null;

            SpecListDto dto = new()
            {
            };

            return dto;
        }

        /// <summary>
        /// Converts to DTO.
        /// </summary>
        /// <param name="dto">The DTO to consider.</param>
        /// <returns>The DTO object.</returns>
        public static IBdoSpecList ConvertToPoco(
            this IBdoScope scope,
            SpecListDto dto,
            IBdoLog log = null)
        {
            if (dto == null) return null;

            BdoSpecList poco = new()
            {
            };

            return poco;
        }
    }
}
