using BindOpen.Framework.Databases.Extensions.Carriers;
using System;
using System.Collections.Generic;

namespace BindOpen.Framework.Databases.Data.Queries
{
    /// <summary>
    /// This class represents an simple database data query.
    /// </summary>
    public class BasicDbQuery : DbQuery, IBasicDbQuery
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// Indicates whether this instance is distinct. When distinct an advanced Select 
        /// database data query only returns distinct records.
        /// </summary>
        public bool IsDistinct { get; set; }

        /// <summary>
        /// Number of top items of this instance. Top items are the items a advanced Select 
        /// database data query will return.
        /// </summary>
        /// <remarks>By default it is -1 meaning no limit.</remarks>
        public int Top { get; set; } = -1;

        /// <summary>
        /// ID fields of this instance.
        /// </summary>
        public List<DbField> IdFields { get; set; } = new List<DbField>();

        /// <summary>
        /// From clause of this instance.
        /// </summary>
        public List<IDbQueryFromStatement> FromClauses { get; set; } = new List<IDbQueryFromStatement>();

        /// <summary>
        /// Order by statements of this instance.
        /// </summary>
        public List<IDbQueryOrderByStatement> OrderByStatements { get; set; } = new List<IDbQueryOrderByStatement>();

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the BasicDbQuery class.
        /// </summary>
        public BasicDbQuery()
        {
        }

        /// <summary>
        /// Instantiates a new instance of the BasicDbQuery class.
        /// </summary>
        /// <param name="kind">Kind of database data query.</param>
        /// <param name="dataModule">Name of the data module.</param>
        /// <param name="schema">Schema of the data module.</param>
        /// <param name="dataTable">Name of data table.</param>
        public BasicDbQuery(
            DbQueryKind kind,
            string dataModule = null,
            string schema = null,
            string dataTable = null)
        {
            this.Kind = kind;
            this.DataModule = dataModule;
            this.Schema = schema;
            this.DataTable = dataTable;
        }

        #endregion

        // ------------------------------------------
        // ACCESSORS
        // ------------------------------------------

        #region Accessors

        /// <summary>
        /// Gets the data field with the specified bound data field name.
        /// </summary>
        /// <param name="boundFieldName">Name of the bound data field.</param>
        /// <returns>The data field with the specified bound data field name.</returns>
        public DbField GetIdFieldWithBoundFieldName(String boundFieldName)
        {
            if ((boundFieldName != null) & (this.IdFields != null))
            {
                foreach (DbField rField in this.IdFields)
                {
                    if (rField.Alias?.Equals(boundFieldName, StringComparison.OrdinalIgnoreCase) == true)
                        return rField;
                    if (rField.Name?.Equals(boundFieldName, StringComparison.OrdinalIgnoreCase) == true)
                        return rField;
                }
            }

            return null;
        }

        #endregion
    }
}