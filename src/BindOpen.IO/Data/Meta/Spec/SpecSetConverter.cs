namespace BindOpen.Data.Meta
{
    /// <summary>
    /// This class represents a Xml helper.
    /// </summary>
    public static class SpecSetConverter
    {
        /// <summary>
        /// Converts to DTO.
        /// </summary>
        /// <param key="poco">The poco to consider.</param>
        /// <returns>The DTO object.</returns>
        public static SpecSetDto ToDto(this IBdoSpecSet poco)
        {
            if (poco == null) return null;

            SpecSetDto dto = new()
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
            this SpecSetDto dto)
        {
            if (dto == null) return null;

            BdoSpecSet poco = new()
            {
            };

            return poco;
        }
    }
}
