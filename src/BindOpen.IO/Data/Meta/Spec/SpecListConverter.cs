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
        public static IBdoSpecList ToPoco(this SpecListDto dto)
        {
            if (dto == null) return null;

            BdoSpecList poco = new()
            {
            };

            return poco;
        }
    }
}
