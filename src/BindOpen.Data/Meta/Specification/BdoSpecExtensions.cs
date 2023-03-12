using BindOpen.Data.Conditions;

namespace BindOpen.Data.Meta
{
    /// <summary>
    /// This class represents a data element set.
    /// </summary>
    public static partial class BdoSpecExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param key="condition"></param>
        public static T WithConditions<T>(
            this T spec,
            params ICondition[] conditions)
            where T : IBdoSpec
        {
            if (spec != null)
            {
                spec.ConditionSet ??= BdoData.NewSet(conditions);
            }
            return spec;
        }
    }
}
