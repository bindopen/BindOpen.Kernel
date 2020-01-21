using System.Collections.Generic;
using System.Linq;

namespace BindOpen.Framework.Data.Queries
{
    /// <summary>
    /// This class represents the From statement of a database data query.
    /// </summary>
    public class DbQueryFromStatement : IDbQueryFromStatement
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// Union statement.
        /// </summary>
        public DbQueryUnionStatement UnionStatement { get; set; }

        /// <summary>
        /// List of join statements.
        /// </summary>
        public List<DbQueryJoinStatement> JoinStatements { get; set; } = new List<DbQueryJoinStatement>();

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the DbQueryFromStatement class.
        /// </summary>
        public DbQueryFromStatement()
        {
        }

        #endregion

        // ------------------------------------------
        // MUTATORS
        // ------------------------------------------

        #region Mutators

        /// <summary>
        /// Sets the specified union statement.
        /// </summary>
        /// <param name="statement">The union statement to consider.</param>
        /// <returns>Returns this instance.</returns>
        public IDbQueryFromStatement Union(IDbQueryUnionStatement statement)
        {
            UnionStatement = statement as DbQueryUnionStatement;
            return this;
        }

        /// <summary>
        /// Adds the specified join statement.
        /// </summary>
        /// <param name="statement">The join statement to consider.</param>
        /// <returns>Returns this instance.</returns>
        public IDbQueryFromStatement Join(IDbQueryJoinStatement statement)
        {
            JoinStatements?.Add(statement as DbQueryJoinStatement);
            return this;
        }

        /// <summary>
        /// Sets the specified join statement.
        /// </summary>
        /// <param name="statements">The DbQueryJoinStatement statements to consider.</param>
        /// <returns>Returns this instance.</returns>
        public IDbQueryFromStatement WithJoins(params IDbQueryJoinStatement[] statements)
        {
            JoinStatements?.AddRange(statements?.Cast<DbQueryJoinStatement>());
            return this;
        }

        #endregion
    }
}