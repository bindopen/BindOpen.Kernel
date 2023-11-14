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
        public static BdoConstraint NewConstraint(
            object value = null,
            IBdoCondition condition = null,
            string groupId = null,
            string resultCode = null)
        {
            var constraint = New<BdoConstraint>()
                .WithValue(value)
                .WithResultCode(resultCode)
                .WithCondition(condition)
                .WithGroupId(groupId);

            return constraint;
        }
    }
}