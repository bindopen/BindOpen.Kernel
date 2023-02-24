using BindOpen.Data.Meta;

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
        /// <param key="poco">The poco to consider.</param>
        /// <returns>The DTO object.</returns>
        public static SpecListDto ToDto(this IBdoSpecSet poco)
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
        /// <param key="dto">The DTO to consider.</param>
        /// <returns>The DTO object.</returns>
        public static IBdoSpecSet ToPoco(
            this SpecListDto dto)
        {
            if (dto == null) return null;

            BdoSpecSet poco = new()
            {
            };

            return poco;
        }
    }
}
