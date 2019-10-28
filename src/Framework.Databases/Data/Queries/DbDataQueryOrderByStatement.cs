using System.ComponentModel;
using BindOpen.Framework.Core.Data.Common;
using BindOpen.Framework.Databases.Extensions.Carriers;

namespace BindOpen.Framework.Databases.Data.Queries
{
    /// <summary>
    /// This class represents the Order-By statement of a database data query.
    /// </summary>
    public class DbDataQueryOrderByStatement : IDbDataQueryOrderByStatement
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
        /// Instantiates a new instance of the DbDataQueryOrderByStatement class.
        /// </summary>
        public DbDataQueryOrderByStatement()
        {
        }

        #endregion
    }
}