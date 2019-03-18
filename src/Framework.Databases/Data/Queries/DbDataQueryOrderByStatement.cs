using BindOpen.Framework.Core.Data.Common;
using BindOpen.Framework.Databases.Extensions.Carriers;
using System.ComponentModel;

namespace BindOpen.Framework.Databases.Data.Queries
{

    /// <summary>
    /// This class represents the Order-By statement of a database data query.
    /// </summary>
    public class DbDataQueryOrderByStatement
    {

        // ------------------------------------------
        // VARIABLES
        // ------------------------------------------

        #region Variables

        private DbField _Field;
        private DataSortingMode _Sorting;

        #endregion


        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// The field of this instance.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public DbField Field
        {
            get { return this._Field; }
            set { this._Field = value; }
        }

        /// <summary>
        /// The sorting order of this instance.
        /// </summary>
        public DataSortingMode Sorting
        {
            get { return this._Sorting; }
            set { this._Sorting = value; }
        }

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