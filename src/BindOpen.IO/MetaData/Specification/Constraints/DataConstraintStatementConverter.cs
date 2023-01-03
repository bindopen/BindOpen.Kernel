namespace BindOpen.Data.Specification
{
    /// <summary>
    /// This static class represents a data reference converter.
    /// </summary>
    public static class DataConstraintStatementConverter
    {
        /// <summary>
        /// Converts to DTO.
        /// </summary>
        /// <param name="poco">The poco to consider.</param>
        /// <returns>The DTO object.</returns>
        public static DataConstraintStatementDto ToDto(this IDataConstraintStatement poco)
        {
            if (poco == null) return null;

            DataConstraintStatementDto dto = new()
            {
            };

            return dto;
        }

        /// <summary>
        /// Converts to DTO.
        /// </summary>
        /// <param name="dto">The DTO to consider.</param>
        /// <returns>The DTO object.</returns>
        public static IDataConstraintStatement ToPoco(this DataConstraintStatementDto dto)
        {
            if (dto == null) return null;

            DataConstraintStatement poco = new()
            {
                 
            };

            return poco;
        }
    }
}
