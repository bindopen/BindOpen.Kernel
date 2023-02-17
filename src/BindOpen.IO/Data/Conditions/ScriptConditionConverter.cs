using BindOpen.Data.Items;
using BindOpen.Logging;
using BindOpen.Runtime.Scopes;

namespace BindOpen.Data.Conditions
{
    /// <summary>
    /// This class represents a Xml helper.
    /// </summary>
    public static class ScriptConditionConverter
    {
        /// <summary>
        /// Converts to DTO.
        /// </summary>
        /// <param name="poco">The poco to consider.</param>
        /// <returns>The DTO object.</returns>
        public static ScriptConditionDto ToDto(this IScriptCondition poco)
        {
            if (poco == null) return null;

            ScriptConditionDto dto = new()
            {
                Expression = poco.Expression.ToDto(),
                TrueValue = poco.TrueValue
            };

            return dto;
        }

        /// <summary>
        /// Converts to POCO.
        /// </summary>
        /// <param name="dto">The dto to consider.</param>
        /// <returns>The POCO object.</returns>
        public static IScriptCondition ConvertToPoco(
            this IBdoScope scope,
            ScriptConditionDto dto,
            IBdoLog log = null)
        {
            if (dto == null) return null;

            ScriptCondition poco = new()
            {
                Expression = scope.ConvertToPoco(dto.Expression, log),
                TrueValue = dto.TrueValue
            };

            return poco;
        }
    }
}
