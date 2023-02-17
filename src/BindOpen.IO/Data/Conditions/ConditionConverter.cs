using BindOpen.Logging;
using BindOpen.Runtime.Scopes;

namespace BindOpen.Data.Conditions
{
    /// <summary>
    /// This static class represents a converter.
    /// </summary>
    public static class ConditionConverter
    {
        /// <summary>
        /// Converts to DTO.
        /// </summary>
        /// <param name="poco">The poco to consider.</param>
        /// <returns>The DTO object.</returns>
        public static ConditionDto ToDto(this ICondition poco)
        {
            if (poco is IAdvancedCondition advancedCondition)
            {
                return advancedCondition.ToDto();
            }
            else if (poco is IBasicCondition basicCondition)
            {
                return basicCondition.ToDto();
            }
            else if (poco is IScriptCondition scriptCondition)
            {
                return scriptCondition.ToDto();
            }

            return null;
        }

        /// <summary>
        /// Converts to POCO.
        /// </summary>
        /// <param name="dto">The dto to consider.</param>
        /// <returns>The POCO object.</returns>
        public static ICondition ConvertToPoco(
            this IBdoScope scope,
            ConditionDto dto,
            IBdoLog log = null)
        {
            if (dto is AdvancedConditionDto advancedConditionDto)
            {
                return scope.ConvertToPoco(advancedConditionDto, log);
            }
            else if (dto is BasicConditionDto basicConditionDto)
            {
                return basicConditionDto.ToPoco();
            }
            else if (dto is ScriptConditionDto scriptConditionDto)
            {
                return scope.ConvertToPoco(scriptConditionDto, log);
            }

            return null;
        }
    }
}
