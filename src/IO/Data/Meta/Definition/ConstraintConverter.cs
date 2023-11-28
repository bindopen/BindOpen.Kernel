using BindOpen.Kernel.Data.Conditions;
using BindOpen.Kernel.Data.Meta;
using System.Linq;

namespace BindOpen.Kernel.Data.Meta
{
    /// <summary>
    /// This class represents a constraint converter.
    /// </summary>
    public static class ConstraintConverter
    {
        /// <summary>
        /// Converts a requirement level conditional statement poco into a DTO one.
        /// </summary>
        /// <param key="poco">The poco to consider.</param>
        /// <returns>The DTO object.</returns>
        public static ConstraintDto ToDto(this IBdoConstraint poco)
        {
            if (poco == null) return null;

            var dto = new ConstraintDto()
            {
                Condition = poco.Condition.ToDto(),
                GroupId = poco.GroupId,
                Id = poco.Id,
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
        public static IBdoConstraint ToPoco(this ConstraintDto dto)
        {
            if (dto == null) return null;

            var poco = new BdoConstraint()
            {
                Condition = dto.Condition.ToPoco(),
                GroupId = dto.GroupId,
                Id = dto.Id,
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
