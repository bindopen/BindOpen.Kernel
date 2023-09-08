using BindOpen.Kernel.Data.Conditions;
using System.Linq;

namespace BindOpen.Kernel.Data
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
        public static BdoCompositeCondition NewCondition(
            CompositeConditionKind kind,
            params IBdoCondition[] conditions) => new(kind, conditions);

        public static BdoBasicCondition NewCondition(
            object arg1,
            DataOperators ope,
            object arg2 = null) => new(arg1, ope, arg2);

        public static BdoExpressionCondition NewCondition(IBdoExpression exp) => new(exp);

        public static BdoExpressionCondition NewCondition(string script) => new(NewExp(script));

        /// <summary>
        /// Creates the exp.
        /// </summary>
        /// <param key="kind">The kind of exp to consider.</param>
        /// <param key="text">The text to consider.</param>
        /// <returns>Returns the created exp.</returns>
        public static TBdoConditionalStatement<T> NewStatement<T>(
            params (T, IBdoCondition)[] items)
        {
            var statement = new TBdoConditionalStatement<T>();
            statement.AddRange(items?.ToList());

            return statement;
        }
    }
}