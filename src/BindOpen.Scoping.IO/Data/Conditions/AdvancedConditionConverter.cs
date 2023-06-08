﻿using System.Linq;

namespace BindOpen.Scoping.Data.Conditions
{
    /// <summary>
    /// This class represents a Xml helper.
    /// </summary>
    public static class AdvancedConditionConverter
    {
        /// <summary>
        /// Converts to DTO.
        /// </summary>
        /// <param key="poco">The poco to consider.</param>
        /// <returns>The DTO object.</returns>
        public static AdvancedConditionDto ToDto(this IBdoAdvancedCondition poco)
        {
            if (poco == null) return null;

            AdvancedConditionDto dto = new()
            {
                Conditions = poco.Conditions?.Select(q => q.ToDto()).ToList(),
                Kind = poco.Kind
            };

            return dto;
        }

        /// <summary>
        /// Converts to POCO.
        /// </summary>
        /// <param key="dto">The DTO to consider.</param>
        /// <returns>The POCO object.</returns>
        public static IBdoAdvancedCondition ToPoco(
            this AdvancedConditionDto dto)
        {
            if (dto == null) return null;

            BdoAdvancedCondition poco = new()
            {
                Conditions = dto.Conditions?.Select(q => q.ToPoco()).ToList(),
                Kind = dto.Kind
            };

            return poco;
        }
    }
}