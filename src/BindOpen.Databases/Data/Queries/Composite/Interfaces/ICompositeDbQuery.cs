using BindOpen.Data.Items;

namespace BindOpen.Data.Queries
{
    /// <summary>
    /// 
    /// </summary>
    public interface ICompositeDbQuery : IDbQuery, IDescribedDataItem, IIdentifiedDataItem
    {
        /// <summary>
        /// The select join statement of this instance.
        /// </summary>
        IDbQueryJoinStatement SelectJoinStatement { get; set; }

        /// <summary>
        /// The matched query of this instance.
        /// </summary>
        IDbQuery MatchedQuery { get; set; }

        /// <summary>
        /// The not-matched query of this instance.
        /// </summary>
        IDbQuery NotMatchedQuery { get; set; }

        /// <summary>
        /// Sets the from statement.
        /// </summary>
        /// <param name="statement">The from statement to consider.</param>
        /// <returns>Returns this instance.</returns>
        ICompositeDbQuery Select(IDbQueryJoinStatement statement);

        /// <summary>
        /// Sets the matched query.
        /// </summary>
        /// <param name="query">The query to consider.</param>
        /// <returns>Returns this instance.</returns>
        ICompositeDbQuery Matching(IDbQuery query);

        /// <summary>
        /// Sets the not-matched query.
        /// </summary>
        /// <param name="query">The query to consider.</param>
        /// <returns>Returns this instance.</returns>
        ICompositeDbQuery NotMatching(IDbQuery query);
    }
}