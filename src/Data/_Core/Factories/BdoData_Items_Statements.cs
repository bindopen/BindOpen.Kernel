using BindOpen.System.Data.Conditions;
using System.Linq;

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
        public static TBdoConditionalStatement<T> NewStatement<T>(
            params (T, IBdoCondition)[] items)
        {
            var statement = new TBdoConditionalStatement<T>();
            statement.AddRange(items?.ToList());

            return statement;
        }
    }
}