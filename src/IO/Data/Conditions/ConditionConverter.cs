namespace BindOpen.Kernel.Data.Conditions
{
    /// <summary>
    /// This abstract class represents a IO converter of conditions.
    /// </summary>
    public static class ConditionConverter
    {
        /// <summary>
        /// Converts a condition poco into a DTO one.
        /// </summary>
        /// <param key="poco">The poco to consider.</param>
        /// <returns>The DTO object.</returns>
        public static ConditionDto ToDto(this IBdoCondition poco)
        {
            if (poco is IBdoCompositeCondition advancedCondition)
            {
                return advancedCondition.ToDto();
            }
            else if (poco is IBdoBasicCondition basicCondition)
            {
                return basicCondition.ToDto();
            }
            else if (poco is IBdoExpressionCondition referenceCondition)
            {
                return referenceCondition.ToDto();
            }

            return null;
        }

        /// <summary>
        /// Converts a condition DTO into a poco one.
        /// </summary>
        /// <param key="dto">The DTO to consider.</param>
        /// <returns>The poco object.</returns>
        public static IBdoCondition ToPoco(
            this ConditionDto dto)
        {
            if (dto is CompositeConditionDto advancedConditionDto)
            {
                return advancedConditionDto.ToPoco();
            }
            else if (dto is BasicConditionDto basicConditionDto)
            {
                return basicConditionDto.ToPoco();
            }
            else if (dto is ExpressionConditionDto referenceConditionDto)
            {
                return referenceConditionDto.ToPoco();
            }

            return null;
        }
    }
}
