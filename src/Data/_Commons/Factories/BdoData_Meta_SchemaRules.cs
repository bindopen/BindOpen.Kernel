using BindOpen.Data.Conditions;
using BindOpen.Data.Schema;

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
        public static BdoSchemaRule NewRequirement(
            string groupId,
            object value,
            IBdoCondition condition = null,
            string resultCode = null)
        {
            var rule = New<BdoSchemaRule>()
                .WithRuleKind(BdoSchemaRuleKinds.Requirement)
                .WithGroupId(groupId)
                .WithValue(value)
                .WithCondition(condition)
                .WithResultCode(resultCode);

            return rule;
        }

        public static BdoSchemaRule NewConstraint(
            string groupId,
            IBdoReference reference = null,
            IBdoCondition condition = null)
        {
            var rule = New<BdoSchemaRule>()
                .WithRuleKind(BdoSchemaRuleKinds.Constraint)
                .WithGroupId(groupId)
                .WithReference(reference)
                .WithCondition(condition);

            return rule;
        }
    }
}