using System.Collections.Generic;
using System.Linq;

namespace BindOpen.Framework.Databases.Data.Queries
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
        /// List of jointure statements.
        /// </summary>
        public List<DbQueryJointureStatement> JointureStatements { get; set; } = new List<DbQueryJointureStatement>();

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
        public IDbQueryFromStatement WithUnion(IDbQueryUnionStatement statement)
        {
            UnionStatement = statement as DbQueryUnionStatement;
            return this;
        }

        /// <summary>
        /// Sets the specified jointure statement.
        /// </summary>
        /// <param name="statement">The jointure statement to consider.</param>
        /// <returns>Returns this instance.</returns>
        public IDbQueryFromStatement WithJointure(IDbQueryJointureStatement statement)
        {
            JointureStatements?.Add(statement as DbQueryJointureStatement);
            return this;
        }

        /// <summary>
        /// Sets the specified jointure statement.
        /// </summary>
        /// <param name="statements">The DbQueryJointureStatement statements to consider.</param>
        /// <returns>Returns this instance.</returns>
        public IDbQueryFromStatement WithJointures(params IDbQueryJointureStatement[] statements)
        {
            JointureStatements?.AddRange(statements?.Cast<DbQueryJointureStatement>());
            return this;
        }

        #endregion
    }
}