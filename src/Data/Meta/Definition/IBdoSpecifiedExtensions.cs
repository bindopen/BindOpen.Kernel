﻿using BindOpen.Data.Conditions;
using System;

namespace BindOpen.Data.Meta
{
    /// <summary>
    /// This class represents a data element set.
    /// </summary>
    public static class IBdoSpecifiedExtensions
    {
        public static T WithSpec<T, TSpec>(
            this T obj,
            Action<IBdoSpec> action)
            where T : IBdoSpecified
            where TSpec : IBdoSpec, new()
        {
            if (obj != null)
            {
                obj.Spec ??= BdoData.NewSpec<TSpec>();
                action?.Invoke(obj.Spec);
            }

            return obj;
        }

        public static T WithSpec<T>(
            this T obj,
            Action<IBdoSpec> action)
            where T : IBdoSpecified
            => obj.WithSpec<T, BdoSpec>(action);

        public static T WithSpecRules<T>(
            this T obj,
            params IBdoSpecRule[] rules)
            where T : IBdoSpecified
        {
            if (obj != null)
            {
                obj.Spec = BdoData.NewSpec()
                    .WithRules(rules);
            }

            return obj;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param key="level"></param>
        public static T WithSpecRuleRequirements<T>(
            this T obj,
            params (string Reference, object Value, IBdoCondition Condition)[] rules)
            where T : IBdoSpecified
        {
            if (obj != null)
            {
                obj.Spec = BdoData.NewSpec();

                foreach (var (Reference, Value, Condition) in rules)
                {
                    obj.AddSpecRuleRequirement(Reference, Value, Condition);
                }
            }

            return obj;
        }

        public static T AddSpecRule<T>(
            this T obj,
            IBdoSpecRule rule)
            where T : IBdoSpecified
        {
            if (obj != null)
            {
                obj.Spec ??= BdoData.NewSpec();
                obj.Spec.GetOrNewRuleSet().Add(rule);
            }

            return obj;
        }

        public static T AddSpecRuleRequirement<T>(
            this T obj,
            string groupId,
            object value,
            IBdoCondition condition = null)
            where T : IBdoSpecified
        {
            return obj.AddSpecRule((BdoSpecRule)(groupId, value, condition));
        }
    }
}
