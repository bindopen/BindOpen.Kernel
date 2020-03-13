using BindOpen.Data.Common;
using BindOpen.Extensions.Carriers;

namespace BindOpen.Data.Queries
{
    /// <summary>
    /// This class represents the Order-By clause of a database data query.
    /// </summary>
    public class DbQueryOrderByStatement : IDbQueryOrderByStatement
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// The sorting order of this instance.
        /// </summary>
        public DataSortingMode Sorting { get; set; }

        /// <summary>
        /// The field of this instance.
        /// </summary>
        public DbField Field { get; set; }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the DbQueryOrderByStatement class.
        /// </summary>
        public DbQueryOrderByStatement()
        {
        }

        #endregion
    }
}