namespace BindOpen.Data.Queries
{
    /// <summary>
    /// This class represents an simple database data query.
    /// </summary>
    public class StoredProcedureDbQuery : DbQuery
    {
        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the StoredProcedureDbQuery class.
        /// </summary>
        public StoredProcedureDbQuery()
        {
        }

        /// <summary>
        /// Instantiates a new instance of the StoredProcedureDbQuery class.
        /// </summary>
        /// <param name="dataModule">Name of the data module.</param>
        /// <param name="schema">Schema of the data module.</param>
        /// <param name="storedProcedureName">Name of stored procedure.</param>
        public StoredProcedureDbQuery(
            string dataModule = null,
            string schema = null,
            string storedProcedureName = null)
        {
            Kind = DbQueryKind.None;
            DataModule = dataModule;
            Schema = schema;
            DataTable = storedProcedureName;
        }

        #endregion
    }
}