namespace BindOpen.Data.Specification
{
    /// <summary>
    /// This static class represents a data reference converter.
    /// </summary>
    public static class DataValueFilterStatementConverter
    {
        /// <summary>
        /// Converts to DTO.
        /// </summary>
        /// <param name="poco">The poco to consider.</param>
        /// <returns>The DTO object.</returns>
        public static DataValueFilterStatementDto ToDto(this IDataValueFilterStatement poco)
        {
            if (poco == null) return null;

            DataValueFilterStatementDto dto = new()
            {
            };

            return dto;
        }

        /// <summary>
        /// Converts to DTO.
        /// </summary>
        /// <param name="dto">The DTO to consider.</param>
        /// <returns>The DTO object.</returns>
        public static IDataValueFilterStatement ToPoco(this DataValueFilterStatementDto dto)
        {
            DataValueFilterStatement poco = new()
            {
            };

            return poco;
        }
    }
}
