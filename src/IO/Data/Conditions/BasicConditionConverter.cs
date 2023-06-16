﻿namespace BindOpen.System.Data.Conditions
{
    /// <summary>
    /// This class represents a Xml helper.
    /// </summary>
    public static class BasicConditionConverter
    {
        /// <summary>
        /// Converts to DTO.
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
        /// Converts to POCO.
        /// </summary>
        /// <param key="dto">The DTO to consider.</param>
        /// <returns>The POCO object.</returns>
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