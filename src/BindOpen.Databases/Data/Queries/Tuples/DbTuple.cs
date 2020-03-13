using BindOpen.Extensions.Carriers;
using System.Collections.Generic;

namespace BindOpen.Data.Queries
{
    /// <summary>
    /// This class represents the database tuple.
    /// </summary>
    public class DbTuple : IDbTuple
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// The fields of this instance.
        /// </summary>
        public List<DbField> Fields { get; set; }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the DbTuple class.
        /// </summary>
        public DbTuple()
        {
        }

        #endregion
    }
}