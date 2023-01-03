using System.Linq;

namespace BindOpen.Data.Conditions
{
    /// <summary>
    /// This class represents a Xml helper.
    /// </summary>
    public static class AdvancedConditionConverter
    {
        /// <summary>
        /// Converts to DTO.
        /// </summary>
        /// <param name="poco">The poco to consider.</param>
        /// <returns>The DTO object.</returns>
        public static AdvancedConditionDto ToDto(this IAdvancedCondition poco)
        {
            if (poco == null) return null;

            AdvancedConditionDto dto = new()
            {
                Conditions= poco.Conditions?.Select(q=> q.ToDto()).ToList(),
                Kind = poco.Kind,
                TrueValue = poco.TrueValue
            };

            return dto;
        }

        /// <summary>
        /// Converts to POCO.
        /// </summary>
        /// <param name="dto">The DTO to consider.</param>
        /// <returns>The POCO object.</returns>
        public static IAdvancedCondition ToPoco(this AdvancedConditionDto dto)
        {
            if (dto == null) return null;

            AdvancedCondition poco = new()
            {
                Conditions = dto.Conditions?.Select(q => q.ToPoco()).ToList(),
                Kind = dto.Kind,
                TrueValue = dto.TrueValue
            };

            return poco;
        }
    }
}
