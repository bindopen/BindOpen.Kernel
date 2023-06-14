using BindOpen.System.Data.Conditions;

namespace BindOpen.System.Data
{
    /// <summary>
    /// This class represents a data exp that can contain a literal and script texts.
    /// </summary>
    public static partial class BdoData
    {
        /// <summary>
        /// Creates the exp.
        /// </summary>
        /// <param key="kind">The kind of exp to consider.</param>
        /// <param key="text">The text to consider.</param>
        /// <returns>Returns the created exp.</returns>
        public static BdoAdvancedCondition NewCondition(
            AdvancedConditionKind kind,
            params IBdoCondition[] conditions)
            => new(kind, conditions);

        public static BdoBasicCondition NewCondition(
            object arg1,
            ConditionOperator ope,
            object arg2 = null)
            => new(arg1, ope, arg2);

        public static BdoReferenceCondition NewCondition(
            IBdoReference exp)
            => new(exp);
    }
}