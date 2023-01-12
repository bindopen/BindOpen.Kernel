namespace BindOpen.Meta.Items
{
    /// <summary>
    /// This class represents a Xml helper.
    /// </summary>
    public static class DataExpressionConverter
    {
        /// <summary>
        /// Converts to DTO.
        /// </summary>
        /// <param name="poco">The poco to consider.</param>
        /// <returns>The DTO object.</returns>
        public static DataExpressionDto ToDto(this IBdoExpression poco)
        {
            if (poco == null) return null;

            DataExpressionDto dto = new()
            {
            };

            return dto;
        }

        /// <summary>
        /// Converts to DTO.
        /// </summary>
        /// <param name="dto">The DTO to consider.</param>
        /// <returns>The DTO object.</returns>
        public static IBdoExpression ToPoco(this DataExpressionDto dto)
        {
            if (dto == null) return null;

            BdoExpression poco = new()
            {
            };

            return poco;
        }
    }
}
