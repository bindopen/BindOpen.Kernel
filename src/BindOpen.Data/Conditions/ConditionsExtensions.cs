using BindOpen.Extensions.Scripting;
using BindOpen.Data.Meta;

namespace BindOpen.Data.Conditions
{
    /// <summary>
    /// This static class provides methods to handle conditions.
    /// </summary>
    public static class ConditionsExtensions
    {
        /// <summary>
        /// Evaluate this instance.
        /// </summary>
        /// <param name="condition">The condition to consider.</param>
        /// <param name="scriptInterpreter">Script interpreter.</param>
        /// <param name="varSet">The variable element set used to evaluate.</param>
        /// <returns>True if this instance is true.</returns>
        public static bool Evaluate(
            this ICondition condition,
            IBdoScriptInterpreter scriptInterpreter,
            IBdoMetaSet varSet)
        {
            if (condition is IAdvancedCondition advancedCondition)
            {
                return advancedCondition.Evaluate(scriptInterpreter, varSet);
            }
            else if (condition is IBasicCondition basicCondition)
            {
                return basicCondition.Evaluate();
            }
            else if (condition is IScriptCondition scriptCondition)
            {
                return scriptCondition.Evaluate(scriptInterpreter, varSet);
            }

            return false;
        }

        /// <summary>
        /// Evaluate this instance.
        /// </summary>
        /// <param name="condition">The condition to consider.</param>
        /// <param name="scriptInterpreter">Script interpreter.</param>
        /// <param name="varSet">The variable element set used to evaluate.</param>
        /// <returns>True if this instance is true.</returns>
        private static bool Evaluate(
            this IAdvancedCondition condition,
            IBdoScriptInterpreter scriptInterpreter,
            IBdoMetaSet varSet)
        {
            if (condition == null) return false;

            var b = true;
            foreach (var subCondition in condition.Conditions)
            {
                switch (condition.Kind)
                {
                    case AdvancedConditionKind.And:
                        b &= subCondition.Evaluate(scriptInterpreter, varSet);
                        break;
                    case AdvancedConditionKind.Or:
                        b |= subCondition.Evaluate(scriptInterpreter, varSet);
                        break;
                    default:
                        break;
                }
            }

            return b == condition.TrueValue;
        }

        /// <summary>
        /// Evaluate this instance.
        /// </summary>
        /// <param name="condition">The condition to consider.</param>
        /// <param name="scriptInterpreter">Script interpreter.</param>
        /// <param name="varSet">The variable element set used to evaluate.</param>
        /// <returns>True if this instance is true.</returns>
        private static bool Evaluate(this IBasicCondition condition)
        {
            if (condition == null) return false;

            var b = false;
            switch (condition.Operator)
            {
                case ConditionOperator.DifferentFrom:
                    b = (condition.Argument1 != condition.Argument2);
                    break;
                case ConditionOperator.EqualTo:
                    b = (condition.Argument1 == condition.Argument2);
                    break;
                case ConditionOperator.Exist:
                    //b = !string.IsNullOrEmpty(condition.Argument1);
                    break;
                case ConditionOperator.GreaterThan:
                    //b = (Argument1 > Argument2);
                    break;
                case ConditionOperator.LesserThan:
                    //b = (Argument1 < Argument2);
                    break;
            }

            return b == condition.TrueValue;
        }

        /// <summary>
        /// Evaluate this instance.
        /// </summary>
        /// <param name="condition">The condition to consider.</param>
        /// <param name="scriptInterpreter">Script interpreter.</param>
        /// <param name="varSet">The variable element set used to evaluate.</param>
        /// <returns>True if the business script value is the true value.</returns>
        private static bool Evaluate(
            this IScriptCondition condition,
            IBdoScriptInterpreter scriptInterpreter,
            IBdoMetaSet varSet)
        {
            if (condition == null) return false;

            if (condition.Expression == null)
                return false;

            var b = scriptInterpreter.Evaluate<bool?>(condition.Expression, varSet);

            return condition.TrueValue ? (b ?? false) : !(b ?? true);
        }
    }
}