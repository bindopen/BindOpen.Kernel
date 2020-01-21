using System.Collections.Generic;

namespace BindOpen.Framework.Data.Queries
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDbQueryFromStatement
    {
        /// <summary>
        /// 
        /// </summary>
        List<DbQueryJoinStatement> JoinStatements { get; set; }

        /// <summary>
        /// 
        /// </summary>
        DbQueryUnionStatement UnionStatement { get; set; }

        /// <summary>
        /// Adds the specified union statement.
        /// </summary>
        /// <param name="statement"></param>
        /// <returns></returns>
        IDbQueryFromStatement Union(IDbQueryUnionStatement statement);

        /// <summary>
        /// Adds the specified join statement.
        /// </summary>
        /// <param name="statement">The join statement to consider.</param>
        /// <returns>Returns this instance.</returns>
        IDbQueryFromStatement Join(IDbQueryJoinStatement statement);

        /// <summary>
        /// Sets the specified join statement.
        /// </summary>
        /// <param name="statements">The DbQueryJoinStatement statements to consider.</param>
        /// <returns>Returns this instance.</returns>
        IDbQueryFromStatement WithJoins(params IDbQueryJoinStatement[] statements);
    }
}