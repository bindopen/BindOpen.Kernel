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
        List<DbQueryJointureStatement> JointureStatements { get; set; }

        /// <summary>
        /// 
        /// </summary>
        DbQueryUnionStatement UnionStatement { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="statement"></param>
        /// <returns></returns>
        IDbQueryFromStatement WithUnion(IDbQueryUnionStatement statement);

        /// <summary>
        /// Sets the specified jointure statement.
        /// </summary>
        /// <param name="statement">The jointure statement to consider.</param>
        /// <returns>Returns this instance.</returns>
        IDbQueryFromStatement WithJointure(IDbQueryJointureStatement statement);

        /// <summary>
        /// Sets the specified jointure statement.
        /// </summary>
        /// <param name="statements">The DbQueryJointureStatement statements to consider.</param>
        /// <returns>Returns this instance.</returns>
        IDbQueryFromStatement WithJointures(params IDbQueryJointureStatement[] statements);
    }
}