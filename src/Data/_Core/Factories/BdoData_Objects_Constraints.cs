using BindOpen.Kernel.Data.Conditions;
using BindOpen.Kernel.Data.Meta;

namespace BindOpen.Kernel.Data
{
    /// <summary>
    /// This class represents a data exp that can contain a literal and script texts.
    /// </summary>
    public static partial class BdoData
    {
        /// <summary>
        /// Creates a new constraint.
        /// </summary>
        /// <param key="kind">The kind of exp to consider.</param>
        /// <param key="text">The text to consider.</param>
        /// <returns>Returns the created exp.</returns>
        public static BdoConstraint NewConstraintRequirement(
            string groupId = null,
            object value = null,
            IBdoCondition condition = null,
            string resultCode = null)
        {
            var constraint = New<BdoConstraint>()
                .WithMode(BdoConstraintModes.Requirement)
                .WithGroupId(groupId)
                .WithValue(value)
                .WithCondition(condition)
                .WithResultCode(resultCode);

            return constraint;
        }

        public static BdoConstraint NewConstraintRule(
            string groupId = null,
            IBdoCondition condition = null,
            string resultCode = null)
        {
            var constraint = New<BdoConstraint>()
                .WithMode(BdoConstraintModes.Rule)
                .WithGroupId(groupId)
                .WithCondition(condition)
                .WithResultCode(resultCode);

            return constraint;
        }
    }
}