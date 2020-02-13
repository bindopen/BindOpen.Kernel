using System.Collections.Generic;

namespace BindOpen.Framework.Data.Queries
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDbQueryFromStatement
    {
        /// <summary>
        /// The alias of this instance.
        /// </summary>
        string Alias { get; set; }

        /// <summary>
        /// 
        /// </summary>
        List<DbQueryJoinStatement> JoinStatements { get; set; }

        /// <summary>
        /// 
        /// </summary>
        DbQueryUnionStatement UnionStatement { get; set; }

        /// <summary>
        /// Sets the specified alias.
        /// </summary>
        /// <param name="alias">The alias to consider.</param>
        /// <returns>Returns this instance.</returns>
        IDbQueryFromStatement WithAlias(string alias);

        /// <summary>
        /// Adds the specified union statement.
        /// </summary>
        /// <param name="statement"></param>
        /// <returns></returns>
        IDbQueryFromStatement WithUnion(IDbQueryUnionStatement statement);

        /// <summary>
        /// Sets the specified join statement.
        /// </summary>
        /// <param name="statements">The DbQueryJoinStatement statements to consider.</param>
        /// <returns>Returns this instance.</returns>
        IDbQueryFromStatement WithJoins(params IDbQueryJoinStatement[] statements);
    }
}