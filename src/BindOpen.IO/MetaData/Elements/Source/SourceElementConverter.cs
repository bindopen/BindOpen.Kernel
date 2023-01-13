namespace BindOpen.MetaData.Elements
{
    /// <summary>
    /// This class represents a Xml helper.
    /// </summary>
    public static class SourceElementConverter
    {
        /// <summary>
        /// Converts to DTO.
        /// </summary>
        /// <param name="poco">The poco to consider.</param>
        /// <returns>The DTO object.</returns>
        public static SourceElementDto ToDto(this IBdoMetaSource poco)
        {
            if (poco == null) return null;

            SourceElementDto dto = new()
            {
            };

            return dto;
        }

        /// <summary>
        /// Converts to DTO.
        /// </summary>
        /// <param name="dto">The DTO to consider.</param>
        /// <returns>The DTO object.</returns>
        public static IBdoMetaSource ToPoco(this SourceElementDto dto)
        {
            if (dto == null) return null;

            BdoMetaSource poco = new()
            {
            };

            return poco;
        }
    }
}
