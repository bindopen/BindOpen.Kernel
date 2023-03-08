using BindOpen.Data;

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
        /// <param key="poco">The poco to consider.</param>
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
        /// <param key="dto">The dto to consider.</param>
        /// <returns>The POCO object.</returns>
        public static IScriptCondition ToPoco(
            this ScriptConditionDto dto)
        {
            if (dto == null) return null;

            ScriptCondition poco = new()
            {
                Expression = dto.Expression.ToPoco(),
                TrueValue = dto.TrueValue
            };

            return poco;
        }
    }
}
