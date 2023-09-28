namespace BindOpen.Kernel.Data.Conditions
{
    /// <summary>
    /// This class represents a IO converter of basic conditions.
    /// </summary>
    public static class BasicConditionConverter
    {
        /// <summary>
        /// Converts a basic condition poco into a DTO one.
        /// </summary>
        /// <param key="poco">The poco to consider.</param>
        /// <returns>The DTO object.</returns>
        public static BasicConditionDto ToDto(this IBdoBasicCondition poco)
        {
            if (poco == null) return null;

            BasicConditionDto dto = new()
            {
                Argument1 = poco.Argument1,
                Argument2 = poco.Argument2,
                Operator = poco.Operator
            };

            return dto;
        }

        /// <summary>
        /// Converts a basic condition DTO into a poco one.
        /// </summary>
        /// <param key="dto">The DTO to consider.</param>
        /// <returns>The poco object.</returns>
        public static IBdoBasicCondition ToPoco(
            this BasicConditionDto dto)
        {
            if (dto == null) return null;

            BdoBasicCondition poco = new()
            {
                Argument1 = dto.Argument1,
                Argument2 = dto.Argument2,
                Operator = dto.Operator
            };

            return poco;
        }
    }
}
