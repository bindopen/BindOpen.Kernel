using BindOpen.Data.Conditions;
using BindOpen.Data.Meta;

namespace BindOpen.Data.Meta
{
    /// <summary>
    /// This class represents a rule converter.
    /// </summary>
    public static class SpecRuleConverter
    {
        /// <summary>
        /// Converts a requirement level conditional statement poco into a DTO one.
        /// </summary>
        /// <param key="poco">The poco to consider.</param>
        /// <returns>The DTO object.</returns>
        public static SpecRuleDto ToDto(this IBdoSpecRule poco)
        {
            if (poco == null) return null;

            var dto = new SpecRuleDto()
            {
                Condition = poco.Condition.ToDto(),
                GroupId = poco.GroupId,
                Identifier = poco.Identifier,
                Kind = poco.Kind,
                Reference = poco.Reference.ToDto(),
                ResultCode = poco.ResultCode,
                ResultDescription = poco.ResultDescription,
                ResultEventKind = poco.ResultEventKind,
                ResultTitle = poco.ResultTitle,
                Value = BdoData.NewScalar(poco.Value).ToDto()
            };

            return dto;
        }

        /// <summary>
        /// Converts a string conditional statement DTO into a poco one.
        /// </summary>
        /// <param key="dto">The DTO to consider.</param>
        /// <returns>The poco object.</returns>
        public static IBdoSpecRule ToPoco(this SpecRuleDto dto)
        {
            if (dto == null) return null;

            var poco = new BdoSpecRule()
            {
                Condition = dto.Condition.ToPoco(),
                GroupId = dto.GroupId,
                Kind = dto.Kind,
                Identifier = dto.Identifier,
                Reference = dto.Reference.ToPoco(),
                ResultCode = dto.ResultCode,
                ResultDescription = dto.ResultDescription,
                ResultEventKind = dto.ResultEventKind,
                ResultTitle = dto.ResultTitle,
                Value = dto.Value
            };

            return poco;
        }
    }
}
