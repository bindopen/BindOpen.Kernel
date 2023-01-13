namespace BindOpen.MetaData.Specification
{
    /// <summary>
    /// This static class represents a data reference converter.
    /// </summary>
    public static class DataValueFilterConverter
    {
        /// <summary>
        /// Converts to DTO.
        /// </summary>
        /// <param name="poco">The poco to consider.</param>
        /// <returns>The DTO object.</returns>
        public static DataValueFilterDto ToDto(this IDataValueFilter poco)
        {
            if (poco == null) return null;

            DataValueFilterDto dto = new()
            {
            };

            return dto;
        }

        /// <summary>
        /// Converts to DTO.
        /// </summary>
        /// <param name="dto">The DTO to consider.</param>
        /// <returns>The DTO object.</returns>
        public static IDataValueFilter ToPoco(this DataValueFilterDto dto)
        {
            DataValueFilter poco = new()
            {
            };

            return poco;
        }
    }
}
