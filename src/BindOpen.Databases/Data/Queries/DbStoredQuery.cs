using System.Collections.Generic;

namespace BindOpen.Data.Queries
{
    /// <summary>
    /// This class represents a stored data query.
    /// </summary>
    public class DbStoredQuery : DbQuery, IDbStoredQuery
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// The query of this instance.
        /// </summary>
        public IDbQuery Query { get; set; }

        /// <summary>
        /// The SQL query text of this instance.
        /// </summary>
        public Dictionary<string, string> QueryTexts { get; set; } = new Dictionary<string, string>();

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the StoredDbQuery class.
        /// </summary>
        public DbStoredQuery()
        {
        }

        /// <summary>
        /// Instantiates a new instance of the StoredDbQuery class.
        /// </summary>
        /// <param name="query">The query to consider.</param>
        /// <param name="name">The name of the query to consider.</param>
        public DbStoredQuery(IDbQuery query, string name = null)
        {
            Query = query;
            Name = name;
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

        /// <summary>
        /// Gets the name of this instance.
        /// </summary>
        /// <returns>Returns the name of this instance.</returns>
        /// <remarks>If the name of this instance is empty or null then the returned name is determined from this instance's properties.</remarks>
        public override string GetName()
        {
            var st = base.GetName();


            if (string.IsNullOrEmpty(st))
            {
                st = Query?.GetName();
            }

            return st;
        }

        #endregion
    }
}