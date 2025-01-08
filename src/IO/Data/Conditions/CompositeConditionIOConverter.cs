using AutoMapper;
using System.Linq;

namespace BindOpen.Data.Conditions
{
    /// <summary>
    /// This class represents a IO converter of composite conditions.
    /// </summary>
    public static class CompositeConditionIOConverter
    {
        /// <summary>
        /// Converts a composite condition poco into a DTO one.
        /// </summary>
        /// <param key="poco">The poco to consider.</param>
        /// <returns>The DTO object.</returns>
        public static CompositeConditionDto ToDto(this IBdoCompositeCondition poco)
        {
            CompositeConditionDto dto = new();
            dto.UpdateFromPoco(poco);

            return dto;
        }

        public static CompositeConditionDto UpdateFromPoco(
            this CompositeConditionDto dto,
            IBdoCompositeCondition poco)
        {
            if (dto == null) return null;

            if (poco == null) return dto;

            var config = new MapperConfiguration(
                cfg => cfg.CreateMap<BdoCompositeCondition, CompositeConditionDto>()
                    .ForMember(q => q.Conditions, opt => opt.Ignore())
            );

            var mapper = new Mapper(config);
            mapper.Map(poco, dto);

            dto.CompositionKind = poco.CompositionKind;
            dto.Conditions = poco.Conditions?.Select(q => q.ToDto()).ToList();
            dto.ParentId = poco.Parent?.Identifier;

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
                Identifier = dto.Identifier,
                Name = dto.Name,
                Parent = null
            };

            return poco;
        }
    }
}
