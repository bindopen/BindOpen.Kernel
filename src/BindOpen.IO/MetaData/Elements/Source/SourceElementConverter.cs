namespace BindOpen.Data.Elements
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
        public static SourceElementDto ToDto(this ISourceElement poco)
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
        public static ISourceElement ToPoco(this SourceElementDto dto)
        {
            if (dto == null) return null;

            SourceElement poco = new()
            {
            };

            return poco;
        }
    }
}
