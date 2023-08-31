using BindOpen.System.Data.Conditions;
using BindOpen.System.Data.Meta;
using BindOpen.System.Logging;
using BindOpen.System.Scoping;
using BindOpen.System.Scoping.Script;

namespace BindOpen.System.Data
{
    /// <summary>
    /// This static class provides methods to handle conditions.
    /// </summary>
    public static class IBdoConditionExtensions
    {
        /// <summary>
        /// Evaluate this instance.
        /// </summary>
        /// <param key="condition">The condition to consider.</param>
        /// <param key="scriptInterpreter">Script interpreter.</param>
        /// <param key="varSet">The variable element set used to evaluate.</param>
        /// <returns>True if this instance is true.</returns>
        public static bool Evaluate(
            this IBdoCondition condition,
            IBdoScope scope,
            IBdoMetaSet varSet,
            IBdoLog log = null)
        {
            return condition.Evaluate(
                scope?.Interpreter, varSet, log);
        }

        /// <summary>
        /// Evaluate this instance.
        /// </summary>
        /// <param key="condition">The condition to consider.</param>
        /// <param key="scriptInterpreter">Script interpreter.</param>
        /// <param key="varSet">The variable element set used to evaluate.</param>
        /// <returns>True if this instance is true.</returns>
        public static bool Evaluate(
            this IBdoCondition condition,
            IBdoScriptInterpreter scriptInterpreter,
            IBdoMetaSet varSet,
            IBdoLog log = null)
        {
            if (condition is IBdoCompositeCondition advancedCondition)
            {
                return advancedCondition.Evaluate(scriptInterpreter, varSet, log);
            }
            else if (condition is IBdoBasicCondition basicCondition)
            {
                return basicCondition.Evaluate();
            }
            else if (condition is IBdoExpressionCondition expCondition)
            {
                return expCondition.Evaluate(scriptInterpreter, varSet, log);
            }

            return false;
        }

        /// <summary>
        /// Evaluate this instance.
        /// </summary>
        /// <param key="condition">The condition to consider.</param>
        /// <param key="scriptInterpreter">Script interpreter.</param>
        /// <param key="varSet">The variable element set used to evaluate.</param>
        /// <returns>True if this instance is true.</returns>
        private static bool Evaluate(
            this IBdoCompositeCondition condition,
            IBdoScriptInterpreter scriptInterpreter,
            IBdoMetaSet varSet,
            IBdoLog log = null)
        {
            if (condition == null) return false;

            var b = true;
            foreach (var subCondition in condition.Conditions)
            {
                switch (condition.Kind)
                {
                    case CompositeConditionKind.And:
                        b &= subCondition.Evaluate(scriptInterpreter, varSet, log);
                        break;
                    case CompositeConditionKind.Or:
                        b |= subCondition.Evaluate(scriptInterpreter, varSet, log);
                        break;
                    default:
                        break;
                }
            }

            return b;
        }

        /// <summary>
        /// Evaluate this instance.
        /// </summary>
        /// <param key="condition">The condition to consider.</param>
        /// <param key="scriptInterpreter">Script interpreter.</param>
        /// <param key="varSet">The variable element set used to evaluate.</param>
        /// <returns>True if this instance is true.</returns>
        private static bool Evaluate(this IBdoBasicCondition condition)
        {
            if (condition == null) return false;

            var b = false;
            switch (condition.Operator)
            {
                case DataOperators.DifferentFrom:
                    b = (condition.Argument1 != condition.Argument2);
                    break;
                case DataOperators.EqualTo:
                    b = (condition.Argument1 == condition.Argument2);
                    break;
                case DataOperators.Exists:
                    b = condition.Argument1 != null;
                    break;
                case DataOperators.GreaterThan:
                    //b = (condition.Argument1 > condition.Argument2);
                    break;
                case DataOperators.LesserThan:
                    //b = (Argument1 < Argument2);
                    break;
            }

            return b;
        }

        /// <summary>
        /// Evaluate this instance.
        /// </summary>
        /// <param key="condition">The condition to consider.</param>
        /// <param key="scriptInterpreter">Script interpreter.</param>
        /// <param key="varSet">The variable element set used to evaluate.</param>
        /// <returns>True if the business script value is the true value.</returns>
        private static bool Evaluate(
            this IBdoExpressionCondition condition,
            IBdoScriptInterpreter scriptInterpreter,
            IBdoMetaSet varSet,
            IBdoLog log = null)
        {
            if (condition == null) return false;

            if (condition.Expression == null)
                return false;

            var b = scriptInterpreter?.Evaluate<bool?>(
                condition.Expression, varSet, log);

            return b ?? false;
        }
    }
}