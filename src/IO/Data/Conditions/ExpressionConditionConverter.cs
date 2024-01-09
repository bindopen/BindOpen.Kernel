namespace BindOpen.Data.Conditions
{
    /// <summary>
    /// This class represents a IO converter of expression conditions.
    /// </summary>
    public static class ExpressionConditionConverter
    {
        /// <summary>
        /// Converts an expression poco into a DTO one.
        /// </summary>
        /// <param key="poco">The poco to consider.</param>
        /// <returns>The DTO object.</returns>
        public static ExpressionConditionDto ToDto(this IBdoExpressionCondition poco)
        {
            if (poco == null) return null;

            ExpressionConditionDto dto = new()
            {
                Id = poco.Id,
                Expression = poco.Expression.ToDto(),
                Name = poco.Name,
                ParentId = poco.Parent?.Id
            };

            return dto;
        }

        /// <summary>
        /// Converts an expression condition DTO into a poco one.
        /// </summary>
        /// <param key="dto">The DTO to consider.</param>
        /// <returns>The poco object.</returns>
        public static IBdoExpressionCondition ToPoco(
            this ExpressionConditionDto dto)
        {
            if (dto == null) return null;

            BdoExpressionCondition poco = new()
            {
                Expression = dto.Expression.ToPoco(),
                Id = dto.Id,
                Name = dto.Name,
                Parent = null
            };

            return poco;
        }
    }
}
