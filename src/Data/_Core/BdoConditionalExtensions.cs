﻿using BindOpen.Kernel.Data.Conditions;
using BindOpen.Kernel.Data.Meta;
using BindOpen.Kernel.Logging;
using BindOpen.Kernel.Scoping;

namespace BindOpen.Kernel.Data
{
    /// <summary>
    /// This static class provides methods to handle conditions.
    /// </summary>
    public static class BdoConditionalExtensions
    {
        /// <summary>
        /// Creates the exp.
        /// </summary>
        /// <param key="kind">The kind of exp to consider.</param>
        /// <param key="text">The text to consider.</param>
        /// <returns>Returns the created exp.</returns>
        public static T WithCondition<T>(
            this T obj,
            BdoCompositeConditionKind kind,
            params IBdoCondition[] conditions)
            where T : IBdoConditional
            => obj.WithCondition(new BdoCompositeCondition(kind, conditions));

        public static T WithCondition<T>(
            this T obj,
            object arg1,
            DataOperators ope,
            object arg2 = null)
            where T : IBdoConditional
            => obj.WithCondition(new BdoBasicCondition(arg1, ope, arg2));

        public static T WithCondition<T>(
            this T obj,
            IBdoExpression exp)
             where T : IBdoConditional
            => obj.WithCondition(new BdoExpressionCondition(exp));
    }
}