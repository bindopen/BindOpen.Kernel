﻿using System.Linq;

namespace BindOpen.System.Data.Conditions
{
    /// <summary>
    /// This static class provides methods to handle conditions.
    /// </summary>
    public static class IBdoCompositeConditionExtensions
    {
        public static T WithKind<T>(
            this T obj,
            CompositeConditionKind kind)
            where T : IBdoCompositeCondition
        {
            if (obj != null)
            {
                obj.Kind = kind;
            }

            return obj;
        }

        public static T WithKind<T>(
            this T obj,
            params IBdoCondition[] conditions)
            where T : IBdoCompositeCondition
        {
            if (obj != null)
            {
                obj.Conditions = conditions?.ToList();
            }

            return obj;
        }

    }
}