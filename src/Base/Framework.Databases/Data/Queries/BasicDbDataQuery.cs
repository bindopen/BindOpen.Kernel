using System;
using System.Collections.Generic;
using BindOpen.Framework.Databases.Extensions.Carriers;

namespace BindOpen.Framework.Databases.Data.Queries
{
    /// <summary>
    /// This class represents an simple database data query.
    /// </summary>
    public class BasicDbDataQuery : DbDataQuery, IBasicDbDataQuery
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// Indicates whether this instance is distinct. When distinct an advanced Select 
        /// database data query only returns distinct records.
        /// </summary>
        public Boolean IsDistinct { get; set; }

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
        public List<IDbDataQueryFromStatement> FromClauses { get; set; } = new List<IDbDataQueryFromStatement>();

        /// <summary>
        /// Order by statements of this instance.
        /// </summary>
        public List<IDbDataQueryOrderByStatement> OrderByStatements { get; set; } = new List<IDbDataQueryOrderByStatement>();

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the BasicDbDataQuery class.
        /// </summary>
        public BasicDbDataQuery()
        {
        }

        /// <summary>
        /// Instantiates a new instance of the BasicDbDataQuery class.
        /// </summary>
        /// <param name="kind">Kind of database data query.</param>
        /// <param name="dataModule">Name of the data module.</param>
        /// <param name="schema">Schema of the data module.</param>
        /// <param name="dataTable">Name of data table.</param>
        public BasicDbDataQuery(
            DbDataQueryKind kind,
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