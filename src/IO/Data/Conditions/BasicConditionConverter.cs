using AutoMapper;
using BindOpen.Data.Assemblies;
using BindOpen.Data.Meta;

namespace BindOpen.Data.Conditions
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
            BasicConditionDto dto = new();
            dto.UpdateFromPoco(poco);

            return dto;
        }

        public static BasicConditionDto UpdateFromPoco(
            this BasicConditionDto dto,
            IBdoBasicCondition poco)
        {
            if (dto == null) return null;

            if (poco == null) return dto;

            var config = new MapperConfiguration(
                cfg => cfg.CreateMap<BdoBasicCondition, BasicConditionDto>()
            );

            var mapper = new Mapper(config);
            mapper.Map(poco, dto);

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
                Argument1 = dto.ArgumentMetaData1?.ToPoco(),
                Argument2 = dto.ArgumentMetaData2?.ToPoco(),
                Identifier = dto.Identifier,
                Operator = dto.Operator,
                Name = dto.Name,
                Parent = null
            };

            return poco;
        }
    }
}
