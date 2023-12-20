using BindOpen.Data.Conditions;
using BindOpen.Data.Meta;

namespace BindOpen.Data
{
    /// <summary>
    /// This class represents a data exp that can contain a literal and script texts.
    /// </summary>
    public static partial class BdoData
    {
        /// <summary>
        /// Creates a new rule.
        /// </summary>
        /// <param key="kind">The kind of exp to consider.</param>
        /// <param key="text">The text to consider.</param>
        /// <returns>Returns the created exp.</returns>
        public static BdoSpecRule NewRequirement(
            string groupId,
            object value,
            IBdoCondition condition = null,
            string resultCode = null)
        {
            var rule = New<BdoSpecRule>()
                .WithMode(BdoSpecRuleKinds.Requirement)
                .WithGroupId(groupId)
                .WithValue(value)
                .WithCondition(condition)
                .WithResultCode(resultCode);

            return rule;
        }

        public static BdoSpecRule NewConstraint(
            string groupId,
            IBdoReference reference = null,
            IBdoCondition condition = null)
        {
            var rule = New<BdoSpecRule>()
                .WithMode(BdoSpecRuleKinds.Constraint)
                .WithGroupId(groupId)
                .WithReference(reference)
                .WithCondition(condition);

            return rule;
        }
    }
}