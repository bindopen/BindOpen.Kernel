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
        /// The alias of this instance.
        /// </summary>
        public string Alias { get; set; }

        /// <summary>
        /// The union statement of this instance.
        /// </summary>
        public DbQueryUnionStatement UnionStatement { get; set; }

        /// <summary>
        /// The list of join statements of this instance.
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
        /// Sets the specified alias.
        /// </summary>
        /// <param name="alias">The alias to consider.</param>
        /// <returns>Returns this instance.</returns>
        public IDbQueryFromStatement WithAlias(string alias)
        {
            Alias = alias;
            return this;
        }

        /// <summary>
        /// Sets the specified union statement.
        /// </summary>
        /// <param name="statement">The union statement to consider.</param>
        /// <returns>Returns this instance.</returns>
        public IDbQueryFromStatement WithUnion(IDbQueryUnionStatement statement)
        {
            UnionStatement = statement as DbQueryUnionStatement;
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