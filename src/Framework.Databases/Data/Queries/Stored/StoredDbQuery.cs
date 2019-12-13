using System.Collections.Generic;

namespace BindOpen.Framework.Databases.Data.Queries
{
    /// <summary>
    /// This class represents a stored data query.
    /// </summary>
    public class StoredDbQuery : DbQuery, IStoredDbQuery
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
        public StoredDbQuery()
        {
        }

        /// <summary>
        /// Instantiates a new instance of the StoredDbQuery class.
        /// </summary>
        /// <param name="query">The query to consider.</param>
        /// <param name="name">The name of the query to consider.</param>
        public StoredDbQuery(IDbQuery query, string name = null)
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

        #endregion
    }
}