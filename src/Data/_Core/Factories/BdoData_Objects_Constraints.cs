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
            string reference = null,
            object value = null,
            IBdoCondition condition = null,
            string resultCode = null)
        {
            var constraint = New<BdoConstraint>()
                .WithMode(BdoConstraintModes.Requirement)
                .WithReference(reference)
                .WithValue(value)
                .WithCondition(condition)
                .WithResultCode(resultCode);

            return constraint;
        }

        public static BdoConstraint NewConstraintRule(
            IBdoReference reference = null,
            IBdoCondition condition = null,
            string resultCode = null)
        {
            var constraint = New<BdoConstraint>()
                .WithMode(BdoConstraintModes.Rule)
                .WithReference(reference)
                .WithCondition(condition)
                .WithResultCode(resultCode);

            return constraint;
        }
    }
}