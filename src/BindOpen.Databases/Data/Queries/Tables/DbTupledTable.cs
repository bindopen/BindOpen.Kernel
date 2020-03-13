using BindOpen.Extensions.Carriers;
using System.Collections.Generic;
using System.Linq;

namespace BindOpen.Data.Queries
{
    /// <summary>
    /// This class represents the tupled table.
    /// </summary>
    public class DbTupledTable : DbTable
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// The tuples of this instance.
        /// </summary>
        public List<DbTuple> Tuples { get; set; }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the DbTupledTable class.
        /// </summary>
        /// <param name="tuples">The tuples to consider.</param>
        public DbTupledTable(params DbTuple[] tuples)
        {
            Tuples = tuples?.ToList();
        }

        #endregion
    }
}