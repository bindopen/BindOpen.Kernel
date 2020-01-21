using BindOpen.Framework.Data.Expression;
using BindOpen.Framework.Extensions.Carriers;

namespace BindOpen.Framework.Data.Queries
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDbQueryJoinStatement
    {
        /// <summary>
        /// 
        /// </summary>
        DataExpression Condition { get; set; }

        /// <summary>
        /// 
        /// </summary>
        DbQueryJoinKind Kind { get; set; }

        /// <summary>
        /// 
        /// </summary>
        DbTable Table { get; set; }

        /// <summary>
        /// Sets the specified condition.
        /// </summary>
        /// <param name="condition">The condition to consider.</param>
        /// <returns>Returns this instance.</returns>
        IDbQueryJoinStatement WithCondition(DataExpression condition);

        /// <summary>
        /// Sets the specified condition.
        /// </summary>
        /// <param name="condition">The condition to consider.</param>
        /// <returns>Returns this instance.</returns>
        IDbQueryJoinStatement WithCondition(string condition);
    }
}