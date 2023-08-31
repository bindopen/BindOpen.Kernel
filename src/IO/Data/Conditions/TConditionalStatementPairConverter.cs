namespace BindOpen.System.Data.Conditions
{
    /// <summary>
    /// This class represents a Xml helper.
    /// </summary>
    public static class TConditionalStatementPairConverter
    {
        /// <summary>
        /// Converts to DTO.
        /// </summary>
        /// <param key="poco">The poco to consider.</param>
        /// <returns>The DTO object.</returns>
        public static TConditionalStatementPairDto<TItem> ToDto<TItem>(this (TItem Item, IBdoCondition Condition) poco)
        {
            TConditionalStatementPairDto<TItem> dto = new()
            {
                Item = poco.Item,
                Condition = poco.Condition?.ToDto()
            };

            return dto;
        }

        /// <summary>
        /// Converts to DTO.
        /// </summary>
        /// <param key="dto">The DTO to consider.</param>
        /// <returns>The DTO object.</returns>
        public static (TItem Item, IBdoCondition Condition) ToPoco<TItem>(this TConditionalStatementPairDto<TItem> dto)
        {
            if (dto == null) return default;

            (TItem Item, IBdoCondition Condition) poco = new()
            {
                Item = dto.Item,
                Condition = dto.Condition?.ToPoco()
            };

            return poco;
        }
    }
}
