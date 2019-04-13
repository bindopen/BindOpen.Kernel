using System;
using BindOpen.Framework.Core.Data.Conditions;
using BindOpen.Framework.Core.System.Scripting;
using static BindOpen.Framework.Core.Data.Conditions.AdvancedCondition;

namespace BindOpen.Framework.Core.Data.Conditions
{
    /// <summary>
    /// This static class provides methods to handle conditions.
    /// </summary>
    public static class ConditionHandler
    {
        /// <summary>
        /// Evaluate this instance.
        /// </summary>
        /// <param name="condition">The condition to consider.</param>
        /// <param name="scriptInterpreter">Script interpreter.</param>
        /// <param name="scriptVariableSet">The script variable set used to evaluate.</param>
        /// <returns>True if this instance is true.</returns>
        public static bool Evaluate(
            this ICondition condition,
            IScriptInterpreter scriptInterpreter,
            IScriptVariableSet scriptVariableSet)
        {
            if (condition is AdvancedCondition advancedCondition)
            {
                return advancedCondition.Evaluate(scriptInterpreter, scriptVariableSet);
            }
            else if (condition is BasicCondition basicCondition)
            {
                return basicCondition.Evaluate(scriptInterpreter, scriptVariableSet);
            }
            else if (condition is ScriptCondition scriptCondition)
            {
                return scriptCondition.Evaluate(scriptInterpreter, scriptVariableSet);
            }

            return false;
        }

        /// <summary>
        /// Evaluate this instance.
        /// </summary>
        /// <param name="condition">The condition to consider.</param>
        /// <param name="scriptInterpreter">Script interpreter.</param>
        /// <param name="scriptVariableSet">The script variable set used to evaluate.</param>
        /// <returns>True if this instance is true.</returns>
        private static bool Evaluate(
            this AdvancedCondition condition,
            IScriptInterpreter scriptInterpreter,
            IScriptVariableSet scriptVariableSet)
        {
            if (condition == null) return false;

            bool isAllConditionSatisfied = true;
            foreach (Condition subCondition in condition.Conditions)
            {
                switch (condition.Kind)
                {
                    case AdvancedConditionKind.And:
                        isAllConditionSatisfied &= subCondition.Evaluate(scriptInterpreter, scriptVariableSet);
                        break;
                    case AdvancedConditionKind.Or:
                        isAllConditionSatisfied |= subCondition.Evaluate(scriptInterpreter, scriptVariableSet);
                        break;
                    default:
                        break;
                }
            }

            return isAllConditionSatisfied == condition.TrueValue;
        }

        /// <summary>
        /// Evaluate this instance.
        /// </summary>
        /// <param name="condition">The condition to consider.</param>
        /// <param name="scriptInterpreter">Script interpreter.</param>
        /// <param name="scriptVariableSet">The script variable set used to evaluate.</param>
        /// <returns>True if this instance is true.</returns>
        private static bool Evaluate(
            this BasicCondition condition,
            IScriptInterpreter scriptInterpreter,
            IScriptVariableSet scriptVariableSet)
        {
            if (condition == null) return false;

            bool isConditionSatisfied = false;
            switch (condition.Operator)
            {
                case ConditionOperator.DifferentFrom:
                    isConditionSatisfied = (condition.Argument1 != condition.Argument2);
                    break;
                case ConditionOperator.EqualTo:
                    isConditionSatisfied = (condition.Argument1 == condition.Argument2);
                    break;
                case ConditionOperator.Exist:
                    //isConditionSatisfied = !string.IsNullOrEmpty(Argument1);
                    break;
                case ConditionOperator.GreaterThan:
                    //isConditionSatisfied = (Argument1 > Argument2);
                    break;
                case ConditionOperator.LesserThan:
                    //isConditionSatisfied = (Argument1 < Argument2);
                    break;
            }

            return isConditionSatisfied == condition.TrueValue;
        }

        /// <summary>
        /// Evaluate this instance.
        /// </summary>
        /// <param name="condition">The condition to consider.</param>
        /// <param name="scriptInterpreter">Script interpreter.</param>
        /// <param name="scriptVariableSet">The script variable set used to evaluate.</param>
        /// <returns>True if the business script value is the true value.</returns>
        private static bool Evaluate(
            this ScriptCondition condition,
            IScriptInterpreter scriptInterpreter,
            IScriptVariableSet scriptVariableSet)
        {
            if (condition == null) return false;

            if (condition.Expression == null)
                return false;

            string st = scriptInterpreter.Interprete(condition.Expression, scriptVariableSet);

            return condition.TrueValue ?
                string.Compare(st, "%true()", StringComparison.OrdinalIgnoreCase) == 0 :
                string.Compare(st, "%false()", StringComparison.OrdinalIgnoreCase) == 0;
        }
    }
}