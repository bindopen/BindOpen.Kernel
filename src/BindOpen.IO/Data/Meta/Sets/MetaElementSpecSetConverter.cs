namespace BindOpen.Data.Meta
{
    /// <summary>
    /// This class represents a Xml helper.
    /// </summary>
    public static class BdoElementSpecSetConverter
    {
        /// <summary>
        /// Converts to DTO.
        /// </summary>
        /// <param name="poco">The poco to consider.</param>
        /// <returns>The DTO object.</returns>
        public static MetaSpecSetDto ToDto(this IBdoMetaSpecList poco)
        {
            if (poco == null) return null;

            MetaSpecSetDto dto = new()
            {
            };

            return dto;
        }

        /// <summary>
        /// Converts to DTO.
        /// </summary>
        /// <param name="dto">The DTO to consider.</param>
        /// <returns>The DTO object.</returns>
        public static IBdoMetaSpecList ToPoco(this MetaSpecSetDto dto)
        {
            if (dto == null) return null;

            BdoMetaSpecList poco = new()
            {
            };

            return poco;
        }
    }
}
