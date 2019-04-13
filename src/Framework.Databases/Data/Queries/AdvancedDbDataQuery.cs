using System;
using System.Collections.Generic;
using System.ComponentModel;
using BindOpen.Framework.Core.Data.Expression;

namespace BindOpen.Framework.Databases.Data.Queries
{
    /// <summary>
    /// This class represents an advanced database data query.
    /// </summary>
    public class AdvancedDbDataQuery : DbDataQuery, IAdvancedDbDataQuery
    {
        // ------------------------------------------
        // VARIABLES
        // ------------------------------------------

        #region Variables

        private List<IDbDataQueryFromStatement> _FromClause= new List<IDbDataQueryFromStatement>();

        #endregion

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
        /// From clause of this instance.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public List<IDbDataQueryFromStatement> FromClauses
        {
            get { return this._FromClause; }
            set { this._FromClause = new List<IDbDataQueryFromStatement>(value); }
        }

        /// <summary>
        /// Where clause of this instance.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public IDataExpression WhereClause { get; set; }

        /// <summary>
        /// Group by statement of this instance.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public IDbDataQueryGroupByStatement GroupByClause { get; set; }

        /// <summary>
        /// Having statement of this instance.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public IDbDataQueryHavingStatement HavingClause { get; set; }

        /// <summary>
        /// Order statements of this instance.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public List<IDbDataQueryOrderByStatement> OrderByStatements { get; set; } = new List<IDbDataQueryOrderByStatement>();

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the AdvancedDbDataQuery class.
        /// </summary>
        public AdvancedDbDataQuery()
        {
        }

        /// <summary>
        /// Instantiates a new instance of the AdvancedDbDataQuery class.
        /// </summary>
        /// <param name="kind">Type of database data query.</param>
        /// <param name="dataModule">Name of the data module.</param>
        /// <param name="schema">Schema of the data module.</param>
        /// <param name="dataTable">Name of data table.</param>
        public AdvancedDbDataQuery(
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
    }
}