using System.Linq;

namespace BindOpen.Data.Conditions
{
    /// <summary>
    /// This class represents a IO converter of composite conditions.
    /// </summary>
    public static class CompositeConditionConverter
    {
        /// <summary>
        /// Converts a composite condition poco into a DTO one.
        /// </summary>
        /// <param key="poco">The poco to consider.</param>
        /// <returns>The DTO object.</returns>
        public static CompositeConditionDto ToDto(this IBdoCompositeCondition poco)
        {
            if (poco == null) return null;

            CompositeConditionDto dto = new()
            {
                CompositionKind = poco.CompositionKind,
                Conditions = poco.Conditions?.Select(q => q.ToDto()).ToList(),
                Kind = poco.Kind,
                Id = poco.Id,
                Name = poco.Name,
                ParentId = poco.Parent?.Id
            };

            return dto;
        }

        /// <summary>
        /// Converts a composite condition DTO into a poco one.
        /// </summary>
        /// <param key="dto">The DTO to consider.</param>
        /// <returns>The poco object.</returns>
        public static IBdoCompositeCondition ToPoco(
            this CompositeConditionDto dto)
        {
            if (dto == null) return null;

            BdoCompositeCondition poco = new()
            {
                CompositionKind = dto.CompositionKind,
                Conditions = dto.Conditions?.Select(q => q.ToPoco()).ToList(),
                Kind = dto.Kind,
                Id = dto.Id,
                Name = dto.Name,
                Parent = null
            };

            return poco;
        }
    }
}
