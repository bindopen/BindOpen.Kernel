namespace BindOpen.Data.Elements
{
    /// <summary>
    /// This class represents a Xml helper.
    /// </summary>
    public static class SourceElementSpecConverter
    {
        /// <summary>
        /// Converts to DTO.
        /// </summary>
        /// <param name="poco">The poco to consider.</param>
        /// <returns>The DTO object.</returns>
        public static SourceElementSpecDto ToDto(this ISourceElementSpec poco)
        {
            if (poco == null) return null;

            SourceElementSpecDto dto = new()
            {
            };

            return dto;
        }

        /// <summary>
        /// Converts to DTO.
        /// </summary>
        /// <param name="dto">The DTO to consider.</param>
        /// <returns>The DTO object.</returns>
        public static ISourceElementSpec ToPoco(this SourceElementSpecDto dto)
        {
            if (dto == null) return null;

            SourceElementSpec poco = new()
            {
            };

            return poco;
        }
    }
}
