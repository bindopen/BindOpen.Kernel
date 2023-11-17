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
        public static T WithConstraintRequirements<T>(
            this T obj,
            params (string GroupId, object Value, IBdoCondition Condition)[] constraints)
            where T : IBdoSpecified
        {
            if (obj != null)
            {
                obj.Spec = BdoData.NewSpec();

                foreach (var (GroupId, Value, Condition) in constraints)
                {
                    obj.AddConstraintRequirement(GroupId, Value, Condition);
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

        public static T AddConstraintRequirement<T>(
            this T obj,
            string groupId,
            object value,
            IBdoCondition condition = null)
            where T : IBdoSpecified
        {
            return obj.AddConstraint((BdoConstraint)(groupId, value, condition));
        }
    }
}
