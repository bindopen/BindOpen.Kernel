using BindOpen.Extensions.Carriers;
using System.Collections.Generic;
using System.Linq;

namespace BindOpen.Data.Queries
{
    /// <summary>
    /// This class represents a merge data query.
    /// </summary>
    public class DbCompositeQuery : DbQuery, IDbCompositeQuery
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// The queries of this instance.
        /// </summary>
        public List<DbQuery> Queries { get; set; }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the DbCTEQuery class.
        /// </summary>
        public DbCompositeQuery()
        {
        }

        /// <summary>
        /// Instantiates a new instance of the DbCTEQuery class.
        /// </summary>
        /// <param name="kind">The kind to consider.</param>
        /// <param name="table">The table to consider.</param>
        public DbCompositeQuery(DbQueryKind kind, DbTable table) : this(null, kind, table)
        {
        }

        /// <summary>
        /// Instantiates a new instance of the DbCTEQuery class.
        /// </summary>
        /// <param name="name">The name of the query to consider.</param>
        /// <param name="kind">The kind to consider.</param>
        /// <param name="table">The table to consider.</param>
        public DbCompositeQuery(string name, DbQueryKind kind, DbTable table) : base(name, kind, table)
        {
        }

        #endregion

        // ------------------------------------------
        // ACCESSORS
        // ------------------------------------------

        #region Accessors

        /// <summary>
        /// Gets the key of the item.
        /// </summary>
        /// <returns>Returns the key of the item.</returns>
        public override string Key() => Name;

        #endregion

        // ------------------------------------------
        // MUTATORS
        // ------------------------------------------

        #region Mutators

        /// <summary>
        /// Sets the specified queries.
        /// </summary>
        /// <param name="queries">The queries to consider.</param>
        /// <returns>Returns this instance.</returns>
        public IDbCompositeQuery WithQueries(params IDbQuery[] queries)
        {
            Queries = queries?.Cast<DbQuery>().ToList();
            return this;
        }

        #endregion
    }
}