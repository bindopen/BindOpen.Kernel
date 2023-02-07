namespace BindOpen.Data.Items
{
    /// <summary>
    /// This static class represents a data reference converter.
    /// </summary>
    public static class StringFilterConverter
    {
        /// <summary>
        /// Converts to DTO.
        /// </summary>
        /// <param name="poco">The poco to consider.</param>
        /// <returns>The DTO object.</returns>
        public static StringFilterDto ToDto(this IStringFilter poco)
        {
            if (poco == null) return null;

            StringFilterDto dto = new()
            {
            };

            return dto;
        }

        /// <summary>
        /// Converts to DTO.
        /// </summary>
        /// <param name="dto">The DTO to consider.</param>
        /// <returns>The DTO object.</returns>
        public static IStringFilter ToPoco(this StringFilterDto dto)
        {
            StringFilter poco = new()
            {
            };

            return poco;
        }
    }
}
