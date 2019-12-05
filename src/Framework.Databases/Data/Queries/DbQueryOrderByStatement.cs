using BindOpen.Framework.Core.Data.Common;
using BindOpen.Framework.Databases.Extensions.Carriers;
using System.ComponentModel;

namespace BindOpen.Framework.Databases.Data.Queries
{
    /// <summary>
    /// This class represents the Order-By statement of a database data query.
    /// </summary>
    public class DbQueryOrderByStatement : IDbQueryOrderByStatement
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// The field of this instance.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public DbField Field { get; set; }

        /// <summary>
        /// The sorting order of this instance.
        /// </summary>
        public DataSortingMode Sorting { get; set; }

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