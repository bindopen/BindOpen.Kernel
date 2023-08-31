namespace BindOpen.System.Data.Conditions
{
    /// <summary>
    /// This class represents a Xml helper.
    /// </summary>
    public static class ExpressionConditionConverter
    {
        /// <summary>
        /// Converts to DTO.
        /// </summary>
        /// <param key="poco">The poco to consider.</param>
        /// <returns>The DTO object.</returns>
        public static ExpressionConditionDto ToDto(this IBdoExpressionCondition poco)
        {
            if (poco == null) return null;

            ExpressionConditionDto dto = new()
            {
                Expression = poco.Expression.ToDto()
            };

            return dto;
        }

        /// <summary>
        /// Converts to POCO.
        /// </summary>
        /// <param key="dto">The dto to consider.</param>
        /// <returns>The POCO object.</returns>
        public static IBdoExpressionCondition ToPoco(
            this ExpressionConditionDto dto)
        {
            if (dto == null) return null;

            BdoExpressionCondition poco = new()
            {
                Expression = dto.Expression.ToPoco()
            };

            return poco;
        }
    }
}
