using System.Collections.Generic;
using System.ComponentModel;

namespace BindOpen.Framework.Databases.Data.Queries
{
    /// <summary>
    /// This class represents the From statement of a database data query.
    /// </summary>
    public class DbDataQueryFromStatement : IDbDataQueryFromStatement
    {
        // ------------------------------------------
        // VARIABLES
        // ------------------------------------------

        #region Variables

        private IDbDataQueryUnionStatement _unionStatement;
        private List<IDbDataQueryJointureStatement> _jointureStatements = new List<IDbDataQueryJointureStatement>();

        #endregion

        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// Union statement.
        /// </summary>
        public IDbDataQueryUnionStatement UnionStatement
        {
            get { return this._unionStatement; }
            set { this._unionStatement = value; }
        }

        /// <summary>
        /// List of jointure statements.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public List<IDbDataQueryJointureStatement> JointureStatements
        {
            get { return this._jointureStatements; }
            set { this._jointureStatements = new List<IDbDataQueryJointureStatement>(value); }
        }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the DbDataQueryFromStatement class.
        /// </summary>
        public DbDataQueryFromStatement()
        {
        }

        #endregion
    }
}