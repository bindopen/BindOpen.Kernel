using BindOpen.System.Scoping.Script;
using BindOpen.System.Data.Conditions;
using BindOpen.System.Data.Meta;
using BindOpen.System.Diagnostics.Logging;
using BindOpen.System.Scoping;

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
            if (condition is IBdoAdvancedCondition advancedCondition)
            {
                return advancedCondition.Evaluate(scriptInterpreter, varSet, log);
            }
            else if (condition is IBdoBasicCondition basicCondition)
            {
                return basicCondition.Evaluate();
            }
            else if (condition is IBdoReferenceCondition referenceCondition)
            {
                return referenceCondition.Evaluate(scriptInterpreter, varSet, log);
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
            this IBdoAdvancedCondition condition,
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
                    case AdvancedConditionKind.And:
                        b &= subCondition.Evaluate(scriptInterpreter, varSet, log);
                        break;
                    case AdvancedConditionKind.Or:
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
            this IBdoReferenceCondition condition,
            IBdoScriptInterpreter scriptInterpreter,
            IBdoMetaSet varSet,
            IBdoLog log = null)
        {
            if (condition == null) return false;

            if (condition.Reference == null)
                return false;

            var b = scriptInterpreter?.Evaluate<bool?>(
                condition.Reference, varSet, log);

            return b ?? false;
        }
    }
}