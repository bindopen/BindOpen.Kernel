using BindOpen.Data.Conditions;
using BindOpen.Data.Meta;
using BindOpen.Logging;
using BindOpen.Scoping;
using BindOpen.Scoping.Script;

namespace BindOpen.Data
{
    /// <summary>
    /// This static class provides methods to handle conditions.
    /// </summary>
    public static class IBdoConditionExtensions
    {
        /// <summary>
        /// Evaluates this instance.
        /// </summary>
        /// <param key="condition">The condition to consider.</param>
        /// <param key="scriptInterpreter">Script interpreter.</param>
        /// <param key="metaSet">The variable element set used to evaluate.</param>
        /// <returns>True if this instance is true.</returns>
        public static bool Evaluate(
            this IBdoScriptInterpreter interpreter,
            IBdoCondition condition,
            IBdoMetaSet varSet = null,
            IBdoLog log = null)
        {
            if (condition is IBdoCompositeCondition advancedCondition)
            {
                return interpreter.Evaluate(advancedCondition, varSet, log);
            }
            else if (condition is IBdoBasicCondition basicCondition)
            {
                return Evaluate(basicCondition);
            }
            else if (condition is IBdoExpressionCondition expCondition)
            {
                return interpreter.Evaluate(expCondition, varSet, log);
            }

            return false;
        }

        /// <summary>
        /// Evaluates this instance.
        /// </summary>
        /// <param key="condition">The condition to consider.</param>
        /// <param key="scriptInterpreter">Script interpreter.</param>
        /// <param key="metaSet">The variable element set used to evaluate.</param>
        /// <returns>True if this instance is true.</returns>
        private static bool Evaluate(
            this IBdoScriptInterpreter interpreter,
            IBdoCompositeCondition condition,
            IBdoMetaSet varSet = null,
            IBdoLog log = null)
        {
            if (condition == null) return false;

            var b = true;
            foreach (var subCondition in condition.Conditions)
            {
                var value = interpreter.Evaluate(subCondition, varSet, log);

                switch (condition.CompositionKind)
                {
                    case BdoCompositeConditionKind.And:
                        b &= value;
                        break;
                    case BdoCompositeConditionKind.Or:
                        b |= value;
                        break;
                    default:
                        break;
                }
            }

            return b;
        }

        /// <summary>
        /// Evaluates this instance.
        /// </summary>
        /// <param key="condition">The condition to consider.</param>
        /// <param key="scriptInterpreter">Script interpreter.</param>
        /// <param key="metaSet">The variable element set used to evaluate.</param>
        /// <returns>True if this instance is true.</returns>
        private static bool Evaluate(IBdoBasicCondition condition)
        {
            if (condition == null) return false;

            var b = false;
            switch (condition.Operator)
            {
                case DataOperators.DifferentFrom:
                    b = condition.Argument1 != condition.Argument2;
                    break;
                case DataOperators.EqualsTo:
                    b = condition.Argument1 == condition.Argument2;
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
        /// Evaluates this instance.
        /// </summary>
        /// <param key="condition">The condition to consider.</param>
        /// <param key="scriptInterpreter">Script interpreter.</param>
        /// <param key="metaSet">The variable element set used to evaluate.</param>
        /// <returns>True if the business script value is the true value.</returns>
        private static bool Evaluate(
            this IBdoScriptInterpreter interpreter,
            IBdoExpressionCondition condition,
            IBdoMetaSet varSet = null,
            IBdoLog log = null)
        {
            if (condition == null) return false;

            if (condition.Expression == null)
                return false;

            var b = interpreter?.Evaluate<bool?>(condition.Expression, varSet, log);

            return b ?? false;
        }
    }
}