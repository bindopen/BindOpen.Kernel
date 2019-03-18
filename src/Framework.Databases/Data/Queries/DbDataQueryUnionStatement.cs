
namespace BindOpen.Framework.Databases.Data.Queries
{

    /// <summary>
    /// This class represents a union statement of a database data query.
    /// </summary>
    public class DbDataQueryUnionStatement
    {

        // ------------------------------------------
        // VARIABLES
        // ------------------------------------------

        #region Variables

        private DbDataQueryUnionKind _Type;
        private AdvancedDbDataQuery _DataQuery;

        #endregion


        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// Type of this instance.
        /// </summary>
        public DbDataQueryUnionKind Type
        {
            get { return this._Type; }
            set { this._Type = value; }
        }

        /// <summary>
        /// Data query of this instance.
        /// </summary>
        public AdvancedDbDataQuery DataQuery
        {
            get { return this._DataQuery; }
            set { this._DataQuery = value; }
        }

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