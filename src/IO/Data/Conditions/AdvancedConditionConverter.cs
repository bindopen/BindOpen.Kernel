using System.Linq;

namespace BindOpen.System.Data.Conditions
{
    /// <summary>
    /// This class represents a Xml helper.
    /// </summary>
    public static class CompositeConditionConverter
    {
        /// <summary>
        /// Converts to DTO.
        /// </summary>
        /// <param key="poco">The poco to consider.</param>
        /// <returns>The DTO object.</returns>
        public static CompositeConditionDto ToDto(this IBdoCompositeCondition poco)
        {
            if (poco == null) return null;

            CompositeConditionDto dto = new()
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
        public static IBdoCompositeCondition ToPoco(
            this CompositeConditionDto dto)
        {
            if (dto == null) return null;

            BdoCompositeCondition poco = new()
            {
                Conditions = dto.Conditions?.Select(q => q.ToPoco()).ToList(),
                Kind = dto.Kind
            };

            return poco;
        }
    }
}
