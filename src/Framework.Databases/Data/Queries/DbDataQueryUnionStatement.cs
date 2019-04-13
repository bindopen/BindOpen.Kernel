
namespace BindOpen.Framework.Databases.Data.Queries
{
    /// <summary>
    /// This class represents a union statement of a database data query.
    /// </summary>
    public class DbDataQueryUnionStatement : IDbDataQueryUnionStatement
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// Type of this instance.
        /// </summary>
        public DbDataQueryUnionKind Type { get; set; }

        /// <summary>
        /// Data query of this instance.
        /// </summary>
        public IAdvancedDbDataQuery Query { get; set; }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the DbDataQueryUnionStatement class.
        /// </summary>
        public DbDataQueryUnionStatement()
        {
        }

        #endregion
    }
}