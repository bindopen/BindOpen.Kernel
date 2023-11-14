using BindOpen.Kernel.Data.Conditions;
using System.Linq;

namespace BindOpen.Kernel.Data.Meta
{
    /// <summary>
    /// This class represents a data element set.
    /// </summary>
    public static class IBdoSpecifiedExtensions
    {
        public static T WithConstraints<T>(
            this T obj,
            params IBdoConstraint[] constraints)
            where T : IBdoSpecified
        {
            if (obj != null)
            {
                obj.Spec = BdoData.NewSpec()
                    .With(constraints);
            }

            return obj;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param key="level"></param>
        public static T WithConstraints<T>(
            this T obj,
            params (RequirementLevels Level, IBdoCondition Condition)[] constraints)
            where T : IBdoSpecified
        {
            obj.WithConstraints(constraints.Select(q=> (BdoConstraint)(q.Level.ToString(), q.Condition, null)).ToArray());

            return obj;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param key="level"></param>
        public static T WithConstraints<T>(
            this T obj,
            params (RequirementLevels Level, IBdoCondition Condition, string GroupId)[] constraints)
            where T : IBdoSpecified
        {
            if (obj != null)
            {
                obj.Spec = BdoData.NewSpec();

                foreach (var (Level, Condition, GroupId) in constraints)
                {
                    obj.AddConstraint(Level, Condition, GroupId);
                }
            }

            return obj;
        }

        public static T AddConstraint<T>(
            this T obj,
            IBdoConstraint constraint)
            where T : IBdoSpecified
        {
            if (obj != null)
            {
                obj.Spec ??= BdoData.NewSpec();
                obj.Spec.Add(constraint);
            }

            return obj;
        }

        public static T AddConstraint<T>(
            this T obj,
            RequirementLevels level,
            IBdoCondition condition = null,
            string groupId = null)
            where T : IBdoSpecified
        {
            return obj.AddConstraint((BdoConstraint)(level.ToString(), condition, groupId));
        }
    }
}
