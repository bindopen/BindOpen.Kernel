using BindOpen.Extensions.Carriers;

namespace BindOpen.Data.Queries
{
    /// <summary>
    /// This class represents a merge data query.
    /// </summary>
    public class CompositeDbQuery : DbQuery, ICompositeDbQuery
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// The join statement of this instance.
        /// </summary>
        public IDbQueryJoinStatement SelectJoinStatement { get; set; }

        /// <summary>
        /// The matched query of this instance.
        /// </summary>
        public IDbQuery MatchedQuery { get; set; }

        /// <summary>
        /// The not-matched query of this instance.
        /// </summary>
        public IDbQuery NotMatchedQuery { get; set; }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the CompositeDbQuery class.
        /// </summary>
        public CompositeDbQuery()
        {
        }

        /// <summary>
        /// Instantiates a new instance of the CompositeDbQuery class.
        /// </summary>
        /// <param name="kind">The kind to consider.</param>
        /// <param name="table">The table to consider.</param>
        public CompositeDbQuery(DbQueryKind kind, DbTable table) : this(null, kind, table)
        {
        }

        /// <summary>
        /// Instantiates a new instance of the CompositeDbQuery class.
        /// </summary>
        /// <param name="name">The name of the query to consider.</param>
        /// <param name="kind">The kind to consider.</param>
        /// <param name="table">The table to consider.</param>
        public CompositeDbQuery(string name, DbQueryKind kind, DbTable table) : base(name, kind, table)
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
        /// Sets the from statement.
        /// </summary>
        /// <param name="statement">The from statement to consider.</param>
        /// <returns>Returns this instance.</returns>
        public ICompositeDbQuery Select(IDbQueryJoinStatement statement)
        {
            SelectJoinStatement = statement;

            return this;
        }

        /// <summary>
        /// Sets the matched query.
        /// </summary>
        /// <param name="query">The query to consider.</param>
        /// <returns>Returns this instance.</returns>
        public ICompositeDbQuery Matching(IDbQuery query)
        {
            MatchedQuery = query;

            return this;
        }

        /// <summary>
        /// Sets the not-matched query.
        /// </summary>
        /// <param name="query">The query to consider.</param>
        /// <returns>Returns this instance.</returns>
        public ICompositeDbQuery NotMatching(IDbQuery query)
        {
            NotMatchedQuery = query;

            return this;
        }

        #endregion
    }
}